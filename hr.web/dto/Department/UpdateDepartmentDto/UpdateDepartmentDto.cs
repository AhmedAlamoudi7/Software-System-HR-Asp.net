using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hr.web.dto.Department.UpdateDepartmentDto
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<int> Employees { get; set; }
    }
}
