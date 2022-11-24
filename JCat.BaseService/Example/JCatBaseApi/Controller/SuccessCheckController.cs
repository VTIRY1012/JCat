using JCat.BaseService;
using Microsoft.AspNetCore.Mvc;

namespace JCatBaseApi.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SuccessCheckController : BaseServiceController
    {
        [HttpGet]
        public async Task<JResult> Number()
        {
            var result = await Task.FromResult(1);
            return Successed(result);
        }

        [HttpGet]
        public async Task<JResult> Boolean()
        {
            var result = await Task.FromResult(true);
            return Successed(result);
        }

        [HttpGet]
        public async Task<JResult> String()
        {
            var str = "{\n  \"test\":\"123\",\n  \"bool\":true,\n  \"num\":123\n}";
            var result = await Task.FromResult(str);
            return Successed(result);
        }

        [HttpGet]
        public async Task<JResult> Object()
        {
            var obj = new
            {
                testString = "123",
                testBool = true,
                testNum = 123,
                testDatetime = DateTime.Now,
                testDateOffSet = DateTimeOffset.Now
            };
            var result = await Task.FromResult(obj);
            return Successed(result);
        }

        [HttpGet]
        public async Task<JResult> Model()
        {
            var result = await Task.FromResult(new SuccessTestModel());
            return Successed(result);
        }
    }

    public class SuccessTestModel
    {
        public string TestString { get; set; } = "123";
        public int TestNum { get; set; } = 1;
        public DateTime TestDateTime { get; set; } = DateTime.Now;
    }
}
