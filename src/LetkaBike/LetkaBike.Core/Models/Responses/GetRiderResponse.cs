using LetkaBike.Core.Models.Items;

namespace LetkaBike.Core.Models.Responses
{
    public class GetRiderResponse : ResponseBase
    {
        public RiderItem Rider { get; set; }
    }
}