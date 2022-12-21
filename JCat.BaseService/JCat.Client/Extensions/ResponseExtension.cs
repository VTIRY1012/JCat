namespace JCat.Client.Extensions;
public static class ResponseExtension
{
    public static T IsFailReturnDefault<T>(this bool successStatus, T Data)
    {
        var fail = !successStatus;
        if (fail)
        {
            return default;
        }

        return Data;
    }
}