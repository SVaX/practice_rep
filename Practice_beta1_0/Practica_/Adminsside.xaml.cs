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
using PracticeLibrary;
using System.Data;

namespace Practica_
{
    /// <summary>
    /// Логика взаимодействия для Adminsside.xaml
    /// </summary>
    public partial class Adminsside : Page
    {
        DataTable dt_products;
        Class1 funcs = new Class1();
        public Adminsside(MainWindow mainWindow)
        {
            InitializeComponent();
            dt_products = funcs.OverallSelect("SELECT name_product AS Название FROM [dbo].[Products]");
            itemsgrid.ItemsSource = dt_products.DefaultView;
        }

        private void AddingGood_Click(object sender, RoutedEventArgs e)
        {
            if(nametext.Text != "" && pricetext.Text !="" && descriptiontext.Text != "")
            {
                string cmd = "INSERT INTO [dbo].[Products] (name_product, price, description) VALUES ('" + nametext.Text + "', " + pricetext.Text + ", '" + descriptiontext.Text + "')";
                bool success = funcs.adding_deleting_changing(cmd);
                if (success)
                {
                    MessageBox.Show("Successfully added!");
                    dt_products = funcs.OverallSelect("SELECT name_product AS Название FROM [dbo].[Products]");
                    itemsgrid.ItemsSource = dt_products.DefaultView;
                    itemsgrid.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Looks like something's gone wrong");
                    return;
                }
            }

        }

        private void ChangeGood_Click(object sender, RoutedEventArgs e)
        {
            if (nametext.Text != "" && pricetext.Text != "" && descriptiontext.Text != "")
            {
                string cmd = "UPDATE Products SET name_product = '"+nametext.Text+"', price = "+pricetext.Text+", description = '"+descriptiontext.Text+"' WHERE name_product='"+nametext.Text+"'";
                bool success = funcs.adding_deleting_changing(cmd);
                if (success)
                {
                    MessageBox.Show("Successfully changed!");
                    dt_products = funcs.OverallSelect("SELECT name_product AS Название FROM [dbo].[Products]");
                    itemsgrid.ItemsSource = dt_products.DefaultView;
                    itemsgrid.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Looks like something's gone wrong");
                    return;
                }
            }
        }

        private void DeleteGood_Click(object sender, RoutedEventArgs e)
        {
            if (nametext.Text != "" && pricetext.Text != "" && descriptiontext.Text != "")
            {
                string cmd = "DELETE FROM [dbo].[Products] WHERE name_product = '"+ nametext.Text +"'";
                bool success = funcs.adding_deleting_changing(cmd);
                if (success)
                {
                    MessageBox.Show("Successfully deleted!");
                    dt_products = funcs.OverallSelect("SELECT name_product AS Название FROM [dbo].[Products]");
                    itemsgrid.ItemsSource = dt_products.DefaultView;
                    itemsgrid.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Looks like something's gone wrong");
                    return;
                }
            }
        }

        private void itemsgrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView drv = itemsgrid.SelectedItem as DataRowView;
                nametext.Text = drv[0].ToString();
                dt_products = funcs.OverallSelect("SELECT price FROM [dbo].[Products] WHERE name_product = '" + drv[0].ToString() + "'");
                for (int i = 0; i < dt_products.Rows.Count; i++)
                {
                    pricetext.Text = dt_products.Rows[0][0].ToString();
                }
                dt_products = funcs.OverallSelect("SELECT description FROM [dbo].[Products] WHERE name_product = '" + drv[0].ToString() + "'");
                for (int i = 0; i < dt_products.Rows.Count; i++)
                {
                    descriptiontext.Text = dt_products.Rows[0][0].ToString();
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }
    }
}
