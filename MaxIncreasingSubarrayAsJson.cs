using System.Text.Json;

namespace PusulaAssessment
{
    class MaxIncreasingSubarrayAsJson
    {
        public static void Main()
        {

            var input4 = new List<int> { 1, 3, 5, 4, 7, 8, 2 };
            //var input5 = new List<int>(); 

            
            Console.WriteLine(MaxIncreasingSubarray(input4));
            //Console.WriteLine(MaxIncreasingSubarrayAsJson(input5)); 
        }


        public static string MaxIncreasingSubarray(List<int> numbers)
        {
            //if list is empty
            if (numbers == null || numbers.Count == 0)
            {
                return "[]";
            }

            List<int> bestArray = new List<int>();
            int bestSum = int.MinValue;

            List<int> currentArray = new List<int>();
            int currentSum = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                // metoda gönderilen dizideki ilgili eleman ile currentArray'e eklenen son eleman karşılaştırılıyor
                if (currentArray.Count == 0 || numbers[i] > currentArray[currentArray.Count - 1])
                {
                    currentArray.Add(numbers[i]);
                    currentSum += numbers[i];
                }
                else
                {
                    if (currentSum > bestSum)
                    {
                        bestSum = currentSum;
                        bestArray = new List<int>(currentArray);
                    }

                    // Artış bozulduğu için mevcut dizi sıfırlanıyor.
                    // Yeni bir artan dizi bu elemandan itibaren başlatılıyor.
                    currentArray.Clear();
                    currentArray.Add(numbers[i]);
                    currentSum = numbers[i];
                }
            }

            //diziyi güncelleme
            if (currentSum > bestSum)
            {
                bestSum = currentSum;
                bestArray = new List<int>(currentArray);
            }

            return JsonSerializer.Serialize(bestArray);
        }
    }
}

    
            

