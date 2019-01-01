using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCards.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCards.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Expansion> Expansion { get; set; }
        public DbSet<CardType> CardType { get; set; }

    }
}
