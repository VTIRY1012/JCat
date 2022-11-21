namespace JCat.BaseService.Ruler
{
    public class IdRuler
    {
        public static string GetId() => Guid.NewGuid().ToString("N");

        public static string GetIdByModule(string moduleName)
        {
            return $"{moduleName}{GetId()}";
        }
    }
}
