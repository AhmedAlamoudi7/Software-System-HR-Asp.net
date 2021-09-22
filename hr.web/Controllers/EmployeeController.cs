using hr.web.Data;
using hr.web.Service;
using hr.web.ViewModel;
using hr.web.ViewModel.Employee;
using hr.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hr.web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileService _IFileService;

        public EmployeeController(ApplicationDbContext db, IFileService IFileService)
        {
            _db = db;
            _IFileService = IFileService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var Department = _db.Employees.Include(x => x.Department).Where(x => !x.IsDelete).Select(x => new EmployeeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Email =x.Email,
                DOB =x.DOB,
                Phone =x.Phone,
                ImgUrl =x.ImgUrl,
                Department =x.Department.Name,
                CreateAt = x.CreatedAt,

            }).ToList();

            return View(Department);


        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DepartmentList"] = new SelectList(_db.Departments.Where(x => !x.IsDelete).ToList(), "Id", "Name");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeViewModel input)
        {
            if (ModelState.IsValid)
            {
                var Employee = new Employee();
                Employee.Name = input.Name;
                Employee.Email = input.Email;
                Employee.DOB = input.DOB;
                Employee.Phone = input.Phone;

                if (input.ImgUrl != null)
                {
                    Employee.ImgUrl = await _IFileService.SaveFile(input.ImgUrl, "Images"); ;

                }

                Employee.DepartmentId = input.DepartmentId;
                Employee.CreatedAt = DateTime.Now;
                ViewData["DepartmentList"] = new SelectList(_db.Departments.Where(x => !x.IsDelete).ToList(), "Id", "Name");
                await _db.Employees.AddAsync(Employee);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepartmentList"] = new SelectList(_db.Departments.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            return View(input);


        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var Employees = await _db.Employees.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);

            if (Employees == null)
            {
                return NotFound();
            }
            var EmployeesUpdate = new UpdateEmployeeViewModel();
            EmployeesUpdate.Id = Employees.Id;
            EmployeesUpdate.Name = Employees.Name;
            EmployeesUpdate.Email = Employees.Email;
            EmployeesUpdate.DOB = Employees.DOB;

            if (EmployeesUpdate.ImgUrl != null)
            {
                Employees.ImgUrl = await _IFileService.SaveFile(EmployeesUpdate.ImgUrl, "Images"); ;

            }
            EmployeesUpdate.Phone = Employees.Phone;
            ViewData["DepartmentList"] = new SelectList(_db.Departments.Where(x => !x.IsDelete).ToList(), "Id", "Name");

            EmployeesUpdate.DepartmentId = Employees.DepartmentId;
            return View(EmployeesUpdate);
        }

        

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel input)
        {
            if (ModelState.IsValid)
            {
                var Employees = await _db.Employees.SingleOrDefaultAsync(x => x.Id == input.Id && !x.IsDelete);

                if (Employees == null)
                {
                    return NotFound();
                }
                Employees.Id = input.Id;
                Employees.Name = input.Name;
                Employees.Name = input.Name;
                Employees.Email = input.Email;
                Employees.DOB = input.DOB;
                if (input.ImgUrl != null)
                {
                    Employees.ImgUrl = await _IFileService.SaveFile(input.ImgUrl, "Images"); ;

                }
                Employees.Phone = input.Phone;
                Employees.DepartmentId = input.DepartmentId;
                ViewData["DepartmentList"] = new SelectList(_db.Departments.Where(x => !x.IsDelete).ToList(), "Id", "Name");
                Employees.UpdatedAt = DateTime.Now;
                _db.Employees.Update(Employees);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View();


        }

       
        public async Task<IActionResult> Delete(int Id)
        {
            var Employees = await _db.Employees.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (Employees == null)
            {
                return NotFound();
            }

            Employees.IsDelete = true;
            _db.Employees.Update(Employees);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
