using JCat.Client;
using JCatBaseSDK.Interface;
using System.Text.Json;

namespace JCatBaseSDK.Model;
public class TestClientSettings : JClientSettings, ITestClientSettings
{
    public TestClientSettings() { }
    public TestClientSettings(string baseUrl, string applicationKey, JsonSerializerOptions serializerOptions)
    {
        this.BaseUrl = baseUrl;
        this.ApplicationKey = applicationKey;
        this.JsonSerializerOptions = serializerOptions;
    }
}