using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RecordBook.Models;

namespace RecordBook.DAL
{
    public class GuestBookContext : DbContext
    {
        public GuestBookContext() : base("DefaultConnection")
        {
        }

        public DbSet<Record> Records { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}