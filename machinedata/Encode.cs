using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace machinedata
{
    internal class Encode
    {
        public static string CompressAndEncodeToBase64(string input)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // เขียน zlib header
                memoryStream.WriteByte(0x78);
                memoryStream.WriteByte(0x9C);

                using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal, true))
                {
                    deflateStream.Write(byteArray, 0, byteArray.Length);
                }

                // คำนวณ Adler-32 checksum และเพิ่มลงท้าย
                uint adler32 = ComputeAdler32(byteArray);
                byte[] adlerBytes = BitConverter.GetBytes(adler32);
                Array.Reverse(adlerBytes); // ให้เป็น Big-Endian
                memoryStream.Write(adlerBytes, 0, adlerBytes.Length);

                byte[] compressedBytes = memoryStream.ToArray();
                return Convert.ToBase64String(compressedBytes);
            }
        }

        private static uint ComputeAdler32(byte[] data)
        {
            const uint MOD_ADLER = 65521;
            uint a = 1, b = 0;

            foreach (byte value in data)
            {
                a = (a + value) % MOD_ADLER;
                b = (b + a) % MOD_ADLER;
            }

            return (b << 16) | a;
        }
    }
}
