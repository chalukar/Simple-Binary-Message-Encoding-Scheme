
using System;
using System.Text;
using SimpleMessageEncorder.Codes;
using SimpleMessageEncorder.Models;

namespace SimpleMessageEncorder
{
    public class Program
    {
        static void Main(string[] args)
        {
            IMessageCodes messageCodes = new BinaryMessageCodes();

            Message msg = CreateMessage();

            byte[] encodeMsg = EncodeMessage(messageCodes, msg);
            PrintEncodeMessage(encodeMsg);

            Message decodedeMsg = DecodeMessage(messageCodes, encodeMsg);
            PrintDecodeMessage(decodedeMsg);

        }

        private static Message CreateMessage()
        {
            return new Message
            {
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "ASCII/Byte" }
                },
                Payload = Encoding.ASCII.GetBytes("{\"message\":\"Simple Message Encode Scheme\"}")
            };
        }

        private static byte[] EncodeMessage(IMessageCodes messageCodes, Message msg)
        {
            return messageCodes.Encode(msg);
        }
        private static Message DecodeMessage(IMessageCodes messageCodes, byte[] encodeMsg)
        {
            return messageCodes.Decode(encodeMsg);
        }

        private static void PrintEncodeMessage(byte[] encodeMsg)
        {
            Console.Write($"Encode message length :{encodeMsg.Length}");
        }

        private static void PrintDecodeMessage(Message decodedeMsg)
        {
            Console.WriteLine("Decode Headers");
            foreach(var pair in decodedeMsg.Headers)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }

            string decodePayloadString = Encoding.ASCII.GetString(decodedeMsg.Payload);
            Console.WriteLine($"Decode payload : {decodePayloadString}");
        }


    }
}



