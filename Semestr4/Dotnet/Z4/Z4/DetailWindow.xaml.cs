using System.Windows;

namespace Z4
{
    public partial class DetailWindow : Window
    {
        public DetailWindow(Subcategory subcategory)
        {
            InitializeComponent();
            DataContext = subcategory;
        }
    }
}
