using LogicBuilder.Domain;

namespace LogicBuilder.App.Bsl.Business.Responses
{
    public class GetEntityResponse : BaseResponse
    {
        public BaseModel? Entity { get; set; }
    }
}
