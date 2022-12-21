using JCat.Client.Interface;
using System.Text.Json;

namespace JCat.Client;
public class JClientSettings : IJClientSettings
{
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions();
    public string BaseUrl { get; set; } = string.Empty;
    public string ApplicationKey { get; set; } = string.Empty;
}