using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab3.Core.Models;
using Lab3.Database.Entities;

namespace Lab3.Core.Services.Abstractions
{
    public interface IStudentsService
    {
        IQueryable<Student> GetAll();

        Task<Student> GetAsync(int studentId);

        Task DeleteAsync(int studentId);

        Task<Student> AddAsync(Student studentCreateData);

        Task UpdateAsync(int studentId, Student studentUpdateData);

        IQueryable<Student> FilterByUriParams(IQueryable<Student> studentsQuery, StudentsQueryParams studentsQueryParams);
    }
}
