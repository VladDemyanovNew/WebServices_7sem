using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using Lab3.Core.Services.Abstractions;
using Lab3.Database;
using Lab3.Database.Entities;
using Lab3.Core.Exceptions;

namespace Lab3.Core.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly StudentsDbContext _dbContext;

        public StudentsService(StudentsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Student> GetAll() => this._dbContext.Students;

        public async Task<Student> GetAsync(int studentId)
        {
            var student = await this._dbContext.Students.FindAsync(studentId);
            if (student == null)
            {
                throw new ApiEntityNotFoundException($"Can't get student with id={studentId}, because it doesn't exist");
            }

            return student;
        }

        public async Task<Student> AddAsync(Student studentCreateData)
        {
            var student = this._dbContext.Students.Add(studentCreateData);
            
            await this._dbContext.SaveChangesAsync();

            return student;
        }

        public async Task DeleteAsync(int studentId)
        {
            var student = await this._dbContext.Students.FindAsync(studentId);
            if (student == null)
            {
                throw new ApiEntityNotFoundException($"Can't delete student with id={studentId}, because it doesn't exist");
            }

            this._dbContext.Students.Remove(student);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int studentId, Student studentUpdateData)
        {
            var student = await this._dbContext.Students.FindAsync(studentId);
            if (student == null)
            {
                throw new ApiEntityNotFoundException($"Can't update student with id={studentId}, because it doesn't exist");
            }

            studentUpdateData.Id = studentId;
            this._dbContext.Entry(studentUpdateData).State = EntityState.Modified;

            await this._dbContext.SaveChangesAsync();
        }
    }
}
