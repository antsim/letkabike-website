using System;
using System.Threading;
using System.Threading.Tasks;
using LetkaBike.Core.Data;
using LetkaBike.Core.Models.Items;
using LetkaBike.Core.Models.Requests;
using LetkaBike.Core.Models.Responses;
using LetkaBike.Core.UnitOfWork;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace LetkaBike.Core.Handlers.Users
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        private readonly IUnitOfWork _uow;

        public AuthenticateUserHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public Task<AuthenticateUserResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var response = new AuthenticateUserResponse();
            
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                response.ErrorMessage = "Missing arguments";
                return Task.FromResult(response);
            }

            var rider = _uow.RidersRepository.GetFirst(r => r.Email.Equals(request.Email));
            
            if (rider == null)
            {
                response.ErrorMessage = "Rider not found; Invalid email";
                return Task.FromResult(response);
            }
            
            var passwordHasher = new PasswordHasher<Rider>();
            var verifyResult = passwordHasher.VerifyHashedPassword(
                rider, 
                rider.PasswordHash, 
                request.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                response.ErrorMessage = "Incorrect password";
                return Task.FromResult(response);
            }

            response.Rider = rider.Adapt<RiderItem>();
            return Task.FromResult(response);
        }
    }
}