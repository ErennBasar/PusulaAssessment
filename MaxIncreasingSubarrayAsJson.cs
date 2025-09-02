using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace PusulaAssessment
{
    class MaxIncreasingSubarrayAsJson
    {
        public static void Main()
        {
            var input = new List<int> { 1, 3, 5, 5, 4, 4, 7, 8 };            
          
            Console.WriteLine(MaxIncreasingSubarray(input));   
        }


        public static string MaxIncreasingSubarray(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return "[]";
            }
            
            var bestArrays = new List<List<int>>(); // Ardışık dizileri tutmak için
            var current = new List<int>();

            foreach (var num in numbers)
            {
                if (current.Count == 0 || num > current.Last())
                {
                    current.Add(num);
                }
                else
                {
                    bestArrays.Add(current);
                    current = new List<int> { num };
                }
            }

            // son alt diziyi ekledim
            bestArrays.Add(current);

            
            var best = bestArrays
                .OrderByDescending(a => a.Sum()) //Tutulan ardışık dizilerden toplamı büyük olanı aldım
                .FirstOrDefault();

            return JsonSerializer.Serialize(best ?? new List<int>());
        }
    }
}

    
            

