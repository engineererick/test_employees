using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Employees.Data;
using Test_Employees.Models;

namespace Test_Employees.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> listEmployees = _context.Employee;
            listEmployees = listEmployees.OrderBy(x => x.BornDate);
            return View(listEmployees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                _context.Employee.Add(employee);
                _context.SaveChanges();

                TempData["success_add"] = "Employee added succesfuly";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var emp = _context.Employee.Where(r => r.RFC == employee.RFC).FirstOrDefault();
                _context.Employee.Remove(emp);
                _context.SaveChanges();
                TempData["duplicate_rfc"] = "Duplicated RFC";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0) return NotFound();

            var employee = _context.Employee.Find(id);

            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                _context.Employee.Update(employee);
                _context.SaveChanges();

                TempData["success_add"] = "Employee updated succesfuly";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList()
                                                .ForEach(e => e.State = EntityState.Detached);
                TempData["duplicate_rfc"] = "Duplicated RFC";
                return View();
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            var employee = _context.Employee.Find(id);

            if (employee is null) return NotFound();

            _context.Employee.Remove(employee);

            _context.SaveChanges();

            TempData["success_deleted"] = "Successfully deleted";

            return RedirectToAction("Index");
        }
    }
}
