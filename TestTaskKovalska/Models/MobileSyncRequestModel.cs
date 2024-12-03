namespace TestKovalska.Models;

public class MobileSyncRequestModel
{
    public string Jsonrpc { get; set; }
    public string Method { get; set; }
    public string Id { get; set; }
    public Params Params { get; set; }
}