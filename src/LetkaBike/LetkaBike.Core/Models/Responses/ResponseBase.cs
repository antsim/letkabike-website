namespace LetkaBike.Core.Models.Responses
{
    public class ResponseBase
    {
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}