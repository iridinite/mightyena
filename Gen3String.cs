﻿/*
 * Mightyena - A Gen-III Pokémon Save Editor
 * (C) Mika Molenkamp, 2017.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Mightyena {

    /// <summary>
    /// Represents a string encoded with the Gen-III proprietary character encoding format.
    /// </summary>
    /// <remarks>
    /// Reference: http://bulbapedia.bulbagarden.net/wiki/Character_encoding_in_Generation_III
    /// </remarks>
    public sealed class Gen3String {

        private string value;
        private int length;
        private bool japanese;

        private static readonly Dictionary<byte, char> tableJapanese = new Dictionary<byte, char> {
            {0x00, ' '}, {0x01, 'あ'}, {0x02, 'い'}, {0x03, 'う'}, {0x04, 'え'}, {0x05, 'お'}, {0x06, 'か'}, {0x07, 'き'},
            {0x08, 'く'}, {0x09, 'け'}, {0x0A, 'こ'}, {0x0B, 'さ'}, {0x0C, 'し'}, {0x0D, 'す'}, {0x0E, 'せ'}, {0x0F, 'そ'},
            {0x10, 'た'}, {0x11, 'ち'}, {0x12, 'つ'}, {0x13, 'て'}, {0x14, 'と'}, {0x15, 'な'}, {0x16, 'に'}, {0x17, 'ぬ'},
            {0x18, 'ね'}, {0x19, 'の'}, {0x1A, 'は'}, {0x1B, 'ひ'}, {0x1C, 'ふ'}, {0x1D, 'へ'}, {0x1E, 'ほ'}, {0x1F, 'ま'},
            {0x20, 'み'}, {0x21, 'む'}, {0x22, 'め'}, {0x23, 'も'}, {0x24, 'や'}, {0x25, 'ゆ'}, {0x26, 'よ'}, {0x27, 'ら'},
            {0x28, 'り'}, {0x29, 'る'}, {0x2A, 'れ'}, {0x2B, 'ろ'}, {0x2C, 'わ'}, {0x2D, 'を'}, {0x2E, 'ん'}, {0x2F, 'ぁ'},
            {0x30, 'ぃ'}, {0x31, 'ぅ'}, {0x32, 'ぇ'}, {0x33, 'ぉ'}, {0x34, 'ゃ'}, {0x35, 'ゅ'}, {0x36, 'ょ'}, {0x37, 'が'},
            {0x38, 'ぎ'}, {0x39, 'ぐ'}, {0x3A, 'げ'}, {0x3B, 'ご'}, {0x3C, 'ざ'}, {0x3D, 'じ'}, {0x3E, 'ず'}, {0x3F, 'ぜ'},
            {0x40, 'ぞ'}, {0x41, 'だ'}, {0x42, 'ぢ'}, {0x43, 'づ'}, {0x44, 'で'}, {0x45, 'ど'}, {0x46, 'ば'}, {0x47, 'び'},
            {0x48, 'ぶ'}, {0x49, 'べ'}, {0x4A, 'ぼ'}, {0x4B, 'ぱ'}, {0x4C, 'ぴ'}, {0x4D, 'ぷ'}, {0x4E, 'ぺ'}, {0x4F, 'ぽ'},
            {0x50, 'っ'}, {0x51, 'ア'}, {0x52, 'イ'}, {0x53, 'ウ'}, {0x54, 'エ'}, {0x55, 'オ'}, {0x56, 'カ'}, {0x57, 'キ'},
            {0x58, 'ク'}, {0x59, 'ケ'}, {0x5A, 'コ'}, {0x5B, 'サ'}, {0x5C, 'シ'}, {0x5D, 'ス'}, {0x5E, 'セ'}, {0x5F, 'ソ'},
            {0x60, 'タ'}, {0x61, 'チ'}, {0x62, 'ツ'}, {0x63, 'テ'}, {0x64, 'ト'}, {0x65, 'ナ'}, {0x66, 'ニ'}, {0x67, 'ヌ'},
            {0x68, 'ネ'}, {0x69, 'ノ'}, {0x6A, 'ハ'}, {0x6B, 'ヒ'}, {0x6C, 'フ'}, {0x6D, 'ヘ'}, {0x6E, 'ホ'}, {0x6F, 'マ'},
            {0x70, 'ミ'}, {0x71, 'ム'}, {0x72, 'メ'}, {0x73, 'モ'}, {0x74, 'ヤ'}, {0x75, 'ユ'}, {0x76, 'ヨ'}, {0x77, 'ラ'},
            {0x78, 'リ'}, {0x79, 'ル'}, {0x7A, 'レ'}, {0x7B, 'ロ'}, {0x7C, 'ワ'}, {0x7D, 'ヲ'}, {0x7E, 'ン'}, {0x7F, 'ァ'},
            {0x80, 'ィ'}, {0x81, 'ゥ'}, {0x82, 'ェ'}, {0x83, 'ォ'}, {0x84, 'ャ'}, {0x85, 'ュ'}, {0x86, 'ョ'}, {0x87, 'ガ'},
            {0x88, 'ギ'}, {0x89, 'グ'}, {0x8A, 'ゲ'}, {0x8B, 'ゴ'}, {0x8C, 'ザ'}, {0x8D, 'ジ'}, {0x8E, 'ズ'}, {0x8F, 'ゼ'},
            {0x90, 'ゾ'}, {0x91, 'ダ'}, {0x92, 'ヂ'}, {0x93, 'ヅ'}, {0x94, 'デ'}, {0x95, 'ド'}, {0x96, 'バ'}, {0x97, 'ビ'},
            {0x98, 'ブ'}, {0x99, 'ベ'}, {0x9A, 'ボ'}, {0x9B, 'パ'}, {0x9C, 'ピ'}, {0x9D, 'プ'}, {0x9E, 'ペ'}, {0x9F, 'ポ'},
            {0xA0, 'ッ'}, {0xA1, '0'}, {0xA2, '1'}, {0xA3, '2'}, {0xA4, '3'}, {0xA5, '4'}, {0xA6, '5'}, {0xA7, '6'},
            {0xA8, '7'}, {0xA9, '8'}, {0xAA, '9'}, {0xAB, '！'}, {0xAC, '？'}, {0xAD, '。'}, {0xAE, 'ー'}, {0xAF, '・'},
            {0xB0, ' '}, {0xB1, '『'}, {0xB2, '』'}, {0xB3, '「'}, {0xB4, '」'}, {0xB5, '♂'}, {0xB6, '♀'}, {0xB7, '円'},
            {0xB8, '.'}, {0xB9, '×'}, {0xBA, '/'}, {0xBB, 'A'}, {0xBC, 'B'}, {0xBD, 'C'}, {0xBE, 'D'}, {0xBF, 'E'},
            {0xC0, 'F'}, {0xC1, 'G'}, {0xC2, 'H'}, {0xC3, 'I'}, {0xC4, 'J'}, {0xC5, 'K'}, {0xC6, 'L'}, {0xC7, 'M'},
            {0xC8, 'N'}, {0xC9, 'O'}, {0xCA, 'P'}, {0xCB, 'Q'}, {0xCC, 'R'}, {0xCD, 'S'}, {0xCE, 'T'}, {0xCF, 'U'},
            {0xD0, 'V'}, {0xD1, 'W'}, {0xD2, 'X'}, {0xD3, 'Y'}, {0xD4, 'Z'}, {0xD5, 'a'}, {0xD6, 'b'}, {0xD7, 'c'},
            {0xD8, 'd'}, {0xD9, 'e'}, {0xDA, 'f'}, {0xDB, 'g'}, {0xDC, 'h'}, {0xDD, 'i'}, {0xDE, 'j'}, {0xDF, 'k'},
            {0xE0, 'l'}, {0xE1, 'm'}, {0xE2, 'n'}, {0xE3, 'o'}, {0xE4, 'p'}, {0xE5, 'q'}, {0xE6, 'r'}, {0xE7, 's'},
            {0xE8, 't'}, {0xE9, 'u'}, {0xEA, 'v'}, {0xEB, 'w'}, {0xEC, 'x'}, {0xED, 'y'}, {0xEE, 'z'}, {0xEF, '▶'},
            {0xF0, ':'}, {0xF1, 'Ä'}, {0xF2, 'Ö'}, {0xF3, 'Ü'}, {0xF4, 'ä'}, {0xF5, 'ö'}, {0xF6, 'ü'}, {0xF7, '⬆'},
            {0xF8, '⬇'}, {0xF9, '⬅'}
        };

        // TODO: How to handle these multichars?
        // {0x34, 'Lv'}, {0x53, 'PK'}, {0x54, 'MN'}, {0x55, 'PO'}, {0x56, 'Ké'}, {0x57, 'Bl'}, {0x58, 'Oc'}, {0x59, 'K'}, 
        private static readonly Dictionary<byte, char> tableEnglish = new Dictionary<byte, char> {
            {0x00, ' '}, {0x01, 'À'}, {0x2, 'Á'}, {0x3, 'Â'}, {0x4, 'Ç'}, {0x5, 'È'}, {0x6, 'É'}, {0x7, 'Ê'},
            {0x08, 'Ë'}, {0x09, 'Ì'}, {0xB, 'Î'}, {0xC, 'Ï'}, {0xD, 'Ò'}, {0xE, 'Ó'}, {0xF, 'Ô'},
            {0x10, 'Œ'}, {0x11, 'Ù'}, {0x12, 'Ú'}, {0x13, 'Û'}, {0x14, 'Ñ'}, {0x15, 'ß'}, {0x16, 'à'}, {0x17, 'á'},
            {0x19, 'ç'}, {0x1A, 'è'}, {0x1B, 'é'}, {0x1C, 'ê'}, {0x1D, 'ë'}, {0x1E, 'ì'},
            {0x20, 'î'}, {0x21, 'ï'}, {0x22, 'ò'}, {0x23, 'ó'}, {0x24, 'ô'}, {0x25, 'œ'}, {0x26, 'ù'}, {0x27, 'ú'},
            {0x28, 'û'}, {0x29, 'ñ'}, {0x2A, 'º'}, {0x2B, 'ª'}, {0x2C, '?'}, {0x2D, '&'}, {0x2E, '+'},
            {0x35, '='}, {0x51, '¿'}, {0x52, '¡'}, {0x5A, 'Í'}, {0x5B, '%'}, {0x5C, '('}, {0x5D, ')'},
            {0x68, 'â'}, {0x6F, 'í'}, {0x79, '⬆'}, {0x7A, '⬇'}, {0x7B, '⬅'}, {0x7C, '➡'},
            {0xAB, '!'}, {0xAC, '?'}, {0xAD, '.'}, {0xAE, '-'}, {0xAF, '・'}, {0xB0, '…'}, {0xB1, '“'}, {0xB2, '”'},
            {0xB3, '‘'}, {0xB4, '’'}, {0xB5, '♂'}, {0xB6, '♀'}, {0xB7, '$'}, {0xB8, ','}
        };

        /// <summary>
        /// Decodes a Poké-string to a Unicode string.
        /// </summary>
        /// <param name="buffer">The byte buffer to read from.</param>
        /// <param name="offset">The offset to start reading at.</param>
        /// <param name="length">The maximum number of bytes to parse, if a terminator is not found.</param>
        /// <param name="japanese">If true, use the Japanese character table.</param>
        public static Gen3String Decode(byte[] buffer, int offset, int length, bool japanese) {
            // look up each byte in the character table
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++) {
                byte encoded = buffer[offset + i];
                if (encoded == 0xFF) break; // terminator character
                // english table functions like an override, so look up english if key is present
                // in the english table, otherwise use jap table
                if (!japanese && tableEnglish.ContainsKey(encoded))
                    sb.Append(tableEnglish[encoded]);
                else
                    sb.Append(tableJapanese[encoded]);
            }
            // store in an instance
            Gen3String ret = new Gen3String();
            ret.value = sb.ToString();
            ret.length = length;
            ret.japanese = japanese;
            return ret;
        }

        /// <summary>
        /// Performs a reverse table lookup, returning the byte associated with the specified character.
        /// </summary>
        private static byte? ReverseLookup(Dictionary<byte, char> dict, char decoded) {
            foreach (var pair in dict) {
                if (pair.Value == decoded)
                    return pair.Key;
            }
            return null;
        }

        /// <summary>
        /// Re-encodes the string to the proprietary character encoding format.
        /// </summary>
        /// <param name="buffer">The byte buffer to write the string to.</param>
        /// <param name="offset">The offset at which to start writing.</param>
        public void Encode(byte[] buffer, int offset) {
            for (int i = 0; i < length; i++) {
                if (i < value.Length) {
                    // encode the character and write it to the buffer
                    char decoded = value[i];
                    byte? encoded;
                    if (!japanese)
                        encoded = ReverseLookup(tableEnglish, decoded) ?? ReverseLookup(tableJapanese, decoded);
                    else
                        encoded = ReverseLookup(tableJapanese, decoded);

                    if (!encoded.HasValue)
                        throw new ArgumentException("The character '" + decoded + "' cannot be mapped to the Gen-III encoding");

                    buffer[offset + i] = encoded.Value;

                } else if (i == value.Length) {
                    // write string terminator
                    buffer[offset + i] = 0xFF;
                } else {
                    // padding bytes
                    buffer[offset + i] = 0x00;
                }
            }
        }

        /// <summary>
        /// Sets the value of this string instance.
        /// </summary>
        public void SetValue(string val) {
            // hard cap to buffer length
            if (val.Length > length)
                val = val.Substring(0, length);
            // replace
            this.value = val;
        }

        /// <summary>
        /// Returns the Unicode <see cref="string"/> value of this <see cref="Gen3String"/>.
        /// </summary>
        public static implicit operator string(Gen3String g3str) {
            return g3str.ToString();
        }

        /// <summary>
        /// Returns the Unicode <see cref="string"/> value of this <see cref="Gen3String"/>.
        /// </summary>
        public override string ToString() {
            return value;
        }

    }

}
