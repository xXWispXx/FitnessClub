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
    public partial class AddEditClientListWindow : Window
    {
        private string pathImage = null;
        private bool isEdit = false;
        private Client editClient;

        public AddEditClientListWindow()
        {
            // конструктор для добавления
            InitializeComponent();

            isEdit = false;
        }

        public AddEditClientListWindow(Client client)
        {
            // конструктор для редактирования

            InitializeComponent();


            // Изменения заголовка и текста кнопки
            TblockTitle.Text = "Редактирование услуги";
            BtnAddEditClient.Content = "Сохранить изменения";

            // Заполнение текстовых полей 
            TbFirstNameClient.Text = client.FirstName.ToString();
            TbLastNameClient.Text = client.LastName.ToString();
            TbPatronymicClient.Text = client.Patronymic.ToString();
            TbPhoneClient.Text = client.Phone.ToString();
            TbEmailClient.Text = client.Email.ToString();


            // вывод изображения

            if (client.PhotoPath != null)
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
            editClient = client;

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

        private void BtnAddEditClient_Click(object sender, RoutedEventArgs e)
        {
            // валидация
            if (isEdit == true)
            {
                //// изменение
                editClient.FirstName = TbFirstNameClient.Text;
                editClient.LastName = TbLastNameClient.Text;
                editClient.Patronymic = TbPatronymicClient.Text;

                if (pathImage != null)
                {
                    editClient.PhotoPath = pathImage;
                }
                EFClass.context.SaveChanges();
                MessageBox.Show("Услуга успешно изменена");

            }
            else
            {
                //// добавление
                Client client = new Client();
                client.FirstName = TbFirstNameClient.Text;
                client.LastName = TbLastNameClient.Text;
                client.Patronymic = TbPatronymicClient.Text;
                client.PhotoPath = pathImage;

                EFClass.context.Client.Add(client);
                EFClass.context.SaveChanges();
                MessageBox.Show("Услуга успешно добавлена");
            }

            this.Close();
        }

    }
}
