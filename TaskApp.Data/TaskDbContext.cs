using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.Data.Models;

namespace TaskApp.Data
{
    public class TaskDbContext : IdentityDbContext
    {
        public TaskDbContext() { }
        public TaskDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Task> Tasks { get; set; }
    }
}
