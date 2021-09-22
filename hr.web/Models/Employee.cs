using HR.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hr.Web.Models
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string ImgUrl { get; set; }

        // Department Relations
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
