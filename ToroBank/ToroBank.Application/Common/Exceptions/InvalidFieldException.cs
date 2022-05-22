namespace ToroBank.Application.Common.Exceptions
{
    public class InvalidFieldExcetion : ApplicationException
    {
        public InvalidFieldExcetion()
            : base()
        {
        }

        public InvalidFieldExcetion(string message)
            : base(message)
        {
        }

        public InvalidFieldExcetion(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidFieldExcetion(string name, object key)
            : base($"Entity \"{name}\" ({key}) was invalid.")
        {
        }
    }

}
