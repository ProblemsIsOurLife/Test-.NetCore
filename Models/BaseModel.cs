
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Testquest.entities;

namespace Testquest.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        { 
            Database.EnsureCreated();
        }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Specs> Specs { get; set; }
        
        
    }
}