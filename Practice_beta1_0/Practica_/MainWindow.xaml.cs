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

namespace Practica_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public enum pages
        {
            login,
            register
            
        }
        public void OpenPage(pages page, Frame frame)
        {
            if(page == pages.login)
            {
                frame.Navigate(new SignInPage(this));
            }
            else if (page == pages.register)
            {
                frame.Navigate(new SighUp(this));
            }
            
        }
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.login, mainframe);
            
        }
        
    }
}
