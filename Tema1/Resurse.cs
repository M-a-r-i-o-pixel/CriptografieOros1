using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    internal static class Resurse
    {

        public static char[] smallAlphabet;
        public static char[] bigAlphabet;
        public static Dictionary<char, bool> smallAlphabetLookup;

        static Resurse()
        {
            smallAlphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            bigAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            smallAlphabetLookup = smallAlphabet.ToDictionary(c => c, c => true);
        }

        public static int match_score(string text, string[] words) {
            Dictionary<string, bool> wordLookup = new Dictionary<string, bool>();
            foreach (string word in words) wordLookup[word] = true;
            int score = 0;
            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < text.Length - i; j += 1) {
                    StringBuilder wordBuilder = new StringBuilder();
                    for (int t = j; t < j + i; t++) wordBuilder.Append(text[t]);
                    string word = wordBuilder.ToString();
                    if (wordLookup.ContainsKey(word)) score++;
                }
            }
            return score;
        }

        public static void genPerm(List<char> v, List<char> chars, HashSet<char> used, List<List<char>> res)
        {
            if (v.Count == chars.Count)
            {
                res.Add(new List<char>(v));
                return;
            }
            foreach (char c in chars)
            {
                if (used.Contains(c)) continue;
                v.Add(c);
                used.Add(c);
                genPerm(v, chars, used, res);
                v.RemoveAt(v.Count - 1);
                used.Remove(c);
            }
        }


        public static List<List<char>> permFirstK(List<char> characters,int k)
        {
            List<char> sub_characters = new List<char>();
            for (int i = 0; i < k; i++) sub_characters.Add(characters[i]);
            List<List<char>> firstPerms = new List<List<char>>();
            HashSet<char> used = new HashSet<char>();
            List<char> v = new List<char>();
            genPerm(v, sub_characters, used, firstPerms);
            List<List<char>> res = new List<List<char>>();
            foreach (List<char> p in firstPerms)
            {
                List<char> aux = new List<char>(characters);
                for (int i = 0; i < k; i++)
                {
                    aux[i] = p[i];
                }
                res.Add(aux);
            }
            return res;
        }




    }
}
