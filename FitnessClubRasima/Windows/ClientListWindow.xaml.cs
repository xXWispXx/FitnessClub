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
using System.Windows.Shapes;

using FitnessClubRasima.DB;
using FitnessClubRasima.Windows;
using FitnessClubRasima.ClassHelper;

namespace FitnessClubRasima.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientListListWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По имени (от А до Я)",
            "По имени (от Я до А)",
            "По фамилия (от А до Я)",
            "По фамилия (от Я до А)",
            "По отчество (от А до Я)",
            "По отчество (от Я до А)",
        };
        public ClientListWindow()
        {
            InitializeComponent();

            CmbSort.ItemsSource = listSort;
            CmbSort.SelectedIndex = 0;

            GetClientList();
        }

        private void GetClientList()
        {
            List<Client> clientList = new List<Client>();

            clientList = EFClass.context.Client.ToList();

            //поиск, фильтрация и сортировка

            //поиск
            //Имя
            clientList = clientList.Where(i => i.FirstName.ToLower().Contains(TbSearch.Text.ToLower())).ToList();

            //сортировка
            var selectedIndexCmb = CmbSort.SelectedIndex;

            switch (selectedIndexCmb)
            {
                case 0:
                    clientList = clientList.OrderBy(i => i.ClientID).ToList();
                    break;
                case 1:
                    clientList = clientList.OrderBy(i => i.FirstName).ToList();
                    break;
                case 2:
                    clientList = clientList.OrderByDescending(i => i.FirstName).ToList();
                    break;
                case 3:
                    clientList = clientList.OrderBy(i => i.LastName).ToList();
                    break;
                case 4:
                    clientList = clientList.OrderByDescending(i => i.LastName).ToList();
                    break;
                case 5:
                    clientList = clientList.OrderBy(i => i.Patronymic).ToList();
                    break;
                case 6:
                    clientList = clientList.OrderByDescending(i => i.Patronymic).ToList();
                    break;

                default:
                    clientList = clientList.OrderBy(i => i.ClientID).ToList();
                    break;

            }

            //фильтрация

            lvClient.ItemsSource = clientList;
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow();
            addEditServiceWindow.ShowDialog();

            GetClientList();
        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var service = button.DataContext as Service;


            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow(service);
            addEditServiceWindow.ShowDialog();

            GetClientList();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetClientList();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetClientList();
        }

        private void lvClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
