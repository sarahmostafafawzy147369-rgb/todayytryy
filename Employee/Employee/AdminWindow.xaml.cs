using Employee.Data;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
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

namespace Employee
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (var db = new MyDbcontext())
            {
                List<string> showstatus = new List<string> { "Pending", "InProgress", "Compeleted" };
                cmb_status.ItemsSource = showstatus;
                dg_admin.ItemsSource=db.Tasks.Include(t => t.Users).ToList();

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_admin.SelectedItem==null)
            {
                return;
            }
            dynamic cc = dg_admin.SelectedItem;
            txt_name.Text = cc.Users.Name;
            txt_taskId.Text = cc.TaskID.ToString();
            txt_title.Text = cc.Title;
            txt_Desc.Text = cc.Description;
            cmb_status.SelectedItem=cc.Status;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Desc.Text)||string.IsNullOrEmpty(txt_name.Text)|string.IsNullOrEmpty(txt_title.Text)||cmb_status.SelectedItem==null)
            {
                MessageBox.Show("Please Compelete Data");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var check = db.Users.FirstOrDefault(t => t.Name==txt_name.Text);
                if (check==null)
                {
                    MessageBox.Show("This Employee Not Found");
                    return;
                }
                var newTask = new Emp_tasks
                {
                    Userd=check.UserID,
                    Title=txt_title.Text,
                    Description=txt_Desc.Text,
                    Status=cmb_status.SelectedItem.ToString(),
                };
                db.Tasks.Add(newTask);
                db.SaveChanges();
                MessageBox.Show("Task Added SuccessFully");
                LoadData();
                return;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Desc.Text)||string.IsNullOrEmpty(txt_name.Text)|string.IsNullOrEmpty(txt_title.Text)||cmb_status.SelectedItem==null)
            {
                MessageBox.Show("Please Compelete Data");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var check = db.Tasks.FirstOrDefault(t => t.TaskID==int.Parse(txt_taskId.Text));
                if (check==null)
                {
                    MessageBox.Show("Task not Found");
                    return;
                }
                db.Tasks.Remove(check);
                db.SaveChanges();
                MessageBox.Show("Task Deleted Yet❤️");
                LoadData();
                return;
            }



        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Desc.Text)||string.IsNullOrEmpty(txt_name.Text)|string.IsNullOrEmpty(txt_title.Text)||cmb_status.SelectedItem==null)
            {
                MessageBox.Show("Please Compelete Data");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var Update = db.Tasks.Include(t=>t.Users).FirstOrDefault(t => t.TaskID==int.Parse(txt_taskId.Text));
                if(Update==null)
                {
                    MessageBox.Show("Task Not Found");
                    return;
                }
                Update.Status=cmb_status.SelectedValue.ToString();
                Update.Title=txt_title.Text;
                Update.Description=txt_Desc.Text;
                Update.Users.Name=txt_name.Text;
                db.SaveChanges();
                MessageBox.Show("Emplyee Updated SuccessFully");
                LoadData();
                return;

            }
        }
    }
}