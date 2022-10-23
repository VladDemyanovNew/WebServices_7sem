using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Ploeh.Hyprlinkr;

using Lab3.Database.Entities;
using Lab3.WebApi.Constants;
using Lab3.WebApi.Models.Resources;
using Lab3.WebApi.Models;

namespace Lab3.WebApi.Helpers
{
    public static class StudentResourceHelper
    {
        public static StudentResource ConvetToResource(Student student, HttpRequestMessage httpRequest)
        {
            var linker = new RouteLinker(httpRequest);

            var links = new List<Link>();
            switch (httpRequest.Method.ToString())
            {
                case "GET":
                    links = ResourcesLinks.GetStudentResourceLinksHttpGet(student.Id, linker);
                    break;
                case "POST":
                    links = ResourcesLinks.GetStudentResourceLinksHttpPost(student.Id, linker);
                    break;
            }

            return new StudentResource
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Lastname = student.Lastname,
                Phone = student.Phone,

                Links = links,
            };
        }

        public static LinkedResourceCollection<StudentResource> ConvetToResource(ICollection<Student> students, HttpRequestMessage httpRequest)
        {
            var linker = new RouteLinker(httpRequest);

            var embedded = students.Select(student => ConvetToResource(student, httpRequest));

            return new LinkedResourceCollection<StudentResource>
            {
                Embedded = embedded.ToList(),
                Links = ResourcesLinks.GetStudentResourceCollectionLinks(linker),
            };
        }
    }
}