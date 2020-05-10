using LetkaBike.Core.Models.Items;

namespace LetkaBike.Core.Models.Responses
{
    public class AuthenticateUserResponse : ResponseBase
    {
        public RiderItem Rider { get; set; }
    }
}