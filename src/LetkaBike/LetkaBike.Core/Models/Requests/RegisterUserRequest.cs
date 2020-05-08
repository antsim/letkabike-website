using LetkaBike.Core.Models.Responses;
using MediatR;

namespace LetkaBike.Core.Models.Requests
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}