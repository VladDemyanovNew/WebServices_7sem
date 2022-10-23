using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Database.Entities
{
    public class Student : BaseEntity
    {
        public string Lastname { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }
    }
}
