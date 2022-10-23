using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.WebApi.Models.Resources
{
    public class Resource<Entity> where Entity : class
    {
        public Resource()
        {
        }

        public Entity Value { get; set; }

        public List<Link> Links { get; } = new List<Link>();
    }
}