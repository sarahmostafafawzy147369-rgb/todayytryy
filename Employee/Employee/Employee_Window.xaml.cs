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
using System.Windows.Xps;

namespace Employee
{
    /// <summary>
    /// Interaction logic for Employee_Window.xaml
    /// </summary>
    public partial class Employee_Window : Window
    {
        private Users _user;
        public Employee_Window(Users users )
        {
            InitializeComponent();
            _user= users;
            txtname.Content=$"[Employee {_user.Name}]";
            LoadData();
        }
        public void LoadData()
        {
            List<string> StatusList = new List<string> { "Pending", "InProgress", "Compeleted" };
            cmb_status.ItemsSource = StatusList;
            using (var db=new MyDbcontext())
            {
                var show=
                    db.Tasks
                    .Include(t => t.Users)
                    .Where(t => t.Users.UserID==_user.UserID&&t.Status=="Pending"||t.Status=="InProgress").ToList();
                dg_piending.ItemsSource = show;
                dg_compeleted.ItemsSource=db.Tasks.Where(t=>t.Userd==_user.UserID&&t.Status=="Compeleted").ToList() ;
            }
        }

        private void dg_piending_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dg_piending.SelectedItem==null)
            {
                return;
            }
            dynamic selected=dg_piending.SelectedItem;
            txt_Id.Text=selected.TaskID.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_Id.Text)||cmb_status.SelectedItem==null)
            {
                MessageBox.Show("Please Compelete Data");
                return;
            }
            using (var db=new MyDbcontext())
            {
                var UpdateData=db.Tasks.Where(t=>t.TaskID==int.Parse(txt_Id.Text)&&t.Userd==_user.UserID).FirstOrDefault();
                if(UpdateData==null)
                {
                    MessageBox.Show("Task Not Found!");
                    return;
                }
                UpdateData.Status=cmb_status.SelectedItem.ToString();
                db.SaveChanges();
                LoadData();
            }
        }

        private void dg_compeleted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dg_compeleted==null)
            {
                return;
            }
            using(var db=new MyDbcontext())
            {
               dynamic ch=dg_compeleted.SelectedItem;
                if(ch==null)
                {
                    return;
                }
                txt_Id.Text=ch.TaskID.ToString();
                cmb_status.SelectedItem=ch.Status;
            }
        }
    }
}
