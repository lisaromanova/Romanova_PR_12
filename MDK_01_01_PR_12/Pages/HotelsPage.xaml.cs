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
        List<Hotel> hotels;
        public HotelsPage()
        {
            InitializeComponent();
            hotels = DataBase.connect.Hotel.ToList();
            paginationClass.CountPage = 10;
            paginationClass.Countlist = hotels.Count;
            lstHotels.ItemsSource = hotels.Skip(0).Take(paginationClass.CountPage).ToList();
            DataContext = paginationClass;
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            switch (tb.Uid)
            {
                case "back":
                    paginationClass.CurrentPage--;
                    break;
                case "next":
                    paginationClass.CurrentPage++;
                    break;
                case "first":
                    paginationClass.CurrentPage = 1;
                    break;
                case "last":
                    paginationClass.CurrentPage = paginationClass.CountPages;
                    break;
                default:
                    paginationClass.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            lstHotels.ItemsSource = hotels.Skip(paginationClass.CurrentPage * paginationClass.CountPage - paginationClass.CountPage).Take(paginationClass.CountPage).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameLoad.frmMain.Navigate(new ToursPage());
        }

        private void tbPages_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                paginationClass.CountPage = Convert.ToInt32(tbPages.Text);
            }
            catch
            {
                paginationClass.CountPage = 10;
            }
            finally
            {
                paginationClass.Countlist = hotels.Count;
                lstHotels.ItemsSource = hotels.Skip(0).Take(paginationClass.CountPage).ToList();
                paginationClass.CurrentPage = 1;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Hotel hotel = DataBase.connect.Hotel.FirstOrDefault(x => x.Id == index);
            AddOrUpdateWindow window = new AddOrUpdateWindow(hotel);
            window.ShowDialog();
            FrameLoad.frmMain.Navigate(new HotelsPage());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstHotels.SelectedItems.Count != 0)
            {
                foreach(Hotel hotel in lstHotels.SelectedItems)
                {
                    MessageBoxResult msg = MessageBox.Show($"Вы точно хотите удалить отель {hotel.Name}?", "Удаление отеля", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (msg == MessageBoxResult.Yes)
                    {
                        int count = hotel.ToursHotel;
                        if (count == 0)
                        {
                            DataBase.connect.Hotel.Remove(hotel);
                        }
                        else
                        {
                            MessageBox.Show("Удаление отеля запрещено, так как он он находится в числе подходящих отелей для актуальных туров", "Удаление отеля", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                DataBase.connect.SaveChanges();
                MessageBox.Show("Отели успешно удалены!", "Удаление отеля", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameLoad.frmMain.Navigate(new HotelsPage());
            }
            else
            {
                MessageBox.Show("Ни выбрано ни одного отеля!", "Удаление отеля", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateWindow window = new AddOrUpdateWindow();
            window.ShowDialog();
            FrameLoad.frmMain.Navigate(new HotelsPage());
        }
    }
}
