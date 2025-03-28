using Microsoft.EntityFrameworkCore;

namespace Equipment_Editor.Models;

    public class EquipmentContext: DbContext
    {
        public required DbSet<Equipment> Equipments { get; set; }
        public required DbSet<Equipment_Model> Equipment_Models { get; set; }
        public required DbSet<Equipment_Type> Equipment_Types { get; set; }
        public required DbSet<Equipment_Repair> Equipment_Repairs { get; set; }
        public required DbSet<Equipment_Repair_Trans> Equipment_Repair_Transes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AplicationDB.db");
        }

    }

