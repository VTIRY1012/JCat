using JCat.BaseService.Model.ReqModel;
using JCat.Client;
using JCatEndpointModel.RequestModel;
using JCatEndpointModel.ResponseModel;

namespace JCatBaseSDK.Interface;
public interface ITestClient : IJClient
{
    Task<Example_RespModel> GetAsync();
    Task<ExampleList_RespModel> GetListAsync(ExampleList_ReqModel param);
    Task<Example_RespModel> CreateAsync(ExampleModify_ReqModel param);
    Task<Example_RespModel> UpdateAsync(ExampleModify_ReqModel param);
    Task<string> DeleteAsync(Id_ReqModel param);
}