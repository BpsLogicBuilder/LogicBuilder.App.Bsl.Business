using System.Collections.Generic;

namespace LogicBuilder.App.Bsl.Business.Responses
{
    public class GetObjectListResponse : BaseResponse
    {
        public IEnumerable<object> List { get; set; } = [];
    }
}
