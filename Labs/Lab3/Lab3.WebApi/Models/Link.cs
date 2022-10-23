using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Lab3.WebApi.Enums;

namespace Lab3.WebApi.Models
{
    public class Link
    {
        public Link() { }

        public Link(HttpMethod method)
        {
            Method = method.ToString();
        }

        public string Rel { get; set; }

        public string Href { get; set; }

        public string Method { get; private set; } = HttpMethod.GET.ToString();

        public void SetMethod(HttpMethod method)
        {
            this.Method = method.ToString();
        }
    }
}