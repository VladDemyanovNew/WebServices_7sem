using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab3.Database.Entities;

namespace Lab3.Core.Helpers
{
    public static class StudentHelper
    {
        public static void Fill(Student src, Student dest)
        {
            dest.Surname = src.Surname ?? dest.Surname;
            dest.Name = src.Name ?? dest.Name;
            dest.Phone = src.Phone ?? dest.Phone;
            dest.Lastname = src.Lastname ?? dest.Lastname;
        }
    }
}
