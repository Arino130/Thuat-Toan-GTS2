using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTS2
{
    class Program
    {
        static void Main(string[] args)
        {
            GTSXuLy gTSXuLy = new GTSXuLy();
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("\t  Thuật toán GTS2\n==========================================\nDanh sách:");
            //Đọc File
            gTSXuLy.ReadFile();
            Console.WriteLine("==========================================");
            gTSXuLy.ShowListWentThrough();
            Console.WriteLine("==========================================");
            gTSXuLy.ShowBestWay();
            Console.WriteLine("==========================================");
            Console.Write("Nhập vị trí muốn bắt đầu: ");
            int choose = int.Parse(Console.ReadLine());
            gTSXuLy.GTS2(choose);
            Console.ReadKey();
        }
    }
}
