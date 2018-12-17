using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Gtp.Framework.ControlLibrary;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;


namespace Gtp.Global.ICM.Ui
{
    public partial class BasketOrders : Form
    {
        Gtp.Global.ICM.Ui.OrderManagement frmana;
        string BUGUNKUTARIH, TAKASTARIHI;

        public BasketOrders()
        {
            InitializeComponent();
        }

        public BasketOrders(Gtp.Global.ICM.Ui.OrderManagement frm)
        {
            InitializeComponent();
            frmana = frm;
        }

        private void BasketOrders_Load(object sender, EventArgs e)
        {
            Musteri_Doldur();
         
        }

   //*****************************************************************************

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

     //*****************************************************************************

        private void simpleButton1_Click(object sender, EventArgs e)
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
                isl.Grid_Initialize_Basket(ref gridControl1, ref gridView1);
                (gridControl1.DataSource as DataTable).Clear();  //* tabloyu temizler.


               if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               {
                  if ((myStream = openFileDialog1.OpenFile()) != null)
                  {
                      StreamReader sr = new StreamReader(myStream);
                      string line = sr.ReadLine();
                      while (line != null)
                      {
                        int pos = line.IndexOf(";", 0);
                        string menkul = line.Substring(0,pos).Trim();

                        line = line.Substring(pos + 1, line.Length - (pos + 1));
                        pos = line.IndexOf(";", 0);
                        string lot = line.Substring(0,pos).Trim();

                        string buysell = line.Substring(pos+1,line.Length-(pos+1)).Trim();

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

     //*****************************************************************************

        public void Gride_Yaz(string menkul, string buysell, string lot)
        {
                DataRow newrow = (gridControl1.DataSource as DataTable).NewRow();
                newrow["MENKUL"] = menkul;
                newrow["ALSAT"]  = buysell;  //* S=Sell , B=Buy
                newrow["ADET"]   = lot;
                newrow["AKIBET"] = "";

                (gridControl1.DataSource as DataTable).Rows.Add(newrow);

        }

        //********************************************************************************************************************

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView currentview = sender as GridView;
            string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ALSAT"));
            if (tut == "S")
                e.Appearance.ForeColor = Color.Red;
            else
                e.Appearance.ForeColor = Color.Green;

        }


       //*******************************************************************************************************************

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataTable Tablo1;
            string BugunkuTarih = "", TakasTarihi = "", alsat="", menkul="", debitcredit = "", fininstid="", custId="", accId="" , donus="";
            string EquityTransactionTypeId = "0000-000001-ETT";  //* defaultu LOT olsun.
            string OrderType               = "0000-000001-ETT";
            string TimeInForce             = "0";   //Gün
            decimal adet, fyt ;
            Int32 maxlot=0;
            Int32 InitialMarketSessionSel=1, EndingMarketSessionSel=2;

            int satirsayisi = gridView1.RowCount;
            if (satirsayisi == 0)
            {
                System.Windows.Forms.MessageBox.Show("Aktarım yapılacak hiçbir kayıt bulunamadı.");
                return;
            }

            if ((musteri.Text == null) || (musteri.Text.Trim().Length == 0))
            {
                System.Windows.Forms.MessageBox.Show("Müşteri seçiniz.");
                return;
            }

            if ( (!piyasa.Checked) && (!piyasadanlimite.Checked) && (!denge.Checked) )
            {
                System.Windows.Forms.MessageBox.Show("'Piyasa , Piyasadan Limite , Denge' birini seçiniz ! ");
                return;
            }


            if (piyasa.Checked)
                     TimeInForce = "3";    //* Piyasa emrinde TimeInForce=3 KİE olmalı.
            else if (piyasadanlimite.Checked)
            {
                 if(gunluk.Checked)
                     TimeInForce = "0";    //* TimeInForce=0 günlük yaptım.  TimeInForce=3 KİE de olabilirdi
                 else if(kie.Checked)
                     TimeInForce = "3";   //* Piyasa emrinde TimeInForce=3 KİE olmalı.
            }
            else if (denge.Checked)
                TimeInForce = "9";        //* Denge emirlerinde TimeInForce=9 EFG olmalı


            System.Windows.Forms.DialogResult dialogResult2 = System.Windows.Forms.MessageBox.Show("Toplu Aktarım İşlemi Yapıyorsunuz. İşlemi Onaylıyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult2 == System.Windows.Forms.DialogResult.No)
                return;


            OrderOperations op = new OrderOperations();
            op.TarihBilgileri_Al(this, ref BugunkuTarih, ref TakasTarihi);

            string hangiseanstayiz = op.HangiSeanstayiz();   //* sql server üzerinden zamanı alalım.
            if (hangiseanstayiz == "1")             //* Eğer 1. seanstaysak 
            {
                InitialMarketSessionSel = 1;
                EndingMarketSessionSel = 2;
            }
            else if (hangiseanstayiz == "2")        //* 2. seanstaysak
            {
                InitialMarketSessionSel = 1;
                EndingMarketSessionSel = 1;
            }


            if (piyasa.Checked)
            {
                EquityTransactionTypeId = "0000-000010-ETT"; 
                OrderType               = "0000-000010-ETT";
            }
            else if (piyasadanlimite.Checked)
            {
                EquityTransactionTypeId = "0000-000011-ETT";
                OrderType               = "0000-000011-ETT";
            }
            else if (denge.Checked)
            {
                EquityTransactionTypeId = "0000-000012-ETT";
                OrderType               = "0000-000012-ETT";
            }

            label2.Visible = true;
            label2.Refresh();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                alsat  = gridView1.GetRowCellValue(i, "ALSAT").ToString();
                menkul = gridView1.GetRowCellValue(i, "MENKUL").ToString();

                if (alsat == "S")
                    debitcredit = "DEBIT";
                else
                    debitcredit = "CREDIT";

                Tablo1 = op.FiyatKademeListesi_Al(this, menkul);
                if (Tablo1.Rows.Count > 0)   //* sadece ilk recordu alalım. En son seansa aitttir.
                {
                    fininstid = Convert.ToString(Tablo1.Rows[0]["FIN_INST_ID"]);
                    maxlot    = Convert.ToInt32(Tablo1.Rows[0]["MAX_LOT"]);
                }

                op.MusteriBilgisi_Detay(this, musteri.Text, ref custId, ref accId);
                fyt  = Convert.ToDecimal("0");
                adet = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ADET").ToString());

                donus = op.Save_Equity_Order(BugunkuTarih, TakasTarihi, fininstid, debitcredit, custId, accId, maxlot, fyt, adet, InitialMarketSessionSel, EndingMarketSessionSel, EquityTransactionTypeId, OrderType, TimeInForce);
                if(donus.Trim().Length==0)
                   gridView1.SetRowCellValue(i, "AKIBET", "OK"); 
                else
                   gridView1.SetRowCellValue(i, "AKIBET", donus); 
            }

            label2.Visible = false;
            label2.Refresh();
        }

        private void piyasa_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            if (piyasa.Checked)
            {
                gunluk.Visible = true;
                kie.Visible    = false;
                efg.Visible    = false;
                gunluk.Checked = true;
            }
        }

        private void piyasadanlimite_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            if (piyasadanlimite.Checked)
            {
                gunluk.Visible = true;
                kie.Visible    = true;
                efg.Visible    = false;
                gunluk.Checked = true;
            }
        }

        private void denge_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            if (denge.Checked)
            {
                gunluk.Visible= false;
                kie.Visible   = false;
                efg.Visible   = true;
                efg.Checked   = true;
            }
        }

        //********************************************************************************************************************




    }
}
