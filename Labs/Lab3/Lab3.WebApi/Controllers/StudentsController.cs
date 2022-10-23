using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

using Ploeh.Hyprlinkr;

using Lab3.Core.Services.Abstractions;
using Lab3.Database.Entities;
using Lab3.WebApi.Models.Resources;
using Lab3.WebApi.Helpers;

namespace Lab3.WebApi.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentsService studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        public LinkedResourceCollection<StudentResource> GetAll()
        {
            var students = this.studentsService.GetAll();

            return StudentResourceHelper.ConvetToResource(students.ToList(), this.Request);
        }

        public async Task<StudentResource> Get(int id)
        {
            var student = await this.studentsService.GetAsync(id);

            return StudentResourceHelper.ConvetToResource(student, this.Request);
        }

        public async Task<IHttpActionResult> Post([FromBody] Student studentCreateData)
        {
            var student = await this.studentsService.AddAsync(studentCreateData);
            var resource = StudentResourceHelper.ConvetToResource(student, this.Request);

            var location = this.Url.GetLink<StudentsController>(controller => this.Get(student.Id)).ToString();

            return this.Created(location, resource);
        }

        public async Task Put(int id, [FromBody] Student studentUpdateData) =>
            await this.studentsService.UpdateAsync(id, studentUpdateData);

        public async Task Delete(int id) => await this.studentsService.DeleteAsync(id);
    }
}
