namespace TestKovalska.Models;

public class TechnicalPlace
{
    public int Code { get; set; }
    public int ParentCode { get; set; }
    public string ModelCode { get; set; }
    public string Name { get; set; }
    public string FactoryCode { get; set; }
    public string FactoryName { get; set; }
    public int DepartmentCode { get; set; }
    public string DepartmentName { get; set; }
    public List<TechnicalPlace> TechnicalPlaces { get; set; }
    public List<Equipment> Equipments { get; set; }
}