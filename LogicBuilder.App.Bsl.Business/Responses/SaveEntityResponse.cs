using LogicBuilder.Attributes;
using LogicBuilder.Domain;

namespace LogicBuilder.App.Bsl.Business.Responses
{
    public class SaveEntityResponse : BaseResponse
    {
        [AlsoKnownAs("SaveEntityResponse_Entity")]
        public BaseModel? Entity { get; set; }
    }
}
