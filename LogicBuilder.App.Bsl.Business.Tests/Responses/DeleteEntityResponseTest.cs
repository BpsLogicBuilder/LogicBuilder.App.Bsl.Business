using LogicBuilder.App.Bsl.Business.Responses;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Responses
{
    public class DeleteEntityResponseTest
    {
        [Fact]
        public void CanSerializeAndDeserializeDeleteEntityResponse()
        {
            // Arrange
            var response = new DeleteEntityResponse
            {
                Success = true,
                ErrorMessages = ["Error1", "Error2"]
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<DeleteEntityResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.NotNull(deserializedResponse.ErrorMessages);
            Assert.Equal(2, deserializedResponse.ErrorMessages.Count);
            Assert.Contains("Error1", deserializedResponse.ErrorMessages);
            Assert.Contains("Error2", deserializedResponse.ErrorMessages);
        }

        [Fact]
        public void CanSerializeDeleteEntityResponseWithEmptyErrorMessages()
        {
            // Arrange
            var response = new DeleteEntityResponse
            {
                Success = false,
                ErrorMessages = []
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<DeleteEntityResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.NotNull(deserializedResponse.ErrorMessages);
            Assert.Empty(deserializedResponse.ErrorMessages);
        }

        [Fact]
        public void CanDeserializeDeleteEntityResponseFromJson()
        {
            // Arrange
            string json = @"{
                ""Success"": true,
                ""ErrorMessages"": [""Warning1""]
            }";

            // Act
            var deserializedResponse = JsonSerializer.Deserialize<DeleteEntityResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.True(deserializedResponse.Success);
            Assert.Single(deserializedResponse.ErrorMessages);
            Assert.Contains("Warning1", deserializedResponse.ErrorMessages);
        }
    }
}
