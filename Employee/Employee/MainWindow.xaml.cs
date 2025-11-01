using Employee.Data;
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

namespace Employee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_name.Text)||string.IsNullOrEmpty(txt_pass.Text))
            {
                MessageBox.Show("User Not Found");
                return;
            }
            using (var db=new MyDbcontext())
            {
                var check = db.Users.FirstOrDefault(t => t.Password==txt_pass.Text&&t.Name==txt_name.Text);
                if(check==null)
                {
                    MessageBox.Show("User Not Found");
                    return;
                }
                if(check.Role=="Employee")
                {
                    Employee_Window employee_Window = new Employee_Window(check);
                    employee_Window.Show();                 
                }
                if(check.Role=="Admin")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
            }
        }
    }
}