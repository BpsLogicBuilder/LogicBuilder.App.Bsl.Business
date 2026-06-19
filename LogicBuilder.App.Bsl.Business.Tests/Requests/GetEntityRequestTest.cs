using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Requests
{
    public class GetEntityRequestTest
    {
        [Fact]
        public void CanSerializeAndDeserializeGetEntityRequestWithStringProperties()
        {
            // Arrange
            var request = new GetEntityRequest
            {
                Filter = new FilterLambdaDescriptor
                (
                    new EqualsBinaryDescriptor(new ConstantDescriptor(1), new ConstantDescriptor(1)),
                    typeof(string).AssemblyQualifiedName!,
                    "f"
                ),
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor(["Id"], [new SelectExpandItemDescriptor("Name")]),
                ModelType = "TestModel",
                DataType = "TestData"
            };

            // Act
            string json = JsonSerializer.Serialize(request);
            var deserializedRequest = JsonSerializer.Deserialize<GetEntityRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.NotNull(deserializedRequest.Filter);
            Assert.NotNull(deserializedRequest.SelectExpandDefinition);
            Assert.Equal("TestModel", deserializedRequest.ModelType);
            Assert.Equal("TestData", deserializedRequest.DataType);
        }

        [Fact]
        public void CanSerializeGetEntityRequestWithNullProperties()
        {
            // Arrange
            var request = new GetEntityRequest
            {
                Filter = null,
                SelectExpandDefinition = null,
                ModelType = null,
                DataType = null
            };

            // Act
            string json = JsonSerializer.Serialize(request);
            var deserializedRequest = JsonSerializer.Deserialize<GetEntityRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Null(deserializedRequest.Filter);
            Assert.Null(deserializedRequest.SelectExpandDefinition);
            Assert.Null(deserializedRequest.ModelType);
            Assert.Null(deserializedRequest.DataType);
        }

        [Fact]
        public void CanDeserializeGetEntityRequestFromJson()
        {
            // Arrange
            string json = @"{
                ""ModelType"": ""Customer"",
                ""DataType"": ""CustomerData"",
                ""Filter"": null,
                ""SelectExpandDefinition"": null
            }";

            // Act
            var deserializedRequest = JsonSerializer.Deserialize<GetEntityRequest>(json);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Equal("Customer", deserializedRequest.ModelType);
            Assert.Equal("CustomerData", deserializedRequest.DataType);
            Assert.Null(deserializedRequest.Filter);
            Assert.Null(deserializedRequest.SelectExpandDefinition);
        }
    }
}
