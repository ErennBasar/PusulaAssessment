using System;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using System.Text.Encodings.Web;

namespace PusulaAssessment
{
    class FilterPeople_FromXml
    { 
        public static string FilterPeopleFromXml(string xmlData)
        {
            // Türkçe karakterleri korumak için
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping             
            };

            if (string.IsNullOrWhiteSpace(xmlData))
            {
                return "{}";
            }

            // XML üzerinde LINQ ile rahat sorgulama yapabilmek için XDocument kullanıyorum
            // xmlData string’ini XDocument nesnesine çeviriyorum
            var doc = XDocument.Parse(xmlData);

            // XML içindeki Person elemanlarını seçip gerekli filtrelemeleri yapıyorum
            var people = doc.Descendants("Person")

                            // Her Person elemanını anonim tipe çeviriyorum
                            // Burada gerekli dönüşümleri de yapıyorum
                            .Select(p => new
                            {
                                Name = (string?)p.Element("Name"),
                                Age = (int?)p.Element("Age"),
                                Department = (string?)p.Element("Department"),
                                Salary = (int?)p.Element("Salary"),
                                HireDate = DateTime.Parse((string?)p.Element("HireDate"))
                            })
                            // Filtreleme işlemleri
                            .Where(p => p.Age > 30)
                            .Where(p => p.Department == "IT")
                            .Where(p => p.Salary > 5000)
                            .Where(p => p.HireDate < new DateTime(2019, 1, 1))                            
                            .ToList();

            var result = new
            {
                // İsimler alfabetik sırayla (OrderBy) listeye çevriliyor.
                Names = people.Select(p => p.Name).OrderBy(n => n).ToList(),

                // Maaşların toplamı (Sum), ortalaması (Average) ve en yükseği (Max) hesaplanıyor
                TotalSalary = people.Sum(p => (int?)p.Salary) ?? 0,
                AverageSalary = people.Any() ? (int?)people.Average(p => p.Salary) : 0,
                MaxSalary = people.Any() ? people.Max(p => p.Salary) : 0,

                // Filtrelenen kişi sayısı
                Count = people.Count
            };

            return JsonSerializer.Serialize(result,options);
        }
    }
}
