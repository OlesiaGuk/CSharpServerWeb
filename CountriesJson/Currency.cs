namespace CountriesJson
{
    class Currency
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public override string ToString()
        {
            return $"{{code: {Code}, name: {Name}, symbol: {Symbol}}}";
        }
    }
}