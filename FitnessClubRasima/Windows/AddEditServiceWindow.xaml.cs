using Microsoft.Win32;
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
using System.IO;

namespace FitnessClubRasima.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditServiceWindow.xaml
    /// </summary>
    public partial class AddEditServiceWindow : Window
    {
        private string pathImage = null;
        private bool isEdit = false;
        private Service editService;

        public AddEditServiceWindow()
        {
            // конструктор для добавления
            InitializeComponent();

            isEdit = false;
        }

        public AddEditServiceWindow(Service service)
        {
            // конструктор для редактирования

            InitializeComponent();


            // Изменения заголовка и текста кнопки
            TblockTitle.Text = "Редактирование услуги";
            BtnAddEditService.Content = "Сохранить изменения";

            // Заполнение текстовых полей 
            TbNameService.Text = service.Title.ToString();
            TbPriceService.Text = service.Cost.ToString();
            TbTimeService.Text = service.Time.ToString();
            TbDescription.Text = service.Description.ToString();

            // вывод изображения

            if (service.PhotoPath != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    ImgService.Source = bitmapImage;
                }
            }
           

            isEdit = true;
            editService = service;

        }

        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            // выбор фото 

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgService.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathImage = openFileDialog.FileName;
            }
        }

        private void BtnAddEditService_Click(object sender, RoutedEventArgs e)
        {
            // валидация
            if (isEdit == true)
            {
                // изменение
                editService.Title = TbNameService.Text;
                editService.Cost = Convert.ToDecimal(TbPriceService.Text);
                editService.Time = Convert.ToInt32(TbTimeService.Text);
                editService.Description = TbDescription.Text;
                if (pathImage != null)
                {
                    editService.PhotoPath = pathImage;
                }
                EFClass.context.SaveChanges();
                MessageBox.Show("Услуга успешно изменена");

            }
            else
            {
                // добавление
                Service service = new Service();
                service.Title = TbNameService.Text;
                service.Cost = Convert.ToDecimal(TbPriceService.Text);
                service.Time = Convert.ToInt32(TbTimeService.Text);
                service.Description = TbDescription.Text;
                service.PhotoPath = pathImage;

                EFClass.context.Service.Add(service);
                EFClass.context.SaveChanges();
                MessageBox.Show("Услуга успешно добавлена");
            }
            
            this.Close();
        }
    }
}
