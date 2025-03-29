using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equipment_Editor.DTO.Equipment;

public class EditEquipmentDTO
{
    public required ReadableEquipmentDTO Equipment { get; set; }

    public required SelectList Types { get; set; }

    public required SelectList Models { get; set; }
}
