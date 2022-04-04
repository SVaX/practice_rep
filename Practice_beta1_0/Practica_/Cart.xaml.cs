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
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        Class1 funcs = new Class1();
        UserWindow uswindow;
        public Cart(UserWindow _uswindow)
        {
            uswindow = _uswindow;
            InitializeComponent();
            DataTable dt_cartitems = funcs.OverallSelect("SELECT * FROM [dbo].[Cart] WHERE id_user = "+uswindow.id_ofuser);
            for(int i = 0; i<dt_cartitems.Rows.Count; i++)
            {
                DataTable dt_items = funcs.OverallSelect("SELECT name_product FROM [dbo].[Products] WHERE id="+dt_cartitems.Rows[i][2]);
                listbox.Items.Add(dt_items.Rows[0][0]);
            }
            
        }

        private void ToCartGoing_Click(object sender, RoutedEventArgs e)
        {
            uswindow.OpenPage(UserWindow.pages.useside, uswindow.listforeverybody);
        }

        private void ToCartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable getid = funcs.OverallSelect("SELECT id FROM [dbo].[Products] WHERE name_product='" + listbox.SelectedItem.ToString() + "'");
                int id = 0;
                for (int i = 0; i < getid.Rows.Count; i++)
                {
                    id = int.Parse(getid.Rows[0][0].ToString());
                }
                string cmd = "DELETE FROM [dbo].[Cart] WHERE id_product = " + id + " AND id_user =" + uswindow.id_ofuser;
                bool success = funcs.adding_deleting_changing(cmd);
                if (success)
                {
                    MessageBox.Show("Successfully deleted!");
                    nametext.Text = " ";
                    pricetext.Text = " ";
                    descriptiontext.Text = " ";
                    listbox.Items.Clear();
                    DataTable dt_cartitems = funcs.OverallSelect("SELECT * FROM [dbo].[Cart] WHERE id_user = " + uswindow.id_ofuser);
                    for (int i = 0; i < dt_cartitems.Rows.Count; i++)
                    {
                        DataTable dt_items = funcs.OverallSelect("SELECT name_product FROM [dbo].[Products] WHERE id=" + dt_cartitems.Rows[i][2]);
                        listbox.Items.Add(dt_items.Rows[0][0]);

                    }
                }
                else
                {
                    MessageBox.Show("Looks like something's gone wrong");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("something went wrong?");
                return;
            }
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                nametext.Text = listbox.SelectedItem.ToString();
                DataTable dt_products;
                dt_products = funcs.OverallSelect("SELECT price FROM [dbo].[Products] WHERE name_product = '" + listbox.SelectedItem.ToString() + "'");
                for (int i = 0; i < dt_products.Rows.Count; i++)
                {
                    pricetext.Text = dt_products.Rows[0][0].ToString();
                }
                dt_products = funcs.OverallSelect("SELECT description FROM [dbo].[Products] WHERE name_product = '" + listbox.SelectedItem.ToString() + "'");
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
