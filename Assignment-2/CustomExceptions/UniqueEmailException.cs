namespace Assignment_2.CustomExceptions
{
    public class UniqueEmailException:GlobalException
    {
        public UniqueEmailException(string message):base(message) {          
        }
    }
}
