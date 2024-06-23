using System.Collections.Generic;
using System.Xml.Serialization;

namespace Z4
{
    [XmlRoot("Categories")]
    public class Categories
    {
        [XmlElement("Category")]
        public List<Category> CategoryList { get; set; }
    }

    public class Category
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlArray("Subcategories")]
        [XmlArrayItem("Subcategory")]
        public List<Subcategory> Subcategories { get; set; }
    }

    public class Subcategory
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
