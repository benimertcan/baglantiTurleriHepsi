using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace baglantiTurleriHepsi
{
    public partial class Form1 : Form
    {
        //YORUM SATIRLARI SQL BAGLANTISI || OLEDB 2002/2003 VERİTABANI OLUŞTURULDU || VERİTABANI ADI: veritabanitablo, kayitlar VE kullanicilar ADINDA 2 TABLO OLUŞTURULDU || 
        OleDbDataReader oledbokuyucu;
        OleDbDataAdapter oledbayarlayici;
        OleDbConnection oledbbaglanti;
        OleDbCommand oledbkomut;
        MySqlConnection baglanti;
        MySqlCommand komut;
        MySqlDataAdapter ayarlayici;
        DataTable veriTablo;
        MySqlDataReader okuyucu;
        Graphics grafik;
        Pen kalem = new Pen(Color.Red,3);
        public string komutText;
        
        public Form1()
        {
            InitializeComponent();
        }
        public void listele(string numara, string ad, string adres, string maas)
        {
            try
            {
                oledbbaglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=veritabani.mdb;");
                oledbbaglanti.Open();
                //baglanti = new MySqlConnection("Server=localhost;Database=veritabanitablo;user=root;Pwd='';SslMode=none");
                //baglanti.Open();

                if (numara == "" && ad == "" && adres == "" && maas =="")
                {
                    //ayarlayici = new MySqlDataAdapter("SELECT * FROM kayitlar", baglanti);
                    oledbayarlayici = new OleDbDataAdapter("SELECT * FROM kayitlar;", oledbbaglanti);
                }
                else
                {
                    string numText = "", adText = "", adresText = "", maasText = "", gondText;
                    int sayac = 0; 
                    if (numara !="" ) { numText = "NUMARA=" + numara + ""; sayac++; } 
                    if( ad != "") { adText = "AD like'%" + ad + "'"; sayac++; }
                    if(adres != "") { adresText = "ADRES like'%" + adres + "'"; sayac++;  }
                    if (maas != "") { maasText = "MAAS=" + maas + ""; sayac++; }
                    gondText =numText+adresText+maasText+adText;
                    if (sayac >= 2)
                    {
                        MessageBox.Show("Aynı anda birden fazla kolondan arama yapılamaz");
                        sayac = 0;
                        gondText = "";
                    }
                    else
                    {
                        
                        oledbayarlayici = new OleDbDataAdapter("SELECT * FROM kayitlar WHERE " + gondText, oledbbaglanti);
                        
                    }
                    //ayarlayici = new MySqlDataAdapter("SELECT * FROM kayitlar WHERE numara='" + numara + "' OR ad='" + ad + "' OR adres='" + adres + "' OR maas='" + maas + "'", baglanti);
                }
                veriTablo = new DataTable();
                //ayarlayici.Fill(veriTablo);
                oledbayarlayici.Fill(veriTablo);
                dataGridView1.DataSource = veriTablo;
                oledbbaglanti.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele("","","","");
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            listele(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                oledbbaglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=veritabani.mdb;");
                oledbbaglanti.Open();
                oledbkomut = new OleDbCommand("insert into kayitlar (NUMARA,AD,ADRES,MAAS) values ('" + textBox1.Text + "' ,'" + textBox2.Text + "' ,'" + textBox3.Text + "' ,'" + textBox4.Text + "')", oledbbaglanti);
                oledbkomut.ExecuteNonQuery();
                oledbbaglanti.Close();
                //baglanti = new MySqlConnection("Server=localhost;Database=veritabanitablo;user=root;Pwd='';SslMode=none");
                //baglanti.Open();
                //komut = new MySqlCommand("insert into kayitlar (NUMARA,AD,ADRES,MAAS) values ('" + textBox1.Text + "' ,'"+textBox2.Text+"' ,'"+textBox3.Text+"' ,'"+textBox4.Text+"')", baglanti);
                //komut.ExecuteNonQuery();
                //baglanti.Close();
                listele("", "", "","");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            listele("","","","");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                oledbbaglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=veritabani.mdb;");
                oledbbaglanti.Open();
                oledbkomut = new OleDbCommand("UPDATE kayitlar SET NUMARA=" + int.Parse(textBox1.Text) + " , AD='" + textBox2.Text + "' ,ADRES='" + textBox3.Text + "' ,MAAS=" + int.Parse(textBox4.Text) + " WHERE NUMARA=" + int.Parse(num) + ";", oledbbaglanti);
                oledbkomut.ExecuteNonQuery();
                oledbbaglanti.Close();
                //baglanti = new MySqlConnection("Server=localhost;Database=veritabanitablo;user=root;Pwd='';SslMode=none");
                //baglanti.Open();
                //komut = new MySqlCommand("UPDATE kayitlar SET NUMARA='" + textBox1.Text + "' , AD='" + textBox2.Text + "' ,ADRES='" + textBox3.Text + "' ,MAAS='" + textBox4.Text + "' WHERE NUMARA='"+num+"';", baglanti);
                //komut.ExecuteNonQuery();
                listele("", "", "","" );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string num = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            num = Convert.ToString(textBox1.Text);
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                oledbbaglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=veritabani.mdb;");
                oledbbaglanti.Open();
                oledbkomut = new OleDbCommand("delete from kayitlar where NUMARA=" + int.Parse(num) + "", oledbbaglanti);
                oledbkomut.ExecuteNonQuery();
                oledbbaglanti.Close();
                //baglanti = new MySqlConnection("Server=localhost;Database=veritabanitablo;user=root;Pwd='';SslMode=none");
                //baglanti.Open();
                //komut = new MySqlCommand("DELETE FROM kayitlar WHERE NUMARA='" + num + "'", baglanti);
                //komut.ExecuteNonQuery();
                //baglanti.Close();
                listele("", "", "","" );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {   
            grafik = panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawRectangle(kalem, 20, 20, 350, 350);
            grafik.Dispose();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            grafik = panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawRectangle(kalem, 20, 20, 400, 300);
            grafik.Dispose();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            grafik=panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawLine(kalem, 20 , 20,200,400);
            grafik.Dispose();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            grafik = panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawEllipse(kalem, 20, 20, 200, 400);
            grafik.Dispose();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            grafik = panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawEllipse(kalem, 20, 20, 350, 350);
            grafik.Dispose();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            grafik = panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawArc(kalem, 20, 20, 250, 250,180,90);
            grafik.Dispose();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if(button12.Text =="Çizime Başla")
            {
                timer1.Start();
                button12.Text = "Çizimi Durdur";
            }
            else
            {
                timer1.Stop();
                button12.Text = "Çizime Başla";
                grafik.Dispose();
            }
        }

        int cap = 40,artis=2;
        public  string kulAd="";
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "" && textBox6.Text != "")
                {
                    oledbbaglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=veritabani.mdb;");
                    oledbbaglanti.Open();
                    oledbkomut = new OleDbCommand("SELECT * FROM kullanicilar WHERE kullaniciAdi='" + textBox5.Text + "' AND sifre=" + textBox6.Text + " ", oledbbaglanti);
                    oledbokuyucu = oledbkomut.ExecuteReader();
                    //baglanti = new MySqlConnection("Server=localhost;Database=veritabanitablo;user=root;Pwd='';SslMode=none");
                    //baglanti.Open();
                    //komut = new MySqlCommand("SELECT * FROM kullanicilar WHERE kullaniciAdi='"+textBox5.Text+"' AND sifre='"+textBox6.Text+ "' ", baglanti);
                    //okuyucu=komut.ExecuteReader();
                    //if (okuyucu.Read())
                    if (oledbokuyucu.Read())
                    {
                        kulAd = textBox5.Text;
                        Form2 sayfa = new Form2();
                        sayfa.kulAd = kulAd;
                        this.Hide();
                        sayfa.ShowDialog();
                    }
                    else { label7.Text = "HATALI GİRİŞ YAPTINIZ!"; }
                    oledbbaglanti.Close();
                    //baglanti.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            grafik=panel1.CreateGraphics();
            grafik.Clear(panel1.BackColor);
            grafik.DrawEllipse(kalem, 20, 20,cap, cap);
            cap +=artis;
            if(cap>=380)
            {
                artis = -2;
            }
            if(cap<=38)
            {
                artis = 2;
            }
            grafik.Dispose();
        }

       
        


    }

}

