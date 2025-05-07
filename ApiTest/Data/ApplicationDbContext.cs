using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
