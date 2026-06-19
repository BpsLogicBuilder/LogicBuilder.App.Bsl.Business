using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.Domain;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Requests
{
    public class DeleteEntityRequestTest
    {
        [Fact]
        public void CanSerializeAndDeserializeDeleteEntityRequest()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var request = new DeleteEntityRequest
            {
                Entity = new TestModel { Id = 1, Name = "Test Entity" }
            };

            // Act
            string json = JsonSerializer.Serialize(request, options);
            var deserializedRequest = JsonSerializer.Deserialize<DeleteEntityRequest>(json, options);

            // Assert
            Assert.NotNull(deserializedRequest);
            Assert.NotNull(deserializedRequest.Entity);
            var entity = Assert.IsType<TestModel>(deserializedRequest.Entity);
            Assert.Equal(1, entity.Id);
            Assert.Equal("Test Entity", entity.Name);
        }

        [Fact]
        public void CanSerializeDeleteEntityRequestWithNullEntity()
        {
            // Arrange
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new LogicBuilder.Domain.Json.ModelConverter());

            var request = new DeleteEntityRequest
            {
                Entity = null
            };

            // Act
            string json = JsonSerializer.Serialize(request, options);
            var deserializedRequest = JsonSerializer.Deserialize<DeleteEntityRequest>(json, options);

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
