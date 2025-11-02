using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;

namespace Tema1
{
    internal class Substitution : Algoritm<string>
    {

        protected Dictionary<char, char> enc_map;
        protected Dictionary<char, char> dec_map;
        internal string manuscriptPath;
        internal Substitution(string key,string inputPath,string outputPath) : base(key,inputPath,outputPath) {
            manuscriptPath = "";
            bool ok_key = true;
            foreach (char c in key) {
                char small_c = Char.ToLower(c);
                if (!Resurse.smallAlphabetLookup.ContainsKey(small_c))
                {
                    ok_key = false;
                    break;
                }
            }
            if (Key.Length != 26) ok_key = false;
            if (!ok_key) { key = new string(Resurse.smallAlphabet); Console.Write("\nThe key is not correct!\n"); }
            enc_map = new Dictionary<char, char>();
            dec_map = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                char c1 = Resurse.smallAlphabet[i];
                char c2 = key[i];
                enc_map[c1] = c2;
                dec_map[c2] = c1;
            }

        
        
        }

        public override void criptare() {
            string plain = File.ReadAllText(InputPath);
            StringBuilder cypher = new StringBuilder();
            foreach (char c in plain) {
                char plain_char = Char.ToLower(c);
                if(!enc_map.ContainsKey(plain_char)) continue;
                char enc_char = enc_map[plain_char];
                cypher.Append(enc_char);
            }
            File.WriteAllText(OutputPath, cypher.ToString());
        }

        public override void decriptare()
        {
            string cypher = File.ReadAllText(InputPath);
            StringBuilder plain = new StringBuilder();
            foreach (char c in cypher) { 
            char cypher_char = Char.ToLower(c);
                if (!dec_map.ContainsKey(cypher_char)) continue;
                char dec_char = dec_map[cypher_char];
                plain.Append(dec_char);
            }
            File.WriteAllText(OutputPath,plain.ToString());
        }

        public void analiza() {
            if (manuscriptPath == "") { Console.WriteLine("\n---['analiza' function can't be executed]\n---[Please, first set 'manuscriptPath' !!!]\n"); return; }
            string manuscript = File.ReadAllText(manuscriptPath).ToLower();
            Dictionary<char, int> fv_language = Resurse.smallAlphabet.ToDictionary(c => c, c => 0);
            foreach(char c in manuscript) { 
                if (!Resurse.smallAlphabetLookup.ContainsKey(c)) continue;
                fv_language[c] += 1;
            }
            Dictionary<char, int> fv_text = new Dictionary<char, int>();
            string cypherText = File.ReadAllText(InputPath).ToLower();
            foreach (char c in cypherText) {
                if (!Resurse.smallAlphabetLookup.ContainsKey(c)) continue;
                if (!fv_text.ContainsKey(c)) fv_text[c] = 1;
                else fv_text[c] += 1;
            }
            List<char> sortedKeysLanguage = fv_language.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).ToList();
            List<char> sortedKeysText = fv_text.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).ToList();
            int commonLength = Math.Min(sortedKeysLanguage.Count,sortedKeysText.Count);

            int v = 0;
            string result = "";
            List<List<char>> sortedKeysLanguage7perm =Resurse.permFirstK(sortedKeysLanguage, 4);
            foreach (List<char> sortedKeysLanguagePerm in sortedKeysLanguage7perm)
            {
                Dictionary<char, char> cypherToPlain = new Dictionary<char, char>();
                for (int i = 0; i < commonLength; i++) cypherToPlain[sortedKeysText[i]] = sortedKeysLanguagePerm[i];
                StringBuilder decyBuilder = new StringBuilder();
                foreach (char c in cypherText)
                {
                    if (!cypherToPlain.ContainsKey(c))
                    {
                        decyBuilder.Append(c);
                        continue;
                    }
                    decyBuilder.Append(cypherToPlain[c]);
                }
                string decy = decyBuilder.ToString();
                v += 1;
                result += $"\n\nvarianta {v}:\n{decy}\n____________________________________________";
            }

            File.WriteAllText(OutputPath, result);
            return;
            
        }




    }
}
