using Equipment_Editor.Models;

namespace Equipment_Editor.DTO.Equipment
{
    public class ReadableEquipmentDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string InvNumber { get; set; }

        public required bool IsActive { get; set; }

        public required string TypeName { get; set; }

        public required string ModelName { get; set; }

        public required DateOnly EntryDate { get; set; }

    }
}
