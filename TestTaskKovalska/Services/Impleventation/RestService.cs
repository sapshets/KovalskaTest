using System.Globalization;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TestKovalska.Models;
using TestKovalska.Services.Abstract;

namespace TestKovalska.Services.Impleventation;

public class RestService : IRestService
{
    private HttpClient client;

    private void Init()
    {
        client = new HttpClient();
    }

    private void EnsureOrInit()
    {
        if (client == null)
            Init();
    }
    
    public async Task<MobileData> GetMobileData(MobileSyncRequestModel requestBodyModel)
    {
        EnsureOrInit();
        try
        {
            var possibleResult = await SendPostRequest<MobileDataSyncRootModel, MobileSyncRequestModel>(
                "https://it-ppd.kovalska.dev/PPD_TOR/ws/api/_MOBILEDATASYNC",
                requestBodyModel, HttpMethod.Post);
            return possibleResult != null ? possibleResult.Result : default;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default;
        }
    }

    private async Task<ResponseType> SendPostRequest<ResponseType, RequestType>(string endpoint,
        RequestType requestModel, HttpMethod httpMethod)
        where ResponseType : new()
    {
        return await BaseRequest<ResponseType, RequestType>(endpoint, requestModel, httpMethod);
    }
    
     private async Task<ResponseType> BaseRequest<ResponseType, RequestType>(string endpoint,
        RequestType requestModel, HttpMethod httpMethod)
        where ResponseType : new()
    {
        try
        {
            var json = JsonConvert.SerializeObject(requestModel);
            var request = new HttpRequestMessage
            {
                Method = httpMethod,

                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.RequestUri = new Uri(endpoint);

            var result = await SendRequest(request);
            var stringContent = await result.Content.ReadAsStringAsync();

            if (result.StatusCode != HttpStatusCode.OK)
                return default;
            
            var fullData = JsonConvert.DeserializeObject<ResponseType>(stringContent);
            return fullData;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            return default(ResponseType);
        }
    }
     
    public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
    {
        HttpResponseMessage res = new HttpResponseMessage();
        try
        {
            res = await client.SendAsync(request);
            return res;
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            return res;
        }
    }
}