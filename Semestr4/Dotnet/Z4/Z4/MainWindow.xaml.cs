using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Z4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(Categories));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    Categories categories = (Categories)serializer.Deserialize(fs);
                    CategoryList.ItemsSource = categories.CategoryList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CategoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CategoryList.SelectedItem is Category selectedCategory)
            {
                var subcategoryWindow = new SubcategoryWindow(selectedCategory);
                subcategoryWindow.Show();
            }
        }
    }
}
