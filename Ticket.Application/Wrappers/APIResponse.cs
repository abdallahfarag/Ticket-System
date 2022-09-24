namespace Ticket.Application.Wrappers
{
    public class APIResponse<T> where T : class
    {
        public string ErrorMessage { get; set; }

        public T Result { get; set; }

        public APIResponse(T result = null)
        {
            Result = result;
        }
        public APIResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
