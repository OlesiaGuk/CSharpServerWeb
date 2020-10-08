using System;
using System.Collections.Generic;

namespace CountriesJsonCore
{
    class Country
    {
        public string Name { get; set; }

        public long Population { get; set; }

        public List<Currency> Currencies { get; set; }

        public override string ToString()
        {
            var currenciesString = string.Join(Environment.NewLine, Currencies);

            return $"Название: {Name}{Environment.NewLine}" +
                   $"Численность населения: {Population}{Environment.NewLine}" +
                   $"Валюта: {currenciesString}";
        }
    }
}