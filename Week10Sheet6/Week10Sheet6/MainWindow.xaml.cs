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

namespace Week10Sheet6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c.CompanyName;

            lbxCustomersEx1.ItemsSource = query.ToList<string>();
        }

        private void btnQueryEx2_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c;

            dgCUstomersEx2.ItemsSource = query.ToList();
        }

        private void btnQueryEx3_Click(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        where o.Customer.City.Equals("London")
                        || o.Customer.City.Equals("Paris")
                        || o.Customer.City.Equals("USA")
                        orderby o.Customer.CompanyName
                        select new
                        {
                            CustomerName = o.Customer.CompanyName,
                            City = o.Customer.City,
                            Address = o.ShipAddress
                        };

            dgCUstomersEx3.ItemsSource = query.ToList().Distinct();
        }

        private void btnQueryEx4_Click(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                           p.ProductID,
                           p.ProductName,
                           p.Category.CategoryName,
                           p.UnitPrice
                        };

            dgCUstomersEx4.ItemsSource = query.ToList().Distinct();
        }

        private void btnQueryEx5_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product()
            {
                ProductName = "Kickapoo Jungle Joy Juice",
                UnitPrice = 12.49m,
                CategoryID = 1
            };

            db.Products.add(p);

            //The method Save Changes doesn't exist.
            //db.SaveChanges();

            ShowProducts(dgCUstomersEx5);
        }

        private void ShowProducts(DataGrid currentGrid)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };

            currentGrid.ItemsSource = query.ToList();
        }

        private void BtnQueryEx6_Click(object sender, RoutedEventArgs e)
        {




            Product p1 = (db.Products.where(p => p.ProductName.StartsWith("Kick"))
                            .Select(p => p)).First();

            p1.UnitPrice = 100m;

            //The method SaveChanges doesn't exist.
            //db.SaveChanges();

            ShowProducts(dgCUstomersEx6);
        }

        private void BtnQueryEx9_Click(object sender, RoutedEventArgs e)
        {
            var query = db.Customers_By_City("London");

            dgCUstomersEx9.ItemsSource = query.ToList();
        }
    }
}
