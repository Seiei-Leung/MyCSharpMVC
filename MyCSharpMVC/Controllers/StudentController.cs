using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Models;
using System.Configuration;

namespace MyCSharpMVC.Controllers
{
    public class StudentController : Controller
    {
        StudentService studentService = new StudentService();

        // GET: Student
        public ActionResult getAll()
        {
            List<Student> studentList = studentService.getAll();
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getInsert()
        {
            Student student = new Student()
            {
                studentName = "123",
                classId = 1,
                birthday = DateTime.Now,
                img = "",
                sex = "男"
            };

            bool result = studentService.insert(student);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}