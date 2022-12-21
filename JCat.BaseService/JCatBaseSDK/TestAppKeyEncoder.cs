using JCat.Client;
using JCatBaseSDK.Interface;

namespace JCatBaseSDK;
public class TestAppKeyEncoder : JAppKeyEncoderAbstract, ITestAppKeyEncoder
{
    public override string EncodeApplicationKey(string applicationKey)
    {
        // todo: encoder.
        return applicationKey;
    }
}