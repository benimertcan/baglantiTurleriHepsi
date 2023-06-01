using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baglantiTurleriHepsi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string kulAd { get; set; }
        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = kulAd;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 anasayfa=new Form1();
            anasayfa.Show();
        }
    }
}
