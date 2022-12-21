namespace JCatBaseSDK.Model;
public class TestClientOptions
{
    public TestAppKeyEncoder Encoder { get; set; } = new TestAppKeyEncoder();
    public TestClientSettings Settings { get; set; } = new TestClientSettings();
}