namespace BankApplicationAPI.Helpers
{
    public class IdHelper
    {
        public static string GenerateUniqueId()
        {
            DateTime now = DateTime.UtcNow;

            string uniqueId = now.ToString("yyyyMMddHHmmssfff");

            return uniqueId;
        }
    }
}
