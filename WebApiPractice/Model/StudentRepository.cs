using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPractice.Model
{
    public class StudentRepository : IStudentREpository
    {
        private readonly AppDBContext appDBContext;
        public StudentRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var result = await appDBContext.Students.AddAsync(student);
            await appDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteStudent(int id)
        {
            var result = await appDBContext.Students.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDBContext.Students.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await appDBContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByFirstName(string firstName)
        {
            return await appDBContext.Students.FirstOrDefaultAsync(e => e.FirstName == firstName);
            }

        public async Task<Student> GetStudentById(int id)
        {
            return await appDBContext.Students.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Student>> Search(string name, Gender? gender)
        {
            IQueryable<Student> query = appDBContext.Students;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e=>e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var result = await appDBContext.Students.FirstOrDefaultAsync(e => e.Id == student.Id);
            if(result == null)
            {
                result = new Student()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Id = student.Id,
                    Gender = student.Gender,
                };
                return result;
            }
            return null;
        }
    }
}
