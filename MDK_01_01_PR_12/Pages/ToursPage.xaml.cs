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
            lstTours.ItemsSource = DataBase.connect.Tour.ToList();
            cbType.Items.Add("Все типы");
            List<Type> types = DataBase.connect.Type.ToList();
            foreach(Type type in types)
            {
                cbType.Items.Add(type.Name);
            }
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
                tours = tours.Where(x => x.Name.ToLower().Contains(tbSearch.Text.ToLower()) || x.Description.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if ((bool)chbActual.IsChecked)
            {
                tours = tours.Where(x => x.IsActual == true).ToList();
            }
            lstTours.ItemsSource = tours;
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
    }
}
