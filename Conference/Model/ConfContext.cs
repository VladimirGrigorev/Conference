using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Conference.Model
{
    public class ConfContext : DbContext
    {
        public ConfContext(DbContextOptions<ConfContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
