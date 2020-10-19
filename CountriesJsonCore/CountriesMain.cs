using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CountriesJsonCore
{
    class CountriesMain
    {
        static void Main(string[] args)
        {
            var countries = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(@"..\..\..\countries.json"));
            countries.ForEach(Console.WriteLine);
            Console.WriteLine();

            var populationSum = countries.Sum(c => c.Population);
            Console.WriteLine($"Общая численность населения: {populationSum}");
            Console.WriteLine();

            var currenciesNamesList = countries
                .SelectMany(country => country.Currencies)
                .Where(currency => currency.Name != null)
                .Select(currency => currency.Name)
                .ToList();

            Console.WriteLine("Общий перечень наименований валют: ");
            currenciesNamesList.ForEach(Console.WriteLine);

            var currenciesDistinctNamesList = currenciesNamesList
                .Distinct()
                .ToList();

            Console.WriteLine();
            Console.WriteLine("Перечень уникальных наименований валют: ");
            currenciesDistinctNamesList.ForEach(Console.WriteLine);
        }
    }
}