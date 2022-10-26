using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using Lab3.Core.Services.Abstractions;
using Lab3.Database;
using Lab3.Database.Entities;
using Lab3.Core.Exceptions;
using Lab3.Core.Models;

namespace Lab3.Core.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly StudentsDbContext _dbContext;

        public StudentsService(StudentsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        private bool AreColumnsValid(string columns)
        {
            var validColumns = new string[] { "id", "name", "surname", "lastname", "phone" };
            return columns
                .ToLower()
                .Split(',')
                .All(validColumns.Contains);
        }

        public IQueryable<Student> GetAll() => this._dbContext.Students;

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

        public IQueryable<object> FilterByUriParams(IQueryable<Student> studentsQuery, StudentsQueryParams studentsQueryParams)
        {
            studentsQuery = studentsQuery
                .Where(student => student.Id >= studentsQueryParams.MinId);

            if (studentsQueryParams.MaxId > 0)
            {
                studentsQuery = studentsQuery.Where(student => student.Id <= studentsQueryParams.MaxId);
            }

            if (studentsQueryParams.Like != null)
            {
                studentsQuery = studentsQuery.Where(student => 
                    student.Name.ToLower().Contains(studentsQueryParams.Like.ToLower()));
            }

            if (studentsQueryParams.GlobalLike != null)
            {
                studentsQuery = studentsQuery.Where(student =>
                    string.Concat(student.Id, student.Name, student.Phone)
                    .ToString()
                    .Contains(studentsQueryParams.GlobalLike.ToLower()));
            }

            studentsQuery = studentsQueryParams.Sort
                ? studentsQuery.OrderBy(student => student.Name)
                : studentsQuery.OrderBy(student => student.Id);

            studentsQuery = studentsQuery
                .Skip(studentsQueryParams.Offset)
                .Take(studentsQueryParams.Limit);

            var columns = studentsQueryParams.Columns;
            if (columns != null && this.AreColumnsValid(columns))
            {
                return (IQueryable<object>)studentsQuery.Select($"new ({studentsQueryParams.Columns})");
                
            }

            return studentsQuery;
        }
    }
}
