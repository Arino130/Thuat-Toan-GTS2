using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace GTS2
{
    public class GTSXuLy
    {
        public GTSXuLy()
        {
            browse = new List<int>();
            dataTemp = new List<int>();
        }
        //Mảng chứa phần tử đã duyệt qua hay thành phố đã đi qua
        private List<int> browse;
        //Mảng chứa dữ liệu
        private int[,] res;
        private int n; //dòng
        private int m; //cột
        private string[] Way;
        //Đọc File
        public void ReadFile()
        {
            String input = File.ReadAllText(@"data.txt");
            int j = 0;
            //Gán giá trị hàng,cột
            var temp=input.Split('\n').ToList();
            temp = temp[0].Trim().Split(' ').ToList();
            n = Convert.ToInt32(temp[0]);
            m = Convert.ToInt32(temp[1]);
            res = new int[n, m];
            Way = new string[n];
            SumAll =new int[n];
            Console.Write("      ");
            //Xuất mảng ra console
            for (int i = 0; i < m; i++)
            {
                Console.Write((i+1) + "= ");
            }
            Console.WriteLine();
            foreach (var row in input.Split('\n'))
            {
                temp = row.Trim().Split(' ').ToList();
                if (j != 0)
                {
                    for (int i=0;i<temp.Count;i++)
                    {
                        //Gán giá trị list TXT vào biến
                        res[j-1, i] = Convert.ToInt32(temp[i]);
                    }
                }
                j++;
            }
            //ii=jj=2 vì bỏ 2 giá trị đầu(giá trị cột,hàng trong txt)
            for (int ii = 0; ii < n; ii++)
            {
                Console.Write("A_" + (ii+1) + ":{ ");
                for (int jj = 0; jj < m; jj++)
                {
                    Console.Write(res[ii, jj]);
                    Console.Write(jj < m - 1 ? "," : " ");
                }
                Console.Write("}\n");
            }
        }
        public Tuple<int,int> FindMin(List<int>data)
        {
            int Min = data[0];
            int index=0;
            for (int i=1;i<data.Count;i++)
            {
                /*Chạy vòng for duyệt tùng ptử từ 1 (Vị trí 0 dùng gán cho MIN <init>)
                 * Tìm kiếm có điều kiện(Nếu mảng "Các điểm đã đi qua" != null)->kiểm tra từng vị trí (i) xem có tồn tại chưa
                 * nếu chưa thì ->Kiểm tra giá trị data[i] có < MIN ko HOẶC sẽ kiểm tra giá trị có tồn tại ko
                 * cùng MIN==data[i] dùng cho trường hợp MIN(vị trí 0) có giá trị bằng giá trị data[i] 
                 * nếu có sẽ gán giá trị MIN và Index
                 * Ngoài ra nếu mảng "Các điểm đã đi qua" == null thì sẽ tìm giá trị MIN bình thường
                 */
                if (browse.Count > 0 )
                {
                      if (Min > data[i] && !browse.Contains(i) || Min==data[i] && browse.Contains(index))
                      {
                            Min = data[i];
                            index = i;
                            goto TT;
                      }
                }
                else
                {
                        if (Min > data[i])
                        {
                            Min = data[i];
                            index = i;
                            goto TT;
                        }
                }
            TT:
                Console.Write("");
            }
            /*Kiểm tra khi mọi phần tử trong mảng đều lớn hơn giá trị Data[0](index = 0) hoặc bằng nhau=>ko tìm dc min 
             *Tiến hành kiểm tra số lượng ptử (Các điểm đã đi qua) nếu == N(số dòng nhập vào)-1<trừ 1 vì mảng bắt đầu từ 0>
             *Chạy vòng FOR tìm xem vị trí nào chưa đi qua->trả về giá trị và index ở vị trí đó.
             */
            if (index == 0 || browse.Count == m-1)
            {
                for(int a = 0; a < m+1; a++)
                {
                    if (!browse.Contains(a))
                    {
                        Min = data[a];
                        index = a;
                        return Tuple.Create(Min, index);
                    }
                }
            }
            return Tuple.Create(Min,index);
        }
        private List<int> dataTemp;
        int count = 0;
        int Start;
        int[] SumAll;
        public void GTS(int Rows,int indexWay)
        {
                int min ;
                if (browse.Count == 0)
                {
                    //Lưu giá trị vị trí bắt đầu để cuối cùng có thể trở về
                    Start = Rows;
                    //Lưu vị trí đặt chân đầu tiên vào mảng
                    browse.Add(Rows);
                }
                else
                {
                    //Lưu vị trí đặt chân tiếp theo vào mảng(nếu chưa từng đến)
                    if (!browse.Contains(Rows))
                        {
                            browse.Add(Rows);
                        }
                }
                //Làm rỗng list temp để chứa value mới
                dataTemp.Clear();
                for (int i = 0; i < m; i++)
                {
                    //Gán value
                    dataTemp.Add(res[Rows, i]);
                }
                //Đệ quy đến khi đến vị trí cuối cùng
                if (count < n-1)
                {
                    count++;
                    //Tìm kiếm phần tử nhỏ nhất khỏa DK là vị trí nhỏ nhất hoặc chưa từng đến
                    /*
                     * Giá trị min và index sau khi FIND
                     * MIN dùng để tính tổng
                     * INDEX dùng để gán vào mảng vị trí đã đi
                     */
                    var result = FindMin(dataTemp);
                    min= result.Item1;
                    //Cộng giá trị Min vào Tổng
                    SumAll[indexWay] += min;
                    Way[indexWay] = Way[indexWay] + (Rows + 1) + ",";
                    Rows = result.Item2;
                    GTS(Rows,indexWay);
                }
                else
                {
                    //Thực hiện đi trở lại vị trí bắt đầu và cộng value trở về vào Tổng
                    SumAll[indexWay] += res[Rows, Start];
                    Way[indexWay] = Way[indexWay] + (Rows + 1) + "," + (Start + 1);
                    count = 0;
                    browse.Clear();
                }
        }
        //Xuất danh sách vị trí đã đi qua
        public void ShowListWentThrough()
        {
            for (int i = 0; i < n; i++)
            {
                GTS(i, i);
            }
            int index = 1;
            int j = 0;
            foreach (string a in Way)
            {
                Console.Write("A_" + index + ": ");
                foreach (string b in a.Trim().Split(',').ToList())
                {
                    Console.Write(b+(j<a.Trim().Split(',').ToList().Count-1?"->":""));
                    j++;
                }
                Console.Write(" ===>Tổng Là: " + SumAll[index - 1]+"\n");
                j = 0;
                index++;
            }
        }
        private int indexMin;
        //Show đường đi tốt nhất
        public void ShowBestWay()
        {
            //Lấy ra vị trí Min
            indexMin= SumAll.ToList().IndexOf(SumAll.Min());
            Console.WriteLine("Đường đi tốt nhất là: \n" + Way[indexMin].Replace(",","->") + " =>> Tổng = " + SumAll.Min());
        }
        private int countWay=0;
        int indexChoose;
        public void GTS2(int choose)
        {
            var ways = Way[indexMin].Split(',').ToList();
            if (countWay < ways.Count)
            {
                //lấy vị trí giá trị
                indexChoose = ways.ToList().IndexOf(choose.ToString());               
                Console.Write(ways[indexChoose] + (countWay<ways.Count-1?"->":""));
                countWay +=1;
                indexChoose += 1;
                if(indexChoose==ways.Count-1)
                {
                    indexChoose = 0;
                }
                GTS2(int.Parse(ways[indexChoose]));
            }
            else
            {
                Console.Write(" =>> Tổng = "+SumAll[indexMin]);
                countWay =0;

            }
        }
    }
}
