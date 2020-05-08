using LetkaBike.Core.Models.Responses;
using MediatR;

namespace LetkaBike.Core.Models.Requests
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>
    {
    }
}