using JCat.BaseService;
using JCat.BaseService.Model.ReqModel;
using JCatBaseSDK.Interface;
using JCatEndpointModel.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace JCatBaseApi.Controller;

[ApiController]
[Route("[controller]/[action]")]
public class ClientExampleController : BaseServiceController
{
    private readonly ITestClient _testClient;
    public ClientExampleController(ITestClient testClient)
    {
        _testClient = testClient;
    }

    [HttpGet]
    public async Task<JResult> Get()
    {
        var result = await _testClient.GetAsync();
        return Successed(result);
    }


    [HttpGet]
    public async Task<JResult> GetList([FromQuery] ExampleList_ReqModel model)
    {
        var result = await _testClient.GetListAsync(model);
        return Successed(result);
    }

    [HttpPost]
    public async Task<JResult> Create(ExampleModify_ReqModel model)
    {
        var result = await _testClient.CreateAsync(model);
        return SuccessedCreate(result);
    }

    [HttpPut("{id}")]
    public async Task<JResult> Update([FromRoute] string id, ExampleModify_ReqModel model)
    {
        model.Id = id;
        var result = await _testClient.UpdateAsync(model);
        return Successed(result);
    }

    [HttpDelete]
    public async Task<JResult> Delete(Id_ReqModel model)
    {
        var result = await _testClient.DeleteAsync(model);
        return Successed(result);
    }
}