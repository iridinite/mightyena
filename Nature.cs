/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Mightyena {

    /// <summary>
    /// Identifies a certain statistic for a Pokémon.
    /// </summary>
    public enum Statistic {
        None = 0,
        Attack = 1,
        Defense = 2,
        Speed = 3,
        SpAttack = 4,
        SpDefense = 5
    }

    /// <summary>
    /// A Pokémon's nature applies a percentage modifier to some stats.
    /// </summary>
    public class Nature {

        private static readonly List<Nature> allNatures = new List<Nature>();
        public static readonly List<string> NatureNames = new List<string>();

        public string Name { get; }
        public Statistic Positive { get; }
        public Statistic Negative { get; }

        public Nature(string dataline) {
            // split the line into chunks
            string[] parts = dataline?.Split(',');
            if (parts == null || parts.Length != 3)
                throw new ArgumentException("Invalid data line", nameof(dataline));

            // parse all the properties in the line
            try {
                Name = parts[0];
                Positive = (Statistic)byte.Parse(parts[1]);
                Negative = (Statistic)byte.Parse(parts[2]);

            } catch (Exception e) {
                throw new ArgumentException("Malformed data line contents", nameof(dataline), e);
            }
        }

        /// <summary>
        /// Returns the multiplier for the specified stat.
        /// </summary>
        /// <param name="stat">The stat to check against this Nature.</param>
        public double GetModifier(Statistic stat) {
            if (stat == Positive) return 1.1;
            if (stat == Negative) return 0.9;
            return 1.0;
        }

        /// <summary>
        /// Loads the list of species from disk.
        /// </summary>
        public static void Load() {
            Debug.Assert(allNatures.Count == 0, "Nature.Load() called twice");

            // parse the species.txt file to get a list of all Gen3 species
            using (StreamReader reader = new StreamReader("Data\\natures.txt")) {
                while (!reader.EndOfStream) {
                    Nature nature = new Nature(reader.ReadLine());
                    allNatures.Add(nature);
                    NatureNames.Add(nature.Name);
                }
            }
        }

        /// <summary>
        /// Returns the Pokémon nature at the specified index.
        /// </summary>
        /// <param name="index">The nature's index.</param>
        public static Nature ByIndex(int index) {
            return allNatures[index];
        }

        /// <summary>
        /// Returns a text representation of this <see cref="Nature"/>.
        /// </summary>
        public override string ToString() {
            return $"{Name} (+{Positive}, -{Negative})";
        }

    }

}
