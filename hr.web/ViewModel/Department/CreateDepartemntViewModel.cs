using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hr.web.ViewModel.Department
{
    public class CreateDepartemntViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<int> Employees { get; set; }
    }
}
