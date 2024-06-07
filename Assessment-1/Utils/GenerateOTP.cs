namespace Assessment_1.Utils
{
    public class GenerateOTP
    {
        public static string GenOTP()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString();
        }
    }
}
