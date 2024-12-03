namespace TestKovalska.Models;

public class MobileData
{
    public DateTime Date { get; set; }
    public string PersonnelNumber { get; set; }
    public string FactoryCode { get; set; }
    public string FactoryName { get; set; }
    public List<TechnicalPlace> TechnicalPlaces { get; set; }
}