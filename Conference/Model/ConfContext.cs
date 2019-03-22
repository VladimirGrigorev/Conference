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
        public DbSet<RoleInLecture> RoleInLectures { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<AdminOfConference> AdminOfConferences { get; set; }


        //todo override SaveChanging in order to delete orphans
    }
}
