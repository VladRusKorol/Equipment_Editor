namespace Equipment_Editor.Models
{
    public class Equipment_Repair
    {
        public  int Id { get; set; }
        public required string Description { get; set; }
        public required bool IsActive { get; set; }

        public List<Equipment_Repair_Trans>? Equipment_Repair_Transes { get; set; }
    }
}
