using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.WebApi.Models.Resources
{
    public class StudentResource : LinkedResource
    {
        public int Id { get; set; }

        public string Lastname { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }
    }
}