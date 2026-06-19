using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.Domain;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Requests
{
    public class SaveEntityRequestTest
    {
        [Fact]
        public void CanSerializeAndDeserializeSaveEntityRequest()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var request = new SaveEntityRequest
            {
                Entity = new TestModel { Id = 42, Name = "Test Save Entity" }
            };

            // Act
            string json = JsonSerializer.Serialize(request, options);
            var deserializedRequest = JsonSerializer.Deserialize<SaveEntityRequest>(json, options);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.NotNull(deserializedRequest.Entity);
            var entity = Assert.IsType<TestModel>(deserializedRequest.Entity);
            Assert.Equal(42, entity.Id);
            Assert.Equal("Test Save Entity", entity.Name);
        }

        [Fact]
        public void CanSerializeSaveEntityRequestWithNullEntity()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var request = new SaveEntityRequest
            {
                Entity = null
            };

            // Act
            string json = JsonSerializer.Serialize(request, options);
            var deserializedRequest = JsonSerializer.Deserialize<SaveEntityRequest>(json, options);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.Null(deserializedRequest.Entity);
        }

        private class TestModel : BaseModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
