using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace students.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Students> GetStudents()
        {
            return StudentRepo.getAll();
        }

        [HttpGet("{id}")]
        public Students GetStudentsbyId([FromRoute] int id)
        {
            return StudentRepo.getById(id);
        }

        [HttpPost]
        public string CreateStudent([FromBody] Students student)
        {
            try
            {
                Console.WriteLine(student.ToString());
                StudentRepo.insert(student);
                return "Student created successfully";
            }
            catch (System.Exception error)
            {
                return "Eroare: " + error.Message;
                throw;
            }
        }

        [HttpPut("{id}")]
        public string UpdateStudent([FromRoute] int id, [FromBody] Students student)
        {
            Students updatedStudent = StudentRepo.update(id, student);
            return updatedStudent.ToString();
        }

        [HttpDelete("{id}")]
        public string DeleteStudentById([FromRoute] int id)
        {
            StudentRepo.deleteById(id);
            return "Student removed successfully";
        }
    }
}
