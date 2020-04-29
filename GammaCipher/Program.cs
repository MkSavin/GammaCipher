using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaCipher {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Gamma code: [int array] [empty = next step]: ");
            var code = ReadInputArray();

            var inputString = "";
            string input;

            Console.WriteLine("Input string [empty = next step]: ");
            do {
                input = Console.ReadLine().Trim().ToLower();
                inputString += input + '\n';
            } while (input != "");

            var result = Gammate(code, inputString);

            Console.WriteLine("Encryption result:");
            Console.WriteLine(result);

            Console.ReadKey();

        }

        static int[] ReadInputArray() {

            List<int> result = new List<int>();

            string input;
            int inputInt;

            do {
                input = Console.ReadLine().Trim().ToLower();

                if (input.Contains(" ")) {
                    foreach (var item in input.Split(' ')) {
                        if (int.TryParse(item, out inputInt)) {
                            result.Add(inputInt);
                        }
                    }

                    continue;
                }

                if (input == "" || !int.TryParse(input, out inputInt)) {
                    break;
                }

                result.Add(inputInt);
            } while (true);

            return result.ToArray();

        }

        public static string Gammate(int[] code, string inputString) {

            var result = "";

            int[] bi;
            int[] bc;

            for (int i = 0; i < inputString.Length; i++) {

                bc = ToBin(code[i % code.Length]);
                bi = ToBin(inputString[i]);

                if (bc != null && bi != null) {
                    bc = BinAdd(bc, bi);

                    result += FromBin(bc);
                } else {
                    result += inputString[i];
                }

            }

            return result;

        }

        public static int[] ToBin(int a) {

            var bin = Convert.ToString(a, 2);
            return bin.Select(o => int.Parse(o + "")).ToArray();

        }

        static string[] alphabet_codes = new string[] {
            "000001",  "001001",  "001010",  "001011",  "001100",  "000010",
            "001101",  "001110",  "000011",  "001111",  "010000",  "010001",
            "010010",  "000100",  "010011",  "010100",  "010101",  "010110",
            "000101",  "010111",  "011000",  "011001",  "011010",  "011011",
            "011100",  "011101",  "011110",  "000110",  "000111",  "001000",
        };

        static char[] alphabet = new char[] {
            'а',  'б',  'в',  'г',  'д',  'е',
            'ж',  'з',  'и',  'к',  'л',  'м',
            'н',  'о',  'п',  'р',  'с',  'т',
            'у',  'ф',  'х',  'ц',  'ч',  'ш',
            'щ',  'ы',  'ь',  'э',  'ю',  'я'
        };

        // Unicode-based table
        //public static int[] ToBin(char c) {

        //    var d = ToBin((int)c);
        //    return d;

        //}

        // Custome table
        public static int[] ToBin(char c) {

            var i = Array.IndexOf(alphabet, c);

            if (i >= 0) {
                return alphabet_codes[i].Select(o => int.Parse(o + "")).ToArray();
            }

            return null;

        }

        // Custome table
        public static char FromBin(int[] b) {

            var s = string.Join("", b.Select(p => p.ToString()).ToArray());

            var i = Array.IndexOf(alphabet_codes, s);

            if (i >= 0) {
                return alphabet[i];
            }

            return (char) 0;

        }

        public static int[] BinAdd(int[] a, int[] b) {

            int minLen = Math.Min(a.Length, b.Length);
            int maxLen = Math.Max(a.Length, b.Length);

            int sub = maxLen - minLen;

            bool aB = a.Length > b.Length;

            int[] result = new int[maxLen];

            for (int i = 0; i < maxLen; i++) {
                result[i] = aB ? a[i] : b[i];
            }

            for (int i = sub; i < maxLen; i++) {
                result[i] = ((a[i - (aB ? 0 : sub)] == 1) ^ (b[i - (aB ? sub : 0)] == 1)) ? 1 : 0;
            }

            return result;

        }
    }
}
