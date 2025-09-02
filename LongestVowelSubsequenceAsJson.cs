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

            var results = new List<object>();

            foreach (var word in words)
            {
                string bestSequence = "";
                string currentSequence = "";

                foreach (char c in word)
                {
                    if (sesliHarfler.Contains(char.ToLower(c)))
                    {
                        currentSequence += c;
                        if (currentSequence.Length > bestSequence.Length)
                        {
                            bestSequence = currentSequence;
                        }
                    }
                    else
                    {
                        currentSequence = ""; // sessiz harf geldiği için sıfırlanır
                    }
                }


                results.Add(new
                {
                    word = word,
                    sequence = bestSequence,
                    length = bestSequence.Length,
                });
            }
        
            return JsonSerializer.Serialize(results);
        
        }

    }
  
}
