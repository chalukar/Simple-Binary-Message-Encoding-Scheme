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
    }
}
