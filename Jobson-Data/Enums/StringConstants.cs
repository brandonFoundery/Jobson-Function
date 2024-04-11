namespace Jobson_Data.Enums
{
    public class StringConstants
    {
        public const string UpworkApiKey = "UpworkApiKey";

        public static string GetTrelloBoardName(string name)
        {
            return $"Upwork: {name}";
        }
    }
}
