using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesJson
{
    class Country
    {
        public string Name { get; set; }

        public long Population { get; set; }

        public List<Currency> Currencies { get; set; }

        public override string ToString()
        {
            var currenciesString = new StringBuilder();

            foreach (var c in Currencies)
            {
                currenciesString.Append(c).Append(Environment.NewLine);
            }

            return $"Название: {Name}{Environment.NewLine}" +
                   $"Численность населения: {Population}{Environment.NewLine}" +
                   $"Валюта: {currenciesString}";
        }
    }
}