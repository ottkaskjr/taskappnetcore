using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApp.Services.Task
{
    public interface ITaskService
    {
        List<Data.Models.Task> GetAll();
        ServiceResponse<Data.Models.Task> Create(Data.Models.Task task);
        ServiceResponse<bool> Delete(int id);
        Data.Models.Task GetById(int id);
    }
}
