using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Klopotenko
{
    public class Recipee
    {
        public string NameReciptes { get; set; }
        [JsonIgnore]
        public List<Food>Foods { get; set; }
        public Recipee(string nameReciptes, List<Food> foods)
        {
            NameReciptes = nameReciptes;
            Foods = foods;
        }
        public Recipee(string nameReciptes)
        {
            NameReciptes = nameReciptes;
        }
    }
}
