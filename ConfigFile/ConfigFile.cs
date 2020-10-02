using System;
using System.Configuration;

namespace ConfigFile
{
    class ConfigFile
    {
        static void Main(string[] args)
        {
            var siteUrl = ConfigurationManager.AppSettings["SiteUrl"];
            Console.WriteLine($"url: {siteUrl}");
        }
    }
}