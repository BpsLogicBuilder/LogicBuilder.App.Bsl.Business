using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Requests
{
    public class GetObjectListRequestTest
    {
        [Fact]
        public void CanSerializeAndDeserializeGetObjectListRequestWithStringProperties()
        {
            // Arrange
            var request = new GetObjectListRequest
            {
                Selector = new SelectorLambdaDescriptor
                (
                    new ParameterDescriptor("q"),
                    typeof(string).AssemblyQualifiedName!,
                    "m"
                ),
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor(["Id"], [new SelectExpandItemDescriptor("Name")]),
                ModelType = "ObjectModel",
                DataType = "ObjectData"
            };

            // Act
            string json = JsonSerializer.Serialize(request);
            var deserializedRequest = JsonSerializer.Deserialize<GetObjectListRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.NotNull(deserializedRequest.Selector);
            Assert.NotNull(deserializedRequest.SelectExpandDefinition);
            Assert.Equal("ObjectModel", deserializedRequest.ModelType);
            Assert.Equal("ObjectData", deserializedRequest.DataType);
        }

        [Fact]
        public void CanSerializeGetObjectListRequestWithNullProperties()
        {
            // Arrange
            var request = new GetObjectListRequest
            {
                Selector = null,
                SelectExpandDefinition = null,
                ModelType = null,
                DataType = null
            };

            // Act
            string json = JsonSerializer.Serialize(request);
            var deserializedRequest = JsonSerializer.Deserialize<GetObjectListRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Null(deserializedRequest.Selector);
            Assert.Null(deserializedRequest.SelectExpandDefinition);
            Assert.Null(deserializedRequest.ModelType);
            Assert.Null(deserializedRequest.DataType);
        }

        [Fact]
        public void CanDeserializeGetObjectListRequestFromJson()
        {
            // Arrange
            string json = @"{
                ""ModelType"": ""Product"",
                ""DataType"": ""ProductData"",
                ""Selector"": null,
                ""SelectExpandDefinition"": null
            }";

            // Act
            var deserializedRequest = JsonSerializer.Deserialize<GetObjectListRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Equal("Product", deserializedRequest.ModelType);
            Assert.Equal("ProductData", deserializedRequest.DataType);
            Assert.Null(deserializedRequest.Selector);
            Assert.Null(deserializedRequest.SelectExpandDefinition);
        }
    }
}
