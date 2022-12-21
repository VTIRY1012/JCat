using JCat.Client.Interface;

namespace JCat.Client;
public abstract class JAppKeyEncoderAbstract : IAppKeyEncoder
{
    public abstract string EncodeApplicationKey(string applicationKey);
}