using TestKovalska.Models;

namespace TestKovalska.Services.Abstract;

public interface IRestService
{
    Task<MobileData> GetMobileData(MobileSyncRequestModel requestBodyModel);
}