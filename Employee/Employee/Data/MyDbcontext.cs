using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Employee.Data
{
    public class MyDbcontext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Emp_tasks> Tasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Employee_Tasks;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
