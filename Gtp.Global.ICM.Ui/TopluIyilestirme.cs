using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Gtp.Framework;
using Gtp.Framework.ControlLibrary;
using Gtp.Global.ICM.Ui;
using DevExpress.XtraGrid.Views.Grid;
using System.Threading;
using System.Globalization;

namespace Gtp.Global.ICM.Ui
{
    public partial class TopluIyilestirme : Form
    {
        int ROW;
        Gtp.Global.ICM.Ui.OrderManagement frmana;
        public decimal FIYAT;
        public int SECIMDURUMU;

        public TopluIyilestirme()
        {
            InitializeComponent();
        }

        public TopluIyilestirme(Gtp.Global.ICM.Ui.OrderManagement frm)
        {
            InitializeComponent();
            frmana = frm;
        }

     //********************************************************************************************************************

        private void TopluIyilestirme_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            PasiftekiEmirler_Islem();
            yenifiyat.Focus();
        }

     //********************************************************************************************************************

        public void Danisman_Doldur()
        {
            DataTable Tablo1;
            string str = " SELECT 'HEPSİ' AS NAME , '000' AS ID";
            str += " UNION ";
            str += " SELECT 'FIX Kullanıcısı' AS NAME , '0000-00001N-PTY' AS ID";
            str += " UNION ";
            str += " SELECT P.NAME AS NAME, PARTY_ID AS ID  from GTPBS_POSITIONS P, GTPBS_EMPLOYEE_POSITIONS E, GTPBS_PARTIES PT WHERE PT.ENTITY_ID = E.EMP_ID AND PARTY_TYPE = 'EMPLOYEE' AND (P.DIV_ID = '0000-00001G-DIV' or P.NAME='FIX Kullanıcısı') AND PT.STATUS = 1 AND P.STATUS = 1 AND E.STATUS=1 And E.POSTN_ID=P.POSTN_ID ORDER BY ID";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            danisman.Properties.DataSource = Tablo1;
            danisman.Properties.DisplayMember = "NAME";
            danisman.Properties.ValueMember = "ID";
            danisman.ItemIndex = 0;
        }


        //********************************************************************************************************************


        public void PasiftekiEmirler_Islem()
        {
            string alissatis = "H", seans = "0", hisse = "";
            try
            {
                
                OrderOperations isl = new OrderOperations(this);
                isl.Grid_Initialize1_Replacement(ref gridControl1, ref gridView1, chkedt, this);
               
                string hangiseanstayiz = isl.HangiSeanstayiz();

                hisse     = Emenkul.EditValue.ToString();
                alissatis = Ealsat.EditValue.ToString();

                if (SECIMDURUMU == 3)  //*Tüm müşteriler ise
                    Ehesapno.Text = "000";

                OrderList beklist = isl.BekleyenEmirler(Ehesapno.EditValue.ToString(), danisman.EditValue.ToString(), hisse, alissatis, seans,frmana.SESSIONDATE);
                List<Order> lst = beklist.Resultlist;
                (gridControl1.DataSource as DataTable).Clear();  //* tabloyu temizler.

                E4.Text = "0";
                foreach (Order a in lst)
                {
                    if (a.Ordinodurumu == "İptal")
                        continue;

                    if (a.Tip != "Limit")   //* sadece Limit emirlerde fiyat alanı doludur. Diğerlerinde boştur, dolayısıyla iyileştirme olmaz.
                       continue;

                    if(SECIMDURUMU==2)
                    {
                        if ((FIYAT != null) && (FIYAT > 0))
                              if (a.Fiyat != FIYAT)
                                 continue;
                    }


                    // PasiftekiEmirler_Doldur(a.Saat, a.Hesap, a.Adsoy, a.Menkul, a.Alsat, a.Lot, a.Fiyat, a.Ordinodurumu, a.Seans, a.Danismankodu, a.Transactionid, a.FinInstid, a.Customerid, a.Accountid, a.Gecerlilik, a.Maximumlot, a.Tip, a.Initialmarketsessiondate, a.Endingmarketsessiondate, a.Settlementdate);
                    E4.Text =  Convert.ToString( Convert.ToDecimal(E4.Text) + a.Lot );
                    DataRow newrow = (gridControl1.DataSource as DataTable).NewRow();
                    newrow["CHECK"]  = true;
                    newrow["HESAP"]  = a.Hesap;
                    newrow["MENKUL"] = a.Menkul;
                    newrow["LOT"]    = a.Lot;
                    newrow["FIYAT"]  = a.Fiyat;
                    newrow["SAAT"]   = a.Saat;

                    newrow["ALSAT"] = a.Alsat;
                    newrow["SEANS"] = a.Seans;
                    newrow["GECERLILIK"] = a.Gecerlilik;

                    newrow["TRANSACTIONID"] = a.Transactionid;
                    newrow["FININSTID"]     = a.FinInstid;
                    newrow["CUSTOMERID"]    = a.Customerid;
                    newrow["ACCOUNTID"]     = a.Accountid;

                    newrow["MAXIMUMLOT"]    = a.Maximumlot;
                    newrow["TIP"] = a.Tip;                                                                  //*2015-11-10 00:00:00.000
                    newrow["INITIAL_MARKET_SESSION_DATE"] = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", a.Initialmarketsessiondate);  // Convert.ToString(initialmarketsessiondate);
                    newrow["ENDING_MARKET_SESSION_DATE"]  = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", a.Endingmarketsessiondate);    // Convert.ToString(endingmarketsessiondate);
                    newrow["SETTLEMENT_DATE"]             = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", a.Settlementdate);            // Convert.ToString(settlementdate);
                    newrow["INITIAL_MARKET_SESSION_SEL"]  = a.Initialmarketsessionsel;
                    newrow["ENDING_MARKET_SESSION_SEL"]   = a.Endingmarketsessionsel;
                    newrow["LAK"] = a.Lak;

                    (gridControl1.DataSource as DataTable).Rows.Add(newrow);

                }


                isl = null;
                beklist = null;
                lst = null;
            }
            catch (Exception ex) { }

        }


        //********************************************************************************************************************


        internal GtpXml ExecCustomSQL(string SQLText)
        {
            GtpXml g = new GtpXml("GLB_CRM_EXEC_CUSTOM_SQL", "1.0");
            g.AddParameter("SQLText", SQLText);
            return frmana.RbmInvoke(g);
        }

        //********************************************************************************************************************

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "CHECK")
            {
                e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Column.OptionsColumn.AllowEdit  = true;
                e.Column.OptionsColumn.AllowFocus = true;
            }
            else 
            {
                e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                e.Column.OptionsColumn.AllowEdit  = false;
                e.Column.OptionsColumn.AllowFocus = false;
            }

        }

        //******************************************************************************************************

        private void chkedt_CheckedChanged(object sender, EventArgs e)
        {
                bool secili;
                secili = Convert.ToBoolean(gridView1.GetRowCellValue(ROW, "CHECK"));
                if (secili == false)
                {
                    E4.Text = Convert.ToString(Convert.ToDecimal(E4.Text) + Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "LOT").ToString()));
                    gridView1.SetRowCellValue(ROW, "CHECK", true);
                }
                else
                {
                    E4.Text = Convert.ToString(Convert.ToDecimal(E4.Text) - Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "LOT").ToString()));
                    gridView1.SetRowCellValue(ROW, "CHECK", false);
                }
           
        }

       //******************************************************************************************************

        private void gridView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(System.Windows.Forms.Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                ROW = info.RowHandle;
            }

        }

      //********************************************************************************************************************

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PasiftekiEmirler_Islem();
        }

      //********************************************************************************************************************


        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Close();
        }

       //********************************************************************************************************************

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Check All";
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.SetRowCellValue(i, "CHECK", false);
                }
            }
            else
            {
                checkBox1.Text = "UnCheck All";
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.SetRowCellValue(i, "CHECK", true);
                }

            }


        }

        //********************************************************************************************************************


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string TransactionId, alsat, debitcredit = "", fininstid, customerid, accountid, valuedate, initialmarketdate, tip, gecerlilik,lak;
            decimal units, price;
            Int32 initialMarketSessionSel, EndingMarketSessionSel, orderMaxlot;
            string taban="0", tavan="0";

            bool secili ;
            decimal ekrlot;

            int satirsayisi = gridView1.RowCount;
            if (satirsayisi == 0)
            {
                System.Windows.Forms.MessageBox.Show("İyileştirme yapılacak seçili hiçbir kayıt bulunamadı.");
                return;
            }

            if ((yenifiyat.Text == null) || (yenifiyat.Text.Trim().Length == 0))
                ekrlot = 0;
            else
                ekrlot= Convert.ToDecimal(yenifiyat.Text);

            OrderOperations op = new OrderOperations(this);

            if (ekrlot == 0)
            {
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Serbest Fiyatlı emir girişi yapmaktasınız. Devam etmek istiyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                    return;

                DataTable Tablo1 = op.FiyatKademeListesi_Al(this, Emenkul.Text);
                for (int i = 0; i < Tablo1.Rows.Count; i++)
                {
                    taban = Tablo1.Rows[i]["VALUE10"].ToString();
                    tavan = Tablo1.Rows[i]["VALUE11"].ToString();
                }

            }


            System.Windows.Forms.DialogResult dialogResult2 = System.Windows.Forms.MessageBox.Show("Toplu İyileştirme İşlemi Yapıyorsunuz. İşlemi Onaylıyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult2 == System.Windows.Forms.DialogResult.No)
                return;


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                secili = Convert.ToBoolean(gridView1.GetRowCellValue(i, "CHECK"));
                if (secili == true)
                {
                         TransactionId = gridView1.GetRowCellValue(i, "TRANSACTIONID").ToString();
                         alsat = gridView1.GetRowCellValue(i, "ALSAT").ToString();

                         if (alsat == "A")
                             debitcredit = "CREDIT";
                         else if (alsat == "S")
                             debitcredit = "DEBIT";

                         if (yenifiyat.Text.Trim().Length == 0)  //*Eğer serbest fiyatlı emir ise Alışta:tavan , Satışta:taban fiyat uygula.
                         {
                             if (alsat == "A")
                                yenifiyat.Text = tavan;
                             else if (alsat == "S")
                                yenifiyat.Text = taban;
                         }

                         fininstid = gridView1.GetRowCellValue(i, "FININSTID").ToString();
                         units = Convert.ToDecimal(gridView1.GetRowCellValue(i, "LOT").ToString());
                         price = Convert.ToDecimal(yenifiyat.Text);
                         customerid = gridView1.GetRowCellValue(i, "CUSTOMERID").ToString();
                         accountid = gridView1.GetRowCellValue(i, "ACCOUNTID").ToString();
                         initialmarketdate = gridView1.GetRowCellValue(i, "INITIAL_MARKET_SESSION_DATE").ToString();
                         valuedate = gridView1.GetRowCellValue(i, "SETTLEMENT_DATE").ToString();

                         initialMarketSessionSel =  Convert.ToInt32(gridView1.GetRowCellValue(i, "INITIAL_MARKET_SESSION_SEL").ToString()) ;
                         EndingMarketSessionSel  =  Convert.ToInt32(gridView1.GetRowCellValue(i, "ENDING_MARKET_SESSION_SEL").ToString()); ;   

                         orderMaxlot = Convert.ToInt32(gridView1.GetRowCellValue(i, "MAXIMUMLOT").ToString());
                         tip = gridView1.GetRowCellValue(i, "TIP").ToString();
                         gecerlilik = gridView1.GetRowCellValue(i, "GECERLILIK").ToString();
                         lak = gridView1.GetRowCellValue(i, "LAK").ToString();


                         bool OrdinoBorsayaIletildimi = op.OrdinoBorsayaIletildimi(TransactionId); //* Sistemde bekliyor , yada borsada...
                         if (OrdinoBorsayaIletildimi == false)
                             Sistemden_Iyilestir(TransactionId, debitcredit, fininstid, units, price, customerid, accountid, valuedate, initialmarketdate, initialMarketSessionSel, EndingMarketSessionSel, orderMaxlot, tip,gecerlilik,lak);
                         else
                             Borsadan_Iyilestir(TransactionId, price, units, units, gecerlilik, initialmarketdate);  

                }
            }

            System.Threading.Thread.Sleep(500);
            FIYAT = Convert.ToDecimal(yenifiyat.Text);
            yenifiyat.Text = "";
            frmana.HisseAlSat_Ekran_Tazele();
            PasiftekiEmirler_Islem();

            //* Close();

        }

       
        //********************************************************************************************************************


        public void Sistemden_Iyilestir(string TransactionId, string debitcredit, string fininstid, decimal units, decimal price, string customerid, string accountid, string valuedate, string initialmarketdate, int initialMarketSessionSel, int EndingMarketSessionSel, int orderMaxlot, string tip, string gecerlilik,string lak)
        {
            OrderOperations op = new OrderOperations(this);
            string donus = op.Update_Equity_Order(TransactionId, debitcredit, fininstid, units, price, customerid, accountid, valuedate, initialmarketdate, initialMarketSessionSel, EndingMarketSessionSel, orderMaxlot,tip, gecerlilik,lak);
            if (donus.Trim().Length > 0)
            {
                System.Windows.Forms.MessageBox.Show("İşleme Konulmadı. " + donus);
                return;
            }

        }

        //********************************************************************************************************************

        public void Borsadan_Iyilestir(string TransactionId, decimal improveprice, decimal improveunits, decimal oldunits, string gecerlilik, string initialmarketdate)
        {
            OrderOperations op = new OrderOperations(this);
            string donus = op.Save_Improve_Order(TransactionId, improveprice, improveunits, oldunits, gecerlilik, initialmarketdate);
        }


        //********************************************************************************************************************

        private void yenifiyat_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyValue == 110) || (e.KeyValue == 188))   // ',' karekteri
            {
                if (yenifiyat.Text.Contains(","))
                {
                    yenifiyat.Text = yenifiyat.Text.Replace(',', '.');
                    // reset cursor position to the end of the text (replacing the text will place
                    // the cursor at the start)
                    yenifiyat.Select(yenifiyat.Text.Length, 0);
                }
            }
            else if (e.KeyValue == 13)   //*Enter tuşu
            {
                simpleButton1.Focus();     
                simpleButton1_Click(sender, e);
            }

        }

        //********************************************************************************************************************

        private void iptalİşlemiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowc = gridView1.RowCount;
            if (rowc > 0)
            {
                if (gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString().Length > 0)
                {
                    System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Emir İptalini Onaylıyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.No)
                        return;

                    OrderOperations op = new OrderOperations(this);
                    string transactionid = gridView1.GetRowCellValue(ROW, "TRANSACTIONID").ToString();

                    string donus = op.Delete_Equity_Order(transactionid);
                    if (donus.Trim().Length > 0)
                    {
                        System.Windows.Forms.MessageBox.Show(transactionid + " Nolu Emir İptal Edilemedi. " + donus);
                    }

                    frmana.HisseAlSat_Ekran_Tazele();
                    PasiftekiEmirler_Islem();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("İptal Edilecek Emri Seçiniz !");
                    return;
                }
            }
        }

        //********************************************************************************************************************

    }
}
