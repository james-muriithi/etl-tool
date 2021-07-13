using System;
using System.Text;

namespace Cryptography
{
    class Base64Decoder : IDecoder
    {
        public string Decode(string encodedText)
        {
            byte[] data = Convert.FromBase64String(encodedText);
            return Encoding.Unicode.GetString(data);
        }
    }
}