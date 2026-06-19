using LogicBuilder.Domain;

namespace LogicBuilder.App.Bsl.Business.Requests
{
    public class SaveEntityRequest : IBaseRequest
    {
        public BaseModel? Entity { get; set; }
    }
}
