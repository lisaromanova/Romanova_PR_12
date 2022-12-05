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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();
            cbType.Items.Add("Все типы");
            List<Type> types = DataBase.connect.Type.ToList();
            foreach(Type type in types)
            {
                cbType.Items.Add(type.Name);
            }
            Filter();
        }

        void Filter()
        {
            List<Tour> tours = DataBase.connect.Tour.ToList();
            if(cbType.SelectedIndex!=0 && cbType.SelectedIndex != -1)
            {
                List<int> types = DataBase.connect.TypeOfTour.Where(x=> x.Type.Name==(string)cbType.SelectedValue).Select(x=> x.TourId).ToList();
                tours = tours.Where(x=> types.Contains(x.Id)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                tours = tours.Where(x => x.Name.ToLower().Contains(tbSearch.Text.ToLower()) || (x.Description!=null&&x.Description.ToLower().Contains(tbSearch.Text.ToLower()))).ToList();
            }
            if ((bool)chbActual.IsChecked)
            {
                tours = tours.Where(x => x.IsActual == true).ToList();
            }
            if (cbSort.SelectedIndex != -1)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 0:
                        tours = tours.OrderBy(x => x.Price).ToList();
                        break;
                    case 1:
                        tours = tours.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }
            if(tours.Count > 0)
            {
                lstTours.Visibility = Visibility.Visible;
                txtEmpty.Visibility = Visibility.Collapsed;
                tbCount.Visibility = Visibility.Visible;
                lstTours.ItemsSource = tours;
                double sum = 0;
                foreach (Tour tour in tours)
                {
                    sum += Convert.ToDouble(tour.Price) * tour.TicketCount;
                }
                tbCount.Text = "Общая стоимость: " + sum.ToString();
            }
            else
            {
                lstTours.Visibility = Visibility.Collapsed;
                txtEmpty.Visibility = Visibility.Visible;
                tbCount.Visibility = Visibility.Collapsed;
            }
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void chbActual_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void btnHotels_Click(object sender, RoutedEventArgs e)
        {
            FrameLoad.frmMain.Navigate(new HotelsPage());
        }
    }
}
