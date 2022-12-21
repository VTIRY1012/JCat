using JCat.BaseService.Extensions.Service;
using JCat.BaseService.Model.ReqModel;
using JCat.Client;
using JCat.Client.Extensions;
using JCat.Client.Model;
using JCatBaseSDK.Interface;
using JCatEndpointModel.RequestModel;
using JCatEndpointModel.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace JCatBaseSDK;
public class JCatTestClient : JClientAbstract, ITestClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHeaderDictionary _headers;
    public JCatTestClient(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        ITestAppKeyEncoder encoder,
        ITestClientSettings settings) :
        base(httpClientFactory, encoder, settings)
    {
        _httpContextAccessor = httpContextAccessor;
        _headers = _httpContextAccessor.HttpContext?.Request?.Headers;
    }

    private const string _controllerName = "ServiceExample";

    public async Task<Example_RespModel> GetAsync()
    {
        var result = await GetAsync<Example_RespModel>(new JClientRequest()
        {
            MethodName = $"{_controllerName}/Get",
            Headers = _headers.AsDictionary()
        });

        return result.IsSuccessStatusCode.IsFailReturnDefault(result.Data);
    }

    public async Task<ExampleList_RespModel> GetListAsync(ExampleList_ReqModel param)
    {
        var result = await GetAsync<ExampleList_RespModel>(new JClientRequest()
        {
            MethodName = $"{_controllerName}/GetList",
            Headers = _headers.AsDictionary(),
            QueryString = new Dictionary<string, string>()
                {
                    { nameof(ExampleList_ReqModel.Limit), param.Limit.ToString() },
                    { nameof(ExampleList_ReqModel.Offset), param.Offset.ToString() }
                }
        });

        return result.IsSuccessStatusCode.IsFailReturnDefault(result.Data);
    }

    public async Task<Example_RespModel> CreateAsync(ExampleModify_ReqModel param)
    {
        var result = await PostAsync<Example_RespModel>(new JClientRequest()
        {
            MethodName = $"{_controllerName}/Create",
            Headers = _headers.AsDictionary(),
            Body = param
        });

        return result.IsSuccessStatusCode.IsFailReturnDefault(result.Data);
    }

    public async Task<Example_RespModel> UpdateAsync(ExampleModify_ReqModel param)
    {
        var result = await PutAsync<Example_RespModel>(new JClientRequest()
        {
            MethodName = $"{_controllerName}/Update/{param.Id}",
            Headers = _headers.AsDictionary(),
            Body = param
        });

        return result.IsSuccessStatusCode.IsFailReturnDefault(result.Data);
    }

    public async Task<string> DeleteAsync(Id_ReqModel param)
    {
        var result = await DeleteAsync<string>(new JClientRequest()
        {
            MethodName = $"{_controllerName}/Delete",
            Headers = _headers.AsDictionary(),
            Body = param
        });

        return result.IsSuccessStatusCode.IsFailReturnDefault(result.Data);
    }
}