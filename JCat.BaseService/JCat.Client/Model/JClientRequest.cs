using Microsoft.AspNetCore.Http;

namespace JCat.Client.Model;
public class JClientRequest
{
    public string AdditionalUrl { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, string> QueryString { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, object> FormValues { get; set; } = new Dictionary<string, object>();
    public IEnumerable<IFormFile> FileCollection { get; set; } = Enumerable.Empty<IFormFile>();
    public object Body { get; set; } = new object();
    public string MethodName { get; set; } = string.Empty;
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;
}