using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War3_Map_Editor_Tools
{
    public class IO
    {
        public static byte[] GetBytes(byte[] readBuffer, int startpos, int len)
        {
            byte[] results = new byte[len];
            Array.Copy(readBuffer, startpos, results, 0, len);
            return results;
        }

        public static int LastBytes(byte[] bytes)
        {
            int offset = 0;
            int countToFind = 6;
            for (int i = 0; i <= bytes.Length - countToFind; i++)
            {
                bool found = true;
                for (int j = 0; j < countToFind; j++)
                {
                    if (bytes[i + j] != (byte)0x00)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    offset = i;
                    break;
                }
            }
            return offset;
        }
    }
}
