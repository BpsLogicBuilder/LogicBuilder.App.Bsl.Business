using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Requests
{
    public class GetTypedListRequestTest
    {
        [Fact]
        public void CanSerializeAndDeserializeGetTypedListRequestWithAllStringProperties()
        {
            // Arrange
            var request = new GetTypedListRequest
            {
                Selector = new SelectorLambdaDescriptor
                (
                    new ParameterDescriptor("q"),
                    typeof(string).AssemblyQualifiedName!,
                    "m"
                ),
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor(["Id"], [new SelectExpandItemDescriptor("Name")]),
                ModelType = "TypedModel",
                DataType = "TypedData",
                ModelReturnType = "ReturnModel",
                DataReturnType = "ReturnData"
            };

            // Act
            string json = JsonSerializer.Serialize(request);
            var deserializedRequest = JsonSerializer.Deserialize<GetTypedListRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.NotNull(deserializedRequest.Selector);
            Assert.NotNull(deserializedRequest.SelectExpandDefinition);
            Assert.Equal("TypedModel", deserializedRequest.ModelType);
            Assert.Equal("TypedData", deserializedRequest.DataType);
            Assert.Equal("ReturnModel", deserializedRequest.ModelReturnType);
            Assert.Equal("ReturnData", deserializedRequest.DataReturnType);
        }

        [Fact]
        public void CanSerializeGetTypedListRequestWithNullProperties()
        {
            // Arrange
            var request = new GetTypedListRequest
            {
                Selector = null,
                SelectExpandDefinition = null,
                ModelType = null,
                DataType = null,
                ModelReturnType = null,
                DataReturnType = null
            };

            // Act
            string json = JsonSerializer.Serialize(request);
            var deserializedRequest = JsonSerializer.Deserialize<GetTypedListRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Null(deserializedRequest.Selector);
            Assert.Null(deserializedRequest.SelectExpandDefinition);
            Assert.Null(deserializedRequest.ModelType);
            Assert.Null(deserializedRequest.DataType);
            Assert.Null(deserializedRequest.ModelReturnType);
            Assert.Null(deserializedRequest.DataReturnType);
        }

        [Fact]
        public void CanDeserializeGetTypedListRequestFromJson()
        {
            // Arrange
            string json = @"{
                ""ModelType"": ""Order"",
                ""DataType"": ""OrderData"",
                ""ModelReturnType"": ""OrderList"",
                ""DataReturnType"": ""OrderDataList"",
                ""Selector"": null,
                ""SelectExpandDefinition"": null
            }";

            // Act
            var deserializedRequest = JsonSerializer.Deserialize<GetTypedListRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Equal("Order", deserializedRequest.ModelType);
            Assert.Equal("OrderData", deserializedRequest.DataType);
            Assert.Equal("OrderList", deserializedRequest.ModelReturnType);
            Assert.Equal("OrderDataList", deserializedRequest.DataReturnType);
            Assert.Null(deserializedRequest.Selector);
            Assert.Null(deserializedRequest.SelectExpandDefinition);
        }
    }
}
