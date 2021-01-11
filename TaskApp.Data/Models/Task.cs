using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskApp.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(200)]
        public string Objective { get; set; }
        public DateTime Deadline { get; set; }
    }
}
