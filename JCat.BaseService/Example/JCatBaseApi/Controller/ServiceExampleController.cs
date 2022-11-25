using JCat.BaseService;
using JCat.BaseService.Model.ReqModel;
using JCatEndpointModel.RequestModel;
using JCatService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JCatBaseApi.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiceExampleController : BaseServiceController
    {
        private readonly IExampleService _exampleService;
        public ServiceExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet]
        public async Task<JResult> Get()
        {
            var result = await _exampleService.GetAsync();
            return Successed(result);
        }

        [HttpGet]
        public async Task<JResult> GetList([FromQuery] ExampleList_ReqModel model)
        {
            var result = await _exampleService.GetListAsync(model);
            return Successed(result);
        }

        [HttpPost]
        public async Task<JResult> Create(ExampleModify_ReqModel model)
        {
            var result = await _exampleService.CreateAsync(model);
            return SuccessedCreate(result);
        }

        [HttpPut("{id}")]
        public async Task<JResult> Update([FromRoute] string id, ExampleModify_ReqModel model)
        {
            model.Id = id;
            var result = await _exampleService.UpdateAsync(model);
            return Successed(result);
        }

        [HttpDelete]
        public async Task<JResult> Delete(Id_ReqModel model)
        {
            var result = await _exampleService.DeleteAsync(model);
            return Successed(result);
        }
    }
}
