using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Responses
{
    public class GetListResponseTest
    {
        [Fact]
        public void CanSerializeAndDeserializeGetListResponse()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var response = new GetListResponse
            {
                Success = true,
                ErrorMessages = [],
                List =
                [
                    new TestModel { Id = 1, Name = "Item 1" },
                    new TestModel { Id = 2, Name = "Item 2" },
                    new TestModel { Id = 3, Name = "Item 3" }
                ]
            };

            // Act
            string json = JsonSerializer.Serialize(response, options);
            var deserializedResponse = JsonSerializer.Deserialize<GetListResponse>(json, options);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.Empty(deserializedResponse.ErrorMessages);
            Assert.NotNull(deserializedResponse.List);
            var items = deserializedResponse.List.ToList();
            Assert.Equal(3, items.Count);
            var firstItem = Assert.IsType<TestModel>(items[0]);
            Assert.Equal(1, firstItem.Id);
            Assert.Equal("Item 1", firstItem.Name);
        }

        [Fact]
        public void CanSerializeGetListResponseWithEmptyList()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var response = new GetListResponse
            {
                Success = true,
                ErrorMessages = [],
                List = []
            };

            // Act
            string json = JsonSerializer.Serialize(response, options);
            var deserializedResponse = JsonSerializer.Deserialize<GetListResponse>(json, options);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.NotNull(deserializedResponse.List);
            Assert.Empty(deserializedResponse.List);
        }

        private class TestModel : BaseModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
