﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace asp_xamar_solution.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<UserDataModel> UserData { get; set; }
        public DbSet<UserWalletData> WalletData { get; set; }
        public DbSet<TransactionsHistoryModel> TransHistory { get; set; }
    }
}