using demekz3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demekz3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ComboSort.ItemsSource = new List<string>()
            {
                "Less cost",
                "Biggest cost",
                "More materials",
                "Less materials"
            };

            ComboFilter.ItemsSource = new List<string>()
            {
                "With materials",
                "Without materials"
            };

            // okay, you can add buttons to stack panel through code
            //PagesPanel.Children.Add(new Button() { Content = "123" });

            UpdateListView();
        }

        private void UpdateListView()
        {
            using (var DbContext = new DatabaseEntities())
            {
                var items = DbContext.Products.ToList();

                // ----------------- SORT ITEMS -----------------
                if (ComboSort.SelectedIndex >= 0)
                {
                    // TODO: материалы херово сортирует
                    switch (ComboSort.SelectedIndex)
                    {
                        case 0:
                            items = items.OrderBy(p => p.ProductCost).ToList();
                            break;
                        case 1:
                            items = items.OrderByDescending(p => p.ProductCost).ToList();
                            break;
                        case 2:
                            items = items.OrderByDescending(p => p.Materials).ToList();
                            break;
                        case 3:
                            items = items.OrderBy(p => p.Materials).ToList();
                            break;
                    }
                }

                // ----------------- FILTER ITEMS -----------------

                if(ComboFilter.SelectedIndex >= 0)
                {
                    switch(ComboFilter.SelectedIndex)
                    {
                        case 0:
                            items = items.Where(p => p.Materials != null).ToList();
                            break;
                        case 1:
                            items = items.Where(p => p.Materials == null).ToList();
                            break;
                    }
                }

                // ----------------- TEXT SEARCH -----------------
                if (TextBoxSearch.Text != "")
                    items = items.Where(p => p.ProductName.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

                ListView1.ItemsSource = items;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListView();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListView();
        }
    }
}
