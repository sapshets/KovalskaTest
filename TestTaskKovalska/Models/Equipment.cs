namespace TestKovalska.Models;

public class Equipment
{
    public string Code { get; set; }
    public int TechPlaceCode { get; set; }
    public string TypeCode { get; set; }
    public string TypeName { get; set; }
    public string ModelCode { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Marking { get; set; }
    public string CriticalityLevel { get; set; }
    public DateTime MonitorDateStart { get; set; }
    public int MonitorFrequencyCode { get; set; }
    public string MonitorFrequency { get; set; }
    public object QrCode { get; set; }
    public List<MonitoringTask> MonitoringTasks { get; set; }
}