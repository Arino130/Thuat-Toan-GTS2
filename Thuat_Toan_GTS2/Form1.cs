using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thuat_Toan_GTS2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ReadFIle()
        {
            var text = File.ReadAllLines(@"path\to\file.txt");
            var Data = text.Select(x => (x.Split(',').Select(Int32.Parse).ToArray())).ToArray();
        }
        public void GTF()
        {

        }
        public void GTF2()
        {

        }
    }
}
