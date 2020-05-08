using System.Threading;
using System.Threading.Tasks;
using LetkaBike.Core.Models.Requests;
using LetkaBike.Core.Models.Responses;
using MediatR;

namespace LetkaBike.Core.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        public Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new RegisterUserResponse());
        }
    }
}