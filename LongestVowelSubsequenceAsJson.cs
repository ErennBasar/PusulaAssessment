using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace PusulaAssessment
{
    class LongestVowelSubsequence
    {
        public static string LongestVowelSubsequenceAsJson(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return "[]";
            }

            char[] sesliHarfler = { 'a', 'e', 'i', 'o', 'u' };

            // words listesindeki her kelime tek tek işlenir
            var results = words.Select(word =>
            {
                // En uzun sesli harf dizisini tutmak için
                string bestSequence = "";

                // Geçici olarak mevcut sesli harf dizisini tutmak için
                string currentSequence = "";
                
                foreach (char c in word)
                {
                    if (sesliHarfler.Contains(char.ToLower(c))) 
                    {
                        // Eğer harf sesli harfler içinde varsa mevcut diziyi güncelle
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
