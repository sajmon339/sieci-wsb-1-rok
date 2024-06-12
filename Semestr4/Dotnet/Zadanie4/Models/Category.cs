using System.Collections.Generic;

namespace Zadanie4.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Subcategory> Subcategories { get; set; }
    }
}
