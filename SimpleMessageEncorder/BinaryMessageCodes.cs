using SimpleMessageEncorder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMessageEncorder.Codes
{
    public class BinaryMessageCodes : IMessageCodes
    {
        //Message limits
        private const int MaxHeaderSize = 1023;
        private const int MaxHeaderCount = 63;
        private const int MaxPayloadSize = 256 * 1024;

        public byte[] Encode(Message msg)
        {
            if (msg.Headers.Count > MaxHeaderCount) throw new InvalidOperationException("Exceeded maximum header count");

            foreach(var pair in msg.Headers)
            {
                if (pair.Key.Length > MaxHeaderSize || pair.Value.Length > MaxHeaderSize) throw new InvalidOperationException("Header name or value exceeds maximum size");
            }
            if (msg.Payload.Length > MaxPayloadSize) throw new InvalidOperationException("Payload size exceeds maximum size");

            //Calculate payload length
            int payloadLength = msg.Payload.Length;

            //Update Content-length header
            msg.Headers["Content-Length"] = payloadLength.ToString();

            using(MemoryStream ms = new MemoryStream())
            {
                //Encode header count
                ms.WriteByte((byte)msg.Headers.Count);

                //Encode header
                foreach(var pair in msg.Headers)
                {
                    //EncodeString(ms, header.Key, header.Value);
                    EncodeString(ms, pair.Key);
                    EncodeString(ms, pair.Value);
                }
                //Encode payload length
                EncodeInt(ms,payloadLength);

                //Encode payload
                ms.Write(msg.Payload,0, payloadLength);

                return ms.ToArray();
                 
            }
        }

        public Message Decode(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();

                //Decode header count
                int headerCount = ms.ReadByte();

                //Decode headers
                for (int i = 0; i < headerCount; i++)
                {
                    string name = DecodeString(ms);
                    string value = DecodeString(ms);
                    headers[name] = value;
                }

                //Decode payload length
                int payloadLength = DecodeInt(ms);

                //Decode payload
                byte[] payload = new byte[payloadLength];
                ms.Read(payload, 0, payloadLength);

                return new Message { Headers= headers, Payload = payload };
            }
        }

        private void EncodeInt(MemoryStream ms, int length)
        {
            byte[] bytes = BitConverter.GetBytes(length);
            ms.Write(bytes, 0, bytes.Length);
        }

        private int DecodeInt(MemoryStream ms)
        {
            byte[] bytes = new byte[4];
            ms.Read(bytes, 0, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        private void EncodeString(MemoryStream ms, string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            EncodeInt(ms, bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
        }

        private string DecodeString(MemoryStream ms)
        {
            int length = DecodeInt(ms);
            byte[] bytes = new byte[length];
            ms.Read(bytes, 0, length);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
