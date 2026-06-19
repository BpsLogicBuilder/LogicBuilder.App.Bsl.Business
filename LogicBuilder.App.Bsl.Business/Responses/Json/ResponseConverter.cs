using LogicBuilder.Domain.Json;

namespace LogicBuilder.App.Bsl.Business.Responses.Json
{
    public class ResponseConverter : JsonTypeConverter<BaseResponse>
    {
        public override string TypePropertyName => nameof(BaseResponse.TypeString);
    }
}
