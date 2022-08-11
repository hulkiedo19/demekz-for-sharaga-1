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
    public partial class MainWindow : Window
    {
        static List<Product> All_products = null;
        static List<List<Product>> All_pages = null;
        static int pages_count = 0;
        static int current_page = 0;

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

            initialize_default_pages();
        }

        void initialize_default_pages()
        {
            if(All_products == null)
                All_products = GetProducts();

            GetPages(All_products);
            SetPage();
            SetButtons();
        }

        List<Product> GetProducts()
        {
            List<Product> products = null;
            using(var DbContext = new DatabaseEntities())
            {
                products = DbContext.Products.ToList();
            }
            return products;
        }

        void GetPages(List<Product> products)
        {
            if(All_pages != null)
                All_pages.Clear();
            else
                All_pages = new List<List<Product>>();

            int element_index = 0;
            int pages = products.Count / 4;
            int last_page_elements = products.Count % 4;

            // here we sets all 4-element pages
            for (int i = 0; i < pages; i++)
            {
                List<Product> page = new List<Product>();

                for(int j = 0; j < 4; j++)
                {
                    page.Add(products[element_index++]);
                }

                All_pages.Add(page);
            }

            // if there are elements left, another page will then be added
            if(last_page_elements > 0)
            {
                List<Product> page = new List<Product>();

                for(int i = 0; i < last_page_elements; i++)
                {
                    page.Add(products[element_index++]);
                }

                All_pages.Add(page);
            }

            pages_count = All_pages.Count;
        }

        void GetPagesWithText(string text)
        {
            List<Product> products_with_text = new List<Product>();
            products_with_text = All_products.Where(p => p.ProductName.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

            GetPages(products_with_text);
        }

        void SetPage()
        {
            ListView1.ItemsSource = All_pages[current_page];
        }

        void SetButtons()
        {
            PagesPanel.Children.Clear();

            // sets page left button
            Button left_page = new Button();
            left_page.Content = "<";
            left_page.Margin = new Thickness(0, 0, 2, 0);
            left_page.BorderBrush = new SolidColorBrush(Colors.White);
            left_page.BorderThickness = new Thickness(0, 0, 0, 0);
            left_page.Background = new SolidColorBrush(Colors.White);
            left_page.Click += PageLeft_Click;

            PagesPanel.Children.Add(left_page);

            // sets main numbered buttons
            for(int i = 0; i < All_pages.Count; i++)
            {
                Button number_page = new Button();
                number_page.Content = $"{i + 1}";
                number_page.Margin = new Thickness(2, 0, 2, 0);
                number_page.BorderBrush = new SolidColorBrush(Colors.White);
                number_page.BorderThickness = new Thickness(0, 0, 0, 0);
                number_page.Background = new SolidColorBrush(Colors.White);
                number_page.Click += specified_page_button_click;

                PagesPanel.Children.Add(number_page);
            }

            // sets page left button
            Button right_page = new Button();
            right_page.Content = ">";
            right_page.Margin = new Thickness(2, 0, 0, 0);
            right_page.BorderBrush = new SolidColorBrush(Colors.White);
            right_page.BorderThickness = new Thickness(0, 0, 0, 0);
            right_page.Background = new SolidColorBrush(Colors.White);
            right_page.Click += PageRight_Click;

            PagesPanel.Children.Add(right_page);
        }

        void specified_page_button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int page_number = Convert.ToInt32(button.Content);

            current_page = page_number - 1;
            SetPage();
        }

        private void PageLeft_Click(object sender, RoutedEventArgs e)
        {
            if (current_page == 0)
                return;

            current_page--;
            SetPage();
        }

        private void PageRight_Click(object sender, RoutedEventArgs e)
        {
            if (current_page == (pages_count - 1))
                return;

            current_page++;
            SetPage();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearch.Text == "")
                GetPages(All_products);

            GetPagesWithText(TextBoxSearch.Text);
            SetPage();
            SetButtons();
        }

        // these methods are doesn't work
        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateListView();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateListView();
        }

        /*private void UpdateListView() {
            var items = DbContext.Products.ToList();
            // ----------------- SORT ITEMS -----------------
            if (ComboSort.SelectedIndex >= 0) {
                // TODO: материалы херово сортирует
                switch (ComboSort.SelectedIndex) {
                    case 0: items = items.OrderBy(p => p.ProductCost).ToList(); break;
                    case 1: items = items.OrderByDescending(p => p.ProductCost).ToList(); break;
                    case 2: items = items.OrderByDescending(p => p.Materials).ToList(); break;
                    case 3: items = items.OrderBy(p => p.Materials).ToList(); break;
                }
            }
            // ----------------- FILTER ITEMS -----------------
            if(ComboFilter.SelectedIndex >= 0) {
                switch(ComboFilter.SelectedIndex) {
                    case 0: items = items.Where(p => p.Materials != null).ToList(); break;
                    case 1: items = items.Where(p => p.Materials == null).ToList(); break;
                }
            }
            ListView1.ItemsSource = items; }*/
    }
}
