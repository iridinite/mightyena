/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mightyena {

    /// <summary>
    /// Specifies the type of a Pokémon or a move.
    /// </summary>
    public enum PokemonType {
        Normal,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Unk1,
        Fire,
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark
    }

    /// <summary>
    /// Specifies the experience group to which a Pokémon belongs.
    /// </summary>
    public enum ExpGroup {
        MediumFast,
        Erratic,
        Fluctuating,
        MediumSlow,
        Fast,
        Slow
    }

    /// <summary>
    /// A container for Pokémon species info.
    /// </summary>
    public class Species {

        public static List<string> SpeciesNames { get; }
            = new List<string>(386);
        private static readonly Dictionary<ushort, Species> allSpecies
            = new Dictionary<ushort, Species>(386);

        public ushort SpeciesIndex { get; }
        public ushort DexNumber { get; }

        public string Name { get; }

        public byte BaseHP { get; }
        public byte BaseAttack { get; }
        public byte BaseDefense { get; }
        public byte BaseSpeed { get; }
        public byte BaseSpAttack { get; }
        public byte BaseSpDefense { get; }

        public PokemonType Type1 { get; }
        public PokemonType Type2 { get; }
        public byte GenderRatio { get; }
        public ExpGroup ExpGroup { get; }

        /// <summary>
        /// Constructs a new PokemonBase instance.
        /// </summary>
        public Species(string dataline) {
            // split the line into chunks
            string[] parts = dataline?.Split(',');
            if (parts == null || parts.Length != 13)
                throw new ArgumentException("Invalid data line", nameof(dataline));

            // parse all the properties in the line
            try {
                SpeciesIndex = ushort.Parse(parts[0]);
                DexNumber = ushort.Parse(parts[1]);

                Name = parts[2];

                BaseHP = byte.Parse(parts[3]);
                BaseAttack = byte.Parse(parts[4]);
                BaseDefense = byte.Parse(parts[5]);
                BaseSpeed = byte.Parse(parts[6]);
                BaseSpAttack = byte.Parse(parts[7]);
                BaseSpDefense = byte.Parse(parts[8]);

                Type1 = (PokemonType)byte.Parse(parts[9]);
                Type2 = (PokemonType)byte.Parse(parts[10]);

                GenderRatio = byte.Parse(parts[11]);
                ExpGroup = (ExpGroup)byte.Parse(parts[12]);

            } catch (Exception e) {
                throw new ArgumentException("Malformed data line contents", nameof(dataline), e);
            }
        }

        /// <summary>
        /// Returns a string identifying this species.
        /// </summary>
        public override string ToString() {
            return $"#{DexNumber} {Name}";
        }

        /// <summary>
        /// Loads the list of species from disk.
        /// </summary>
        public static void Load() {
            Debug.Assert(allSpecies.Count == 0, "Species.Load() called twice");

            // parse the species.txt file to get a list of all Gen3 species
            using (StreamReader reader = new StreamReader("Data\\species.txt")) {
                while (!reader.EndOfStream) {
                    Species species = new Species(reader.ReadLine());
                    allSpecies.Add(species.SpeciesIndex, species);
                }
            }

            // save species names, sorted by dex number
            for (ushort i = 1; i <= 386; i++)
                SpeciesNames.Add(ByDexNumber(i).Name);
        }

        /// <summary>
        /// Returns the Pokémon species at the specified index in the ROM.
        /// </summary>
        /// <param name="index">The species index.</param>
        public static Species ByIndex(ushort index) {
            return allSpecies[index];
        }

        /// <summary>
        /// Returns the Pokémon species with the specified National Dex number.
        /// </summary>
        /// <param name="dexno">A National Dex number.</param>
        public static Species ByDexNumber(ushort dexno) {
            return allSpecies.Values.FirstOrDefault(species => species.DexNumber == dexno);
        }

    }

}
