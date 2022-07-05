using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeautySalonDatabaseImplement.Models;

namespace BeautySalonDatabaseImplement
{
    public class BeautySalonDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1TH8GFI\SQLEXPRESS;Initial Catalog=BeautySalonDatabase1;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<OrderProcedure> OrderProcedures { set; get; }
        public virtual DbSet<OrderCosmetic> OrderCosmetics { set; get; }
        public virtual DbSet<Service> Services { set; get; }
        public virtual DbSet<Procedure> Procedures { set; get; }
        public virtual DbSet<ProcedureService> ProcedureServices { set; get; }
        public virtual DbSet<Cosmetic> Cosmetics { set; get; }
        public virtual DbSet<CosmeticService> CosmeticServices { set; get; }
        public virtual DbSet<Employee> Emplyees { set; get; }
        public virtual DbSet<Estimate> Estimates { set; get; }
        public virtual DbSet<LaborCost> LaborCosts { set; get; }
    }
}
