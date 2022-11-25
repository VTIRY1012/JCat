using JCat.BaseService.Model.ReqModel;
using JCatEndpointModel.RequestModel;
using JCatEndpointModel.ResponseModel;
using JCatService.Interface;

namespace JCatService
{
    public class ExampleService : IExampleService
    {
        public async Task<Example_RespModel> GetAsync()
        {
            // need to do: read database
            var result = new Example_RespModel()
            {
                Date = DateTime.UtcNow,
                Summary = "test",
                TemperatureC = 0,
                TemperatureF = 70
            };
            return await Task.FromResult(result);
        }

        public async Task<ExampleList_RespModel> GetListAsync(ExampleList_ReqModel param)
        {
            // need to do: read database
            var result = new ExampleList_RespModel()
            {
                Items = new List<Example_RespModel>()
                {
                    new Example_RespModel()
                    {
                        Id= Guid.NewGuid().ToString(),
                        Date = DateTime.UtcNow,
                        Summary = "test",
                        TemperatureC = 0,
                        TemperatureF = 70
                    }
                },
                TotalCount = 1
            };
            return await Task.FromResult(result);
        }

        public async Task<Example_RespModel> CreateAsync(ExampleModify_ReqModel param)
        {
            // need to do: read database
            param.Id = Guid.NewGuid().ToString();
            var result = new Example_RespModel()
            {
                Id = param.Id,
                Date = param.Date,
                Summary = param.Summary,
                TemperatureC = param.TemperatureC,
                TemperatureF = param.TemperatureF
            };
            return await Task.FromResult(result);
        }

        public async Task<Example_RespModel> UpdateAsync(ExampleModify_ReqModel param)
        {
            // need to do: read database
            var result = new Example_RespModel()
            {
                Id = param.Id,
                Date = param.Date,
                Summary = param.Summary,
                TemperatureC = param.TemperatureC,
                TemperatureF = param.TemperatureF
            };
            return await Task.FromResult(result);
        }

        public async Task<string> DeleteAsync(Id_ReqModel param)
        {
            // need to do: read database
            return await Task.FromResult(param.Id);
        }
    }
}
