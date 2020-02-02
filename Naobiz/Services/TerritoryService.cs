using Microsoft.AspNetCore.Hosting;
using Naobiz.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Naobiz.Services
{
    class TerritoryService
    {
        public TerritoryService(IWebHostEnvironment environment)
        {
            Countries = Territory.Load(Path.Combine(environment.WebRootPath, "countries.json"));
            Provinces = Territory.Load(Path.Combine(environment.WebRootPath, "provinces.json"));
        }

        public List<Territory> Countries { get; }

        public string GetCountryName(string code) => Countries.SingleOrDefault(territory => territory.Code == code)?.Name;

        public List<Territory> Provinces { get; }

        public string GetProvinceName(string code) => Provinces.SingleOrDefault(territory => territory.Code == code)?.Name;
    }
}
