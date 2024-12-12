using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondASPcorePractice.Data;
using SecondASPcorePractice.Models;
using SecondASPcorePractice.Models.Entities;

namespace SecondASPcorePractice.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddStudent(AddStudent addStudent)
        {
            var student = new StudentEnroll
            {
                Name = addStudent.Name,
                Phone = addStudent.Phone,
                Subject = addStudent.Subject,
                Status  = addStudent.Status,
            };
            await applicationDbContext.studentEnrollment.AddAsync(student);

           await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("List","Student");
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var studentList=await applicationDbContext.studentEnrollment.ToListAsync();

            return View(studentList);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var student = await applicationDbContext.studentEnrollment.FindAsync(id);

            if (student == null)
            {
                return NotFound("Error");
            }

            return View(student);

        }


        [HttpPost]

        public async Task<IActionResult> Edit(StudentEnroll studentEnroll)
        {
            var Student = await applicationDbContext.studentEnrollment.FindAsync(studentEnroll.Id);

            if (Student == null)
            {
                return NotFound("Error");

            }

            Student.Name = studentEnroll.Name;
            Student.Phone = studentEnroll.Phone;
            Student.Subject = studentEnroll.Subject;
            Student.Status = studentEnroll.Status;
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("List","Student");


        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentEnroll studentEnroll)
        {
            var Student = await applicationDbContext.studentEnrollment.FindAsync(studentEnroll.Id);

            if (Student == null)
            {
                return NotFound("Student not found."); // Entity doesn't exist
            }

            // Remove the entity
            applicationDbContext.studentEnrollment.Remove(Student);
            await applicationDbContext.SaveChangesAsync();

            // Redirect back to the list after successful deletion
            return RedirectToAction("List", "Student");
        }


    }
}
