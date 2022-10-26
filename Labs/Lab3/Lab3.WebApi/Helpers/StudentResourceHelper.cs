using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Ploeh.Hyprlinkr;

using Lab3.Database.Entities;
using Lab3.WebApi.Constants;
using Lab3.WebApi.Models.Resources;
using Lab3.WebApi.Models;
using Lab3.Core.Models;

namespace Lab3.WebApi.Helpers
{
    public static class StudentResourceHelper
    {
        private static dynamic ConvetToResource(object student, RouteLinker linker)
        {
            dynamic resource = student.ToExpandoObject();

            var hasId = ((IDictionary<string, object>)resource).ContainsKey("Id");
            if (hasId)
            {
                var links = ResourcesLinks.GetStudentResourceLinksHttpGet(resource.Id, linker);
                resource.Links = links;
            }

            return resource;
        }

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

        public static LinkedResourceCollection<object> ConvetToResource(
            ICollection<object> students,
            StudentsQueryParams studentsQueryParams,
            int studentsCount,
            HttpRequestMessage httpRequest)
        {
            var linker = new RouteLinker(httpRequest);
            var embedded = students.Select(student => ConvetToResource(student, linker)).ToList();

            return new LinkedResourceCollection<object>
            {
                TotalCount = studentsCount,
                Embedded = embedded,
                Links = ResourcesLinks.GetStudentResourceCollectionLinks(studentsQueryParams, studentsCount, linker),
            };
        }
    }
}