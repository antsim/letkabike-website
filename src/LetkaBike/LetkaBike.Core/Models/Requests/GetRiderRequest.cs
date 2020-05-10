using LetkaBike.Core.Models.Items;
using LetkaBike.Core.Models.Responses;
using MediatR;

namespace LetkaBike.Core.Models.Requests
{
    public class GetRiderRequest : IRequest<GetRiderResponse>
    {
        public string Email { get; set; }
    }
}