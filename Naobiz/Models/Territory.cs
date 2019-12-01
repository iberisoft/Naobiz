using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Naobiz.Models
{
    class Territory
    {
        public Territory()
            : this(null, "") { }

        public Territory(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; }

        public string Name { get; }

        public static List<Territory> Load(string filePath)
        {
            return JObject.Parse(File.ReadAllText(filePath)).Properties()
                .Select(property => new Territory(property.Name, (string)property.Value))
                .OrderBy(territory => territory.Name)
                .ToList();
        }
    }
}
