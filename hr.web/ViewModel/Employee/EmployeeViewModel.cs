using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hr.web.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreateAt { get; set; }

        
        // Department Relations
        public string Department { get; set; }
    }
}
