using JCat.BaseService.Const;

namespace JCat.BaseService
{
    internal class JSystemResult
    {
        public string Type { get; set; } = StringConst.Empty;
        public string Title { get; set; } = StringConst.Empty;
        public int Status { get; set; }
        public string TraceId { get; set; } = StringConst.Empty;
        public object Errors { get; set; } = StringConst.Empty;
    }
}
