using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiPractice.Model
{
    public interface IStudentREpository
    {
        Task<IEnumerable<Student>> Search(string name, Gender? gender);
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByFirstName(string firstName);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task DeleteStudent(int id);
        Task<IEnumerable<Student>> GetAllStudents();
    }
}
