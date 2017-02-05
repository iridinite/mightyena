/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace Mightyena {

    /// <summary>
    /// Represents the language of the game where a Pokémon comes from.
    /// </summary>
    public enum Language : ushort {
        Japanese = 0x0201,
        English = 0x0202,
        French = 0x0203,
        Italian = 0x0204,
        German = 0x0205,
        Korean = 0x0206,
        Spanish = 0x0207
    }

    /// <summary>
    /// Represents a Pokémon stored in the Gen-III format.
    /// </summary>
    public class Gen3Pokemon {

        /// <summary>
        /// Represents the order of the encrypted substructures in the data section.
        /// </summary>
        private struct SubstructureOrder {
            public readonly int GrowthOffset;
            public readonly int AttackOffset;
            public readonly int EVOffset;
            public readonly int MiscOffset;

            public SubstructureOrder(string order) {
                GrowthOffset = order.IndexOf('G') * 12;
                AttackOffset = order.IndexOf('A') * 12;
                EVOffset = order.IndexOf('E') * 12;
                MiscOffset = order.IndexOf('M') * 12;
            }
        }

        // http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_in_Generation_III#Substructure_order
        private static readonly List<SubstructureOrder> structOrders = new List<SubstructureOrder> {
            new SubstructureOrder("GAEM"), new SubstructureOrder("GAME"), new SubstructureOrder("GEAM"),
            new SubstructureOrder("GEMA"), new SubstructureOrder("GMAE"), new SubstructureOrder("GMEA"),
            new SubstructureOrder("AGEM"), new SubstructureOrder("AGME"), new SubstructureOrder("AEGM"),
            new SubstructureOrder("AEMG"), new SubstructureOrder("AMGE"), new SubstructureOrder("AMEG"),
            new SubstructureOrder("EGAM"), new SubstructureOrder("EGMA"), new SubstructureOrder("EAGM"),
            new SubstructureOrder("EAMG"), new SubstructureOrder("EMGA"), new SubstructureOrder("EMAG"),
            new SubstructureOrder("MGAE"), new SubstructureOrder("MGEA"), new SubstructureOrder("MAGE"),
            new SubstructureOrder("MAEG"), new SubstructureOrder("MEGA"), new SubstructureOrder("MEAG")
        };

        private readonly byte[] frame;  // reference to the containing save section
        private readonly byte[] data;   // decrypted inner data
        private readonly int offset;
        private bool boxed;             // whether the entry is missing the last 20 bytes
        private SubstructureOrder order;

        /// <summary>
        /// The Pokémon's 32-bit personality value.
        /// </summary>
        public uint Personality {
            get { return BitConverter.ToUInt32(frame, offset); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, frame, offset, 4); }
        }

        /// <summary>
        /// The Trainer ID of the Original Trainer.
        /// </summary>
        public uint OTID {
            get { return BitConverter.ToUInt32(frame, offset + 4); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, frame, offset + 4, 4); }
        }

        /// <summary>
        /// The language of the game of origin.
        /// </summary>
        public Language Lang {
            get { return (Language)BitConverter.ToUInt16(frame, offset + 0x12); }
            set { Buffer.BlockCopy(BitConverter.GetBytes((ushort)value), 0, frame, offset + 0x12, 2); }
        }

        /// <summary>
        /// The Pokémon's nickname.
        /// </summary>
        public Gen3String Nickname { get; private set; }

        /// <summary>
        /// The name of the Original Trainer.
        /// </summary>
        public Gen3String OTName { get; private set; }

        /// <summary>
        /// The markings seen in the Storage Box.
        /// </summary>
        public byte Markings {
            get { return frame[offset + 27]; }
            set { frame[offset + 27] = value; }
        }

        /// <summary>
        /// A checksum for the frame bytes. If corrupted, the Pokémon is interpreted as a Bad Egg.
        /// </summary>
        private ushort Checksum {
            get { return BitConverter.ToUInt16(frame, offset + 28); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, frame, offset + 28, 2); }
        }

        /// <summary>
        /// Returns a value indicating whether this Pokémon entry is in use.
        /// </summary>
        public bool Exists {
            get { return SpeciesIndex > 0; }
        }

        /// <summary>
        /// Returns a value indicating whether this Pokémon is Shiny.
        /// </summary>
        public bool Shiny {
            get {
                ushort p1 = (ushort)((Personality & 0xFFFF0000) >> 16);
                ushort p2 = (ushort)(Personality & 0xFFFF);
                ushort otid = (ushort)(OTID & 0xFFFF);
                ushort scid = (ushort)((OTID & 0xFFFF0000) >> 16);
                return (otid ^ scid ^ p1 ^ p2) < 8;
            }
        }

        /// <summary>
        /// Gets or sets the species index.
        /// </summary>
        public ushort SpeciesIndex {
            get { return BitConverter.ToUInt16(data, order.GrowthOffset); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.GrowthOffset, 2); }
        }

        /// <summary>
        /// Gets or sets the <seealso cref="Species"/> of the Pokémon.
        /// </summary>
        public Species Species {
            get { return Species.ByIndex(SpeciesIndex); }
            set { SpeciesIndex = value.SpeciesIndex; }
        }

        /// <summary>
        /// Gets or sets the index of the held item.
        /// </summary>
        public ushort HeldItem {
            get { return BitConverter.ToUInt16(data, order.GrowthOffset + 2); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.GrowthOffset + 2, 2); }
        }

        /// <summary>
        /// Gets or sets the number of Exp. Points this Pokémon has.
        /// </summary>
        public uint Experience {
            get { return BitConverter.ToUInt32(data, order.GrowthOffset + 4); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.GrowthOffset + 4, 4); }
        }

        public byte PPBonuses {
            get { return data[order.GrowthOffset + 8]; }
            set { data[order.GrowthOffset + 8] = value; }
        }

        /// <summary>
        /// Gets or sets the Pokémon's friendship value.
        /// </summary>
        public byte Friendship {
            get { return data[order.GrowthOffset + 9]; }
            set { data[order.GrowthOffset + 9] = value; }
        }

        /// <summary>
        /// Gets or sets the index of the first move.
        /// </summary>
        public ushort Move1 {
            get { return BitConverter.ToUInt16(data, order.AttackOffset); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.AttackOffset, 2); }
        }

        /// <summary>
        /// Gets or sets the index of the second move.
        /// </summary>
        public ushort Move2 {
            get { return BitConverter.ToUInt16(data, order.AttackOffset + 2); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.AttackOffset + 2, 2); }
        }

        /// <summary>
        /// Gets or sets the index of the third move.
        /// </summary>
        public ushort Move3 {
            get { return BitConverter.ToUInt16(data, order.AttackOffset + 4); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.AttackOffset + 4, 2); }
        }

        /// <summary>
        /// Gets or sets the index of the fourth move.
        /// </summary>
        public ushort Move4 {
            get { return BitConverter.ToUInt16(data, order.AttackOffset + 6); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.AttackOffset + 6, 2); }
        }

        /// <summary>
        /// Gets or sets the number of PP the first move has left.
        /// </summary>
        public byte PP1 {
            get { return data[order.AttackOffset + 8]; }
            set { data[order.AttackOffset + 8] = value; }
        }

        /// <summary>
        /// Gets or sets the number of PP the second move has left.
        /// </summary>
        public byte PP2 {
            get { return data[order.AttackOffset + 9]; }
            set { data[order.AttackOffset + 9] = value; }
        }

        /// <summary>
        /// Gets or sets the number of PP the third move has left.
        /// </summary>
        public byte PP3 {
            get { return data[order.AttackOffset + 10]; }
            set { data[order.AttackOffset + 10] = value; }
        }

        /// <summary>
        /// Gets or sets the number of PP the fourth move has left.
        /// </summary>
        public byte PP4 {
            get { return data[order.AttackOffset + 11]; }
            set { data[order.AttackOffset + 11] = value; }
        }

        public byte IVHP {
            get { return (byte)(Genes & 0x1FU); }
            set { Genes = (Genes & ~0x1FU) | (uint)(value & 0x1F); }
        }

        public byte IVAttack {
            get { return (byte)((Genes & 0x3E0U) >> 5); }
            set { Genes = (Genes & ~0x3E0U) | ((uint)(value & 0x1F) << 5); }
        }

        public byte IVDefense {
            get { return (byte)((Genes & 0x7C00U) >> 10); }
            set { Genes = (Genes & ~0x7C00U) | ((uint)(value & 0x1F) << 10); }
        }

        public byte IVSpeed {
            get { return (byte)((Genes & 0xF8000U) >> 15); }
            set { Genes = (Genes & ~0xF8000U) | ((uint)(value & 0x1F) << 15); }
        }

        public byte IVSpAttack {
            get { return (byte)((Genes & 0x1F00000U) >> 20); }
            set { Genes = (Genes & ~0x1F00000U) | ((uint)(value & 0x1F) << 20); }
        }

        public byte IVSpDefense {
            get { return (byte)((Genes & 0x3E000000U) >> 25); }
            set { Genes = (Genes & ~0x3E000000U) | ((uint)(value & 0x1F) << 25); }
        }

        public byte EVHP {
            get { return data[order.EVOffset]; }
            set { data[order.EVOffset] = value; }
        }

        public byte EVAttack {
            get { return data[order.EVOffset + 1]; }
            set { data[order.EVOffset + 1] = value; }
        }

        public byte EVDefense {
            get { return data[order.EVOffset + 2]; }
            set { data[order.EVOffset + 2] = value; }
        }

        public byte EVSpeed {
            get { return data[order.EVOffset + 3]; }
            set { data[order.EVOffset + 3] = value; }
        }

        public byte EVSpAttack {
            get { return data[order.EVOffset + 4]; }
            set { data[order.EVOffset + 4] = value; }
        }

        public byte EVSpDefense {
            get { return data[order.EVOffset + 5]; }
            set { data[order.EVOffset + 5] = value; }
        }

        public byte Coolness {
            get { return data[order.EVOffset + 6]; }
            set { data[order.EVOffset + 6] = value; }
        }

        public byte Beauty {
            get { return data[order.EVOffset + 7]; }
            set { data[order.EVOffset + 7] = value; }
        }

        public byte Cuteness {
            get { return data[order.EVOffset + 8]; }
            set { data[order.EVOffset + 8] = value; }
        }

        public byte Smartness {
            get { return data[order.EVOffset + 9]; }
            set { data[order.EVOffset + 9] = value; }
        }

        public byte Toughness {
            get { return data[order.EVOffset + 10]; }
            set { data[order.EVOffset + 10] = value; }
        }

        public byte Feel {
            get { return data[order.EVOffset + 11]; }
            set { data[order.EVOffset + 11] = value; }
        }

        public byte PokeRus {
            get { return data[order.MiscOffset]; }
            set { data[order.MiscOffset] = value; }
        }

        public byte MetLocation {
            get { return data[order.MiscOffset + 1]; }
            set { data[order.MiscOffset + 1] = value; }
        }

        public ushort Origins {
            get { return BitConverter.ToUInt16(data, order.MiscOffset + 2); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.MiscOffset + 2, 2); }
        }

        public uint Genes {
            get { return BitConverter.ToUInt32(data, order.MiscOffset + 4); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.MiscOffset + 4, 4); }
        }

        public uint Ribbons {
            get { return BitConverter.ToUInt32(data, order.MiscOffset + 8); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, order.MiscOffset + 8, 4); }
        }

        public bool FatefulEncounter {
            get { return (Ribbons & 0x80000000) > 0; }
            set {
                if (value)
                    Ribbons |= 0x80000000;
                else
                    Ribbons &= 0x7FFFFFFF;
            }
        }

        /// <summary>
        /// The Pokémon's status condition.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Decrypts a Pokémon byte buffer and constructs a new Gen3Pokemon instance.
        /// </summary>
        /// <param name="frame">A reference to the encapsulating save section.</param>
        /// <param name="offset">The byte offset within the save section.</param>
        /// <param name="boxed">If true, the Pokémon is in a PC box (the last 20 bytes on frame are missing).</param>
        public Gen3Pokemon(byte[] frame, int offset, bool boxed) {
            this.frame = frame;
            this.offset = offset;
            this.boxed = boxed;
            this.data = new byte[48];
            Decrypt();
        }

        /// <summary>
        /// Parses a <see cref="Gen3Pokemon"/> from a .pkm file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        public static Gen3Pokemon FromFile(string filename) {
            byte[] frame = File.ReadAllBytes(filename);
            return new Gen3Pokemon(frame, 0, true);
        }

        /// <summary>
        /// Writes this <see cref="Gen3Pokemon"/> to a file on disk, which can later be loaded with FromFile.
        /// </summary>
        /// <param name="filename"></param>
        public void Export(string filename) {
            // make sure frame is up to date
            Save();
            // copy out this specific mon and save it
            byte[] fileframe = new byte[80]; // always boxed
            Buffer.BlockCopy(frame, offset, fileframe, 0, 80);
            File.WriteAllBytes(filename, fileframe);
        }

        /// <summary>
        /// Copies the data of this Pokémon to the target.
        /// </summary>
        /// <param name="target">The Pokémon instance whose data to overwrite.</param>
        public void CopyTo(Gen3Pokemon target) {
            // make sure our frame is accurate
            Save();

            // copy frame and metadata
            Buffer.BlockCopy(frame, offset, target.frame, target.offset, boxed ? 80 : 100);
            // fixed: don't change boxed on target, because this will cause pkm imported
            // as party pokemon to not update their footer on save, causing game glitches
            //target.boxed = this.boxed;
            target.order = this.order;
            target.Decrypt();
        }

        /// <summary>
        /// Calculates the final value of a statistic.
        /// </summary>
        /// <param name="level">The level of the Pokémon.</param>
        /// <param name="statbase">The base value of the stat, as determined by the species.</param>
        /// <param name="iv">The Inherent Value of the stat.</param>
        /// <param name="ev">The Effort Value of the stat.</param>
        /// <param name="nature">The stat multiplier. 0.9 if hindered, 1.0 if neutral, 1.1 if benificial.</param>
        private static ushort CalculateStat(byte level, byte statbase, ushort iv, ushort ev, double nature) {
            double neutralstat = Math.Floor((2.0 * statbase + iv + Math.Floor(ev / 4.0)) * level / 100) + 5;
            return (ushort)Math.Floor(neutralstat * nature);
        }

        /// <summary>
        /// Parses and decrypts the Pokémon entry.
        /// </summary>
        private void Decrypt() {
            Nickname = Gen3String.Decode(frame, offset + 0x08, 10, Lang == Language.Japanese);
            OTName = Gen3String.Decode(frame, offset + 0x14, 7, Lang == Language.Japanese);

            // read the inner data section
            uint cryptokey = OTID ^ Personality;
            order = structOrders[(int)(Personality % 24)];
            for (int i = 0; i < 12; i++) {
                // decrypt the data, 4 bytes at a time
                uint decrypted = BitConverter.ToUInt32(frame, offset + 32 + i * 4) ^ cryptokey;
                Buffer.BlockCopy(BitConverter.GetBytes(decrypted), 0, data, i * 4, 4);
            }
        }

        /// <summary>
        /// Deletes the Pokémon by zeroing the data buffer.
        /// </summary>
        public void Delete() {
            Array.Clear(data, 0, 48);
            Array.Clear(frame, offset, boxed ? 80 : 100);
            Decrypt();
        }

        /// <summary>
        /// Saves and re-encrypts changes back to the encapsulating frame.
        /// </summary>
        public void Save() {
            // avoid saving stats to empty slots
            if (Exists) {
                // recalculate statistics
                if (!boxed) {
                    byte level = (byte)Utils.GetLevelForExp(Species.ExpGroup, Experience);
                    ushort hp = (ushort)(Math.Floor((2.0 * Species.BaseHP + IVHP + Math.Floor(EVHP / 4.0)) * level / 100) + level + 10);

                    // write stats
                    frame[offset + 84] = level;
                    Buffer.BlockCopy(BitConverter.GetBytes(hp), 0, frame, offset + 86, 2); // current hp
                    Buffer.BlockCopy(BitConverter.GetBytes(hp), 0, frame, offset + 88, 2); // total hp
                    Buffer.BlockCopy(BitConverter.GetBytes(CalculateStat(level, Species.BaseAttack, IVAttack, EVAttack, 1.0)), 0, frame, offset + 90, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(CalculateStat(level, Species.BaseDefense, IVDefense, EVDefense, 1.0)), 0, frame, offset + 92, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(CalculateStat(level, Species.BaseSpeed, IVSpeed, EVSpeed, 1.0)), 0, frame, offset + 94, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(CalculateStat(level, Species.BaseSpAttack, IVSpAttack, EVSpAttack, 1.0)), 0, frame, offset + 96, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(CalculateStat(level, Species.BaseSpDefense, IVSpDefense, EVSpDefense, 1.0)), 0, frame, offset + 98, 2);
                }

                // save strings
                Nickname.Encode(frame, offset + 0x08);
                OTName.Encode(frame, offset + 0x14);
            }

            // re-order the substructures based on the new pval (it might have changed)
            byte[] finaldata = new byte[48];
            var neworder = structOrders[(int)(Personality % 24)];
            Buffer.BlockCopy(data, order.GrowthOffset, finaldata, neworder.GrowthOffset, 12);
            Buffer.BlockCopy(data, order.AttackOffset, finaldata, neworder.AttackOffset, 12);
            Buffer.BlockCopy(data, order.EVOffset, finaldata, neworder.EVOffset, 12);
            Buffer.BlockCopy(data, order.MiscOffset, finaldata, neworder.MiscOffset, 12);

            // calculate the data checksum
            ushort calculatedSum = 0;
            for (int i = 0; i < 24; i++)
                unchecked {
                    calculatedSum += BitConverter.ToUInt16(finaldata, i * 2);
                }
            Checksum = calculatedSum;

            // re-encrypt the data
            uint cryptokey = OTID ^ Personality;
            for (int i = 0; i < 12; i++) {
                // 4 bytes at a time, and copy it back to the frame
                uint encrypted = BitConverter.ToUInt32(finaldata, i * 4) ^ cryptokey;
                Buffer.BlockCopy(BitConverter.GetBytes(encrypted), 0, frame, offset + 32 + i * 4, 4);
            }
        }

        /// <summary>
        /// Returns a string that quickly identifies this Pokémon.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Exists ? $"{Nickname}, Lv {Utils.GetLevelForExp(Species.ExpGroup, Experience)} {Species.Name}" : "Empty";
        }

    }

}
