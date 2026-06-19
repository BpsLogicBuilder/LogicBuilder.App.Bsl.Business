using LogicBuilder.App.Bsl.Business.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Responses
{
    public class GetObjectListResponseTest
    {
        [Fact]
        public void CanSerializeAndDeserializeGetObjectListResponse()
        {
            // Arrange
            var response = new GetObjectListResponse
            {
                Success = true,
                ErrorMessages = [],
                List =
                [
                    new { Id = 1, Name = "Object 1" },
                    new { Id = 2, Name = "Object 2" },
                    new { Id = 3, Name = "Object 3" }
                ]
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<GetObjectListResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.Empty(deserializedResponse.ErrorMessages);
            Assert.NotNull(deserializedResponse.List);
            var items = deserializedResponse.List.ToList();
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void CanSerializeGetObjectListResponseWithEmptyList()
        {
            // Arrange
            var response = new GetObjectListResponse
            {
                Success = false,
                ErrorMessages = ["No items found"],
                List = []
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<GetObjectListResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.Single(deserializedResponse.ErrorMessages);
            Assert.NotNull(deserializedResponse.List);
            Assert.Empty(deserializedResponse.List);
        }

        [Fact]
        public void CanDeserializeGetObjectListResponseFromJson()
        {
            // Arrange
            string json = @"{
                ""Success"": true,
                ""ErrorMessages"": [],
                ""List"": [
                    {""Id"": 100, ""Value"": ""First""},
                    {""Id"": 200, ""Value"": ""Second""}
                ]
            }";

            // Act
            var deserializedResponse = JsonSerializer.Deserialize<GetObjectListResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.Empty(deserializedResponse.ErrorMessages);
            Assert.NotNull(deserializedResponse.List);
            var items = deserializedResponse.List.ToList();
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void CanSerializeGetObjectListResponseWithMixedTypes()
        {
            // Arrange
            var response = new GetObjectListResponse
            {
                Success = true,
                ErrorMessages = [],
                List =
                [
                    "String item",
                    42,
                    true,
                    new { Key = "Value" }
                ]
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<GetObjectListResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.NotNull(deserializedResponse.List);
            var items = deserializedResponse.List.ToList();
            Assert.Equal(4, items.Count);
        }
    }
}
