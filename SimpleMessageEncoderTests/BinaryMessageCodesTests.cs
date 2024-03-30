using SimpleMessageEncorder.Codes;
using SimpleMessageEncorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMessageEncoder
{
    public class BinaryMessageCodesTests
    {
        [Fact]
        public void Encode_Decode_Success()
        {
            // Arrange
            var codec = new BinaryMessageCodes();
            var expectedMessage = new Message
            {
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "ASCII/Byte" }
                },
                Payload = Encoding.ASCII.GetBytes("{\"message\":\"Simple Binary Message Encode Scheme\"}")
            };

            // Act
            var encodedMessage = codec.Encode(expectedMessage);
            var decodedMessage = codec.Decode(encodedMessage);

            // Assert
            Assert.NotNull(decodedMessage);
            Assert.Equal(expectedMessage.Headers.Count, decodedMessage.Headers.Count);
            Assert.Equal(expectedMessage.Headers["Content-Type"], decodedMessage.Headers["Content-Type"]);
            Assert.Equal(expectedMessage.Payload, decodedMessage.Payload);
        }

        [Fact]
        public void Encode_ExceedMaxHeaderCount_ThrowsException()
        {
            // Arrange
            var codec = new BinaryMessageCodes();
            var message = new Message
            {
                Headers = new Dictionary<string, string>()
            };

            for (int i = 0; i < BinaryMessageCodes.MaxHeaderCount + 1; i++)
            {
                message.Headers.Add($"Header{i}", $"Value{i}");
            }

            message.Payload = Encoding.ASCII.GetBytes("Payload");

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => codec.Encode(message));
            Assert.Equal("Exceeded maximum header count", exception.Message);
        }
    }
}
