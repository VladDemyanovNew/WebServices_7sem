using Lab3.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Database
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext() : base("Students") { }

        public DbSet<Student> Students { get; set; }
    }
}
