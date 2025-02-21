using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace machinedata
{
    internal class Encode
    {
        public static string CompressAndEncodeToBase64(string input)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal, true))
                {
                    deflateStream.Write(byteArray, 0, byteArray.Length);
                }

                byte[] compressedBytes = memoryStream.ToArray();

                return Convert.ToBase64String(compressedBytes);
            }
        }
    }
}
