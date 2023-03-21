using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LMS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> obj = _context.Students.ToList();
            return View(obj);
        }
        public IActionResult Create()
        {

            return View();
        }


        //[HttpPost]

        public IActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(stu);
                _context.SaveChanges();
                return new JsonResult("Data Succefull save");
               // return RedirectToAction("Index");
            }

            return View(stu);
        }
        public IActionResult Edit(int id)
        {
            return View();
           
        }
        public IActionResult EditStudent(int id)
        {
            var stu = _context.Students.Find(id);
            _context.Students.Update(stu);
            _context.SaveChanges();
            return new JsonResult("Updated Successfull");

        }
        [HttpPost]
        public IActionResult Edit(Student stu)
        {

            if (ModelState.IsValid) {
                _context.Students.Update(stu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(m => m.StuId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public IActionResult Delete(int id)
        {
            
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return new JsonResult("Deleted Successfully");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Student stu)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(stu);
            _context.SaveChanges();
            return View();
        }

        public JsonResult GetStudents()
        {
            return GetAll();
        }
        [HttpGet]
        public JsonResult GetAll()
        {
           
            var Res = _context.Students.OrderByDescending(x => x.StuId).ToList();
            var Data = new
            {
                data = Res,
                TotalCount = Res.Count
                
            };
            return new JsonResult(Data);
        }

        public IActionResult StudentForm(int id)
        {
            if (id == 0)
            {
                return PartialView("_student");
            }
            else
            {
                Student d = _context.Students.Find(id);
                return PartialView("_student", d);
            }
        }

        public IActionResult SaveStudent(Student stu)
        {
           
            if (stu.StuId==0)
            {
                _context.Students.Add(stu);
                _context.SaveChanges();
                return new JsonResult("done");
            }
            else if (stu.StuId != null)
            {
                // var upd = _context.Students.Find(stu.StuId);            
                _context.Students.Update(stu);
                _context.SaveChanges();
                return new JsonResult("updated");
            }
            return new JsonResult("false");
        }
  
     
    }
}
