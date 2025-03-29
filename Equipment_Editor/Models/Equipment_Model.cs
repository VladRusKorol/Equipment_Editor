namespace Equipment_Editor.Models
{
    public class Equipment_Model
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public required bool IsActive { get; set; }
        public List<Equipment>? Equipments { get; set; }

    }
}
