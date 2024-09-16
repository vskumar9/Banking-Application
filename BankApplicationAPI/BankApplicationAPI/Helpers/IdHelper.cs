namespace BankApplicationAPI.Helpers
{
    public class IdHelper
    {
        private static string GenerateUniqueId()
        {
            DateTime now = DateTime.UtcNow;

            string uniqueId = now.ToString("yyyyMMddHHmmssfff");

            return uniqueId;
        }

        public string GenerateEmployeeUniqueId()
        {
            return "E"+GenerateUniqueId();
        }

        public string GenerateCustomerUniqueId()
        {
            return "C" + GenerateUniqueId();
        }
    }
}
