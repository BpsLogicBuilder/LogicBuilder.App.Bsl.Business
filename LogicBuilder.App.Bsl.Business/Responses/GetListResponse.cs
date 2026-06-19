using LogicBuilder.Domain;
using System.Collections.Generic;

namespace LogicBuilder.App.Bsl.Business.Responses
{
    public class GetListResponse : BaseResponse
    {
        public IEnumerable<BaseModel> List { get; set; } = [];
    }
}
