using System.ComponentModel;

namespace JCat.BaseService
{
    public sealed class JHeader
    {
        /// <summary>
        /// API Pass Unit Id
        /// </summary>
        [DisplayName("D-EventId")]
        public string D_EventId { get; set; } = string.Empty;

        /// <summary>
        /// API Version Check
        /// </summary>
        [DisplayName("D-Version")]
        public string D_Version { get; set; } = string.Empty;

        /// <summary>
        /// Service Cache Key
        /// </summary>
        [DisplayName("D-CacheKey")]
        public string D_CacheKey { get; set; } = string.Empty;

        /// <summary>
        /// API use cache or not
        /// </summary>
        [DisplayName("D-IsFromCache")]
        public bool D_IsFromCache { get; set; } = false;

        /// <summary>
        /// Call API time
        /// </summary>
        [DisplayName("D-ts")]
        public DateTime D_ts { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Enter API
        /// </summary>
        [DisplayName("D-ReqTime")]
        public DateTime D_ReqTime { get; set; }

        /// <summary>
        /// Leave API
        /// </summary>
        [DisplayName("D-RespTime")]
        public DateTime D_RespTime { get; set; }

        /// <summary>
        /// Check Service to Service
        /// </summary>
        [DisplayName("D-IsFromApplication")]
        public bool D_IsFromApplication { get; set; } = false;

        /// <summary>
        /// Check Is File
        /// </summary>
        [DisplayName("D-IsFile")]
        public bool D_IsFile { get; set; }

        /// <summary>
        /// Application key
        /// </summary>
        [DisplayName("ApplicationKey")]
        public string ApplicationKey { get; set; } = string.Empty;
    }
}
