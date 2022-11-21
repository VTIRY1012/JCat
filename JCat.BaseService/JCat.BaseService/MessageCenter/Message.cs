namespace JCat.BaseService.MessageCenter
{
    // todo: Multilingual
    public static class Message
    {
        // Api Message
        public const string NotFound = "Not Found.";
        public const string ApiNotFound = "API Path NotFound";
        // Converter Message
        public const string JsonDateTimeConverterArgError = "Convert to [DateTime] error.";
        public const string JsonDateTimeOffSetConverterArgError = "Convert to [DateTimeOffSet] error.";
        public const string JsonDecimelConverterArgError = "Convert to [Decimel] error.";
        public const string JsonNotSupportDecimelTypeError = "Not support to convert decimal type.";
        // Exception
        public const string GetEnumNameError = " Can't map property";
        public const string GetEnvironmentError = "EnvironmentMode has not been [Initialize] or [ASPNETCORE_ENVIRONMENT] is not set.";
        public const string GetEnvironmentShortNameError = "Environment Name is not correct.";
        // Version
        public const string VersionReadError = "Can not read version.";
        public const string VersionRuleError = "Version is not match rule.";
        // JCat Message
        public const string JcatResultUsedError = "Please use [JResult] and set [Code].";
    }
}
