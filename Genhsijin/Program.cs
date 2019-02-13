using System;

namespace Genhsijin
{
    class Program
    {
        static void Main(string[] args)
        {
            Genshijin converter = new Genshijin(GenshiType.Genshijin);
            var text = "";
            var type = "";

            if (args.Length >= 2)
            {
                type = args[1];
                if (type == "gien")
                {
                    converter = new Genshijin(GenshiType.Gien);
                }
            }
            if (args.Length >= 1)
            {
                text = args[0];
                Console.WriteLine(converter.Convert(text));
                return;
            }

            // 以下テスト用
            var genshijin = new Genshijin(GenshiType.Genshijin);
            var gien = new Genshijin(GenshiType.Gien);

            Console.WriteLine(genshijin.Convert("俺は原始人だ"));
            Console.WriteLine(gien.Convert("我は真の三國無双だ！"));
            Console.WriteLine(genshijin.Convert("大いなる力には大いなる責任が伴う"));
            Console.WriteLine(genshijin.Convert("人民の人民による人民のための政治"));
            Console.WriteLine(genshijin.Convert("ポニョはそうすけが好きー！"));
        }
    }
}
