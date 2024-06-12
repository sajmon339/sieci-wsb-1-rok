using System.Windows;
using Zadanie4.Models;

namespace Zadanie4.Views
{
    public partial class CategoryWindow : Window
    {
        public CategoryWindow(Category category)
        {
            InitializeComponent();
            DataContext = category;
        }

        private void OnSubcategoryDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((System.Windows.Controls.ListBox)sender).SelectedItem is Subcategory subcategory)
            {
                var subcategoryWindow = new SubcategoryWindow(subcategory);
                subcategoryWindow.ShowDialog();
            }
        }
    }
}
