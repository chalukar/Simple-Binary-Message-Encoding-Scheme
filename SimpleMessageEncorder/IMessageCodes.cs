using SimpleMessageEncorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMessageEncorder.Codes
{
    public interface IMessageCodes
    {
        byte[] Encode(Message msg);
        Message Decode(byte[] data);

    }
}
