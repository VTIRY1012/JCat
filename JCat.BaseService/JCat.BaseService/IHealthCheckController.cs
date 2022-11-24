namespace JCat.BaseService
{
    public interface IHealthCheckController
    {
        public Task<JResult> DataBaseVersion();
        public Task<JResult> RedisVersion();
    }
}
