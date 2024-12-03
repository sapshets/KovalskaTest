namespace TestKovalska.Models;

public class MobileDataSyncRootModel
{
    public string Id { get; set; }
    public MobileData Result { get; set; }
    public object Error { get; set; }
}