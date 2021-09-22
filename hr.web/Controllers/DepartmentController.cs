using hr.web.Data;
using hr.web.ViewModel;
using hr.web.ViewModel.Department;
using hr.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hr.web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;


        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var Department = _db.Departments.Include(x => x.Employees).Where(x=>!x.IsDelete).Select(x => new DepartmentViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Employees = _db.Employees.Select(x => x.Name).ToList(),
                CreateAt =x.CreatedAt,
            
            }).ToList();

            return View(Department);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartemntViewModel input)
        {
            if (ModelState.IsValid)
            {
                var Department = new Department();
                Department.Name = input.Name;
                Department.CreatedAt = DateTime.Now;
                await _db.Departments.AddAsync(Department);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(input);
 

        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var Department = await _db.Departments.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);

            if (Department == null)
            {
                return NotFound();
            }
            var DepartmentUpdate = new UpdateDepartmentViewModel();
            DepartmentUpdate.Id = Department.Id;
            DepartmentUpdate.Name = Department.Name;
            return View(DepartmentUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDepartmentViewModel input)
        {
            if (ModelState.IsValid)
            {
                var Department = await _db.Departments.SingleOrDefaultAsync(x => x.Id == input.Id && !x.IsDelete);

                if (Department == null)
                {
                    return NotFound();
                }
                Department.Name = input.Name;
                Department.UpdatedAt = DateTime.Now;

                _db.Departments.Update(Department);
                await  _db.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View();

     
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var Department = await _db.Departments.SingleOrDefaultAsync(x => x.Id ==Id && !x.IsDelete);
            if (Department == null)
            {
                return NotFound();
            }

            Department.IsDelete = true;
            _db.Departments.Update(Department);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
