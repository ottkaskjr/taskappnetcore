using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.ViewModels;

namespace TaskApp.Web.Serialization
{
    public static class TaskMapper
    {
        /// <summary>
        /// Serializes a Task data model into a TaskModel view model
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TaskModel SerializeTask(Data.Models.Task task)
        {
            return new TaskModel
            {
                Id = task.Id,
                CreatedOn = task.CreatedOn,
                UpdatedOn = task.UpdatedOn,
                Deadline = task.Deadline,
                Objective = task.Objective,
            };
        }

        /// <summary>
        /// Serializes a TaskModel view model into a Task data model
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static Data.Models.Task SerializeTask(TaskModel task)
        {
            return new Data.Models.Task
            {
                Id = task.Id,
                CreatedOn = task.CreatedOn,
                UpdatedOn = task.UpdatedOn,
                Deadline = task.Deadline,
                Objective = task.Objective,
            };
        }
    }
}
