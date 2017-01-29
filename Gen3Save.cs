/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.IO;

namespace Mightyena {

    /// <summary>
    /// Identifies the game to which a save applies.
    /// </summary>
    public enum GameVersion {
        RubySapphire,
        FireRedLeafGreen,
        Emerald
    }
    
    /// <summary>
    /// Represents a Gen-III save game file.
    /// </summary>
    public class Gen3Save {

        /// <summary>
        /// Contains a single section in the save file.
        /// </summary>
        private class Section {
            public byte[] data;
            public SectionID id;
            public ushort checksum;
            public uint saveindex;

            /// <summary>
            /// Returns a debug string used to identify this section.
            /// </summary>
            /// <returns></returns>
            public override string ToString() {
                return $"{saveindex} / {id}";
            }

            /// <summary>
            /// Returns the number of bytes of data used by this section.
            /// </summary>
            /// <returns></returns>
            public int GetSize() {
                switch (id) {
                    case SectionID.Trainer:
                        return 3884;
                    case SectionID.Rival:
                        return 3848;
                    case SectionID.Team:
                    case SectionID.Unk1:
                    case SectionID.Unk2:
                    case SectionID.PCBoxA:
                    case SectionID.PCBoxB:
                    case SectionID.PCBoxC:
                    case SectionID.PCBoxD:
                    case SectionID.PCBoxE:
                    case SectionID.PCBoxF:
                    case SectionID.PCBoxG:
                    case SectionID.PCBoxH:
                        return 3968;
                    case SectionID.PCBoxI:
                        return 2000;
                    default:
                        return 0;
                }
            }

            /// <summary>
            /// Recalculates the checksum field.
            /// </summary>
            public void CalculateChecksum() {
                // http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_III#Checksum
                unchecked {
                    uint result = 0U;
                    for (int i = 0; i < GetSize() / 4; i++)
                        result += BitConverter.ToUInt32(data, i * 4);
                    result = ((result & 0xFFFF0000) >> 16) + (result & 0x0000FFFF);
                    checksum = (ushort)result;
                }
            }

        }

        /// <summary>
        /// Describes the contents of a <seealso cref="Section"/>.
        /// </summary>
        private enum SectionID : ushort {
            Trainer = 0,
            Team = 1,
            Unk1 = 2,
            Unk2 = 3,
            Rival = 4,
            PCBoxA = 5,
            PCBoxB = 6,
            PCBoxC = 7,
            PCBoxD = 8,
            PCBoxE = 9,
            PCBoxF = 10,
            PCBoxG = 11,
            PCBoxH = 12,
            PCBoxI = 13
        }

        /// <summary>
        /// References the save file that is currently being edited.
        /// </summary>
        public static Gen3Save Inst { get; set; }

        private byte[] sram;
        private byte[] boxdata;
        private int sectionOffset;
        private Section[] sections;

        public Gen3String Name { get; private set; }
        public byte Gender { get; set; }
        public uint TrainerID { get; set; }
        public string SaveIndexDesc { get; private set; }

        public TimeSpan TimePlayed { get; set; }
        public GameVersion GameCode { get; private set; }
        public uint SecurityKey { get; private set; }

        public uint TeamSize { get; set; }
        public Gen3Pokemon[] Team { get; private set; }
        public Gen3Pokemon[] Box { get; private set; }
        public Gen3String[] BoxNames { get; private set; }
        public byte[] BoxWallpapers { get; private set; }
        public uint BoxActive { get; set; }

        public uint Money { get; set; }
        public ushort Coins { get; set; }
        public Gen3Item[] ItemsPC { get; private set; }
        public Gen3Item[] ItemsPocket { get; private set; }
        public Gen3Item[] ItemsKey { get; private set; }
        public Gen3Item[] ItemsBall { get; private set; }
        public Gen3Item[] ItemsTM { get; private set; }
        public Gen3Item[] ItemsBerry { get; private set; }

        /// <summary>
        /// Returns the <seealso cref="Section"/> with the specified <seealso cref="SectionID"/> for the latest saved game.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Section GetSection(SectionID id) {
            for (int i = 0; i < 14; i++)
                if (sections[sectionOffset + i].id == id)
                    return sections[sectionOffset + i];
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Parses and returns an array of Pokémon.
        /// </summary>
        /// <param name="data">The data buffer that contains the array.</param>
        /// <param name="offset">The index at which to start reading.</param>
        /// <param name="count">The number of Pokémon (not bytes) to read.</param>
        /// <param name="boxed">If true, indicates the Pokémon are stored in a PC box (missing the last 20 bytes).</param>
        private static Gen3Pokemon[] ParsePokemonArray(byte[] data, int offset, int count, bool boxed) {
            Gen3Pokemon[] ret = new Gen3Pokemon[count];
            int bytesPerMon = boxed ? 80 : 100;

            for (int i = 0; i < count; i++) {
                //byte[] frame = new byte[bytesPerMon];
                //Buffer.BlockCopy(data, offset + i * bytesPerMon, frame, 0, bytesPerMon);
                ret[i] = new Gen3Pokemon(data, offset + bytesPerMon * i, boxed);
            }
            return ret;
        }

        /// <summary>
        /// Parses and returns an array of items.
        /// </summary>
        /// <param name="data">The data buffer that contains the array.</param>
        /// <param name="offset">The index at which to start reading.</param>
        /// <param name="count">The number of items (not bytes) to read.</param>
        /// <param name="key">The 32-bit key with which to decrypt the item quantity.</param>
        private static Gen3Item[] ParseItemArray(byte[] data, int offset, int count, uint key) {
            Gen3Item[] ret = new Gen3Item[count];
            for (int i = 0; i < count; i++) {
                //Gen3Item item = new Gen3Item();
                //item.Index = (ItemIndex)BitConverter.ToUInt16(data, offset + i * 4);
                //item.Quantity = (ushort)(BitConverter.ToUInt16(data, offset + i * 4 + 2) ^ key);
                ret[i] = new Gen3Item(data, offset + i * 4, key);
            }
            return ret;
        }

        /// <summary>
        /// Loads and parses a Gen-III save file.
        /// </summary>
        /// <param name="filename">The path to the save file.</param>
        public static Gen3Save FromFile(string filename) {
            Gen3Save ret = new Gen3Save();
            ret.sram = File.ReadAllBytes(filename);

            if (ret.sram.Length < 131072) // 128 kB
                throw new InvalidDataException("File is smaller than 128 kB and cannot be a valid save file");

            // load all sections in the file
            ret.sections = new Section[28]; // 14 sections * 2 saves
            for (int i = 0; i < 28; i++) {
                Section section = new Section();
                section.data = new byte[4096];

                Buffer.BlockCopy(ret.sram, 4096 * i, section.data, 0, 4096);
                section.id = (SectionID)BitConverter.ToUInt16(section.data, 0x0FF4);
                section.checksum = BitConverter.ToUInt16(section.data, 0x0FF6);
                section.saveindex = BitConverter.ToUInt32(section.data, 0x0FFC);
                ret.sections[i] = section;
            }

            // determine which of the two game saves is more recent
            if (ret.sections[0].saveindex > ret.sections[14].saveindex) {
                ret.sectionOffset = 0;
                ret.SaveIndexDesc = $"Game A (#{ret.sections[0].saveindex})";
            } else {
                ret.sectionOffset = 14;
                ret.SaveIndexDesc = $"Game B (#{ret.sections[14].saveindex})";
            }

            // --------- TRAINER section --------- //
            byte[] trainerData = ret.GetSection(SectionID.Trainer).data;
            ret.Name = Gen3String.Decode(trainerData, 0, 7, false); // TODO: How do we know what language the save is?
            ret.Gender = trainerData[0x0008];
            ret.TrainerID = BitConverter.ToUInt32(trainerData, 0x000A);

            // save playtime in a TimeSpan object
            ushort timeHours = BitConverter.ToUInt16(trainerData, 0x000E);
            byte timeMinutes = trainerData[0x0010];
            byte timeSeconds = trainerData[0x0011];
            byte timeFrames = trainerData[0x0012];
            ret.TimePlayed = new TimeSpan(0, timeHours, timeMinutes, timeSeconds, timeFrames * 1000 / 60);

            // identify the save game version
            uint gameCode = BitConverter.ToUInt32(trainerData, 0x00AC);
            switch (gameCode) {
                case 0:
                    ret.GameCode = GameVersion.RubySapphire;
                    ret.SecurityKey = 0;
                    break;
                case 1:
                    ret.GameCode = GameVersion.FireRedLeafGreen;
                    ret.SecurityKey = BitConverter.ToUInt32(trainerData, 0x0AF8);
                    break;
                default:
                    ret.GameCode = GameVersion.Emerald;
                    ret.SecurityKey = gameCode; // 0x00AC
                    break;
            }

            // --------- TEAM section --------- //
            byte[] teamData = ret.GetSection(SectionID.Team).data;
            switch (ret.GameCode) { // games have different addresses
                case GameVersion.RubySapphire:
                    ret.TeamSize = BitConverter.ToUInt32(teamData, 0x0234);
                    ret.Team = ParsePokemonArray(teamData, 0x0238, 6, false);
                    ret.Money = BitConverter.ToUInt32(teamData, 0x0490);
                    ret.Coins = BitConverter.ToUInt16(teamData, 0x0494);
                    ret.ItemsPC = ParseItemArray(teamData, 0x0498, 50, 0);
                    ret.ItemsPocket = ParseItemArray(teamData, 0x0560, 20, ret.SecurityKey);
                    ret.ItemsKey = ParseItemArray(teamData, 0x05B0, 20, ret.SecurityKey);
                    ret.ItemsBall = ParseItemArray(teamData, 0x0600, 16, ret.SecurityKey);
                    ret.ItemsTM = ParseItemArray(teamData, 0x0640, 64, ret.SecurityKey);
                    ret.ItemsBerry = ParseItemArray(teamData, 0x0740, 46, ret.SecurityKey);
                    break;
                case GameVersion.FireRedLeafGreen:
                    ret.TeamSize = BitConverter.ToUInt32(teamData, 0x0034);
                    ret.Team = ParsePokemonArray(teamData, 0x0038, 6, false);
                    ret.Money = BitConverter.ToUInt32(teamData, 0x0290) ^ ret.SecurityKey;
                    ret.Coins = (ushort)(BitConverter.ToUInt16(teamData, 0x0294) ^ ret.SecurityKey);
                    ret.ItemsPC = ParseItemArray(teamData, 0x0298, 30, 0);
                    ret.ItemsPocket = ParseItemArray(teamData, 0x0310, 42, ret.SecurityKey);
                    ret.ItemsKey = ParseItemArray(teamData, 0x03B8, 30, ret.SecurityKey);
                    ret.ItemsBall = ParseItemArray(teamData, 0x0430, 13, ret.SecurityKey);
                    ret.ItemsTM = ParseItemArray(teamData, 0x0464, 58, ret.SecurityKey);
                    ret.ItemsBerry = ParseItemArray(teamData, 0x054C, 43, ret.SecurityKey);
                    break;
                case GameVersion.Emerald:
                    ret.TeamSize = BitConverter.ToUInt32(teamData, 0x0234);
                    ret.Team = ParsePokemonArray(teamData, 0x0238, 6, false);
                    ret.Money = BitConverter.ToUInt32(teamData, 0x0490) ^ ret.SecurityKey;
                    ret.Coins = (ushort)(BitConverter.ToUInt16(teamData, 0x0494) ^ ret.SecurityKey);
                    ret.ItemsPC = ParseItemArray(teamData, 0x0498, 50, 0);
                    ret.ItemsPocket = ParseItemArray(teamData, 0x0560, 30, ret.SecurityKey);
                    ret.ItemsKey = ParseItemArray(teamData, 0x05D8, 30, ret.SecurityKey);
                    ret.ItemsBall = ParseItemArray(teamData, 0x0650, 16, ret.SecurityKey);
                    ret.ItemsTM = ParseItemArray(teamData, 0x0690, 64, ret.SecurityKey);
                    ret.ItemsBerry = ParseItemArray(teamData, 0x0790, 46, ret.SecurityKey);
                    break;
            }

            // --------- PC BOX sections --------- //
            ret.boxdata = new byte[33744];

            // copy all box sections into one big buffer
            int boxoffset = 0;
            for (SectionID sid = SectionID.PCBoxA; sid <= SectionID.PCBoxI; sid++) {
                Section boxsection = ret.GetSection(sid);
                Buffer.BlockCopy(boxsection.data, 0, ret.boxdata, boxoffset, boxsection.GetSize());
                boxoffset += boxsection.GetSize();
            }

            // build the arrays of decrypted box entries, plus metadata
            ret.Box = ParsePokemonArray(ret.boxdata, 0x0004, 420, true);
            ret.BoxNames = new Gen3String[14];
            ret.BoxWallpapers = new byte[14];
            for (int i = 0; i < 14; i++) {
                ret.BoxNames[i] = Gen3String.Decode(ret.boxdata, 0x8344 + i * 9, 8, false);
                ret.BoxWallpapers[i] = ret.boxdata[0x83C2 + i];
            }
            
            return ret;
        }

    }

}
