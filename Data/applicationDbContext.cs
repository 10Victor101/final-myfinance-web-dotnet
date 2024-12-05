﻿using final_my_finance.Models;
using Microsoft.EntityFrameworkCore;

namespace final_my_finance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }
        public DbSet<PlanoDeContas> PlanoDeContas { get; set; }
    }
}