using MDK_01_01_PR_12.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MDK_01_01_PR_12
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateWindow.xaml
    /// </summary>
    public partial class AddOrUpdateWindow : Window
    {
        Hotel hotel;
        string str;
        public AddOrUpdateWindow()
        {
            InitializeComponent();
            str = "Добавление отеля";
            DataLoad();
            hotel = new Hotel();
            DataBase.connect.Hotel.Add(hotel);
        }
        public AddOrUpdateWindow(Hotel hotel)
        {
            InitializeComponent();
            this.hotel = hotel;
            str = "Изменение отеля";
            DataLoad();
            tbTitle.Text = hotel.Name;
            tbStars.Text = hotel.CountOfStars.ToString();
            tbDescription.Text = hotel.Description;
            cbCountry.SelectedValue = hotel.CountryCode;
        }
        void DataLoad()
        {
            btnAdd.Content = "Сохранить";
            txtHeader.Text = str;
            Title = str;
            cbCountry.ItemsSource = DataBase.connect.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";
        }

        bool CheckFields()
        {
            if (tbTitle.Text != "")
            {
                if (Regex.IsMatch(tbStars.Text,"^[1-5]$"))
                {
                    if (tbDescription.Text != "")
                    {
                        if (cbCountry.SelectedIndex!=-1)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Выберите страну из списка!", str, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заполните поле Описание отеля!", str, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите количество звезд корректно!", str, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните поле Название отеля!", str, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(CheckFields())
            {
                hotel.Name = tbTitle.Text;
                hotel.Description = tbDescription.Text;
                hotel.CountOfStars = Convert.ToInt32(tbStars.Text);
                hotel.CountryCode = (string)cbCountry.SelectedValue;
                DataBase.connect.SaveChanges();
                MessageBox.Show("Успешное сохранение данных!", str, MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
