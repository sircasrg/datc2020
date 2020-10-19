using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private IStudentRepository _studentRepository;
        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentEntity>> Get()
        {
            return await _studentRepository.GetAllStudents();
        }

        [HttpPost]
        public async Task Post([FromBody] StudentEntity student)
        {
            await _studentRepository.Create(student);    
        }

        [HttpPut("{partitionKey}/{rowKey}")]
        public async Task Update([FromRoute] string partitionKey, [FromRoute] string rowKey, [FromRoute] StudentEntity student)
        {
            await _studentRepository.Update(partitionKey, rowKey, student);
        }

        [HttpDelete("{partitionKey}/{rowKey}")]
        public async Task Delete([FromRoute] string partitionKey, [FromRoute] string rowKey)
        {
            await _studentRepository.Delete(partitionKey, rowKey);
        }
    }
}
