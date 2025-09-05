using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace PusulaAssessment
{
    class LongestVowelSubsequenceAsJson
    {
        public static string LongestVowelSubsequence(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return "[]";
            }

            char[] sesliHarfler = { 'a', 'e', 'i', 'o', 'u' };

            var results = words.Select(word =>
            {
                string bestSequence = "";
                string currentSequence = "";

                foreach (char c in word)
                {
                    if (sesliHarfler.Contains(char.ToLower(c))) // LINQ kullanımı
                    {
                        currentSequence += c;
                        if (currentSequence.Length > bestSequence.Length)
                        {
                            bestSequence = currentSequence;
                        }
                    }
                    else
                    {
                        currentSequence = ""; // Sessiz harf geldiği için sıfırlanır
                    }
                }

                return new
                {
                    word = word,
                    sequence = bestSequence,
                    length = bestSequence.Length
                };
            }).ToList();

            return JsonSerializer.Serialize(results);
        
        }

    }
  
}
