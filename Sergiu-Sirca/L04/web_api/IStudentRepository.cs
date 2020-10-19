using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IStudentRepository {

    Task<List<StudentEntity>> GetAllStudents();

    Task Create(StudentEntity student);

    Task Update(string partitionKey, string rowKey, StudentEntity student);

    Task Delete(string partitionKey, string rowKey);

}