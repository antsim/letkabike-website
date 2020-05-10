using System.Threading;
using System.Threading.Tasks;
using LetkaBike.Core.Models.Items;
using LetkaBike.Core.Models.Requests;
using LetkaBike.Core.Models.Responses;
using LetkaBike.Core.UnitOfWork;
using Mapster;
using MediatR;

namespace LetkaBike.Core.Handlers.Users
{
    public class GetRiderHandler : IRequestHandler<GetRiderRequest, GetRiderResponse>
    {
        private readonly IUnitOfWork _uow;
        
        public GetRiderHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public Task<GetRiderResponse> Handle(GetRiderRequest request, CancellationToken cancellationToken)
        {
            var response = new GetRiderResponse();
            var rider = _uow.RidersRepository.Find(r => r.Email.Equals(request.Email));

            if (rider == null)
            {
                response.ErrorMessage = "Rider not found";
                return Task.FromResult(response);
            }
            
            response.Rider = rider.Adapt<RiderItem>();
            return Task.FromResult(response);
        }
    }
}