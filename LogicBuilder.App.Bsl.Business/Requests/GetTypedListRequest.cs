using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;

namespace LogicBuilder.App.Bsl.Business.Requests
{
    public class GetTypedListRequest : IBaseRequest
    {
        public SelectorLambdaDescriptor? Selector { get; set; }
        public SelectExpandDefinitionDescriptor? SelectExpandDefinition { get; set; }
        public string? ModelType { get; set; }
        public string? DataType { get; set; }
        public string? ModelReturnType { get; set; }
        public string? DataReturnType { get; set; }
    }
}
