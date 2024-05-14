using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSA_BeyzaUzungel
{
    public partial class Form1 : Form
    {
        YSA ysa;
        public Form1()
        {
            InitializeComponent();
        }


        private void renkAyari(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = (btn.BackColor == Color.White) ? Color.Black : Color.White;
        }


        private void temizle_Click(object sender, EventArgs e)
        {
           List<Button> btnList = groupBox1.Controls.OfType<Button>().ToList();

         foreach (Button button in btnList)
          {
             if (button.BackColor == Color.Black)
         {
              button.BackColor = Color.White;
             }
                 }

           
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

         
            numericUpDown1.Value = numericUpDown1.Minimum;

          
            richTextBox.Clear();

            
            Array.Clear(ysa.Giris_ver, 0, ysa.Giris_ver.Length);
            Array.Clear(ysa.Cikis_ver, 0, ysa.Cikis_ver.Length);
            Array.Clear(ysa.Dizim, 0, ysa.Dizim.Length);
            Array.Clear(ysa.Esik_dgr_1, 0, ysa.Esik_dgr_1.Length);
            Array.Clear(ysa.Esik_dgr_2, 0, ysa.Esik_dgr_2.Length);
            Array.Clear(ysa.Ara, 0, ysa.Ara.Length);
        }

        private void ButonlariMatriseYazdir()
        {
            int[,] matris = new int[7, 5];

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Control control = this.Controls.Find("button" + ((i * 5) + j + 1), true).FirstOrDefault();
                    if (control != null && control is Button)
                    {
                        Button btn = (Button)control;
                        if (btn.BackColor == Color.Black)
                        {
                            matris[i, j] = 1;
                        }
                        else
                        {
                            matris[i, j] = 0;
                        }
                    }
                }
            }

            richTextBox.Clear();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    richTextBox.AppendText(matris[i, j].ToString() + " ");
                }
                richTextBox.AppendText("\n");
            }

            richTextBox.SelectionStart = 0;
            richTextBox.SelectionLength = richTextBox.Text.Length;
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void tanımla_Click(object sender, EventArgs e)
        {
            Button[] btn = {
                button1,  button2,  button3,  button4,  button5,
                button6,  button7,  button8,  button9,  button10,
                button11, button12, button13, button14, button15,
                button16, button17, button18, button19, button20 ,
                button21, button22, button23, button24, button25 ,
                button26, button27, button28, button29, button30 ,
                button31, button32, button33, button34, button35 ,
                  };
            ysa = new YSA( Convert.ToDouble(numericUpDown1.Value));
            ysa.ButonRenkleriniDiziyeAktar(btn);
            ysa.RastgeleAğırlıkOluştur();
            hesap.Enabled = true;
            tanımla.Enabled = false;
            ButonlariMatriseYazdir();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hesap.Enabled = false;
        }

        private void hesap_Click(object sender, EventArgs e)
        {

            TextBox[] tx =
             {
                textBox1,textBox2,textBox3,textBox4,textBox5
             };
            ysa.HangiDiziEslesiyor();

            for (int i = 0; i < 5; i++)
            {


                if (ysa.Index == i)
                {

                    ysa.hesapla(ysa.Dizim, ysa.Giris_ver, ysa.Cikis_ver, ysa.Esik_dgr_1, ysa.Esik_dgr_2);
                    ysa.GeriyeYayilim(ysa.Giris_ver, ysa.Cikis_ver, ysa.Esik_dgr_1, ysa.Esik_dgr_2, ysa.Ara, i);

                    tx[i].Text = ysa.Cıktı.ToString();


                }
                else if (ysa.Index != i && ysa.Index != -1)
                {

                    ysa.KatmanCikisiHesapla(ysa.ABCDE_Matris, ysa.Giris_ver, ysa.Cikis_ver, ysa.Esik_dgr_1, ysa.Esik_dgr_2, i);
                    ysa.AgirlikGuncelle(ysa.Giris_ver, ysa.Cikis_ver, ysa.Esik_dgr_1, ysa.Esik_dgr_2, ysa.Ara, i);
                    double son = 1 - ysa.Cıktı;
                    tx[i].Text = son.ToString();


                }
                else if (ysa.Index == -1)
                {

                    ysa.KatmanCikisiHesapla(ysa.ABCDE_Matris, ysa.Giris_ver, ysa.Cikis_ver, ysa.Esik_dgr_1, ysa.Esik_dgr_2, i);
                    ysa.AgirlikGuncelle(ysa.Giris_ver, ysa.Cikis_ver, ysa.Esik_dgr_1, ysa.Esik_dgr_2, ysa.Ara, i);
                    double son = 1 - ysa.Cıktı;
                    tx[i].Text = son.ToString();

                }

            }
            hesap.Enabled = false;
            tanımla.Enabled = true;
        }

       
    }
}