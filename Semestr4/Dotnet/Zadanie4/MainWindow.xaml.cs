using System.Windows;
using Zadanie4.Models;
using Zadanie4.Views;

namespace Zadanie4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = CategoryData.LoadFromXml("Data/categories.xml");
        }

        private void OnCategoryDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((System.Windows.Controls.ListBox)sender).SelectedItem is Category category)
            {
                var categoryWindow = new CategoryWindow(category);
                categoryWindow.ShowDialog();
            }
        }
    }
}
