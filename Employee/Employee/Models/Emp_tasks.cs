using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Emp_tasks
    {
        [Key]
        public int TaskID { get; set; }
        public int Userd { get; set; }
        [ForeignKey("Userd")]
        public Users Users { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
