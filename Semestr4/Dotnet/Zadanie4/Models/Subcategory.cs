using System.Collections.Generic;

namespace Zadanie4.Models
{
    public class Subcategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Details { get; set; }
        public List<Element> Elements { get; set; }
    }
}
