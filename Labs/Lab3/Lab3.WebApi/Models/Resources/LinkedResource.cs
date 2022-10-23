using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.WebApi.Models.Resources
{
    public abstract class LinkedResource
    {
        public List<Link> Links { get; set; } = new List<Link>();

        public string Href { get; set; }
    }
}