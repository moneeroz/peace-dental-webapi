using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using peace_api.Models;

namespace peace_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DotEnv.Load();
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(d => d.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Appointment>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("timezone('mdt', NOW())");

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Doctor>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("timezone('mdt', NOW())");

            modelBuilder.Entity<Invoice>()
                .Property(d => d.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Invoice>()
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("timezone('mdt', NOW())");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Patient>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("timezone('mdt', NOW())");
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

    }
}