using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CountriesJson
{
    class CountriesMain
    {
        static void Main(string[] args)
        {

            var countries = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(@"..\..\countries.json"));
            countries.ForEach(Console.WriteLine);

            var populationSum = countries
                .Sum(c => c.Population);
            Console.WriteLine($"Общая численность населения: {populationSum}");
            Console.WriteLine();

            var currenciesList = countries
                .Select(c => c.Currencies)
                .ToList();

            Console.WriteLine("Перечень валют: ");

            foreach (var currencies in currenciesList)
            {
                foreach (var c in currencies)
                {
                    Console.WriteLine(c);
                }
            }

            var currenciesNamesList = countries
                    .SelectMany(c => c.Currencies)
                    .Select(c => c.Name)
                    .Distinct()
                    .ToList();

            Console.WriteLine();
            Console.WriteLine("Перечень уникальных наименований валют: ");
            currenciesNamesList.ForEach(Console.WriteLine);
        }
    }
}