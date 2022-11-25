using JCat.BaseService.Model.ReqModel;
using JCatEndpointModel.RequestModel;
using JCatEndpointModel.ResponseModel;

namespace JCatService.Interface
{
    public interface IExampleService
    {
        Task<Example_RespModel> GetAsync();
        Task<ExampleList_RespModel> GetListAsync(ExampleList_ReqModel param);
        Task<Example_RespModel> CreateAsync(ExampleModify_ReqModel param);
        Task<Example_RespModel> UpdateAsync(ExampleModify_ReqModel param);
        Task<string> DeleteAsync(Id_ReqModel param);
    }
}
