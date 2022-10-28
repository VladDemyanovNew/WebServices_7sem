using System.Collections.Generic;

using Ploeh.Hyprlinkr;

using Lab3.WebApi.Models;
using Lab3.WebApi.Controllers;
using Lab3.WebApi.Enums;
using Lab3.Core.Models;
using Lab3.Core.Helpers;

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

        public static List<Link> GetStudentResourceCollectionLinks(StudentsQueryParams uriParams, int studentsCount, RouteLinker linker)
        {
            var uriParamsFirstPage = new StudentsQueryParams();
            var uriParamsLastPage = new StudentsQueryParams();
            var uriParamsPrevPage = new StudentsQueryParams();
            var uriParamsNextPage = new StudentsQueryParams();

            StudentsQueryParamsHelper.Fill(uriParams, uriParamsFirstPage);
            StudentsQueryParamsHelper.Fill(uriParams, uriParamsLastPage);
            StudentsQueryParamsHelper.Fill(uriParams, uriParamsPrevPage);
            StudentsQueryParamsHelper.Fill(uriParams, uriParamsNextPage);

            uriParamsLastPage.Offset = studentsCount < uriParamsNextPage.Limit
                ? 0
                : studentsCount - uriParamsNextPage.Limit;
            uriParamsFirstPage.Offset = 0;
            uriParamsNextPage.Offset += uriParamsNextPage.Limit;
            uriParamsPrevPage.Offset =
                uriParamsPrevPage.Offset < uriParamsPrevPage.Limit
                ? 0
                : uriParamsPrevPage.Offset - uriParamsPrevPage.Limit;

            var links = new List<Link>()
            {
                new Link(HttpMethod.POST)
                {
                    Rel = "add-new",
                    Href = linker.GetUri<StudentsController>(controller =>
                        controller.Post(null)).ToString(),
                },
                new Link(HttpMethod.GET)
                {
                    Rel = "first-page",
                    Href = linker.GetUri<StudentsController>(controller =>
                        controller.GetAll(null)).ToString() +
                        "?" + uriParamsFirstPage.ToQueryString(),
                },
                new Link(HttpMethod.GET)
                {
                    Rel = "last-page",
                    Href = linker.GetUri<StudentsController>(controller =>
                        controller.GetAll(null)).ToString() +
                        "?" + uriParamsLastPage.ToQueryString(),
                },
            };

            bool isNextVisible = uriParamsNextPage.Offset < studentsCount;
            if (isNextVisible)
            {
                links.Add(new Link(HttpMethod.GET)
                {
                    Rel = "next-page",
                    Href = linker.GetUri<StudentsController>(controller =>
                        controller.GetAll(null)).ToString() +
                        "?" + uriParamsNextPage.ToQueryString(),
                });
            }

            bool isPreviousVisible = uriParams.Offset - uriParamsPrevPage.Limit >= 0;
            if (isPreviousVisible)
            {
                links.Add(new Link(HttpMethod.GET)
                {
                    Rel = "previous-page",
                    Href = linker.GetUri<StudentsController>(controller =>
                        controller.GetAll(null)).ToString() +
                        "?" + uriParamsPrevPage.ToQueryString(),
                });
            }

            return links;
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