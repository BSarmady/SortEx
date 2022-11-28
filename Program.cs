using System;
using System.Collections.Generic;
using System.IO;

namespace SortEX {
    class Program {
        static void Main(string[] args) {
            string inFilename = "";
            string outFilename = "";
            bool TrimLines = true;

            if (args.Length < 1 || args.Length > 3) {
                Console.WriteLine("Removes space from start and end of lines, Sorts and filters duplicate lines");
                Console.WriteLine("");
                Console.WriteLine("Usage:");
                Console.WriteLine("    SortEx inFileName outFileName -notrim");
                Console.WriteLine("");
                Console.WriteLine("    -notrim: Do not trim spaces and tabs from lines");
                Console.WriteLine("    if output file exists it will overwrite it.");
                Console.WriteLine("");
                Console.ReadKey();
                return;
            }
            if (args.Length >= 1) {
                inFilename = args[0];
            }
            if (args.Length >= 2) {
                outFilename = args[1];
            }
            if (args.Length == 3 && args[2].ToLower() == "-notrim") {
                TrimLines = false;
            }
            if (!File.Exists(inFilename)) {
                Console.WriteLine("Input file does not exists.");
                Console.ReadKey();
                return;
            }
            if (outFilename == "") {
                outFilename = Path.GetDirectoryName(inFilename) + "\\" + Path.GetFileNameWithoutExtension(inFilename) + ".sorted.txt";
            }
            if (File.Exists(outFilename)) {
                string bakfile = Path.GetDirectoryName(inFilename) + "\\" + Path.GetFileNameWithoutExtension(outFilename) + ".out.bak";
                File.Delete(bakfile);
                File.Move(outFilename, bakfile);
            }

            List<string> st = new List<string> { };
            Console.WriteLine("Using " + inFilename + " as input file");
            string[] arrSt = File.ReadAllLines(inFilename);
            foreach (string line in arrSt) {
                string st1;
                if (TrimLines) {
                    st1 = line.Trim(new char[] { '\t', ' ' });
                } else {
                    st1 = line;
                }
                if (!st.Contains(st1)) {
                    st.Add(st1);
                }
            }

            st.Sort();
            File.WriteAllLines(outFilename, st.ToArray());
            Console.WriteLine("Output to " + outFilename);

        }
    }
}
