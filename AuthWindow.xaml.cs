using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

using exam.Models;

namespace exam
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim(); // Получаем данные из 1 бокса и обязательно с помощью Trim убираем пробелы по бокам
            string password = passwordTextBox.Text.Trim(); // Получаем данные из 2 бокса и обязательно с помощью Trim убираем пробелы по бокам


            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            string hash = Convert.ToHexString(bytes).ToLower(); // Эти 2 строчки нужны для того чтобы хэшировать пароль который ввел  пользов.

            var db = new examContext();
            var user = db.Users.FirstOrDefault(x => x.Login == login); // Ищем пользователя в бд
            if (user == null)
            {

                MessageBox.Show("Неверный логин"); // Если не нашли пользователя в бд выводим сообщение и выходим из обработки
                return;
            }

            if (hash != user.Password)
            {
                MessageBox.Show("Неверный пароль"); // Если хэш не совпал выводим сообщение и выходим из обработки
                return;
            }

            AppState.CurrentUser = user; // Сохраняем нашего пользователя в State

            var mainWindow = new MainWindow(); // Создаем нужное нам окно куда нужно дальше отправить пользователя
            mainWindow.Show(); // Показываем новое окно
            Close();  // закрываем старое окно



        }
    }
}
