using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using exam.Models;

namespace exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadUsers();
            authTextBlock.Text = $"{AppState.CurrentUser!.Name}";
        }

        private void LoadUsers()
        {
            try
            {
                var db = new examContext();
                var users = db.Users.ToList();

                userTable.ItemsSource = users;

                var col = userTable.Columns
                    .FirstOrDefault(c => (c as DataGridBoundColumn)?.Binding is Binding b
                                && b.Path.Path == "Order");
                if (col != null)
                    userTable.Columns.Remove(col);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new examContext(); // создаем DB context

            var lastName = searchBox.Text; // добавляем переменную данные из пользовательского ввода

            var users = string.IsNullOrWhiteSpace(lastName)
             ? db.Users.ToList()
             : db.Users
             .Where(x => x.Name.Contains(lastName)) // тут x.LastName это поле модели
             .ToList();
            // ищем пользователей у которых содержаться данные в интересующем нам столбце, если пользовательский ввод пустой выводим всех пользователей

            userTable.ItemsSource = users; // обновляем таблицу

        }
    }
}