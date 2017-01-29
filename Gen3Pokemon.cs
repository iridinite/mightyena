/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;

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
        private static readonly List<SubstructureOrder> structOrders = new List<SubstructureOrder>() {
            new SubstructureOrder("GAEM"), new SubstructureOrder("GAME"), new SubstructureOrder("GEAM"),
            new SubstructureOrder("GEMA"), new SubstructureOrder("GMAE"), new SubstructureOrder("GMEA"),
            new SubstructureOrder("AGEM"), new SubstructureOrder("AGME"), new SubstructureOrder("AEGM"),
            new SubstructureOrder("AEMG"), new SubstructureOrder("AMGE"), new SubstructureOrder("AMEG"),
            new SubstructureOrder("EGAM"), new SubstructureOrder("EGMA"), new SubstructureOrder("EAGM"),
            new SubstructureOrder("EAMG"), new SubstructureOrder("EMGA"), new SubstructureOrder("EMAG"),
            new SubstructureOrder("MGAE"), new SubstructureOrder("MGEA"), new SubstructureOrder("MAGE"),
            new SubstructureOrder("MAEG"), new SubstructureOrder("MEGA"), new SubstructureOrder("MEAG")
        };

        private readonly byte[] frame;   // reference to the containing save section
        private readonly byte[] data;    // decrypted inner data
        private readonly int offset;
        private readonly SubstructureOrder order;

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
            get { return Utils.GetIsShiny(OTID, Personality); }
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
            get { return (byte)((Genes & 0xF8000000U) >> 27); }
            set { Genes = (Genes & ~0xF8000000U) | ((uint)(value & 0x1F) << 27); }
        }

        public byte IVAttack {
            get { return (byte)((Genes & 0x07C00000U) >> 22); }
            set { Genes = (Genes & ~0x07C00000U) | ((uint)(value & 0x1F) << 22); }
        }

        public byte IVDefense {
            get { return (byte)((Genes & 0x3E0000U) >> 17); }
            set { Genes = (Genes & ~0x3E0000U) | ((uint)(value & 0x1F) << 17); }
        }

        public byte IVSpeed {
            get { return (byte)((Genes & 0x1F000U) >> 12); }
            set { Genes = (Genes & ~0x1F000U) | ((uint)(value & 0x1F) << 12); }
        }

        public byte IVSpAttack {
            get { return (byte)((Genes & 0xF80U) >> 7); }
            set { Genes = (Genes & ~0xF80U) | ((uint)(value & 0x1F) << 7); }
        }

        public byte IVSpDefense {
            get { return (byte)((Genes & 0x7CU) >> 2); }
            set { Genes = (Genes & ~0x7CU) | ((uint)(value & 0x1F) << 2); }
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
            Nickname = Gen3String.Decode(frame, offset + 0x08, 10, Lang == Language.Japanese);
            OTName = Gen3String.Decode(frame, offset + 0x14, 7, Lang == Language.Japanese);

            // read the inner data section
            uint cryptokey = OTID ^ Personality;
            order = structOrders[(int)(Personality % 24)];
            data = new byte[48];
            for (int i = 0; i < 12; i++) {
                // decrypt the data, 4 bytes at a time
                uint decrypted = BitConverter.ToUInt32(frame, offset + 32 + i * 4) ^ cryptokey;
                Buffer.BlockCopy(BitConverter.GetBytes(decrypted), 0, data, i * 4, 4);
            }
        }

        /// <summary>
        /// Saves and re-encrypts changes back to the encapsulating frame.
        /// </summary>
        public void Save() {
            // recalculate the checksum
            ushort calculatedSum = 0;
            for (int i = 0; i < 24; i++)
                unchecked {
                    calculatedSum += BitConverter.ToUInt16(data, i * 2);
                }
            Checksum = calculatedSum;

            // TODO; re-encrypt data and strings
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
