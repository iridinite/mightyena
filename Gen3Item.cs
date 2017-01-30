/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;

namespace Mightyena {
    
    /// <summary>
    /// Represents an item entry in the player's Bag or PC.
    /// </summary>
    public class Gen3Item {

        private readonly byte[] data;
        private readonly int offset;
        private readonly uint key;

        /// <summary>
        /// Gets or sets the type of the item entry.
        /// </summary>
        public ushort Index {
            get { return BitConverter.ToUInt16(data, offset); }
            set { Buffer.BlockCopy(BitConverter.GetBytes(value), 0, data, offset, 2); }
        }

        /// <summary>
        /// Gets or sets the quantity of the item entry.
        /// </summary>
        public ushort Quantity {
            get { return (ushort)(BitConverter.ToUInt16(data, offset + 2) ^ key); }
            set { Buffer.BlockCopy(BitConverter.GetBytes((ushort)(value ^ key)), 0, data, offset + 2, 2); }
        }

        /// <summary>
        /// Decrypts an item entry and constructs a new instance to manipulate it with.
        /// </summary>
        /// <param name="data">A reference to the encapsulating save section.</param>
        /// <param name="offset">An offset that points to the item entry within the save section.</param>
        /// <param name="key">The 32-bit security key to use for decryption.</param>
        public Gen3Item(byte[] data, int offset, uint key) {
            this.data = data;
            this.offset = offset;
            this.key = key;
        }

        /// <summary>
        /// Returns a string describing the contents of this item entry.
        /// </summary>
        public override string ToString() {
            return $"{Utils.ItemNames[Index]} x {Quantity}";
        }

    }

}
