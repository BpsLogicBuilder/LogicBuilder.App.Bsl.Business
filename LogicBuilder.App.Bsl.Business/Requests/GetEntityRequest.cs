using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;

namespace LogicBuilder.App.Bsl.Business.Requests
{
    public class GetEntityRequest : IBaseRequest
    {
        public FilterLambdaDescriptor? Filter { get; set; }
        public SelectExpandDefinitionDescriptor? SelectExpandDefinition { get; set; }
        public string? ModelType { get; set; }
        public string? DataType { get; set; }
    }
}
