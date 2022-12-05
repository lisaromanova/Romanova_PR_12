using MDK_01_01_PR_12.Classes;
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

namespace MDK_01_01_PR_12.Pages
{
    /// <summary>
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        PaginationClass paginationClass = new PaginationClass();
        public HotelsPage()
        {
            InitializeComponent();
            lstHotels.ItemsSource = DataBase.connect.Hotel.ToList();
            paginationClass.CountPage = 10;
            DataContext = paginationClass;
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameLoad.frmMain.Navigate(new ToursPage());
        }
    }
}
