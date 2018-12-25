/* code insight  test mahmut cirtek*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

// using System.Windows.Forms;
using Gtp.Framework;
using Gtp.Framework.ControlLibrary;
using Gtp.Global.ICM.Ui;

namespace Gtp.Global.ICM.Ui
{
    public partial class AlSat : Form
    {
        Gtp.Global.ICM.Ui.OrderManagement frmana;
        string FIN_INST_ID, CUSTID, ACCID, BUGUNKUTARIH, TAKASTARIHI;
        public string ISLEM ;
        Int32 MAX_LOT;
        public bool EMIRONAYLANDI;

        public AlSat()
        {
            InitializeComponent();
        }

        public AlSat(Gtp.Global.ICM.Ui.OrderManagement frm)
        {
            InitializeComponent();
            frmana = frm;
        }

        
        //********************************************************************************************************************

        private void AlSat_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");

            Tarih_Al();
            Musteri_Doldur();
            Menkul_Doldur();
        }

        //********************************************************************************************************************

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
            if (tabControl1.SelectedIndex == 0)
            {
                ISLEM = "A";
                panel3.BackColor = Color.MediumSeaGreen;
                this.Text = "ALIŞ                         ";
            }
            else
            {
                ISLEM = "S";
                panel3.BackColor = Color.Red;
                this.Text = "SATIŞ                         ";
            }
           
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

        private void musteri_EditValueChanged(object sender, EventArgs e)
        {
            string CustId = "", AccId="" ;
            OrderOperations op = new OrderOperations();
            op.MusteriBilgisi_Detay(this, musteri.Text , ref CustId, ref AccId);
            CUSTID = CustId;
            ACCID  = AccId;
         
        }

        //********************************************************************************************************************

        public void Tarih_Al()
        {
            string BugunkuTarih = "", TakasTarihi = "";
            OrderOperations op = new OrderOperations();
            op.TarihBilgileri_Al(this, ref BugunkuTarih, ref TakasTarihi);
            BUGUNKUTARIH = BugunkuTarih;
            TAKASTARIHI = TakasTarihi;
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

        private void menkul_Leave(object sender, EventArgs e)
        {
            DataTable Tablo1;
            decimal degfiyat, tavan, taban, kademe, alis, satis, sonfiy;

            OrderOperations op = new OrderOperations();
            Tablo1 = op.FiyatKademeListesi_Al(this, menkul.Text);

            fiyat.Text = "";
            fiyat.Properties.Items.Clear();

            if (Tablo1.Rows.Count > 0)   //* sadece ilk recordu alalım. En son seansa aitttir.
            {
                FIN_INST_ID = Convert.ToString(Tablo1.Rows[0]["FIN_INST_ID"]);
                MAX_LOT = Convert.ToInt32(Tablo1.Rows[0]["MAX_LOT"]);

                sonfiy = Convert.ToDecimal(Tablo1.Rows[0]["VALUE1"]);
                alis = Convert.ToDecimal(Tablo1.Rows[0]["VALUE3"]);
                satis = Convert.ToDecimal(Tablo1.Rows[0]["VALUE2"]);
                taban = Convert.ToDecimal(Tablo1.Rows[0]["VALUE10"]);
                tavan = Convert.ToDecimal(Tablo1.Rows[0]["VALUE11"]);
                // kademe = Convert.ToDecimal(Tablo1.Rows[0]["VALUE12"]);
                kademe = Kademe_Bul(sonfiy);

                degfiyat = taban;
                while (degfiyat <= tavan)
                {
                    fiyat.Properties.Items.Add(string.Format("{0:#,0.000}", degfiyat));
                    degfiyat += kademe;
                }

                if (tabControl1.SelectedIndex == 0)
                {
                    this.Text = "ALIŞ - " + menkul.Text + "                         ";
                    fiyat.EditValue = string.Format("{0:#,0.000}", alis);
                }
                else
                {
                    this.Text = "SATIŞ - " + menkul.Text + "                         ";
                    fiyat.EditValue = string.Format("{0:#,0.000}", satis);
                }
            }

            fiyat.Enabled = true;

            tip.SelectedIndex = 0;
            gecerlilik.Text = "";
            gecerlilik.Properties.Items.Clear();
            gecerlilik.Properties.Items.Add("Gün");
            gecerlilik.Properties.Items.Add("KİE");
            gecerlilik.SelectedIndex = 0;

        }


        //********************************************************************************************************************

        private decimal Kademe_Bul(decimal sonfiy)
        {
            decimal kademe = new decimal(0.01);

            if ((sonfiy > new decimal(0.01)) && (sonfiy <= new decimal(19.99)))
                kademe = new decimal(0.01);
            else if ((sonfiy >= new decimal(20)) && (sonfiy <= new decimal(49.98)))
                kademe = new decimal(0.02);
            else if ((sonfiy >= new decimal(50)) && (sonfiy <= new decimal(99.95)))
                kademe = new decimal(0.05);
            else if (sonfiy >= new decimal(100))
                kademe = new decimal(0.10);

            return kademe;
        }

        //********************************************************************************************************************



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            decimal mik, fyt;
            string alsat = "" ;
            Int32 InitialMarketSessionSel = 1, EndingMarketSessionSel = 2 ;
            string EquityTransactionTypeId = "0000-000001-ETT";  //* defaultu LOT olsun.
            string OrderType = "0000-000001-ETT";
            string TimeInForce = "0"; //Gün


            if (Ekran_Deger_Kontrol())
                return;

            if (ISLEM == "A")
                alsat = "CREDIT";
            else if (ISLEM == "S")
                alsat = "DEBIT";

            if ((fiyat.Text == null) || (fiyat.Text.Trim().Length == 0))
                fyt = 0;
            else fyt = Convert.ToDecimal(fiyat.Text);

           
            mik = Convert.ToDecimal(lot.Text);

            OrderOperations op = new OrderOperations(this);
            string hangiseanstayiz = op.HangiSeanstayiz();   //* sql server üzerinden zamanı alalım.

            //decimal zaman = Convert.ToDecimal(DateTime.Now.ToString("HHmmss"));
            //if (zaman < 123000)
            //    hangiseanstayiz = 1;
            //else hangiseanstayiz = 2;

            if (hangiseanstayiz == "1")             //* Eğer 1. seanstaysak 
            {
                 InitialMarketSessionSel = 1;
                 EndingMarketSessionSel  = 2;
            }
            else if (hangiseanstayiz == "2")        //* 2. seanstaysak
            {
                 InitialMarketSessionSel = 1;
                 EndingMarketSessionSel  = 1;
            }

            

            if (tip.Text == "Limit")
            {
                EquityTransactionTypeId = "0000-000001-ETT";  //LOT
                OrderType = "0000-000001-ETT";                // Limitli
            }
            else if (tip.Text == "Piyasa")
            {
                EquityTransactionTypeId = "0000-000010-ETT"; 
                OrderType = "0000-000010-ETT";
            }
            else if (tip.Text == "Piyasadan Limite")
            {
                EquityTransactionTypeId = "0000-000011-ETT";
                OrderType = "0000-000011-ETT";
            }
            else if (tip.Text == "Denge")
            {
                EquityTransactionTypeId = "0000-000012-ETT";
                OrderType = "0000-000012-ETT";
            }

            if (gecerlilik.Text == "Gün")
                TimeInForce = "0";
            else if (gecerlilik.Text == "KİE")
                TimeInForce = "3";
            else if (gecerlilik.Text == "EFG")
                TimeInForce = "9";


            decimal ekrlot;
            if ((fiyat.Text == null) || (fiyat.Text.Trim().Length == 0))
                ekrlot = 0;
            else
                ekrlot = Convert.ToDecimal(fiyat.Text);

            if (ekrlot == 0)  
            {
                if ((tip.Text == "Limit") && (gecerlilik.Text == "Gün"))
                {
                    System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Serbest Fiyatlı emir girişi yapmaktasınız. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.No)
                        return;
                }
            }


            AlSatTeyit tyt = new AlSatTeyit(this);
            tyt.E1.Text = BUGUNKUTARIH;
            tyt.E2.Text = TAKASTARIHI;
            //* tyt.E3.Text = seans.Text;
            tyt.E3.Text = "1";
            tyt.E4.Text = menkul.Text;
            tyt.E5.Text = fiyat.Text;
            tyt.E6.Text = lot.Text;
            tyt.E7.Text = Convert.ToString(fyt*mik);
            if (ISLEM == "A")
            {
                tyt.label6.Text = "    A L I Ş";
                tyt.panel1.BackColor = Color.MediumSeaGreen;
            }
            else if (ISLEM == "S")
            {
                tyt.label6.Text = "   S A T I Ş";
                tyt.panel1.BackColor = Color.Red;
            }

            tyt.BUGUNKUTARIH = BUGUNKUTARIH;
            tyt.TAKASTARIHI  = TAKASTARIHI;
            tyt.FIN_INST_ID  = FIN_INST_ID;
            tyt.alsat        = alsat;
            tyt.CUSTID       = CUSTID;
            tyt.ACCID        = ACCID;
            tyt.MAX_LOT      = MAX_LOT;
            tyt.fyt          = fyt;
            tyt.mik          = mik;
            tyt.InitialMarketSessionSel = InitialMarketSessionSel;
            tyt.EndingMarketSessionSel  = EndingMarketSessionSel;
            tyt.EquityTransactionTypeId = EquityTransactionTypeId;

            EMIRONAYLANDI = false;
            tyt.ShowDialog();
            if (EMIRONAYLANDI)
            {
                string donus = op.Save_Equity_Order(BUGUNKUTARIH, TAKASTARIHI, FIN_INST_ID, alsat, CUSTID, ACCID, MAX_LOT, fyt, mik, InitialMarketSessionSel, EndingMarketSessionSel, EquityTransactionTypeId,OrderType,TimeInForce);
                if (donus.Trim().Length == 0)
                {

                    if (frmana != null)
                    {
                      frmana.HisseAlSat_Ekran_Tazele();
                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("İşleme Konulmadı. " + donus);
                    return;
                }
            }

        }



        private bool Ekran_Deger_Kontrol()
        {
            decimal mik, fyt;

            if ((BUGUNKUTARIH == null) || (BUGUNKUTARIH.Trim().Length == 0))
            {
                System.Windows.Forms.MessageBox.Show("Yanlızca bugünkü işlem gününe emir girmelisiniz !");
                return true;
            }

            if ( (musteri.Text ==null) || (musteri.Text.Trim().Length==0) )
            {
                System.Windows.Forms.MessageBox.Show("Müşteri seçiniz.");
                return true;
            }

            if ((menkul.Text == null) || (menkul.Text.Trim().Length == 0))
            {
                System.Windows.Forms.MessageBox.Show("Menkul seçiniz.");
                return true;
            }


            if ((lot.Text == null) || (lot.Text.Trim().Length == 0))
            {
                System.Windows.Forms.MessageBox.Show("Miktar(Lot) değeri giriniz.");
                return true;
            }


            if ((fiyat.Text == null) || (fiyat.Text.Trim().Length == 0))
                    fyt = 0;
            else    fyt = Convert.ToDecimal(fiyat.Text);



            return false;
        }



        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void fiyat_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyValue == 110) || (e.KeyValue == 188))   // ',' karekteri
            {
                if (fiyat.Text.Contains(","))
                {
                    fiyat.Text = fiyat.Text.Replace(',', '.');
                    // reset cursor position to the end of the text (replacing the text will place
                    // the cursor at the start)
                    fiyat.Select(fiyat.Text.Length, 0);
                }
            }
        }

      //********************************************************************************************
    
        private void tip_EditValueChanged(object sender, EventArgs e)
        {
            gecerlilik.Text = "";
            gecerlilik.Properties.Items.Clear();

            if (tip.Text == "Limit")
            {
                gecerlilik.Properties.Items.Add("Gün");
                gecerlilik.Properties.Items.Add("KİE");
                gecerlilik.SelectedIndex = 0;
                fiyat.Enabled = true;
                menkul_Leave(sender, e);
            }
            else if (tip.Text == "Piyasa")
            {
                gecerlilik.Properties.Items.Add("KİE");
                gecerlilik.SelectedIndex = 0;
                fiyat.Text = "";
                fiyat.Enabled = false;
            }
            else if (tip.Text == "Piyasadan Limite")
            {
                gecerlilik.Properties.Items.Add("Gün");
                gecerlilik.Properties.Items.Add("KİE");
                gecerlilik.SelectedIndex = 0;
                fiyat.Text = "";
                fiyat.Enabled = false;
            }
            else if (tip.Text == "Denge")
            {
                gecerlilik.Properties.Items.Add("EFG");
                gecerlilik.SelectedIndex = 0;
                fiyat.Text = "";
                fiyat.Enabled = false;
            }


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



    }
}


﻿/* code in github.com */
