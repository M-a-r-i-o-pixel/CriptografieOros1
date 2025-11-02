using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tema1
{
    internal class NRot : Algoritm<int>
    {
       protected Dictionary<char, char> enc_map;
       protected Dictionary<char, char> dec_map;
        protected int k;
        internal string wordlistPath;
        internal NRot(string inputPath, string outputPath, int k,string wordlistPath="wordlist_romana.txt") : base(k, inputPath, outputPath)
        {
            this.wordlistPath = wordlistPath;
            enc_map = new Dictionary<char, char>();
            dec_map = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                char plain_char = Resurse.smallAlphabet[i];
                char cypher_char = Resurse.smallAlphabet[(i + k) % 26];
                enc_map[plain_char] = cypher_char;
                dec_map[cypher_char] = plain_char;

            }
        }
        internal void setKey(int k) { this.k = k; }
        public override void criptare()
        {
            string plain_text = File.ReadAllText(InputPath);
            StringBuilder cypher_text = new StringBuilder();
            foreach (char c in plain_text)
            {
                char c2 = Char.ToLower(c);
                if (!enc_map.ContainsKey(c)) continue;
                c2 = enc_map[c2];
                cypher_text.Append(c2);
            }
            File.WriteAllText(OutputPath, cypher_text.ToString());
        }
        public override void decriptare()
        {
            string cyphered_text = File.ReadAllText(InputPath);
            cyphered_text = cyphered_text.ToLower();
            StringBuilder plain_text = new StringBuilder();
            foreach (char c in cyphered_text)
            {
                if (!dec_map.ContainsKey(c)) continue;
                char c2 = dec_map[c];
                plain_text.Append(c2);
            }
            File.WriteAllText(OutputPath, plain_text.ToString());
        }

        public void analiza()
        {
            string[] words = File.ReadAllText(wordlistPath).Split(new char[] { ' ','\t','\r','\n'});
            string cypherText=File.ReadAllText(InputPath);
            HashSet<string> commonWords=new HashSet<string>(words);
            int bestScore = 0;
            int bestKey = 0;
            string bestText = "";
            for (int k = 0; k < 26; k++) {
                StringBuilder candidateBuilder = new StringBuilder();
                foreach (char c in cypherText) {
                    if (!Resurse.smallAlphabet.Contains(c)) continue;
                    int enc_idx = Array.IndexOf(Resurse.smallAlphabet,c);
                    int plain_idx = (enc_idx - k + 26) % 26;
                    char plain_char = Resurse.smallAlphabet[plain_idx];
                    candidateBuilder.Append(plain_char);
                }
                string candidateText = candidateBuilder.ToString();
                int score = Resurse.match_score(candidateText, words);
                if (score >= bestScore) {
                    bestScore = score;
                    bestKey = k;
                    bestText = candidateText;
                }
                
            }
            File.WriteAllText(OutputPath,bestText);
        }


    }
}
