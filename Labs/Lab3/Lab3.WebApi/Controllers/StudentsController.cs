using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using Ploeh.Hyprlinkr;
using System.Linq;

using Lab3.Core.Services.Abstractions;
using Lab3.Database.Entities;
using Lab3.WebApi.Models;
using Lab3.WebApi.Models.Resources;
using Lab3.WebApi.Enums;

namespace Lab3.WebApi.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentsService studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        public Resource<ICollection<Resource<Student>>> GetAll()
        {
            var students = this.studentsService.GetAll();

            var links = new List<Link>
            {
                new Link
                {
                    Rel = "self",
                    Href = this.Url.GetLink<StudentsController>(controller => this.GetAll()).ToString(),
                },
                new Link(HttpMethod.POST)
                {
                    Rel = "add-new",
                    Href = this.Url.GetLink<StudentsController>(controller => this.Post(new Student { })).ToString(),
                },
            };

            var resources = new Resource<ICollection<Resource<Student>>>();
            resources.Links.AddRange(links);

            return resources;
        }

        public async Task<Resource<Student>> Get(int id)
        {
            var student = await this.studentsService.GetAsync(id);

            var links = new List<Link>
            {
                new Link
                {
                    Rel = "self",
                    Href = this.Url.GetLink<StudentsController>(controller => this.Get(id)).ToString(),
                },
                new Link(HttpMethod.PUT)
                {
                    Rel = "update-it",
                    Href = this.Url.GetLink<StudentsController>(controller => this.Put(id, new Student { })).ToString(),
                },
                new Link(HttpMethod.DELETE)
                {
                    Rel = "delete-it",
                    Href = this.Url.GetLink<StudentsController>(controller => this.Delete(id)).ToString(),
                },
            };

            var resource = new Resource<Student> { Value = student };
            resource.Links.AddRange(links);

            return resource;
        }

        public async Task<IHttpActionResult> Post([FromBody] Student studentCreateData)
        {
            var student = await this.studentsService.AddAsync(studentCreateData);

            var location = this.Url.GetLink<StudentsController>(controller => this.Get(student.Id)).ToString();
            var links = new List<Link>
            {
                new Link
                {
                    Rel = "get-it",
                    Href = location,
                },
                new Link(HttpMethod.PUT)
                {
                    Rel = "update-it",
                    Href = this.Url.GetLink<StudentsController>(controller => this.Put(student.Id, new Student { })).ToString(),
                },
                new Link(HttpMethod.DELETE)
                {
                    Rel = "delete-it",
                    Href = this.Url.GetLink<StudentsController>(controller => this.Delete(student.Id)).ToString(),
                },
            };

            var resource = new Resource<Student> { Value = student };
            resource.Links.AddRange(links);
            
            return this.Created(location, resource);
        }

        public async Task Put(int id, [FromBody] Student studentUpdateData) =>
            await this.studentsService.UpdateAsync(id, studentUpdateData);

        public async Task Delete(int id) => await this.studentsService.DeleteAsync(id);
    }
}
