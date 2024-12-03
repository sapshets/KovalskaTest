namespace TestKovalska.Models;

public class ValueRange
{
    public int Code { get; set; }
    public int MonitoringTaskCode { get; set; }
    public int ReferenceCode { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
    public int NormalValue { get; set; }
    public int ValueCode { get; set; }
    public string ValueName { get; set; }
    public string CategoryCode { get; set; }
    public string CategoryName { get; set; }
    public string Status { get; set; }
}