using System;

namespace Common
{
    public class IDCreater
    {

        private static readonly char[] ConversionTable =
        {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
        'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
        'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
        'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
        'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
        '8', '9',
        };

        // 年
        private static string Transport999(int dt)
        {

            string str = null;

            str += ConversionTable[dt / (ConversionTable.Length - 1)];
            str += ConversionTable[dt % (ConversionTable.Length - 1)];

            return str;
        }

        // 10進数を64進数に変換する(年とミリ秒は変換可能）
        private static char Transport64(int dt)
        {

            return ConversionTable[dt];
        }

        private static int Transpoer999(int dt)
        {

            int a = dt / 1000;

            dt = dt - a * 1000;

            Console.WriteLine(dt);

            return dt;
        }

        public static string IDOutPut()
        {

            string str = null;
            DateTime dt = DateTime.Now;

            str += Transport999(Transpoer999(dt.Year));
            str += Transport64(dt.Month);
            str += Transport64(dt.Day);
            str += Transport64(dt.Hour);
            str += Transport64(dt.Minute);
            str += Transport64(dt.Second);
            str += Transport999(Transpoer999(dt.Millisecond));

            return str;
        }

    }

}
