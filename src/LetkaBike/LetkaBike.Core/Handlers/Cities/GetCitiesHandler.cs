using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LetkaBike.Core.Models.Items;
using LetkaBike.Core.Models.Requests;
using LetkaBike.Core.Models.Responses;
using LetkaBike.Core.UnitOfWork;
using Mapster;
using MediatR;

namespace LetkaBike.Core.Handlers.Cities
{
    public class GetCitiesHandler : IRequestHandler<GetCitiesRequest, GetCitiesResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetCitiesHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public Task<GetCitiesResponse> Handle(GetCitiesRequest request, CancellationToken cancellationToken)
        {
            var cities = _uow.CitiesRepository.GetAll().Adapt<IEnumerable<CityItem>>().ToList();
            return Task.FromResult(new GetCitiesResponse {Cities = cities});
        }
    }
}