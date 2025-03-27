namespace Equipment_Editor.Models;

public class Equipment_Repair_Trans
{
    public required int Id { get; set; }
    public required DateOnly RepairDate { get; set; }
    public required int EquipmentId {  get; set; }
    public required Equipment Equipment {  get; set; }
    public required int EquipmentRepairId { get; set; }
    public required Equipment_Repair EquipmentRepair { get; set; }
    public required DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
    public string RepairComment { get; set; } = string.Empty;
}
