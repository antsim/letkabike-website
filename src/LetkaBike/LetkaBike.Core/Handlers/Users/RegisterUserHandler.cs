using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LetkaBike.Core.Data;
using LetkaBike.Core.Models.Requests;
using LetkaBike.Core.Models.Responses;
using LetkaBike.Core.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LetkaBike.Core.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUnitOfWork _uow;

        public RegisterUserHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var response = new RegisterUserResponse();
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                response.ErrorMessage = "Password is required";
                return Task.FromResult(response);
            }

            if (_uow.RidersRepository.GetFirst(r => r.UserName.Equals(request.Username)) != null)
            {
                response.ErrorMessage = $"Username '{request.Username}' is already taken";
                return Task.FromResult(response);
            }

            var passwordHasher = new PasswordHasher<Rider>();

            var rider = new Rider();
            var hash = passwordHasher.HashPassword(rider, request.Password);

            rider.UserName = request.Username;
            rider.Email = request.Email;
            rider.PasswordHash = hash;

            _uow.RidersRepository.Create(rider);
            _uow.SaveChanges();

            response.Id = rider.Id;
            response.Username = rider.UserName;

            return Task.FromResult(response);
        }
    }
}