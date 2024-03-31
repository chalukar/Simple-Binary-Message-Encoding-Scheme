# Simple Binary Message Encoder

Description:
Simple Message Encoder is a lightweight library designed to encode and decode binary messages for use in signaling protocols. It provides a simple and efficient way to pass messages between peers in real-time communication applications.

**Implementation:**

•	**BinaryMessageCodes:** This class implements the IMessageCodes interface and provides methods to encode and decode messages. It enforces message limits such as maximum header size, maximum header count, and maximum payload size to ensure efficient message handling.

•	**IMessageCodes:** This interface defines the contract for message encoding and decoding. It includes methods to encode a message into binary data and decode binary data into a message object.

•	**Message:** This class represents a message and contains headers (name-value pairs) and a payload (binary data). It encapsulates the data to be encoded or decoded by the message encoder.

•	**Program:** The Program class demonstrates the usage of the message encoder. It creates a sample message with headers and a payload, encodes the message using the **BinaryMessageCodes** implementation, and then decodes the encoded message back into its original form. It also provides methods to print the encoded message length and decoded message headers and payload.

**Usage**: 
To use the Simple Message Encoder library, follow these steps:
1.	Include the **BinaryMessageCodes**, **IMessageCodes**, and **Message** classes in your project.
2.	Create an instance of **BinaryMessageCodes**.
3.	Create a **Message** object with headers and payload.
4.	Encode the message using the Encode method of **BinaryMessageCodes**.
5.	Send the encoded message over the network or any communication channel.
6.	Decode the received binary data using the **Decode** method of **BinaryMessageCodes** to retrieve the original message.

In Addition, The **BinaryMessageCodesTests** class contains **xUnit** test methods to validate the functionality of the **BinaryMessageCodes** class, which is responsible for encoding and decoding binary messages. These tests ensure that the encoding and decoding processes work correctly and handle edge cases such as exceeding maximum header count gracefully. 

**Test Methods:**

•	**Encode_Decode_Success:** This test verifies that encoding and decoding a message results in the original message. It creates a sample message with headers and a payload, encodes the message, and then decodes the encoded message back into its original form. It asserts that the decoded message is not null, has the same number of headers as the original message, and has identical header values and payload.

•	**Encode_ExceedMaxHeaderCount_ThrowsException:** This test ensures that attempting to encode a message with headers exceeding the maximum allowed count throws an **InvalidOperationException** with the appropriate error message. It creates a message with headers exceeding the maximum count and asserts that invoking the **Encode** method with this message throws the expected exception.

**Usage:**
To use the** xUnit** test suite for BinaryMessageCodes, follow these steps:

1.	Include the **BinaryMessageCodesTests** class in your test project.
2.	Add a reference to the project containing the **BinaryMessageCodes** class.
3.	Run the test suite to verify the correctness of the message encoding and decoding logic.

**Example:**


public class BinaryMessageCodesTests
{

    [Fact]
    public void Encode_Decode_Success()
    {
        // Test implementation
    }

    [Fact]
    public void Encode_ExceedMaxHeaderCount_ThrowsException()
    {
        // Test implementation
    }
}

Thank you


