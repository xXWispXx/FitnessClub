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
    /// Логика взаимодействия для ServiceListWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
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
        public ServiceWindow()
        {
            InitializeComponent();

            CmbSort.ItemsSource = listSort;
            CmbSort.SelectedIndex = 0;

            GetServiceList();
        }

        private void GetServiceList()
        {
            List<Service> serviceList = new List<Service>();

            serviceList = EFClass.context.Service.ToList();

            //поиск, фильтрация и сортировка

            //поиск
            //Название
            serviceList = serviceList.Where(i => i.Title.ToLower().Contains(TbSearch.Text.ToLower())).ToList();


            //Описание
            //serviceList = serviceList.Where(i => i.Description.Contains(TbSearch.Text)).ToList();


            //Сортировка
            var selectedIndexCmb = CmbSort.SelectedIndex;

            switch (selectedIndexCmb)
            {
                case 0:
                    serviceList = serviceList.OrderBy(i => i.ServiceID).ToList();
                    break;
                case 1:
                    serviceList = serviceList.OrderBy(i => i.Title).ToList();
                    break;
                case 2:
                    serviceList = serviceList.OrderByDescending(i => i.Title).ToList();
                    break;
                case 3:
                    serviceList = serviceList.OrderBy(i => i.Cost).ToList();
                    break;
                case 4:
                    serviceList = serviceList.OrderByDescending(i => i.Cost).ToList();
                    break;
                case 5:
                    serviceList = serviceList.OrderBy(i => i.Description).ToList();
                    break;
                case 6:
                    serviceList = serviceList.OrderByDescending(i => i.Description).ToList();
                    break;
                case 7:
                    serviceList = serviceList.OrderBy(i => i.Time).ToList();
                    break;
                case 8:
                    serviceList = serviceList.OrderByDescending(i => i.Time).ToList();
                    break;

                default:
                    serviceList = serviceList.OrderBy(i => i.ServiceID).ToList();
                    break;
            }

            //фильтрация
            
            lvService.ItemsSource = serviceList;
        }
        
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow();
            addEditServiceWindow.ShowDialog();

            GetServiceList();
        }
        
                private void BtnEditProduct_Click(object sender, RoutedEventArgs e)
                {
                    var button = sender as Button;
                    if (button == null)
                    {
                        return;
                    }

                    var service = button.DataContext as Service;


                    AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow(service);
                    addEditServiceWindow.ShowDialog();

                    GetServiceList(); 
                }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetServiceList();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetServiceList();
        }
    }
}
