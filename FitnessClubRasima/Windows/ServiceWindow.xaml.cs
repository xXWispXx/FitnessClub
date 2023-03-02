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
        public ServiceWindow()
        {
            InitializeComponent();
            GetServiceList();

        }

        private void GetServiceList()
        {
            List<Service> serviceList = new List<Service>();

            serviceList = EFClass.context.Service.ToList();

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
    }
}
