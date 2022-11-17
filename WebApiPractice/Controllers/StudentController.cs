using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebApiPractice.Model;

namespace WebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentREpository studentREpository;
        public StudentController(IStudentREpository studentREpository)
        {
            this.studentREpository = studentREpository;
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string name)
        {
            try
            {
                var result = await studentREpository.GetAllStudents();
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok (await studentREpository.GetAllStudents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await studentREpository.GetStudentById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");

            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if(student == null) return BadRequest();

                var stu = await studentREpository.GetStudentById(student.Id);
                if(stu != null)
                {
                    ModelState.AddModelError("Id", "sudent already present");
                    return BadRequest(ModelState);
                }
                var createStudent = await studentREpository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudent), new {id = createStudent.Id}, createStudent);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");

            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            try
            {
                if(id != student.Id)
                {
                    return BadRequest("Student ID missnatched");
                }
                var studentToUpdate = await studentREpository.GetStudentById(id);
                if(studentToUpdate != null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }
                return await studentREpository.UpdateStudent(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");

            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var studentToDelete = await studentREpository.GetStudentById(id);
                if (studentToDelete == null) return NotFound($"Stydent with id {id} not found");

                await studentREpository.DeleteStudent(id);
                return Ok($"Student with id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");

            }
        }
       
    }
}
