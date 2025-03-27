namespace Equipment_Editor.Models;

public class Equipment
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public required string InvNumber { get; set; }

    public required bool IsActive {  get; set; }
    
    public required int TypeId { get; set; }
    public required Equipment_Type Type { get; set; }
    
    public required int ModelId { get; set; }
    public required Equipment_Model Model { get; set; }

    public DateOnly EntryDate { get; set; }

    public List<Equipment_Repair_Trans>? Equipment_Repair_Transes { get; set; }

}
