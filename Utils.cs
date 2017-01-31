/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Mightyena.Properties;

namespace Mightyena {

    /// <summary>
    /// Exposes some common functionality used throughout the program.
    /// </summary>
    internal static class Utils {

        private static readonly Random rng = new Random();
        private static readonly Bitmap CacheSprites, CacheSpriteShiny, CacheIcons, CacheItems;
        public static List<string> MoveNames { get; }
        public static List<string> LocationNames { get; }
        public static List<string> NatureNames { get; }
        public static List<string> ItemNames { get; }

        public const int SpriteSize = 80;
        public const int IconSizeW = 40;
        public const int IconSizeH = 30;
        public const int ItemIconSize = 30;

        static Utils() {
            // prepare sprite bitmap in 32bppPARGB format
            {
                int width = Resources.PokeSprites.Width;
                int height = Resources.PokeSprites.Height;
                CacheSprites = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                CacheSpriteShiny = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                using (Graphics gfx = Graphics.FromImage(CacheSprites))
                    gfx.DrawImage(Resources.PokeSprites, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
                using (Graphics gfx = Graphics.FromImage(CacheSpriteShiny))
                    gfx.DrawImage(Resources.PokeSpritesShiny, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            }

            // prepare pokemon icons bitmap in 32bppPARGB format
            {
                int width = Resources.PokeIcons.Width;
                int height = Resources.PokeIcons.Height;
                CacheIcons = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                using (Graphics gfx = Graphics.FromImage(CacheIcons))
                    gfx.DrawImage(Resources.PokeIcons, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            }

            // prepare item icons bitmap in 32bppPARGB format
            {
                int width = Resources.ItemIcons.Width;
                int height = Resources.ItemIcons.Height;
                CacheItems = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                using (Graphics gfx = Graphics.FromImage(CacheItems))
                    gfx.DrawImage(Resources.ItemIcons, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            }

            // load strings from text files
            MoveNames = ReadStringList("Data\\moves.txt");
            LocationNames = ReadStringList("Data\\locations.txt");
            NatureNames = ReadStringList("Data\\natures.txt");
            ItemNames = ReadStringList("Data\\items.txt");
        }

        /// <summary>
        /// Returns a list of all lines in the specified file.
        /// </summary>
        /// <param name="filename">The path to the file to read.</param>
        private static List<string> ReadStringList(string filename) {
            var ret = new List<string>();
            using (StreamReader reader = new StreamReader(filename)) {
                while (!reader.EndOfStream)
                    ret.Add(reader.ReadLine());
            }
            return ret;
        }

        /// <summary>
        /// Generates a random integer in the range [0, n).
        /// </summary>
        /// <param name="upper">The exclusive upper bound.</param>
        public static int RandInt(int upper) {
            return rng.Next(upper);
        }

        /// <summary>
        /// Generates a random integer in the range [n, m).
        /// </summary>
        /// <param name="lower">The inclusive lower bound.</param>
        /// <param name="upper">The exclusive upper bound.</param>
        public static int RandInt(int lower, int upper) {
            return rng.Next(lower, upper);
        }

        /// <summary>
        /// Draws a small icon of the specified Pokémon.
        /// </summary>
        /// <param name="gfx">The <seealso cref="Graphics"/> to draw with.</param>
        /// <param name="pokeID">The Pokémon's National Dex number.</param>
        public static void DrawPokemonIcon(Graphics gfx, int pokeID) {
            int col = (pokeID - 1) % 25;
            int row = (pokeID - 1) / 25;
            gfx.DrawImage(CacheIcons,
                new Rectangle(0, 0, IconSizeW, IconSizeH),
                new Rectangle(col * IconSizeW, row * IconSizeH, IconSizeW, IconSizeH),
                GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Draws a large sprite of the specified Pokémon.
        /// </summary>
        /// <param name="gfx">The <seealso cref="Graphics"/> to draw with.</param>
        /// <param name="pokeID">The Pokémon's National Dex number.</param>
        /// <param name="shiny">If true, draw the shiny version of the sprite.</param>
        public static void DrawPokemonSprite(Graphics gfx, int pokeID, bool shiny = false) {
            int col = (pokeID - 1) % 28;
            int row = (pokeID - 1) / 28;
            gfx.DrawImage(shiny ? CacheSpriteShiny : CacheSprites,
                new Rectangle(0, 0, SpriteSize, SpriteSize),
                new Rectangle(col * SpriteSize, row * SpriteSize, SpriteSize, SpriteSize),
                GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Draws an icon associated with the specified item.
        /// </summary>
        /// <param name="gfx">The <seealso cref="Graphics"/> to draw with.</param>
        /// <param name="itemID">The item index number.</param>
        /// <param name="x">X coordinate to draw at.</param>
        /// <param name="y">Y coordinate to draw at.</param>
        public static void DrawItemIcon(Graphics gfx, int itemID, int x, int y) {
            int col = itemID % 25;
            int row = itemID / 25;
            gfx.DrawImage(CacheItems,
                new Rectangle(x, y, ItemIconSize, ItemIconSize),
                new Rectangle(col * ItemIconSize, row * ItemIconSize, ItemIconSize, ItemIconSize),
                GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Returns a value indicating whether the specified OT ID and PVal make the Pokémon shiny.
        /// </summary>
        /// <param name="OTID">The Original Trainer ID.</param>
        /// <param name="PVal">A 32-bit personality value.</param>
        public static bool GetIsShiny(uint OTID, uint PVal) {
            ushort p1 = (ushort)((PVal & 0xFFFF0000) >> 16);
            ushort p2 = (ushort)(PVal & 0xFFFF);
            ushort otid = (ushort)(OTID & 0xFFFF);
            ushort scid = (ushort)((OTID & 0xFFFF0000) >> 16);
            return (otid ^ scid ^ p1 ^ p2) < 8;
        }

        /// <summary>
        /// Returns the total XP required to advance to a certain level.
        /// </summary>
        /// <param name="group">The experience group to use in the calculation.</param>
        /// <param name="n">The level of the Pokémon.</param>
        public static int GetExpForLevel(ExpGroup group, int n) {
            // http://bulbapedia.bulbagarden.net/wiki/Experience#Relation_to_level
            switch (group) {
                case ExpGroup.Erratic:
                    if (n <= 50)
                        return n * n * n * (100 - n) / 50;
                    if (n <= 68)
                        return n * n * n * (150 - n) / 100;
                    if (n <= 98)
                        return n * n * n * (int)Math.Floor((1911 - 10 * n) / 3.0) / 500;
                    return n * n * n * (160 - n) / 100;

                case ExpGroup.Fast:
                    return 4 * n * n * n / 5;

                case ExpGroup.MediumFast:
                    return n * n * n;

                case ExpGroup.MediumSlow:
                    return (int)(1.2 * n * n * n - 15 * n * n + 100 * n - 140);

                case ExpGroup.Slow:
                    return 5 * n * n * n / 4;

                case ExpGroup.Fluctuating:
                    if (n <= 15)
                        return n * n * n * (((int)Math.Floor((n + 1) / 3.0) + 24) / 50);
                    if (n <= 36)
                        return n * n * n * ((n + 14) / 50);
                    return n * n * n * (((int)Math.Floor(n / 2.0) + 32) / 50);

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns the level tied to the specified number of Exp. Points.
        /// </summary>
        /// <param name="group">The experience group to use in the calculation.</param>
        /// <param name="exp">The number of Exp. Points.</param>
        public static int GetLevelForExp(ExpGroup group, uint exp) {
            int delta = 25;
            int level = 50;
            while (true) {
                // divide and conquer
                int required = GetExpForLevel(group, level);
                if (required > exp)
                    level -= delta;
                else
                    level += delta;
                // check if exp fits in this level
                if (GetExpForLevel(group, level) <= exp && GetExpForLevel(group, level + 1) > exp)
                    return Math.Max(level, 1);
                // half delta, but must be at least 1
                delta = Math.Max(delta / 2, 1);
            }
        }

        /// <summary>
        /// Returns the name of the specified move.
        /// </summary>
        /// <param name="id">The ID of the move.</param>
        public static string GetMoveName(int id) {
            return MoveNames[id];
        }

    }

}
