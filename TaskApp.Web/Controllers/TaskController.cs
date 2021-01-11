using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Services.Task;
using TaskApp.Web.Serialization;
using TaskApp.Web.ViewModels;

namespace TaskApp.Web.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet("/api/tasks")]
        public ActionResult GetTasks()
        {
            _logger.LogInformation("Getting tasks");
            var tasks = _taskService.GetAll();
            /*
            var customerModels = customers.Select(customer => new CustomerModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = CustomerMapper
                    .MapCustomerAddress(customer.PrimaryAddress),
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn
            }).OrderByDescending(customer => customer.CreatedOn)
            .ToList();*/
            //return Ok(taskModels);
            return Ok(tasks);
        }


        /// <summary>
        /// Returns Task by its Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/tasks/{id}")]
        public ActionResult GetTask(int id)
        {
            _logger.LogInformation("Getting task by id");
            var task = _taskService.GetById(id);
            return Ok(task);
        }


        /// <summary>
        /// Adds new Task to db
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost("/api/tasks")]
        public ActionResult Create([FromBody] TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating a new task");
            task.CreatedOn = DateTime.UtcNow;
            task.UpdatedOn = DateTime.UtcNow;
            var taskData = TaskMapper.SerializeTask(task);
            var newTask = _taskService.Create(taskData);
            return Ok(newTask);
        }

        /// <summary>
        /// Removes Task by Id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/tasks/{id}")]
        public ActionResult DeleteTask(int id)
        {
            _logger.LogInformation("Deleting a Task");
            var response = _taskService.Delete(id);
            return Ok(response);
        }
    }
}
