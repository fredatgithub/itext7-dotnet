/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Text;

namespace iText.Kernel.Numbering {
    /// <summary>
    /// This class is responsible for converting integer numbers to their
    /// Greek alphabet letter representations.
    /// </summary>
    /// <remarks>
    /// This class is responsible for converting integer numbers to their
    /// Greek alphabet letter representations.
    /// We are aware of the fact that the original Greek numbering is different.
    /// See http://www.cogsci.indiana.edu/farg/harry/lan/grknum.htm#ancient
    /// but this isn't implemented yet; the main reason being the fact that we
    /// need a font that has the obsolete Greek characters qoppa and sampi.
    /// So we use standard 24 letter Greek alphabet
    /// </remarks>
    public class GreekAlphabetNumbering {
        protected internal static readonly char[] ALPHABET_LOWERCASE;

        protected internal static readonly char[] ALPHABET_UPPERCASE;

        protected internal const int ALPHABET_LENGTH = 24;

        static GreekAlphabetNumbering() {
            ALPHABET_LOWERCASE = new char[ALPHABET_LENGTH];
            ALPHABET_UPPERCASE = new char[ALPHABET_LENGTH];
            for (int i = 0; i < ALPHABET_LENGTH; i++) {
                ALPHABET_LOWERCASE[i] = (char)(945 + i + (i > 16 ? 1 : 0));
                ALPHABET_UPPERCASE[i] = (char)(913 + i + (i > 16 ? 1 : 0));
            }
        }

        /// <summary>Converts the given number to its Greek alphabet lowercase string representation.</summary>
        /// <remarks>
        /// Converts the given number to its Greek alphabet lowercase string representation.
        /// E.g. 1 will be converted to a string consisting of a unicode character for greek small letter alpha,
        /// 2 - a string consisting of a unicode character for greek small letter beta,
        /// 25 - a string consisting of two unicode characters for greek small letter alpha, and so on.
        /// </remarks>
        /// <param name="number">the number greater than zero to be converted</param>
        /// <returns>Greek alphabet lowercase string representation of an integer.</returns>
        public static String ToGreekAlphabetNumberLowerCase(int number) {
            return AlphabetNumbering.ToAlphabetNumber(number, ALPHABET_LOWERCASE);
        }

        /// <summary>Converts the given number to its Greek alphabet uppercase string representation.</summary>
        /// <remarks>
        /// Converts the given number to its Greek alphabet uppercase string representation.
        /// E.g. 1 will be converted to a string consisting of a unicode character for greek capital letter alpha,
        /// 2 - a string consisting of a unicode character for greek capital letter beta,
        /// 25 - a string consisting of two unicode characters for greek capital letter alpha, and so on.
        /// </remarks>
        /// <param name="number">the number greater than zero to be converted</param>
        /// <returns>Greek alphabet uppercase string representation of an integer.</returns>
        public static String ToGreekAlphabetNumberUpperCase(int number) {
            return AlphabetNumbering.ToAlphabetNumber(number, ALPHABET_UPPERCASE);
        }

        /// <summary>Converts the given number to its Greek alphabet string representation.</summary>
        /// <remarks>
        /// Converts the given number to its Greek alphabet string representation.
        /// E.g. for <c>upperCase</c> set to false,
        /// 1 will be converted to a string consisting of a unicode character for greek small letter alpha,
        /// 2 - a string consisting of a unicode character for greek small letter beta,
        /// 25 - a string consisting of two unicode characters for greek small letter alpha, and so on.
        /// </remarks>
        /// <param name="number">the number greater than zero to be converted</param>
        /// <param name="upperCase">whether to use uppercase or lowercase alphabet</param>
        /// <returns>Greek alphabet string representation of an integer.</returns>
        public static String ToGreekAlphabetNumber(int number, bool upperCase) {
            return ToGreekAlphabetNumber(number, upperCase, false);
        }

        /// <summary>Converts the given number to its Greek alphabet string representation.</summary>
        /// <remarks>
        /// Converts the given number to its Greek alphabet string representation.
        /// E.g. for <c>upperCase</c> set to false,
        /// 1 will be converted to a string consisting of a unicode character for greek small letter alpha
        /// if <c>symbolFont</c> is set to false,
        /// otherwise - a string consisting of the corresponding symbol code in Symbol standard font;
        /// 26 will be converted to a string consisting of two unicode characters:
        /// greek small letter alpha followed by greek small letter beta
        /// if <c>symbolFont</c> is set to false,
        /// otherwise - a string consisting of the corresponding sequence of symbol codes in Symbol standard font.
        /// </remarks>
        /// <param name="number">the number greater than zero to be converted</param>
        /// <param name="upperCase">whether to use uppercase or lowercase alphabet</param>
        /// <param name="symbolFont">if <c>true</c>, then the string representation will be returned ready to write it in Symbol font
        ///     </param>
        /// <returns>Greek alphabet string representation of an integer.</returns>
        public static String ToGreekAlphabetNumber(int number, bool upperCase, bool symbolFont) {
            String result = upperCase ? ToGreekAlphabetNumberUpperCase(number) : ToGreekAlphabetNumberLowerCase(number
                );
            if (symbolFont) {
                StringBuilder symbolFontStr = new StringBuilder();
                for (int i = 0; i < result.Length; i++) {
                    symbolFontStr.Append(GetSymbolFontChar(result[i]));
                }
                return symbolFontStr.ToString();
            }
            else {
                return result;
            }
        }

        /// <summary>Converts a given greek unicode character code into the code of the corresponding char Symbol font.
        ///     </summary>
        /// <param name="unicodeChar">original unicode char</param>
        /// <returns>the corresponding symbol code in Symbol standard font</returns>
        private static char GetSymbolFontChar(char unicodeChar) {
            switch (unicodeChar) {
                case (char)913: {
                    // ALFA
                    return 'A';
                }

                case (char)914: {
                    // BETA
                    return 'B';
                }

                case (char)915: {
                    // GAMMA
                    return 'G';
                }

                case (char)916: {
                    // DELTA
                    return 'D';
                }

                case (char)917: {
                    // EPSILON
                    return 'E';
                }

                case (char)918: {
                    // ZETA
                    return 'Z';
                }

                case (char)919: {
                    // ETA
                    return 'H';
                }

                case (char)920: {
                    // THETA
                    return 'Q';
                }

                case (char)921: {
                    // IOTA
                    return 'I';
                }

                case (char)922: {
                    // KAPPA
                    return 'K';
                }

                case (char)923: {
                    // LAMBDA
                    return 'L';
                }

                case (char)924: {
                    // MU
                    return 'M';
                }

                case (char)925: {
                    // NU
                    return 'N';
                }

                case (char)926: {
                    // XI
                    return 'X';
                }

                case (char)927: {
                    // OMICRON
                    return 'O';
                }

                case (char)928: {
                    // PI
                    return 'P';
                }

                case (char)929: {
                    // RHO
                    return 'R';
                }

                case (char)931: {
                    // SIGMA
                    return 'S';
                }

                case (char)932: {
                    // TAU
                    return 'T';
                }

                case (char)933: {
                    // UPSILON
                    return 'U';
                }

                case (char)934: {
                    // PHI
                    return 'F';
                }

                case (char)935: {
                    // CHI
                    return 'C';
                }

                case (char)936: {
                    // PSI
                    return 'Y';
                }

                case (char)937: {
                    // OMEGA
                    return 'W';
                }

                case (char)945: {
                    // alfa
                    return 'a';
                }

                case (char)946: {
                    // beta
                    return 'b';
                }

                case (char)947: {
                    // gamma
                    return 'g';
                }

                case (char)948: {
                    // delta
                    return 'd';
                }

                case (char)949: {
                    // epsilon
                    return 'e';
                }

                case (char)950: {
                    // zeta
                    return 'z';
                }

                case (char)951: {
                    // eta
                    return 'h';
                }

                case (char)952: {
                    // theta
                    return 'q';
                }

                case (char)953: {
                    // iota
                    return 'i';
                }

                case (char)954: {
                    // kappa
                    return 'k';
                }

                case (char)955: {
                    // lambda
                    return 'l';
                }

                case (char)956: {
                    // mu
                    return 'm';
                }

                case (char)957: {
                    // nu
                    return 'n';
                }

                case (char)958: {
                    // xi
                    return 'x';
                }

                case (char)959: {
                    // omicron
                    return 'o';
                }

                case (char)960: {
                    // pi
                    return 'p';
                }

                case (char)961: {
                    // rho
                    return 'r';
                }

                case (char)962: {
                    // sigma
                    return 'V';
                }

                case (char)963: {
                    // sigma
                    return 's';
                }

                case (char)964: {
                    // tau
                    return 't';
                }

                case (char)965: {
                    // upsilon
                    return 'u';
                }

                case (char)966: {
                    // phi
                    return 'f';
                }

                case (char)967: {
                    // chi
                    return 'c';
                }

                case (char)968: {
                    // psi
                    return 'y';
                }

                case (char)969: {
                    // omega
                    return 'w';
                }

                default: {
                    return ' ';
                }
            }
        }
    }
}
