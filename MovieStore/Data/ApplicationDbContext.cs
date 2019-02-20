using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models;

namespace MovieStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MovieStore.Models.Genre> Genre { get; set; }
        public DbSet<MovieStore.Models.Director> Director { get; set; }
        public DbSet<MovieStore.Models.Movie> Movie { get; set; }
    }
}
