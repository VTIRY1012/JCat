using JCat.BaseService;

namespace JCatEndpointModel.ResponseModel
{
    public class Example_RespModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; } = String.Empty;
    }

    public class ExampleList_RespModel : JListResult<Example_RespModel>
    {

    }
}
