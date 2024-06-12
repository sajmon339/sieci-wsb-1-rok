using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Zadanie4.Models
{
    [XmlRoot("CategoryData")]
    public class CategoryData
    {
        [XmlElement("Categories")]
        public List<Category> Categories { get; set; }

        public static CategoryData LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryData));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (CategoryData)serializer.Deserialize(fs);
            }
        }
    }
}
