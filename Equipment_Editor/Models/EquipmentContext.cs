using Microsoft.EntityFrameworkCore;

namespace Equipment_Editor.Models
{
    public class EquipmentContext: DbContext
    {
        public DbSet<Equipment>? Equipments { get; set; }
        public DbSet<Equipment_Model>? Equipment_Models { get; set; }
        public DbSet<Equipment_Type>? Equipment_Types { get; set; }
        public DbSet<Equipment_Repair>? Equipment_Repairs { get; set; }
        public DbSet<Equipment_Repair_Trans>? Equipment_Repair_Transes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AplicationDB.db");
        }

    }
}
