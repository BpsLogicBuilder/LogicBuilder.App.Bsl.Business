using LogicBuilder.App.Bsl.Business.Responses;
using System.Collections.Generic;
using System.Text.Json;

namespace LogicBuilder.App.Bsl.Business.Tests.Responses
{
    public class ErrorResponseTest
    {
        [Fact]
        public void CanSerializeAndDeserializeErrorResponse()
        {
            // Arrange
            var response = new ErrorResponse
            {
                Success = false,
                ErrorMessages = ["Error occurred", "Another error"]
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<ErrorResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.NotNull(deserializedResponse.ErrorMessages);
            Assert.Equal(2, deserializedResponse.ErrorMessages.Count);
            Assert.Contains("Error occurred", deserializedResponse.ErrorMessages);
            Assert.Contains("Another error", deserializedResponse.ErrorMessages);
        }

        [Fact]
        public void CanSerializeErrorResponseWithEmptyErrorMessages()
        {
            // Arrange
            var response = new ErrorResponse
            {
                Success = false,
                ErrorMessages = []
            };

            // Act
            string json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<ErrorResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.NotNull(deserializedResponse.ErrorMessages);
            Assert.Empty(deserializedResponse.ErrorMessages);
        }

        [Fact]
        public void CanDeserializeErrorResponseFromJson()
        {
            // Arrange
            string json = @"{
                ""Success"": false,
                ""ErrorMessages"": [""Invalid operation"", ""Database error""]
            }";

            // Act
            var deserializedResponse = JsonSerializer.Deserialize<ErrorResponse>(json);

            // Assert
            Assert.NotNull(deserializedResponse);
            Assert.False(deserializedResponse.Success);
            Assert.Equal(2, deserializedResponse.ErrorMessages.Count);
            Assert.Contains("Invalid operation", deserializedResponse.ErrorMessages);
            Assert.Contains("Database error", deserializedResponse.ErrorMessages);
        }
    }
}
