using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hr.web.ViewModel.Employee
{
    public class CreateEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public IFormFile ImgUrl { get; set; }
        // Department Relations
        public int DepartmentId { get; set; }
    }
}
