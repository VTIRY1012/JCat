namespace JCat.BaseService
{
    public interface IHealthCheckController
    {
        public Task<JResult> HealthCheck();
        public Task<JResult> DataBaseVersion();
        public Task<JResult> RedisVersion();
    }
}
