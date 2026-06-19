using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.Domain;
using System.Collections.Generic;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Responses
{
    public class SaveEntityResponseTest
    {
        [Fact]
        public void CanSerializeAndDeserializeSaveEntityResponse()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var response = new SaveEntityResponse
            {
                Success = true,
                ErrorMessages = [],
                Entity = new TestModel { Id = 123, Name = "Saved Entity" }
            };

            // Act
            string json = JsonSerializer.Serialize(response, options);
            var deserializedResponse = JsonSerializer.Deserialize<SaveEntityResponse>(json, options);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.Empty(deserializedResponse.ErrorMessages);
            Assert.NotNull(deserializedResponse.Entity);
            var entity = Assert.IsType<TestModel>(deserializedResponse.Entity);
            Assert.Equal(123, entity.Id);
            Assert.Equal("Saved Entity", entity.Name);
        }

        [Fact]
        public void CanSerializeSaveEntityResponseWithNullEntity()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var response = new SaveEntityResponse
            {
                Success = false,
                ErrorMessages = ["Save failed", "Validation error"],
                Entity = null
            };

            // Act
            string json = JsonSerializer.Serialize(response, options);
            var deserializedResponse = JsonSerializer.Deserialize<SaveEntityResponse>(json, options);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.Equal(2, deserializedResponse.ErrorMessages.Count);
            Assert.Null(deserializedResponse.Entity);
        }

        private class TestModel : BaseModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
