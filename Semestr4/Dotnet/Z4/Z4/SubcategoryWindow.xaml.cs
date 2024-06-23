using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Z4
{
    public partial class SubcategoryWindow : Window
    {
        private List<Subcategory> subcategories;

        public SubcategoryWindow(Category category)
        {
            InitializeComponent();
            subcategories = category.Subcategories;
            SubcategoryList.ItemsSource = subcategories;
        }

        private void SubcategoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SubcategoryList.SelectedItem is Subcategory selectedSubcategory)
            {
                var detailWindow = new DetailWindow(selectedSubcategory);
                detailWindow.Show();
            }
        }
    }
}
