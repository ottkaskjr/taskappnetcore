using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskApp.Data;

namespace TaskApp.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _db;
        public TaskService(TaskDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Returns a list of Tasks from the database
        /// </summary>
        /// <returns>List<Customer></returns>
        public List<Data.Models.Task> GetAll()
        {
            // we have to include also the CustomerAddress table to the Customer table
            return _db.Tasks
                //.Include(customer => customer.PrimaryAddress) // Include => using Microsoft.EntityFrameworkCore;
                .OrderBy(task => task.Deadline) // OrderBy => using System.Linq;
                .ToList();
        }

        public ServiceResponse<Data.Models.Task> Create(Data.Models.Task task)
        {
            DateTime now = DateTime.UtcNow;
            try
            {
                _db.Tasks.Add(task);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Task>
                {
                    IsSuccess = true,
                    Message = "New Task created",
                    Time = DateTime.UtcNow,
                    Data = task
                };

            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Task>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = task
                };
            }
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var task = _db.Tasks.Find(id);
            var now = DateTime.UtcNow;

            if(task == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Task to delete not found",
                    Data = false
                };
            }
            try
            {
                _db.Tasks.Remove(task);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = $"Task {task.Id} deleted",
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Data = false
                };
            }
        }

        public Data.Models.Task GetById(int id)
        {
            return _db.Tasks.Find(id);
        }

        
    }
}
