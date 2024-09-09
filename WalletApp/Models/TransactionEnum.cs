namespace WalletProject.Models
{
    public class TransactionEnum
    {
        public enum Categories
        {
            None = 0,
            Transport = 1,
            Food,
            Other
        }
        public enum ModeOfPayment
        {
            None = 0,
            Mpesa = 1,
            Bank,
            Cash
        }
    }
}
