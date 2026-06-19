using LogicBuilder.Domain;

namespace LogicBuilder.App.Bsl.Business.Requests
{
    public class DeleteEntityRequest : IBaseRequest
    {
        public BaseModel? Entity { get; set; }
    }
}
