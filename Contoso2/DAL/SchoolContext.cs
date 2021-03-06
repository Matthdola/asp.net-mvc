﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.Data.Entity.ModelConfiguration.Conventions;

using Contoso2.Models;
namespace Contoso2.DAL
{
    public class SchoolContext: DbContext
    {
        public SchoolContext()
            : base("SchoolContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                .MapRightKey("InstructorID")
                .ToTable("CourseInstructor"));
            //modelBuilder.Entity<Department>().MapToStoredProcedures();
        }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Enrollment> Enrollments { get; set; } 
        public DbSet<Instructor> Instructors { get; set; } 
        public DbSet<Student> Students { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
    }


    /*
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base("SchoolContext")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        static SchoolContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
     */
}