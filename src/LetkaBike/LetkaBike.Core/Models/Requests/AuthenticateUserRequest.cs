using LetkaBike.Core.Models.Responses;
using MediatR;

namespace LetkaBike.Core.Models.Requests
{
    public class AuthenticateUserRequest : IRequest<AuthenticateUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}