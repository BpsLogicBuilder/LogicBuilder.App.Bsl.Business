using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.Domain;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Responses
{
    public class GetEntityResponseTest
    {
        [Fact]
        public void CanSerializeAndDeserializeGetEntityResponse()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var response = new GetEntityResponse
            {
                Success = true,
                ErrorMessages = ["Warning1"],
                Entity = new TestModel { Id = 42, Name = "Test Entity" }
            };

            // Act
            string json = JsonSerializer.Serialize(response, options);
            var deserializedResponse = JsonSerializer.Deserialize<GetEntityResponse>(json, options);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.Single(deserializedResponse.ErrorMessages);
            Assert.NotNull(deserializedResponse.Entity);
            var entity = Assert.IsType<TestModel>(deserializedResponse.Entity);
            Assert.Equal(42, entity.Id);
            Assert.Equal("Test Entity", entity.Name);
        }

        [Fact]
        public void CanSerializeGetEntityResponseWithNullEntity()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var response = new GetEntityResponse
            {
                Success = false,
                ErrorMessages = ["Entity not found"],
                Entity = null
            };

            // Act
            string json = JsonSerializer.Serialize(response, options);
            var deserializedResponse = JsonSerializer.Deserialize<GetEntityResponse>(json, options);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.Single(deserializedResponse.ErrorMessages);
            Assert.Null(deserializedResponse.Entity);
        }

        private class TestModel : BaseModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
