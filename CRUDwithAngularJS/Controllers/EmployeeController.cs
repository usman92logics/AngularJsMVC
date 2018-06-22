using CRUDwithAngularJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDwithAngularJS.Controllers
{
    public class EmployeeController : Controller
    {
        private AngularJSEntities db = new AngularJSEntities();


        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //GET Employee/GetEmployee
        public JsonResult GetEmployee()
        {
            List<Employee> empList = db.Employees.ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);
        }

        //POST insert new Employee
        public JsonResult Insert(Employee employee)
        {
            if (employee != null)
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        //POST Employee/Update
        [HttpPost]
        public JsonResult Update(Employee updatedEmployee)
        {
            Employee existingEmployee = db.Employees.Find(updatedEmployee.EmpID);
            if (existingEmployee == null)
            {
                return Json(new { success = false });
            }
            else
            {
                existingEmployee.EmpName = updatedEmployee.EmpName;
                existingEmployee.DeptName = updatedEmployee.DeptName;
                existingEmployee.Email = updatedEmployee.Email;
                existingEmployee.Designation = updatedEmployee.Designation;

                db.Entry(existingEmployee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
        }

        //POST Employee/Delete/1
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Json(new { success = false });
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}