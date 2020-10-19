using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IStudentRepository {

    Task<List<StudentEntity>> GetAllStudents();

    Task Create(StudentEntity student);
}