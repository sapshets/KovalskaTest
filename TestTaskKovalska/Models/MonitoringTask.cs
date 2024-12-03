namespace TestKovalska.Models;

public class MonitoringTask
{
    public int Code { get; set; }
    public string EquipmentCode { get; set; }
    public int TechPlaceCode { get; set; }
    public string ModelCode { get; set; }
    public string ModelName { get; set; }
    public int CharacteristicCode { get; set; }
    public string CharacteristicName { get; set; }
    public string UnitName { get; set; }
    public int Value { get; set; }
    public DateTime PlanDate { get; set; }
    public DateTime FactDate { get; set; }
    public string EquipmentTypeCode { get; set; }
    public string EquipmentTypeName { get; set; }
    public string Description { get; set; }
    public List<ValueRange> ValueRange { get; set; }
}