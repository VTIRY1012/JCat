using System.Text.Json;

namespace JCat.Client.Interface;
public interface IJClientSettings
{
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public string BaseUrl { get; set; }
    public string ApplicationKey { get; set; }
}