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
            "По названию (По возрастанию)",
            "По названию (По убыванию)",
            "По цене (По возрастанию)",
            "По цене (По убыванию)",
            "По описанию (По возрастанию)",
            "По описанию (По убыванию)",
            "По времени (По возрастанию)",
            "По времени (По убыванию)",
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
            //Название
            clientList = clientList.Where(i => i.FirstName.ToLower().Contains(TbSearch.Text.ToLower())).ToList();


            //Описание
            //serviceList = serviceList.Where(i => i.Description.Contains(TbSearch.Text)).ToList();


            //Сортировка - пофиксить!
            //var selectedIndexCmb = CmbSort.SelectedIndex;

            //switch (selectedIndexCmb)
            //{
            //    case 0:
            //        clientList = clientList.OrderBy(i => i.ClientID).ToList();
            //        break;
            //    case 1:
            //        clientList = clientList.OrderBy(i => i.FirstName).ToList();
            //        break;
            //    case 2:
            //        clientList = clientList.OrderByDescending(i => i.LastName).ToList();
            //        break;
            //    case 3:
            //        clientList = clientList.OrderBy(i => i.Patronymic).ToList();
            //        break;
            //    case 4:
            //        clientList = clientList.OrderByDescending(i => i.DateOfBirth).ToList();
            //        break;
            //    case 5:
            //        clientList = clientList.OrderBy(i => i.Address).ToList();
            //        break;
            //    case 6:
            //        clientList = clientList.OrderByDescending(i => i.Email).ToList();
            //        break;
            //    case 7:
            //        clientList = clientList.OrderBy(i => i.Phone).ToList();
            //        break;

            //    default:
            //        clientList = clientList.OrderBy(i => i.ClientID).ToList();
            //        break;
           
            //}     

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
    }
}
