using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;


namespace PusulaAssessment
{
    class Filter_Employees
    {
        
        public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Türkçe karakterleri korumak için                
            };

            if (employees == null || !employees.Any())
            {
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0m,
                    AverageSalary = 0m,
                    MinSalary = 0m,
                    MaxSalary = 0m,
                    Count = 0
                }, options);
            }

            // Filtreleme işlemleri 
            var filtered = employees
                .Where(e => e.Age >= 25 && e.Age <= 40)
                .Where(e => e.Department == "IT" || e.Department == "Finance")
                .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
                .Where(e => e.HireDate > new DateTime(2017, 1, 1))
                .ToList();

            var result = new
            {
                Names = filtered
                    .Select(e => e.Name) 
                    .OrderByDescending(n => n.Length) // Uzunluğa göre azalan sıralama 
                    .ThenBy(n => n) // Alfabetik sıralama 
                    .ToList(), 

                // Maaşların toplamı hesaplanıyor (Sum)
                // Eğer hiç eleman yoksa null dönebilir, bu yüzden ?? 0m ile 0’a çekiyorum
                TotalSalary = filtered.Sum(e => (decimal?)e.Salary) ?? 0m,

                // Ortalama maaş hesaplanıyor (Average)
                // Eleman yoksa hata olmaması için Any() ile kontrol yapılıyor
                // Varsa ortalamayı alıp 2 basamak yuvarlanıyor (Math.Round)
                AverageSalary = filtered.Any() ? Math.Round(filtered.Average(e => e.Salary), 2) : 0m,

                // Maaşlar arasındaki en küçük değer alınıyor (Min)
                // Eğer hiç eleman yoksa 0 döndürülüyor
                MinSalary = filtered.Any() ? filtered.Min(e => e.Salary) : 0m,

                // Maaşlar arasındaki en büyük değeri alınıyor (Max)
                // Eğer hiç eleman yoksa 0 döndürülüyor
                MaxSalary = filtered.Any() ? filtered.Max(e => e.Salary) : 0m,

                // Filtrelenmiş toplam çalışan sayısı alınıyor (Count)
                Count = filtered.Count
            };

            return JsonSerializer.Serialize(result, options);
        }
    }
}
