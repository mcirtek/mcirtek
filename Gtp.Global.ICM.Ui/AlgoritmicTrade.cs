using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Gtp.Framework.ControlLibrary;
using System.Messaging;
using System.Threading;
using System.Globalization;


using System.Net.Sockets;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;


namespace Gtp.Global.ICM.Ui
{
    public partial class AlgoritmicTrade : Form
    {
        int ROW;
        public static Socket client;
        byte[] buffer;

        Gtp.Global.ICM.Ui.OrderManagement frmana;
        string BUGUNKUTARIH, TAKASTARIHI;
        public static decimal BASZAMAN_SEANS1, BITZAMAN_SEANS1, BASZAMAN_SEANS2, BITZAMAN_SEANS2;


        public AlgoritmicTrade()
        {
            InitializeComponent();
        }

        public AlgoritmicTrade(Gtp.Global.ICM.Ui.OrderManagement frm)
        {
            InitializeComponent();
            frmana = frm;
        }

        private void AlgoritmicTrade_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");

            System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer(); 
            timer2.Tick += new EventHandler(timer2_Tick); 
            timer2.Start(); //*21.30 da pencereyi kapat.

            Tarih_Al();
            Musteri_Doldur();
            Menkul_Doldur();
            EkranTazele();

            socket_start();
            if (client.Connected)      //* ilk girişte hardbit gönder...
                SendToServer("HBT%");   

        }




        //****** CLIENT SOCKET - StreamServer'a bağlanır***********************************************
        //*********************************************************************************************

        public void socket_start()
        {
            try
            {
                if (client == null)
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                if (!client.Connected)
                {
                     //* client.BeginConnect("128.10.254.244", 8055, new AsyncCallback(ClientServerBaglandi), null);
                      client.BeginConnect("10.2.7.59", 8055, new AsyncCallback(ClientServerBaglandi), null);
                }
            }
            catch (SocketException ex) { }
        }

        private void ClientServerBaglandi(IAsyncResult re)
        {
            try
            {
                buffer = new byte[20000];
                client.EndConnect(re);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Algo Server'a Ulaşılamıyor. Emir girişi yapmayınız, IT ye bilgi veriniz", " U Y A R I", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.ServiceNotification);
                return;
            }

            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Serverdanmesajgeldi), client);
        }


        private void Serverdanmesajgeldi(IAsyncResult res)
        {
            Socket scr = (Socket)res.AsyncState;
            SocketError errm;
            int bytesRead = scr.EndReceive(res, out errm);
            if (SocketError.Success == errm)
            {
                string gelen = Encoding.Default.GetString(buffer, 0, bytesRead);
                //**** SetText("Koşul Sağlandı...\n\r " + gelen);
                Array.Clear(buffer, 0, buffer.Length);
            }
            try
            {
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Serverdanmesajgeldi), client);
            }
            catch (SocketException sexc) { }
            catch (NullReferenceException nexc) { }
            catch (Exception gexc) { }
        }


        public static void SendToServer(string mesaj)
        {
            client.Send(Encoding.Default.GetBytes(mesaj));
        }

        //****** CLIENT SOCKET - StreamServer'a bağlanır***********************************************
        //*********************************************************************************************



        public void Tarih_Al()
        {
            string BugunkuTarih = "", TakasTarihi = "";
            OrderOperations op = new OrderOperations();
            op.TarihBilgileri_Al(this, ref BugunkuTarih, ref TakasTarihi);
            BUGUNKUTARIH = BugunkuTarih;
            TAKASTARIHI = TakasTarihi;

            op.BorsaZamanlari(ref BASZAMAN_SEANS1, ref BITZAMAN_SEANS1, ref BASZAMAN_SEANS2, ref BITZAMAN_SEANS2);
        }

        //********************************************************************************************************************

        public void Musteri_Doldur()
        {
            DataTable Tablo1;

            bool hepsidahil = false;
            OrderOperations op = new OrderOperations();
            Tablo1 = op.MusteriListesi_Al(this, hepsidahil);

            musteri.Properties.DataSource = Tablo1;
            musteri.Properties.DisplayMember = "ID";
            musteri.Properties.ValueMember = "NAME";

        }

        //********************************************************************************************************************

        public void Musteri_Detay(string musterino , ref string custid, ref string accid)
        {
            OrderOperations op = new OrderOperations();
            op.MusteriBilgisi_Detay(this, musterino , ref custid, ref accid);
        }

        //********************************************************************************************************************

        public void Menkul_Doldur()
        {
            DataTable Tablo1;
            OrderOperations op = new OrderOperations();
            Tablo1 = op.MenkulListesi_Al(this);

            menkul.Text = "";
            menkul.Properties.Items.Clear();
            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {
                menkul.Properties.Items.Add(Convert.ToString(Tablo1.Rows[i]["NAME"]));
            }


        }


        //********************************************************************************************************************

        private void menkul_detay(string menkulkodu,ref string fininstid, ref int maxlot)
        {
            DataTable Tablo1;
            OrderOperations op = new OrderOperations();
            Tablo1 = op.MenkulDetay(this, menkulkodu);

            if (Tablo1.Rows.Count > 0)   //* sadece ilk recordu alalım. En son seansa aitttir.
            {
                fininstid = Convert.ToString(Tablo1.Rows[0]["FIN_INST_ID"]);
                maxlot    = Convert.ToInt32(Tablo1.Rows[0]["MAX_LOT"]);
            }

        }

        //********************************************************************************************************************

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool TopluEmirGirisimi = false;
            decimal mik, fyt;
            string alsat = "";
            string EmirTipi = "";
            string IslemTipi = "1";    //*  1=Yeni Giriş , 2=Düzeltme , 3=İptal 
            string giden = "";
            string RefId = "";
            string wgib_yuzde1, wgib_yuzde2, wgib_baszmn1, wgib_baszmn2, wgib_baszmn3, wgib_bitzmn1, wgib_bitzmn2, wgib_bitzmn3, wgib_parcasayisi, aktsek = "";

            //* Socket kapalı ise çıkış yap.
            bool cikis = new bool();
            cikis = false;
            socket_start();
            if (!client.Connected)
            {
                cikis = true;
                this.Close();
            }
            if (cikis == true) return;
            //* Socket kapalı ise çıkış yap.


            if (radioButton1.Checked)
                EmirTipi = "Z.A";
            else if (radioButton4.Checked)
                EmirTipi = "G.I.B";


            if (label9.Text == "Yeni Giriş")
            {
                if (Ekran_Deger_Kontrol(TopluEmirGirisimi))
                    return;

                IslemTipi = "1";
                OrderOperations isl = new OrderOperations(this);
                RefId = Convert.ToString(isl.YeniRefNoGetir());  //* yeni bir refid alalım.

                System.Windows.Forms.DialogResult dialogResult = TopMostMessageBox.Show("Algo Emir Girişi yapmaktasınız. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                //* System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Algo Emir Girişi yapmaktasınız. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.No) return;
            }
            else if (label9.Text == "İptal Et")
            {
                IslemTipi = "3";
                RefId = Erefid.Text;

                System.Windows.Forms.DialogResult dialogResult = TopMostMessageBox.Show("Algo Emir iptal edilecek. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                // System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Algo Emir iptal edilecek. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.No) return;
            }


            if (radioButton2.Checked)
                alsat = "CREDIT";
            else if (radioButton3.Checked)
                alsat = "DEBIT";

            mik = Convert.ToDecimal(lot.Text);


            if (gib_yuzde1.Text.Length == 0) wgib_yuzde1   = "0"; else wgib_yuzde1 = gib_yuzde1.Text;
            if (gib_yuzde2.Text.Length == 0) wgib_yuzde2   = "0"; else wgib_yuzde2 = gib_yuzde2.Text;
            if (gib_baszmn1.Text.Length == 0) wgib_baszmn1 = "0"; else wgib_baszmn1 = gib_baszmn1.Text;
            if (gib_baszmn2.Text.Length == 0) wgib_baszmn2 = "0"; else wgib_baszmn2 = gib_baszmn2.Text;
            if (gib_baszmn3.Text.Length == 0) wgib_baszmn3 = "0"; else wgib_baszmn3 = gib_baszmn3.Text;
            if (gib_bitzmn1.Text.Length == 0) wgib_bitzmn1 = "0"; else wgib_bitzmn1 = gib_bitzmn1.Text;
            if (gib_bitzmn2.Text.Length == 0) wgib_bitzmn2 = "0"; else wgib_bitzmn2 = gib_bitzmn2.Text;
            if (gib_bitzmn3.Text.Length == 0) wgib_bitzmn3 = "0"; else wgib_bitzmn3 = gib_bitzmn3.Text;
            if (gib_parcasayisi.Text.Length == 0) wgib_parcasayisi = "0"; else wgib_parcasayisi = gib_parcasayisi.Text;
            string gib_enson_aktifolanparca = "0";

            if (radioButton5.Checked)
                aktsek = "AKTIFE_VER";
            else if (radioButton6.Checked)
                aktsek = "PASIFE_VER";
            else if (radioButton7.Checked)
                aktsek = "TAHTAYI_BOYA";
            else if (radioButton8.Checked)
                aktsek = "SONISLEM_FIYATI";
            else if (radioButton9.Checked)
                aktsek = "VWAP_IKIISLEMORT";
            else if (radioButton10.Checked)
                aktsek = "VWAP_SEANSORT";


            try
            {
                bool ekrantazele = true;
                string fininstid="" , custid="" , accid="";
                int maxlot=0;
                menkul_detay(menkul.Text, ref fininstid, ref maxlot);
                Musteri_Detay(musteri.Text, ref  custid, ref accid);

                if( (fininstid.Trim().Length>0)  && (custid.Trim().Length>0) )
                    SendToTCP(RefId, EmirTipi, BUGUNKUTARIH, TAKASTARIHI, menkul.Text, fininstid, alsat, musteri.Text, custid, accid, lot.Text, Convert.ToString(maxlot), marj.Text, zmn1.Text, zmn2.Text, zmn3.Text, IslemTipi, wgib_yuzde1, wgib_yuzde2, wgib_baszmn1, wgib_baszmn2, wgib_baszmn3, wgib_bitzmn1, wgib_bitzmn2, wgib_bitzmn3, wgib_parcasayisi, gib_enson_aktifolanparca, aktsek, ekrantazele);
                
                if (label9.Text == "İptal Et")
                    EkranBosalt();
            }
            catch (Exception exx)
            {
                TopMostMessageBox.Show("İşleme Konulmadı.");
                //System.Windows.Forms.MessageBox.Show("İşleme Konulmadı.");
            }


        }


        //********************************************************************************************************************

        
        private bool Ekran_Deger_Kontrol(bool TopluEmirGirisimi)
        {
            decimal mik, fyt , gibyuzde1=0, gibyuzde2=0, gib_topyuzde=0 ;

            if ((BUGUNKUTARIH == null) || (BUGUNKUTARIH.Trim().Length == 0))
            {
                TopMostMessageBox.Show("Yanlızca bugünkü işlem gününe emir girmelisiniz !");
                return true;
            }

            if ((musteri.Text == null) || (musteri.Text.Trim().Length == 0))
            {
                TopMostMessageBox.Show("Müşteri seçiniz.");
                return true;
            }

            if (TopluEmirGirisimi == false)
            {
                if ((menkul.Text == null) || (menkul.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Menkul seçiniz.");
                    return true;
                }
                if ((lot.Text == null) || (lot.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Miktar(Lot) değeri giriniz.");
                    return true;
                }
                if ((!radioButton2.Checked) && (!radioButton3.Checked))
                {
                    TopMostMessageBox.Show("İşlem tipini seçiniz");
                    return true;
                }
            }


            if (radioButton1.Checked)   //*Zaman Aktv. lu emir.
            {
                if ((marj.Text == null) || (marj.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Marj % değeri giriniz.");
                    //System.Windows.Forms.MessageBox.Show("Marj % değeri giriniz.");
                    return true;
                }

                if (Convert.ToDecimal((marj.Text)) > 3)
                {
                    TopMostMessageBox.Show("Marj alanı 3 ten büyük olamaz.");
                    return true;
                }


                if ((zmn1.Text == null) || (zmn1.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Saat değeri giriniz.");
                    // System.Windows.Forms.MessageBox.Show("Saat değeri giriniz.");
                    return true;
                }

                if ((zmn2.Text == null) || (zmn2.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Dakika değeri giriniz.");
                    //System.Windows.Forms.MessageBox.Show("Dakika değeri giriniz.");
                    return true;
                }

                if ((zmn3.Text == null) || (zmn3.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Saniye değeri giriniz.");
                   // System.Windows.Forms.MessageBox.Show("Saniye değeri giriniz.");
                    return true;
                }

                OrderOperations isl = new OrderOperations(this);
                decimal suankizaman = isl.GetServerTime();

                if (((zmn1.Text != null) && (zmn1.Text.Trim().Length != 0)))
                {
                    string zaman = SifirKoy(zmn1.Text, 2) + SifirKoy(zmn2.Text, 2) + SifirKoy(zmn3.Text, 2);
                    if (Convert.ToDecimal(zaman) < suankizaman)
                    {
                        TopMostMessageBox.Show("Zaman alanına girilen değer, içinde bulunduğumuz zamandan küçük olamaz.");
                        //System.Windows.Forms.MessageBox.Show("Zaman alanına girilen değer, içinde bulunduğumuz zamandan küçük olamaz.");
                        return true;
                    }

                    if (Convert.ToDecimal(zaman) > BITZAMAN_SEANS2)
                    {
                        TopMostMessageBox.Show("Zaman alanına girilen değer, 2.Seans kapanış zamanından büyük olamaz.");
                        //System.Windows.Forms.MessageBox.Show("Zaman alanına girilen değer, 2.Seans kapanış zamanından büyük olamaz.");
                        return true;
                    }

                    if (Convert.ToDecimal(zaman) > BITZAMAN_SEANS2)
                    {
                        TopMostMessageBox.Show("Başlangıç zamanı, 2.Seans kapanış zamanından büyük olamaz.");
                        //System.Windows.Forms.MessageBox.Show("Başlangıç zamanı, 2.Seans kapanış zamanından büyük olamaz.");
                        return true;
                    }

                    if ((Convert.ToDecimal(zaman) >= BITZAMAN_SEANS1) && (Convert.ToDecimal(zaman) < BASZAMAN_SEANS2))
                    {
                        TopMostMessageBox.Show("Başlangıç zamanı, işlem saatleri içerisinde olmalıdır.");
                        //System.Windows.Forms.MessageBox.Show("Başlangıç zamanı, işlem saatleri içerisinde olmalıdır.");
                        return true;
                    }

                }


            }

            if (radioButton4.Checked)   //* G.İ.B emir.
            {
                if ((gib_yuzde1.Text == null) || (gib_yuzde1.Text.Trim().Length == 0))
                    gibyuzde1 = 0;
                else
                    gibyuzde1 = Convert.ToDecimal(gib_yuzde1.Text);

                if ((gib_yuzde2.Text == null) || (gib_yuzde2.Text.Trim().Length == 0))
                    gibyuzde2 = 0;
                else
                    gibyuzde2 = Convert.ToDecimal(gib_yuzde2.Text);



                gib_topyuzde = gibyuzde1 + gibyuzde2;
                if (gib_topyuzde != 100)
                {
                    TopMostMessageBox.Show("1.Seans ve 2.Seans % değerlerinin toplamı 100 olmalıdır.");
                   // System.Windows.Forms.MessageBox.Show("1.Seans ve 2.Seans % değerlerinin toplamı 100 olmalıdır.");
                    return true;
                }

           
                OrderOperations isl = new OrderOperations(this);
                string hangiseanstayiz = isl.HangiSeanstayiz();
                decimal suankizaman = isl.GetServerTime();

                if ( (hangiseanstayiz == "2") && (gibyuzde1 > 0) )
                {
                    TopMostMessageBox.Show("2.Seanstayız, 1.seans yüzdesi dolu olamaz.");
                   // System.Windows.Forms.MessageBox.Show("2.Seanstayız, 1.seans yüzdesi dolu olamaz.");
                    return true;
                }
           

                if ( ((gib_baszmn1.Text != null) && (gib_baszmn1.Text.Trim().Length != 0))  )
                {
                     string bslzmn = SifirKoy(gib_baszmn1.Text, 2) + SifirKoy(gib_baszmn2.Text, 2) + SifirKoy(gib_baszmn3.Text, 2);
                     if( Convert.ToDecimal(bslzmn) < suankizaman)
                     {
                         TopMostMessageBox.Show("Başlangıç zamanı, içinde bulunduğumuz zamandan küçük olamaz.");
                         // System.Windows.Forms.MessageBox.Show("Başlangıç zamanı, içinde bulunduğumuz zamandan küçük olamaz.");
                         return true;
                     }

                     if (Convert.ToDecimal(bslzmn) > BITZAMAN_SEANS2)
                     {
                         TopMostMessageBox.Show("Başlangıç zamanı, 2.Seans kapanış zamanından büyük olamaz.");
                         //System.Windows.Forms.MessageBox.Show("Başlangıç zamanı, 2.Seans kapanış zamanından büyük olamaz.");
                         return true;
                     }

                     if ( (Convert.ToDecimal(bslzmn) >= BITZAMAN_SEANS1) && (Convert.ToDecimal(bslzmn) < BASZAMAN_SEANS2) )
                     {
                         TopMostMessageBox.Show("Başlangıç zamanı, işlem saatleri içerisinde olmalıdır.");
                        // System.Windows.Forms.MessageBox.Show("Başlangıç zamanı, işlem saatleri içerisinde olmalıdır.");
                         return true;
                     }

                }


                if (((gib_bitzmn1.Text != null) && (gib_bitzmn1.Text.Trim().Length != 0)))
                {
                    string bitzmn = SifirKoy(gib_bitzmn1.Text, 2) + SifirKoy(gib_bitzmn2.Text, 2) + SifirKoy(gib_bitzmn3.Text, 2);
                    if (Convert.ToDecimal(bitzmn) < suankizaman)
                    {
                        TopMostMessageBox.Show("Bitiş zamanı, içinde bulunduğumuz zamandan küçük olamaz.");
                        //System.Windows.Forms.MessageBox.Show("Bitiş zamanı, içinde bulunduğumuz zamandan küçük olamaz.");
                        return true;
                    }
                    if (Convert.ToDecimal(bitzmn) > BITZAMAN_SEANS2)
                    {
                        TopMostMessageBox.Show("Bitiş zamanı, 2.Seans kapanış zamanından büyük olamaz.");
                        //System.Windows.Forms.MessageBox.Show("Bitiş zamanı, 2.Seans kapanış zamanından büyük olamaz.");
                        return true;
                    }

                    if ((Convert.ToDecimal(bitzmn) > BITZAMAN_SEANS1) && (Convert.ToDecimal(bitzmn) <= BASZAMAN_SEANS2))
                    {
                        TopMostMessageBox.Show("Bitiş zamanı, işlem saatleri içerisinde olmalıdır.");
                        //System.Windows.Forms.MessageBox.Show("Bitiş zamanı, işlem saatleri içerisinde olmalıdır.");
                        return true;
                    }

                    if (Convert.ToDecimal(bitzmn) > 172800)  //*Gib emirlerini 17:28:00 dan sonraya girmeyelim. 
                    {                                        //* çünki 17:29:00 da tüm GİB emirlerini kapatma yapacağız.
                        gib_bitzmn1.Text = "17";
                        gib_bitzmn2.Text = "28";
                        gib_bitzmn3.Text = "00";
                    }
                }
                else
                {
                    if (gibyuzde2 > 0)  
                    {
                        gib_bitzmn1.Text = "17";
                        gib_bitzmn2.Text = "28";
                        gib_bitzmn3.Text = "00";
                    }
                }

                

                if ( ((gib_baszmn1.Text != null) && (gib_baszmn1.Text.Trim().Length != 0)) && ((gib_bitzmn1.Text != null) && (gib_bitzmn1.Text.Trim().Length != 0)) )
                {
                     string bslzmn = SifirKoy(gib_baszmn1.Text, 2) + SifirKoy(gib_baszmn2.Text, 2) + SifirKoy(gib_baszmn3.Text, 2);
                     string bitzmn = SifirKoy(gib_bitzmn1.Text, 2) + SifirKoy(gib_bitzmn2.Text, 2) + SifirKoy(gib_bitzmn3.Text, 2);
                     if (Convert.ToDecimal(bslzmn) > Convert.ToDecimal(bitzmn))
                     {
                         TopMostMessageBox.Show("Başlangıç zamanı bitiş zamanından küçük olmalıdır.");
                         //System.Windows.Forms.MessageBox.Show("Başlangıç zamanı bitiş zamanından küçük olmalıdır.");
                         return true;
                     }
                }


                if ((hangiseanstayiz == "1") && (gibyuzde1 == 100))
                {
                    if ((gib_bitzmn1.Text != null) && (gib_bitzmn1.Text.Trim().Length != 0))
                    {
                        if (Convert.ToDecimal(SifirKoy(gib_bitzmn1.Text, 2) + SifirKoy(gib_bitzmn2.Text, 2) + SifirKoy(gib_bitzmn3.Text, 2)) > BITZAMAN_SEANS1)
                        {
                            TopMostMessageBox.Show("1.seans yüzdesini %100 girdiniz. Bit.Zamanı " + Convert.ToString(BITZAMAN_SEANS1) + " dan küçük olmalıdır.");
                            // System.Windows.Forms.MessageBox.Show("1.seans yüzdesini %100 girdiniz. Bit.Zamanı " + Convert.ToString(BITZAMAN_SEANS1) + " dan küçük olmalıdır.");
                            return true;
                        }
                    }
                }


                if (gibyuzde1 == 0)
                {
                    if ((gib_baszmn1.Text != null) && (gib_baszmn1.Text.Trim().Length != 0))
                    {
                        if (Convert.ToDecimal(SifirKoy(gib_baszmn1.Text, 2) + SifirKoy(gib_baszmn2.Text, 2) + SifirKoy(gib_baszmn3.Text, 2)) < BASZAMAN_SEANS2)
                        {
                            TopMostMessageBox.Show("1.seans yüzdesini boş girdiniz. Bas.Zamanı " + Convert.ToString(BASZAMAN_SEANS2) + " dan büyük olmalıdır.");
                            // System.Windows.Forms.MessageBox.Show("1.seans yüzdesini boş girdiniz. Bas.Zamanı " + Convert.ToString(BASZAMAN_SEANS2) + " dan büyük olmalıdır.");
                            return true;
                        }
                    }
                }
                else if (gibyuzde1 > 0)
                {
                    if ((gib_baszmn1.Text != null) && (gib_baszmn1.Text.Trim().Length != 0))
                    {
                        if (Convert.ToDecimal(SifirKoy(gib_baszmn1.Text, 2) + SifirKoy(gib_baszmn2.Text, 2) + SifirKoy(gib_baszmn3.Text, 2)) > BITZAMAN_SEANS1)
                        {
                            TopMostMessageBox.Show("1.seans yüzdesi doluyken. Bas.Zamanı " + Convert.ToString(BITZAMAN_SEANS1) + " dan büyük olamaz.");
                            // System.Windows.Forms.MessageBox.Show("1.seans yüzdesi doluyken. Bas.Zamanı " + Convert.ToString(BITZAMAN_SEANS1) + " dan büyük olamaz.");
                            return true;
                        }
                    }
                }



                if (hangiseanstayiz == "2")
                {
                    if (gibyuzde1 != 0)
                    {
                        TopMostMessageBox.Show("2.seanstayız, 1.seansa ait % değerini girmeyiniz.");
                        // System.Windows.Forms.MessageBox.Show("2.seanstayız, 1.seansa ait % değerini girmeyiniz.");
                        return true;
                    }

                    if ((gib_baszmn1.Text != null) && (gib_baszmn1.Text.Trim().Length != 0))
                    {
                        if (Convert.ToDecimal(SifirKoy(gib_baszmn1.Text, 2) + SifirKoy(gib_baszmn2.Text, 2) + SifirKoy(gib_baszmn3.Text, 2)) < BASZAMAN_SEANS2)
                        {
                            TopMostMessageBox.Show("1.seans yüzdesini boş girdiniz. Bas.Zamanı " + Convert.ToString(BASZAMAN_SEANS2) + " dan büyük olmalıdır.");
                            //System.Windows.Forms.MessageBox.Show("1.seans yüzdesini boş girdiniz. Bas.Zamanı " + Convert.ToString(BASZAMAN_SEANS2) + " dan büyük olmalıdır.");
                            return true;
                        }
                    }
                }


                if (gibyuzde2 > 0)
                {
                    if ((gib_bitzmn1.Text != null) && (gib_bitzmn1.Text.Trim().Length != 0))
                    {
                        if (Convert.ToDecimal(SifirKoy(gib_bitzmn1.Text, 2) + SifirKoy(gib_bitzmn2.Text, 2) + SifirKoy(gib_bitzmn3.Text, 2)) < BASZAMAN_SEANS2)
                        {
                            TopMostMessageBox.Show("2.seans yüzdesini girdiniz. Bit.Zamanı " + Convert.ToString(BASZAMAN_SEANS2) + " dan büyük olmalıdır.");
                            //System.Windows.Forms.MessageBox.Show("2.seans yüzdesini girdiniz. Bit.Zamanı " + Convert.ToString(BASZAMAN_SEANS2) + " dan büyük olmalıdır.");
                            return true;
                        }
                    }
                }


                if ((gib_parcasayisi.Text == null) || (gib_parcasayisi.Text.Trim().Length == 0))
                {
                    TopMostMessageBox.Show("Parça sayısını giriniz.");
                    //System.Windows.Forms.MessageBox.Show("Parça sayısını giriniz.");
                    return true;
                }
                else
                {
                    if (Convert.ToInt32(gib_parcasayisi.Text) == 0)
                    {
                        TopMostMessageBox.Show("Parça sayısını giriniz.");
                        //System.Windows.Forms.MessageBox.Show("Parça sayısını giriniz.");
                        return true;
                    }
                }

                if (TopluEmirGirisimi == false)  //*Tek Algo Emir Girişi ise lot ile ilgili kontrolleri ön yüzden yapsın. 
                {
                    if (Convert.ToInt32(gib_parcasayisi.Text) > Convert.ToInt32(lot.Text))
                    {
                        TopMostMessageBox.Show("Parça sayısı, Lot değerinden büyük olamaz.");
                        return true;
                    }


                    if (gibyuzde1 > 0)
                    {
                        int herbirparcayadusenlot = Convert.ToInt32(lot.Text) / Convert.ToInt32(gib_parcasayisi.Text);
                        decimal ilkseansta_gonderilecek_toplamlot = Convert.ToInt32(lot.Text) * (gibyuzde1 / 100);
                        decimal ilkseanstaki_herbirparcaninlotu = ilkseansta_gonderilecek_toplamlot / herbirparcayadusenlot;

                        if (ilkseanstaki_herbirparcaninlotu < 1)
                        {
                            TopMostMessageBox.Show("1. Seansta gönderilecek herbir emir başına düşen lot adedi 1 lotun üzerinde olmalıdır.");
                            return true;
                        }
                    }


                    if (gibyuzde2 > 0)
                    {
                        int herbirparcayadusenlot = Convert.ToInt32(lot.Text) / Convert.ToInt32(gib_parcasayisi.Text);
                        decimal ikinciseansta_gonderilecek_toplamlot = Convert.ToInt32(lot.Text) * (gibyuzde2 / 100);
                        decimal ikinciseanstaki_herbirparcaninlotu = ikinciseansta_gonderilecek_toplamlot / herbirparcayadusenlot;

                        if (ikinciseanstaki_herbirparcaninlotu < 1)
                        {
                            TopMostMessageBox.Show("2. Seansta gönderilecek herbir emir başına düşen lot adedi 1 lotun üzerinde olmalıdır.");
                            return true;
                        }
                    }
                }

                if ((!radioButton5.Checked) && (!radioButton6.Checked) && (!radioButton7.Checked) && (!radioButton8.Checked) && (!radioButton9.Checked) && (!radioButton10.Checked))
                {
                    TopMostMessageBox.Show("Aktifleşme şeklini seçiniz");
                    //System.Windows.Forms.MessageBox.Show("Aktifleşme şeklini seçiniz");
                    return true;
                }


            }


            return false;
        }

        //********************************************************************************************************************

        private void SendToTCP(string RefId, string EmirTipi, string BugunTarihi, string TakasTarihi, string Menkul, string Fin_Inst_Id, string Alsat, string HesapNo, string CustId, string AccId, string Lot, string Maxlot, string MarjYuzde, string Zmn1, string Zmn2, string Zmn3, string IslemTipi, string gib_yuzde1, string gib_yuzde2, string gib_baszmn1, string gib_baszmn2, string gib_baszmn3, string gib_bitzmn1, string gib_bitzmn2, string gib_bitzmn3, string gib_parcasayisi, string gib_enson_aktifolanparca, string aktsek ,bool ekrantazele)
        {
            var userInfo = ApplicationBrowser.ActiveUser;
            string s1 = userInfo.ApplicationName + ";";
            s1 += userInfo.ChannelId + ";";
            s1 += userInfo.CustomSectionName + ";";
            s1 += userInfo.DivisionId + ";";
            s1 += userInfo.EmployeeId + ";";
            s1 += userInfo.HostName + ";";     // s1 += "192.168.0.251" + ";";
            s1 += userInfo.OrganizationGroupId + ";";
            s1 += userInfo.OrganizationId + ";";
            s1 += userInfo.PartyId + ";";
            s1 += userInfo.PositionId + ";";
            s1 += userInfo.SessionId + ";";

            string s2 = EmirTipi + "&" + BugunTarihi + "&" + TakasTarihi + "&" + RefId + "&" + Menkul + "&" + Fin_Inst_Id + "&" + Alsat + "&" + HesapNo + "&" + CustId + "&" + AccId + "&" + Lot + "&" + Maxlot + "&" + MarjYuzde + "&" + Zmn1 + "&" + Zmn2 + "&" + Zmn3 + "&" + IslemTipi + "&" + gib_yuzde1 + "&" + gib_yuzde2 + "&" + gib_baszmn1 + "&" + gib_baszmn2 + "&" + gib_baszmn3 + "&" + gib_bitzmn1 + "&" + gib_bitzmn2 + "&" + gib_bitzmn3 + "&" + gib_parcasayisi + "&" + gib_enson_aktifolanparca + "&" + aktsek;
            string giden = s1 + "|" + s2 + "%";

            try
            {
                if (client.Connected == false)
                    socket_start();

                SendToServer("HBT%");
                SendToServer(giden);

                System.Threading.Thread.Sleep(800);  //* Bekle.
                if (ekrantazele==true) 
                    EkranTazele();
            }
            catch (Exception exx) { }
        }


        //********************************************************************************************************************

        private void SendToQueue(String EmirTipi, String BugunTarihi, String TakasTarihi, String Menkul, String Fin_Inst_Id, String Alsat, String HesapNo, String CustId, String AccId, String Lot, String Maxlot, String MarjYuzde, String Zmn1, String Zmn2, String Zmn3, String IslemTipi, String gib_yuzde1, String gib_yuzde2, String gib_baszmn1, String gib_baszmn2, String gib_baszmn3, String gib_bitzmn1, String gib_bitzmn2, String gib_bitzmn3, String gib_parcasayisi, string gib_enson_aktifolanparca, string aktsek)
        {
            //*         SendToTCP yerine SendToQueue ile MsMq üzerindende haberleşilebilebir....
            //* UYARI : Bu uygulamanın çalıştığı makinayada Msmq'yu kurmak ""gerekmektedir. yoksa hata verir.
            //*         Control Panel-> Programs And Features -> Turns Windows features on or off -> Check Msmq to install.

            var userInfo = ApplicationBrowser.ActiveUser;
            string s1 = userInfo.ApplicationName + ";";
            s1 += userInfo.ChannelId + ";";
            s1 += userInfo.CustomSectionName + ";";
            s1 += userInfo.DivisionId + ";";
            s1 += userInfo.EmployeeId + ";";
            s1 += userInfo.HostName + ";";     // s1 += "192.168.0.251" + ";";
            s1 += userInfo.OrganizationGroupId + ";";
            s1 += userInfo.OrganizationId + ";";
            s1 += userInfo.PartyId + ";";
            s1 += userInfo.PositionId + ";";
            s1 += userInfo.SessionId + ";";

            OrderOperations isl = new OrderOperations(this);
            string RefId = isl.GetDateTime();

            string s2 = EmirTipi + "&" + BugunTarihi + "&" + TakasTarihi + "&" + RefId + "&" + Menkul + "&" + Fin_Inst_Id + "&" + Alsat + "&" + HesapNo + "&" + CustId + "&" + AccId + "&" + Lot + "&" + Maxlot + "&" + MarjYuzde + "&" + Zmn1 + "&" + Zmn2 + "&" + Zmn3 + "&" + IslemTipi + "&" + gib_yuzde1 + "&" + gib_yuzde2 + "&" + gib_baszmn1 + "&" + gib_baszmn2 + "&" + gib_baszmn3 + "&" + gib_bitzmn1 + "&" + gib_bitzmn2 + "&" + gib_bitzmn3 + "&" + gib_parcasayisi + "&" + gib_enson_aktifolanparca + "&" + aktsek; 
            string giden = s1 + "|" + s2;

            string queuePath = "FormatName:Direct=OS:ist15429.global.local\\private$\\algoemirler"; //streamserver makinasına gönder... 
            MessageQueue queue = new MessageQueue();
            queue.Path = queuePath;

            MessageQueueTransaction msqt = new MessageQueueTransaction();
            Message message   = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Label     = "AlgoEmir";
            message.Body      = giden;

            msqt.Begin();
            try
            {
                queue.Send(message, MessageQueueTransactionType.Single);
                msqt.Commit();
            }
            catch (MessageQueueException)
            {
                msqt.Abort();
            }

            queue.Close();

        }

        //********************************************************************************************************************

         private void EkranTazele()
        {
            OrderOperations opr = new OrderOperations();
            opr.AlgoBekleyenEmirler_Al(ref gridControl1, ref gridView1, chkedt, btn_detay, this);
            checkBox1.Checked = false;
        }

        //********************************************************************************************************************

         private void gridView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
         {
             GridView view = (GridView)sender;
             Point pt = view.GridControl.PointToClient(System.Windows.Forms.Control.MousePosition);
             DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
             if (info.InRow || info.InRowCell)
             {
                 ROW = info.RowHandle;
                 // COL = info.Column.AbsoluteIndex;
             }
         }

         //********************************************************************************************************************

         private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
         {
             GridView currentview = sender as GridView;
             string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "EMIRTIPI"));

             if ( (e.Column.FieldName == "CHECK") || (e.Column.FieldName == "ONAY") )
             {
                 e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                 e.Column.OptionsColumn.AllowEdit = true;
                 e.Column.OptionsColumn.AllowFocus = true;

                 if (e.Column.FieldName == "ONAY") 
                 {
                     if (Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ONAY")).Trim().Length >0)
                     {
                       e.Appearance.ForeColor = Color.White;
                       e.Appearance.BackColor = Color.Black;
                     }
                 }

             }
             else if ((e.Column.FieldName == "LOT") || (e.Column.FieldName == "YUZDE"))
             {
                 e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                 e.Column.OptionsColumn.AllowEdit = false;
                 e.Column.OptionsColumn.AllowFocus = false;
             }
             else
             {
                 e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                 e.Column.OptionsColumn.AllowEdit = false;
                 e.Column.OptionsColumn.AllowFocus = false;
             }

         }


         //********************************************************************************************************************


         private void iptalEtToolStripMenuItem_Click(object sender, EventArgs e)
         {
             //* Socket kapalı ise çıkış yap.
                bool cikis = false;    
                socket_start();
                if (!client.Connected)
                {   cikis = true;   
                    this.Close();  }
                if (cikis == true) return;
             //* Socket kapalı ise çıkış yap.

             int rowc = gridView1.RowCount;
             if (rowc > 0)
             {
                 if (gridView1.GetRowCellValue(ROW, "REFERANSID").ToString().Length > 0)
                 {
                      if (gridView1.GetRowCellValue(ROW, "STATU").ToString() != "Bekliyor")
                     {
                         TopMostMessageBox.Show("İptal işlemi yapılamaz. !");
                         //System.Windows.Forms.MessageBox.Show("İptal işlemi yapılamaz. !");
                         return;
                     }

                     label9.Text = "İptal Et";
                     Erefid.Text = gridView1.GetRowCellValue(ROW, "REFERANSID").ToString();
                     Eemirtipi.Text = gridView1.GetRowCellValue(ROW, "EMIRTIPI").ToString();
                     string hesapno = gridView1.GetRowCellValue(ROW, "HESAPNO").ToString();
                     musteri.EditValue = musteri.Properties.GetKeyValueByDisplayText(hesapno);

                     menkul.EditValue = gridView1.GetRowCellValue(ROW, "MENKUL").ToString();
                     lot.EditValue = gridView1.GetRowCellValue(ROW, "LOT").ToString();
                     panel5.Enabled = false;

                     if (gridView1.GetRowCellValue(ROW, "ALSAT").ToString() == "CREDIT")
                     {
                         radioButton2.Checked = true;
                         radioButton3.Checked = false;
                     }
                     else
                     {
                         radioButton2.Checked = false;
                         radioButton3.Checked = true;
                     }

                     if (gridView1.GetRowCellValue(ROW, "EMIRTIPI").ToString() == "Z.A")
                     {
                         panel9.Visible  = true;
                         panel10.Visible = false;
                         radioButton1.Checked = true;
                         radioButton4.Checked = false;

                         marj.EditValue = gridView1.GetRowCellValue(ROW, "YUZDE").ToString();
                         zmn1.EditValue = gridView1.GetRowCellValue(ROW, "TETIKSAAT").ToString();
                         zmn2.EditValue = gridView1.GetRowCellValue(ROW, "TETIKDAKIKA").ToString();
                         zmn3.EditValue = gridView1.GetRowCellValue(ROW, "TETIKSANIYE").ToString();
                     }
                     else if (gridView1.GetRowCellValue(ROW, "EMIRTIPI").ToString() == "G.I.B")
                     {
                         panel9.Visible  = false;
                         panel10.Visible = true;
                         radioButton1.Checked = false;
                         radioButton4.Checked = true;

                         gib_yuzde1.Text  = gridView1.GetRowCellValue(ROW, "GIB_YUZDE1").ToString();
                         gib_yuzde2.Text  = gridView1.GetRowCellValue(ROW, "GIB_YUZDE2").ToString();
                         gib_baszmn1.Text = SifirKoy(gridView1.GetRowCellValue(ROW, "GIB_BAS_ZMN1_SAAT").ToString(),2);
                         gib_baszmn2.Text = SifirKoy(gridView1.GetRowCellValue(ROW, "GIB_BAS_ZMN1_DAKIKA").ToString(),2);
                         gib_baszmn3.Text = SifirKoy(gridView1.GetRowCellValue(ROW, "GIB_BAS_ZMN1_SANIYE").ToString(),2);
                         gib_bitzmn1.Text = SifirKoy(gridView1.GetRowCellValue(ROW, "GIB_BIT_ZMN1_SAAT").ToString(),2);
                         gib_bitzmn2.Text = SifirKoy(gridView1.GetRowCellValue(ROW, "GIB_BIT_ZMN1_DAKIKA").ToString(),2);
                         gib_bitzmn3.Text = SifirKoy(gridView1.GetRowCellValue(ROW, "GIB_BIT_ZMN1_SANIYE").ToString(),2);
                         gib_parcasayisi.Text = gridView1.GetRowCellValue(ROW, "GIB_PARCASAYISI").ToString();
                     }


                 }
             }


         }

         //********************************************************************************************************************


         private string SifirKoy(string sayi, int uzunluk)
         {
             string rc;
             int len = sayi.Trim().Length;
             rc = sayi;
             for (int i = len; i < uzunluk; i++)
                 rc = "0" + rc;
             return rc.Trim();
         }

         //********************************************************************************************************************

         private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
         {
             GridView currentview = sender as GridView;

             string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "STATU"));
             string tut2 = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "GIB_AKTIFLESMESEKLI"));
             string alsat = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ALSAT"));   

             if (alsat == "DEBIT")
             {
                 e.Appearance.ForeColor = Color.Red;
                 e.Appearance.BackColor = Color.White;
             }
             else if (alsat == "CREDIT")
             {
                 e.Appearance.ForeColor = Color.Green;
                 e.Appearance.BackColor = Color.White;
             }


             if ((tut == "İptal Edildi") || (tut2 == "İPTAL EDİLDİ") )
             {
                 e.Appearance.ForeColor = Color.White;
                 e.Appearance.BackColor = Color.Gray;
             }
             else if (tut == "Emir İletildi")
             {
                 e.Appearance.ForeColor = Color.Black;
                 e.Appearance.BackColor = Color.Yellow;
             }
         }

         //********************************************************************************************************************

         private void simpleButton3_Click(object sender, EventArgs e)
         {
             EkranBosalt();
         }

         //********************************************************************************************************************

         private void EkranBosalt()
         {
             label9.Text = "Yeni Giriş";
             panel5.Enabled = true;
             Erefid.Text = "";
             Eemirtipi.Text = "";
             musteri.EditValue = "";
             musteri.ItemIndex = -1;
             menkul.EditValue = "";
             menkul.SelectedItem = -1;
             lot.EditValue = "";
             marj.EditValue = "";
             zmn1.EditValue = "";
             zmn2.EditValue = "";
             zmn3.EditValue = "";
             radioButton2.Checked = false;
             radioButton3.Checked = false;
             gib_yuzde1.EditValue = "";
             gib_yuzde2.EditValue = "";
             gib_baszmn1.EditValue = "";
             gib_baszmn2.EditValue = "";
             gib_baszmn3.EditValue = "";
             gib_bitzmn1.EditValue = "";
             gib_bitzmn2.EditValue = "";
             gib_bitzmn3.EditValue = "";
             gib_parcasayisi.EditValue = "";
             radioButton5.Checked = false;
             radioButton6.Checked = false;
             radioButton7.Checked = false;
             radioButton8.Checked = false;
             radioButton9.Checked = false;
             radioButton10.Checked = false;
         }

         //******************************************************************************************************************** 

         private void simpleButton2_Click(object sender, EventArgs e)
         {
             EkranTazele();
         }

         //******************************************************************************************************************** 

         private void timer2_Tick(object sender, EventArgs e)
         {
             if (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 30 && DateTime.Now.Second == 00) //Close Form if the current time is 21:30:00
             {
                 this.Close();
             }
         }

         //********************************************************************************************************************

         private void radioButton1_Click(object sender, EventArgs e)
         {
             if (radioButton1.Checked)
             {
                 panel9.Visible  = true;
                 panel10.Visible = false;
             }
         }

         //******************************************************************************************************************** 

         private void radioButton4_Click(object sender, EventArgs e)
         {
             if (radioButton4.Checked)
             {
                 panel9.Visible = false;
                 panel10.Visible = true;
             }
         }


       //********************************************************************************************************************
      
         private void btn_detay_Click(object sender, EventArgs e)
         {
             if (gridView1.GetRowCellValue(ROW, "EMIRTIPI").ToString() == "G.I.B")
             {
                 AlgoOrderDetail frm = new AlgoOrderDetail();
                 frm.REFERANSID = Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "REFERANSID").ToString());
                 frm.BaseFormInit(this.ApplicationBrowser);
                 frm.Show();

             }
         }

         //********************************************************************************************************************


         private void AlgoritmicTrade_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
         {
             try
             {
                 SendToServer("HBT%");
                 SendToServer("KAP%");              //* pencere kapanırsa client socket nesnesini serverdaki listeden siler ...
                 client = null;
             }
             catch (Exception ex) { }
         }

         //********************************************************************************************************************


         private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
         {

         }

         //********************************************************************************************************************


         private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (tabControl1.SelectedIndex == 0)            //*Tek Algo giriş
             {
                 simpleButton1.Visible = true;
                 simpleButton5.Visible = false;
                 
                 label2.Visible = true; menkul.Visible = true;
                 label4.Visible = true; lot.Visible = true;
                 groupBox2.Visible = true;
             }
             else if (tabControl1.SelectedIndex == 1)      //*Toplu Algo giriş
             {
                 simpleButton1.Visible = false;
                 simpleButton5.Visible = true;
                 label19.Visible = false;
                 label19.Refresh();

                 EkranBosalt();
                 label2.Visible = false; menkul.Visible = false;
                 label4.Visible = false; lot.Visible = false;
                 groupBox2.Visible = false;
             }
         }

         //********************************************************************************************************************


         private void simpleButton4_Click(object sender, EventArgs e)
         {
             OrderOperations isl;
             Stream myStream;
             System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

             openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
             openFileDialog1.FilterIndex = 1;
             openFileDialog1.RestoreDirectory = true;


             try
             {
                 isl = new OrderOperations(this);
                 isl.Grid_Initialize_AlgoTopluSecim(ref gridControl2, ref gridView2);
                 (gridControl2.DataSource as DataTable).Clear();  //* tabloyu temizler.


                 if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                 {
                     if ((myStream = openFileDialog1.OpenFile()) != null)
                     {
                         StreamReader sr = new StreamReader(myStream);
                         string line = sr.ReadLine();
                         while (line != null)
                         {
                             int pos = line.IndexOf(";", 0);
                             string menkul = line.Substring(0, pos).Trim();

                             line = line.Substring(pos + 1, line.Length - (pos + 1));
                             pos = line.IndexOf(";", 0);
                             string lot = line.Substring(0, pos).Trim();
                             lot = Rakam(lot);  //* ',' varsa kaldırır.

                             string buysell = line.Substring(pos + 1, line.Length - (pos + 1)).Trim();

                             Gride_Yaz(menkul, buysell, lot);
                             line = sr.ReadLine();
                         }

                         myStream.Close();
                     }
                 }

                 isl = null;
             }
             catch (Exception ex) { }

         }

     
         //********************************************************************************************************************

         public void Gride_Yaz(string menkul, string buysell, string lot)
         {
             DataRow newrow = (gridControl2.DataSource as DataTable).NewRow();
             newrow["MENKUL"] = menkul;
             newrow["ALSAT"]  = buysell;  //* S=Sell , B=Buy
             newrow["ADET"]   = lot;

             (gridControl2.DataSource as DataTable).Rows.Add(newrow);
         }

         //********************************************************************************************************************


         private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
         {
             e.Column.OptionsColumn.AllowEdit = false;
             e.Column.OptionsColumn.AllowFocus = false;
             e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

             if (e.Column.FieldName == "ADET")
                 e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
             else if (e.Column.FieldName == "ALSAT")
                 e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

         }

         //********************************************************************************************************************


         private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
         {
             GridView currentview = sender as GridView;
             string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "AKIBET"));
             if (tut != "Başarılı")
                 e.Appearance.ForeColor = Color.Red;
             else
                 e.Appearance.ForeColor = Color.Black;

         }

         //********************************************************************************************************************


         private void simpleButton5_Click(object sender, EventArgs e)
         {
             bool TopluEmirGirisimi = true;
             decimal fyt;
             string alsat = "";
             string EmirTipi = "";
             string IslemTipi = "1";    //* 1=Yeni Giriş
             string giden = "";
             string RefId = "";
             string hisse="", mik="";
             string wgib_yuzde1, wgib_yuzde2, wgib_baszmn1, wgib_baszmn2, wgib_baszmn3, wgib_bitzmn1, wgib_bitzmn2, wgib_bitzmn3, wgib_parcasayisi, aktsek = "";
             decimal gibyuzde1 = 0, gibyuzde2 = 0;

             //* Socket kapalı ise çıkış yap.
                bool cikis = new bool();
                cikis = false;
                socket_start();
                if (!client.Connected)
                {
                  cikis = true;
                  this.Close();
                }
                if (cikis == true) return;
             //* Socket kapalı ise çıkış yap.


             if (radioButton1.Checked)
                 EmirTipi = "Z.A";
             else if (radioButton4.Checked)
                 EmirTipi = "G.I.B";


             if (Ekran_Deger_Kontrol(TopluEmirGirisimi))
                 return;

             IslemTipi = "1";
             OrderOperations isl = new OrderOperations(this);

             System.Windows.Forms.DialogResult dialogResult = TopMostMessageBox.Show("TOPLU Algo Emir Girişi yapmaktasınız. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
             if (dialogResult == System.Windows.Forms.DialogResult.No) return;



             if (gib_yuzde1.Text.Length == 0) wgib_yuzde1 = "0"; else wgib_yuzde1 = gib_yuzde1.Text;
             if (gib_yuzde2.Text.Length == 0) wgib_yuzde2 = "0"; else wgib_yuzde2 = gib_yuzde2.Text;
             if (gib_baszmn1.Text.Length == 0) wgib_baszmn1 = "0"; else wgib_baszmn1 = gib_baszmn1.Text;
             if (gib_baszmn2.Text.Length == 0) wgib_baszmn2 = "0"; else wgib_baszmn2 = gib_baszmn2.Text;
             if (gib_baszmn3.Text.Length == 0) wgib_baszmn3 = "0"; else wgib_baszmn3 = gib_baszmn3.Text;
             if (gib_bitzmn1.Text.Length == 0) wgib_bitzmn1 = "0"; else wgib_bitzmn1 = gib_bitzmn1.Text;
             if (gib_bitzmn2.Text.Length == 0) wgib_bitzmn2 = "0"; else wgib_bitzmn2 = gib_bitzmn2.Text;
             if (gib_bitzmn3.Text.Length == 0) wgib_bitzmn3 = "0"; else wgib_bitzmn3 = gib_bitzmn3.Text;
             if (gib_parcasayisi.Text.Length == 0) wgib_parcasayisi = "0"; else wgib_parcasayisi = gib_parcasayisi.Text;
             string gib_enson_aktifolanparca = "0";

             if (radioButton5.Checked)
                 aktsek = "AKTIFE_VER";
             else if (radioButton6.Checked)
                 aktsek = "PASIFE_VER";
             else if (radioButton7.Checked)
                 aktsek = "TAHTAYI_BOYA";
             else if (radioButton8.Checked)
                 aktsek = "SONISLEM_FIYATI";
             else if (radioButton9.Checked)
                 aktsek = "VWAP_IKIISLEMORT";
             else if (radioButton10.Checked)
                 aktsek = "VWAP_SEANSORT";

             label19.Visible = true;
             label19.Refresh();

             for (int i = 0; i < gridView2.RowCount; i++)  //* Toplu Gönderim için Tablodan Okur...
             {

                 RefId = Convert.ToString(isl.YeniRefNoGetir());  //* yeni bir refid alalım.
                 if (gridView2.GetRowCellValue(i, "ALSAT").ToString() == "S")
                     alsat = "DEBIT";
                 else
                     alsat = "CREDIT";

                 hisse = gridView2.GetRowCellValue(i, "MENKUL").ToString();
                 mik = gridView2.GetRowCellValue(i, "ADET").ToString();


                 if (radioButton4.Checked)   //* G.İ.B emir.
                 {
                     if ((gib_yuzde1.Text == null) || (gib_yuzde1.Text.Trim().Length == 0))
                         gibyuzde1 = 0;
                     else
                         gibyuzde1 = Convert.ToDecimal(gib_yuzde1.Text);

                     if ((gib_yuzde2.Text == null) || (gib_yuzde2.Text.Trim().Length == 0))
                         gibyuzde2 = 0;
                     else
                         gibyuzde2 = Convert.ToDecimal(gib_yuzde2.Text);


                     if (Convert.ToInt32(gib_parcasayisi.Text) > Convert.ToInt32(mik))
                     {
                         gridView2.SetRowCellValue(i, "AKIBET", "HATA : Parça sayısı, Lot değerinden büyük olamaz."); 
                         continue;
                     }

                     if (gibyuzde1 > 0)
                     {
                         int herbirparcayadusenlot = Convert.ToInt32(mik) / Convert.ToInt32(gib_parcasayisi.Text);
                         decimal ilkseansta_gonderilecek_toplamlot = Convert.ToInt32(mik) * (gibyuzde1 / 100);
                         decimal ilkseanstaki_herbirparcaninlotu = ilkseansta_gonderilecek_toplamlot / herbirparcayadusenlot;

                         if (ilkseanstaki_herbirparcaninlotu < 1)
                         {
                             gridView2.SetRowCellValue(i, "AKIBET", "HATA : 1. Seansta gönderilecek herbir emir başına düşen lot adedi 1 lotun üzerinde olmalıdır."); 
                             continue;
                         }
                     }

                     if (gibyuzde2 > 0)
                     {
                         int herbirparcayadusenlot = Convert.ToInt32(mik) / Convert.ToInt32(gib_parcasayisi.Text);
                         decimal ikinciseansta_gonderilecek_toplamlot = Convert.ToInt32(mik) * (gibyuzde2 / 100);
                         decimal ikinciseanstaki_herbirparcaninlotu = ikinciseansta_gonderilecek_toplamlot / herbirparcayadusenlot;

                         if (ikinciseanstaki_herbirparcaninlotu < 1)
                         {
                             gridView2.SetRowCellValue(i, "AKIBET", "HATA : 2. Seansta gönderilecek herbir emir başına düşen lot adedi 1 lotun üzerinde olmalıdır."); 
                             continue;
                         }
                     }
                 }



                 try
                 {
                     bool ekrantazele = true;
                     string fininstid = "", custid = "", accid = "";
                     int maxlot = 0;
                     menkul_detay(hisse, ref fininstid, ref maxlot);
                     Musteri_Detay(musteri.Text, ref  custid, ref accid);

                     if ((fininstid.Trim().Length > 0) && (custid.Trim().Length > 0))
                     {
                         SendToTCP(RefId, EmirTipi, BUGUNKUTARIH, TAKASTARIHI, hisse, fininstid, alsat, musteri.Text, custid, accid, mik, Convert.ToString(maxlot), marj.Text, zmn1.Text, zmn2.Text, zmn3.Text, IslemTipi, wgib_yuzde1, wgib_yuzde2, wgib_baszmn1, wgib_baszmn2, wgib_baszmn3, wgib_bitzmn1, wgib_bitzmn2, wgib_bitzmn3, wgib_parcasayisi, gib_enson_aktifolanparca, aktsek, ekrantazele);
                         gridView2.SetRowCellValue(i, "AKIBET", "Başarılı");
                     }
                 }
                 catch (Exception exx) { }

             }

             label19.Visible = false;
             label19.Refresh();

         }


         //********************************************************************************************************************


         private void aktifeVerToolStripMenuItem_Click(object sender, EventArgs e)
         {
             string aktsek = "AKTIFE_VER";
             Kalani_Aktife_Ver(aktsek);
         }

         private void pasifeVerToolStripMenuItem_Click(object sender, EventArgs e)
         {
             string aktsek = "PASIFE_VER";
             Kalani_Aktife_Ver(aktsek);
         }

         private void tahtayıBoyaToolStripMenuItem_Click(object sender, EventArgs e)
         {
             string aktsek = "TAHTAYI_BOYA";
             Kalani_Aktife_Ver(aktsek);

         }

        //********************************************************************************************************************

         public void Kalani_Aktife_Ver(string aktiflesmesekli)
         {
             bool secili;
             int secili_sayisi = 0;
             decimal fyt;
             string giden = "", alsat = "", mik = "", hesapno = "", EmirTipi = "", IslemTipi = "",statu;
             string RefId = "", hisse = "", fininstid = "", custid = "", accid = "", maxlot = "", marjyuzde = "", za_zmn1 = "", za_zmn2 = "", za_zmn3 = "";
             string wgib_yuzde1, wgib_yuzde2, wgib_baszmn1, wgib_baszmn2, wgib_baszmn3, wgib_bitzmn1, wgib_bitzmn2, wgib_bitzmn3, wgib_parcasayisi, aktsek = "";

             //* Socket kapalı ise çıkış yap.
                bool cikis = false;
                socket_start();
                if (!client.Connected)
                {
                    cikis = true;
                    this.Close();
                }
                if (cikis == true) return;
             //* Socket kapalı ise çıkış yap.


             System.Windows.Forms.DialogResult dialogResult = TopMostMessageBox.Show("Seçili Algolar Gönderilecek. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
             if (dialogResult == System.Windows.Forms.DialogResult.No) return;

             aktsek = aktiflesmesekli;
             IslemTipi = "2";  //*  1=Yeni Giriş , 2=Kalanı Aktife Ver , 3=İptal 

             for (int i = 0; i < gridView1.RowCount; i++)
             {
                 secili = Convert.ToBoolean(gridView1.GetRowCellValue(i, "CHECK"));
                 //* statu  = gridView1.GetRowCellValue(ROW, "STATU").ToString();

                 if (secili == true) 
                 {
                     EmirTipi  = gridView1.GetRowCellValue(i, "EMIRTIPI").ToString();
                     RefId     = gridView1.GetRowCellValue(i, "REFERANSID").ToString();
                     hisse     = gridView1.GetRowCellValue(i, "MENKUL").ToString();
                     alsat     = gridView1.GetRowCellValue(i, "ALSAT").ToString();
                     mik       = gridView1.GetRowCellValue(i, "LOT").ToString();
                     fininstid = gridView1.GetRowCellValue(i, "FININSTID").ToString();
                     hesapno   = gridView1.GetRowCellValue(i, "HESAPNO").ToString();
                     custid    = gridView1.GetRowCellValue(i, "CUSTID").ToString();
                     accid     = gridView1.GetRowCellValue(i, "ACCID").ToString();
                     maxlot    = gridView1.GetRowCellValue(i, "MAXLOT").ToString();
                     marjyuzde = gridView1.GetRowCellValue(i, "YUZDE").ToString();
                     za_zmn1   = gridView1.GetRowCellValue(i, "TETIKSAAT").ToString();
                     za_zmn2   = gridView1.GetRowCellValue(i, "TETIKDAKIKA").ToString();
                     za_zmn3   = gridView1.GetRowCellValue(i, "TETIKSANIYE").ToString();


                     if (gridView1.GetRowCellValue(i, "GIB_YUZDE1").ToString().Length == 0) wgib_yuzde1 = "0"; else wgib_yuzde1 = gridView1.GetRowCellValue(i, "GIB_YUZDE1").ToString();
                     if (gridView1.GetRowCellValue(i, "GIB_YUZDE2").ToString().Length == 0) wgib_yuzde2 = "0"; else wgib_yuzde2 = gridView1.GetRowCellValue(i, "GIB_YUZDE2").ToString();
                     if (SifirKoy(gridView1.GetRowCellValue(i, "GIB_BAS_ZMN1_SAAT").ToString(), 2).Length == 0) wgib_baszmn1 = "0"; else wgib_baszmn1 = SifirKoy(gridView1.GetRowCellValue(i, "GIB_BAS_ZMN1_SAAT").ToString(), 2);
                     if (SifirKoy(gridView1.GetRowCellValue(i, "GIB_BAS_ZMN1_DAKIKA").ToString(), 2).Length == 0) wgib_baszmn2 = "0"; else wgib_baszmn2 = SifirKoy(gridView1.GetRowCellValue(i, "GIB_BAS_ZMN1_DAKIKA").ToString(), 2);
                     if (SifirKoy(gridView1.GetRowCellValue(i, "GIB_BAS_ZMN1_SANIYE").ToString(), 2).Length == 0) wgib_baszmn3 = "0"; else wgib_baszmn3 = SifirKoy(gridView1.GetRowCellValue(i, "GIB_BAS_ZMN1_SANIYE").ToString(), 2);
                     if (SifirKoy(gridView1.GetRowCellValue(i, "GIB_BIT_ZMN1_SAAT").ToString(), 2).Length == 0) wgib_bitzmn1 = "0"; else wgib_bitzmn1 = SifirKoy(gridView1.GetRowCellValue(i, "GIB_BIT_ZMN1_SAAT").ToString(), 2);
                     if (SifirKoy(gridView1.GetRowCellValue(i, "GIB_BIT_ZMN1_DAKIKA").ToString(), 2).Length == 0) wgib_bitzmn2 = "0"; else wgib_bitzmn2 = SifirKoy(gridView1.GetRowCellValue(i, "GIB_BIT_ZMN1_DAKIKA").ToString(), 2);
                     if (SifirKoy(gridView1.GetRowCellValue(i, "GIB_BIT_ZMN1_SANIYE").ToString(), 2).Length == 0) wgib_bitzmn3 = "0"; else wgib_bitzmn3 = SifirKoy(gridView1.GetRowCellValue(i, "GIB_BIT_ZMN1_SANIYE").ToString(), 2);
                     if (gridView1.GetRowCellValue(i, "GIB_PARCASAYISI").ToString().Length == 0) wgib_parcasayisi = "0"; else wgib_parcasayisi = gridView1.GetRowCellValue(i, "GIB_PARCASAYISI").ToString();
                     string gib_enson_aktifolanparca = "0";

                     try
                     {
                         bool ekrantazele = false;
                         SendToTCP(RefId, EmirTipi, BUGUNKUTARIH, TAKASTARIHI, hisse, fininstid, alsat, hesapno, custid, accid, mik, maxlot, marjyuzde, za_zmn1, za_zmn2, za_zmn3, IslemTipi, wgib_yuzde1, wgib_yuzde2, wgib_baszmn1, wgib_baszmn2, wgib_baszmn3, wgib_bitzmn1, wgib_bitzmn2, wgib_bitzmn3, wgib_parcasayisi, gib_enson_aktifolanparca, aktsek, ekrantazele);
                     }
                     catch (Exception exx)
                     { }

                     secili_sayisi++;
                 }
             }

             if (secili_sayisi == 0)
             {
                 TopMostMessageBox.Show("Tick'leyerek emir veya emirleri seçmelisiniz !");
                 return;
             }

             TopMostMessageBox.Show("İşleme Konuldu.");
             System.Threading.Thread.Sleep(200); 
             EkranTazele();
         }

         //********************************************************************************************************************

         private void checkBox1_Click(object sender, EventArgs e)
         {
             string statu;
             if (checkBox1.Checked == true)
             {
                 for (int i = 0; i < gridView1.RowCount; i++)
                 {
                    // statu = gridView1.GetRowCellValue(i, "STATU").ToString();
                    // if (statu == "Bekliyor")
                         gridView1.SetRowCellValue(i, "CHECK", true);
                 }
             }
             else
             {
                 for (int i = 0; i < gridView1.RowCount; i++)
                 {
                     gridView1.SetRowCellValue(i, "CHECK", false);
                 }
             }

         }

         //********************************************************************************************************************

         private void timer1_Tick(object sender, EventArgs e)
         {
             try
             {
                 //* Socket kapalı ise çıkış yap.
                 bool cikis = new bool();
                 cikis = false;
                 socket_start();
                 if (!client.Connected)
                 {
                     cikis = true;
                     this.Close();
                 }
                 if (cikis == true) return;
                 //* Socket kapalı ise çıkış yap.

                 SendToServer("HBT%");
             }
             catch (Exception ex) { }
         }

         private void lot_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
         {
                
             if ((e.KeyValue < 48) || (e.KeyValue > 57))
             {
                     lot.Text = "";
                     // reset cursor position to the end of the text (replacing the text will place
                     // the cursor at the start)
                 
                     lot.Select(lot.Text.Length, 0);
                     TopMostMessageBox.Show("Lot alanına yanlızca rakam giriniz.");
             }

         }

     

        //********************************************************************************************************************
         private string Rakam(string sayi)
         {
             string rc = "";
             int len = sayi.Trim().Length;
             for (int i = 0; i < len; i++)
             {
                 if (sayi[i] != ',')
                     rc = rc + sayi[i];
             }

             return rc.Trim();
         }

    }


}
