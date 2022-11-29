using JCat.BaseService.Model.Base;

namespace JCatEndpointModel.RequestModel
{
    public class ExampleList_ReqModel : PageModel
    {
    }

    public class ExampleModify_ReqModel
    {
        /// <summary>
        /// only for update.
        /// </summary>
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; set; } = String.Empty;
    }
}
