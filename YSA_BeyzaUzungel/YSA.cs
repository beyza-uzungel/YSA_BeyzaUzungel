using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSA_BeyzaUzungel
{
    class YSA
    {
        private int[,] a_b_c_d_e_matris =
        {
            { 0,0,1,0,0, 0,1,0,1,0, 1,0,0,0,1, 1,0,0,0,1, 1,1,1,1,1, 1,0,0,0,1, 1,0,0,0,1 },
            { 1,1,1,1,0, 1,0,0,0,1, 1,0,0,0,1, 1,1,1,1,0, 1,0,0,0,1, 1,0,0,0,1, 1,1,1,1,0 },
            { 0,0,1,1,1, 0,1,0,0,0, 1,0,0,0,0, 1,0,0,0,0, 1,0,0,0,0, 0,1,0,0,0, 0,0,1,1,1 },
            { 1,1,1,0,0, 1,0,0,1,0, 1,0,0,0,1, 1,0,0,0,1, 1,0,0,0,1, 1,0,0,1,0, 1,1,1,0,0 },
            { 1,1,1,1,1, 1,0,0,0,0, 1,0,0,0,0, 1,1,1,1,1, 1,0,0,0,0, 1,0,0,0,0, 1,1,1,1,1 }
        };

        private double momentumKatsayisi = 0.60; 
        private double ogrenmeParametresi = 0.20; 
        private double hataoran;
        private int[] dizim;

        public YSA(double hataOran)
        {
            this.hataoran = hataOran;
        }
        public double[] Cikis_ver { get => cikisVer; set => cikisVer = value; }
        public int[] Dizim { get => dizim; set => dizim = value; }
        public double[,] Giris_ver { get => girisVer; set => girisVer = value; }
        public double[] Esik_dgr_1 { get => esik_dgr_1; set => esik_dgr_1 = value; }
        public double[] Esik_dgr_2 { get => esik_dgr_2; set => esik_dgr_2 = value; }
        public int[,] ABCDE_Matris { get => a_b_c_d_e_matris; set => a_b_c_d_e_matris = value; }
        public YSA()
        {
            Giris_ver = new double[35, 3];
            Esik_dgr_1 = new double[3];
            Esik_dgr_2 = new double[5];
            Cikis_ver = new double[3];
            ABCDE_Matris = new int[5, 35];
            Dizim = new int[35];

        }

        public void ButonRenkleriniDiziyeAktar(Button[] bt)
        {
            Dizim = new int[bt.Length]; 
            for (int i = 0; i < bt.Length; i++)
            {
                Dizim[i] = (bt[i].BackColor == Color.Black) ? 1 : 0; 
            }
        }

        private double[,] girisVer;
        private double[] esik_dgr_1;
        private double[] esik_dgr_2;
        private double[] cikisVer;

        public void RastgeleAğırlıkOluştur()
        {
            girisVer = new double[3, 35];
            esik_dgr_1 = new double[3];
            esik_dgr_2 = new double[1];
            Cikis_ver = new double[3];
           
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 35; j++)
                {
                    Giris_ver[i, j] = Math.Round(rnd.NextDouble(), 2);
                }
            }

           
            for (int i = 0; i < 3; i++)
            {
                esik_dgr_1[i] = Math.Round(rnd.NextDouble(), 2);
            }
            esik_dgr_2[0] = Math.Round(rnd.NextDouble(), 2);

          
            for (int i = 0; i < 3; i++)
            {
                Cikis_ver[i] = Math.Round(rnd.NextDouble(), 2);
            }
        }


        private double[,] araKatman;
        private double hata;
        private double cıktı;
        private double net;
        public double[,] Ara { get => araKatman; set => araKatman = value; }
        public double Hata { get => hata; set => hata = value; }
        public double Cıktı { get => cıktı; set => cıktı = value; }
        public double NetGiris { get => net; set => net = value; }
        public void hesapla(int[] girdi, double[,] agirliklarGiris, double[] agirliklarCikis, double[] esikDegerlerAra, double[] esikDegerlerCikis)
        {
            araKatman = new double[3, 2];
            NetGiris = 0;
            Cıktı = 0;
            Hata = 0;

            // Ara katmanın çıkış hesabi
            for (int i = 0; i < 3; i++)
            {
                double gecici = 0;
                for (int j = 0; j < 35; j++)
                {
                    gecici += girdi[j] * agirliklarGiris[i, j];
                }
                gecici += esikDegerlerAra[i];
                araKatman[i, 0] = gecici;
                araKatman[i, 1] = 1 / (1 + Math.Exp(-gecici));
            }

            // Çıkış katmanı net giriş hesabi
            for (int i = 0; i < 3; i++)
            {
                NetGiris += araKatman[i, 1] * agirliklarCikis[i];
            }

            // Ağın çıkış ve hata hesabı
            double ckt = NetGiris + esikDegerlerCikis[0];
            Cıktı = 1 / (1 + Math.Exp(-ckt));
            Hata = 1 - Cıktı;
        }

        private double[] sigma;

        private bool dgr;
        public bool Dgr { get => dgr; set => dgr = value; }
        public double Momentum_katsaisi { get => momentumKatsayisi; set => momentumKatsayisi = value; }
        public double Ogrenme_parametresi { get => ogrenmeParametresi; set => ogrenmeParametresi = value; }
        public double Hata_orani { get => hataoran; set => hataoran = value; }
        public void GeriyeYayilim(double[,] girisAğırlıkları, double[] cikisAğırlıkları, double[] araEsik1, double[] cikisEsik2, double[,] araDegerleri, int iterasyon)
        {
            double[] hataTurevleri = new double[3];
            bool devam = true;

            while (devam)
            {
                double hatanınTurevi = Cıktı * (1 - Cıktı) * Hata;

                // Ara katman hata türevlerinin hesaplanması
                for (int i = 0; i < 3; i++)
                {
                    hataTurevleri[i] = Ara[i, 1] * (1 - Ara[i, 1]) * cikisAğırlıkları[i] * hatanınTurevi;
                }

                // Ağırlıkların güncellenmesi
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 35; j++)
                    {
                        girisAğırlıkları[i, j] += Ogrenme_parametresi * hataTurevleri[i] * girisAğırlıkları[i, j] + Momentum_katsaisi * iterasyon;
                    }
                }

                // Eşik değerlerin güncellenmesi
                for (int i = 0; i < 3; i++)
                {
                    araEsik1[i] += Ogrenme_parametresi * hataTurevleri[i] * 1 + Momentum_katsaisi * iterasyon;
                }

                // Çıkış katmanı ağırlıklarının güncellenmesi
                for (int i = 0; i < 3; i++)
                {
                    cikisAğırlıkları[i] += Ogrenme_parametresi * hatanınTurevi * araDegerleri[i, 1] + Momentum_katsaisi * iterasyon;
                }

                // Çıkış katmanı eşik değerinin güncellenmesi
                cikisEsik2[0] += Ogrenme_parametresi * hatanınTurevi * 1 + Momentum_katsaisi * iterasyon;

                // Hatanın tekrar hesaplanması
                hesapla (Dizim, Giris_ver, Cikis_ver, Esik_dgr_1, Esik_dgr_2);

                // Hatanın eşik değerini karşılayıp karşılamadığının kontrolü
                if (Hata < Hata_orani)
                {
                    devam = false;
                }
            }
        }

       
        private int index;
        public int Index { get => index; set => index = value; }

        public void HangiDiziEslesiyor()
        {
            int[,] eslesmeSayilari = new int[5, 2]; //  eşleşme  tutan matris

            //eşleşme sayılarını hesaplama
            for (int i = 0; i < 5; i++)
            {
                int eslesmeSayisi = 0;
                for (int j = 0; j < 35; j++)
                {
                    if (Dizim[j] == ABCDE_Matris[i, j])
                    {
                        eslesmeSayisi++;
                    }
                }
                eslesmeSayilari[i, 0] = i; // Dizinin indeksi
                eslesmeSayilari[i, 1] = eslesmeSayisi; // Eşleşme sayısı
            }

            // Tam eşleşme olan diziyi bul
            for (int i = 0; i < 5; i++)
            {
                if (eslesmeSayilari[i, 1] == 35)
                {
                    Index = eslesmeSayilari[i, 0];
                    return;
                }
            }

            // Tam eşleşme olmayan durumda Index'i -1 olarak ayarla
            Index = -1;
        }

        public void KatmanCikisiHesapla(int[,] girdiMatrisi, double[,] agirlikMatrisi, double[] cikisAğırlıkları, double[] esikDegerleri, double[] cikisEsigi, int indis)
        {
            araKatman = new double[3, 2];
            NetGiris = 0;
            Cıktı = 0;
            Hata = 0;
            double gecici = 0;
            for (int i = 0; i < 3; i++)
            {
                gecici = 0;
                for (int j = 0; j < 35; j++)
                {
                    gecici += girdiMatrisi[indis, j] * agirlikMatrisi[i, j];
                }
                gecici += gecici + esikDegerleri[i];
                araKatman[i, 0] = gecici;
                araKatman[i, 1] = (1 / (1 + Math.Exp(-gecici)));
            }

         


            for (int i = 0; i < 3; i++)
            {
                NetGiris += Ara[i, 1] * cikisAğırlıkları[i];
            }

            double ckt = NetGiris + cikisEsigi[0];
            Cıktı = (1 / (1 + Math.Exp(-ckt)));
            Hata = 0 - Cıktı;


        }

        public void AgirlikGuncelle(double[,] rgiris, double[] rcıkıs, double[] esik1, double[] esik2, double[,] aradızı, int a)
        {
            sigma = new double[3];
            Dgr = true;


            while (Dgr == true)
            {

                double sigma = Cıktı * (1 - Cıktı) * Hata;
                for (int i = 0; i < 3; i++)
                {
                    this.sigma[i] = Ara[i, 1] * (1 - Ara[i, 1]) * rcıkıs[i] * sigma;
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 35; j++)
                    {
                        rgiris[i, j] = rgiris[i, j] + ((Ogrenme_parametresi * this.sigma[i] * rgiris[i, j]) + Momentum_katsaisi * a);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    esik1[i] = esik1[i] + ((Ogrenme_parametresi * this.sigma[i] * 1) + Momentum_katsaisi * a);
                }
                for (int i = 0; i < 3; i++)
                {
                    rcıkıs[i] = rcıkıs[i] + ((Ogrenme_parametresi * sigma * aradızı[i, 1]) + Momentum_katsaisi * a);
                }
                esik2[0] = esik2[0] + ((Ogrenme_parametresi * sigma * 1) + Momentum_katsaisi * a);

                KatmanCikisiHesapla(ABCDE_Matris, Giris_ver, Cikis_ver, Esik_dgr_1, Esik_dgr_2, a);

                if (Hata < Hata_orani)
                {
                    Dgr = false;

                }
            }

        }



    }

}
