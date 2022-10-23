using System.Collections.Generic;

using Ploeh.Hyprlinkr;

using Lab3.WebApi.Models;
using Lab3.WebApi.Controllers;
using Lab3.WebApi.Enums;

namespace Lab3.WebApi.Constants
{
    public static class ResourcesLinks
    {
        public static List<Link> GetStudentResourceLinksHttpGet(int studentId, RouteLinker linker)
        {
            return new List<Link>
            {
                new Link
                {
                    Rel = "self",
                    Href = linker.GetUri<StudentsController>(controller => controller.Get(studentId)).ToString(),
                },
                new Link(HttpMethod.PUT)
                {
                    Rel = "update-it",
                    Href = linker.GetUri<StudentsController>(controller => controller.Put(studentId, null)).ToString(),
                },
                new Link(HttpMethod.DELETE)
                {
                    Rel = "delete-it",
                    Href = linker.GetUri<StudentsController>(controller => controller.Delete(studentId)).ToString(),
                },
            };
        }

        public static List<Link> GetStudentResourceLinksHttpPost(int studentId, RouteLinker linker)
        {
            return new List<Link>
            {
                new Link
                {
                    Rel = "get-it",
                    Href = linker.GetUri<StudentsController>(controller => controller.Get(studentId)).ToString(),
                },
                new Link(HttpMethod.PUT)
                {
                    Rel = "update-it",
                    Href = linker.GetUri<StudentsController>(controller => controller.Put(studentId, null)).ToString(),
                },
                new Link(HttpMethod.DELETE)
                {
                    Rel = "delete-it",
                    Href = linker.GetUri<StudentsController>(controller => controller.Delete(studentId)).ToString(),
                },
            };
        }

        public static List<Link> GetStudentResourceCollectionLinks(RouteLinker linker)
        {
            return new List<Link>
            {
                new Link(HttpMethod.POST)
                {
                    Rel = "add-new",
                    Href = linker.GetUri<StudentsController>(controller => controller.Post(null)).ToString(),
                },
            };
        }

        public static List<Link> GetErrorResourceLinks(string guid, RouteLinker linker)
        {
            return new List<Link>
            {
                new Link
                {
                    Rel = "get-error-info",
                    Href = linker.GetUri<ErrorsController>(controller => controller.Get(guid)).ToString(),
                },
            };
        }
    }
}