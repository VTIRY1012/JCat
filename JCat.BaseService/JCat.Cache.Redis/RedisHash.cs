using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace JCat.Cache.Redis;
public static class RedisHash
{
    public static string ParameterToKeyByMD5(object param)
    {
        var value = JsonSerializer.Serialize(param, RedisConfig.JsonSerializerOptions);
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }
        var bytes = ASCIIEncoding.ASCII.GetBytes(value);
        var result = new MD5CryptoServiceProvider().ComputeHash(bytes);
        return ByteArrayToString(result);
    }

    public static string ParameterToKeyBySHA256(object param, string salt = "")
    {
        var value = JsonSerializer.Serialize(param, RedisConfig.JsonSerializerOptions);
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        using var sha = new SHA256Managed();
        byte[] textBytes = Encoding.UTF8.GetBytes(value + salt);
        byte[] hashBytes = sha.ComputeHash(textBytes);
        string result = BitConverter
            .ToString(hashBytes)
            .Replace("-", string.Empty);

        return result;
    }

    private static string ByteArrayToString(byte[] arrInput)
    {
        int i;
        StringBuilder sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length - 1; i++)
        {
            sOutput.Append(arrInput[i].ToString("X2"));
        }
        return sOutput.ToString();
    }
}