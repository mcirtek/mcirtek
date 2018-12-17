using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Gtp.Framework;
using System.Windows.Forms;

using System.Threading;
using System.Globalization;


namespace Gtp.Global.ICM.Ui
{
    class OrderOperations
    {
        Gtp.Framework.ControlLibrary.Form frmx;

        public OrderOperations()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
        }

        public OrderOperations(Gtp.Framework.ControlLibrary.Form frm)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
            frmx = frm;
          
        } 

      //********************************************************************************************************************


        public void Grid_Initialize1(ref GridControl gridControl1, ref GridView gridView1,  Gtp.Framework.ControlLibrary.Form frm)
        {
            frmx = frm;

            DataTable dt = new DataTable();
            dt.Columns.Add("SAAT"        , Type.GetType("System.String"));
            dt.Columns.Add("HESAP"       , Type.GetType("System.String"));
            dt.Columns.Add("ADSOY"       , Type.GetType("System.String"));
            dt.Columns.Add("MENKUL"      , Type.GetType("System.String"));
            dt.Columns.Add("ALSAT"       , Type.GetType("System.String"));
            dt.Columns.Add("LOT"         , Type.GetType("System.Decimal"));
            dt.Columns.Add("FIYAT"       , Type.GetType("System.Decimal"));
            dt.Columns.Add("ORDINODURUMU", Type.GetType("System.String"));
            dt.Columns.Add("EMIRNEREDE"  , Type.GetType("System.String"));
            dt.Columns.Add("SEANS"       , Type.GetType("System.Int32"));

            dt.Columns.Add("GECERLILIK"   , Type.GetType("System.String"));
            dt.Columns.Add("DANISMANKODU" , Type.GetType("System.String"));
            dt.Columns.Add("TRANSACTIONID", Type.GetType("System.String"));
            dt.Columns.Add("FININSTID"    , Type.GetType("System.String"));
            dt.Columns.Add("CUSTOMERID"   , Type.GetType("System.String"));
            dt.Columns.Add("ACCOUNTID"    , Type.GetType("System.String"));
            dt.Columns.Add("MERGE_TRANSACTIONID", Type.GetType("System.String"));
            dt.Columns.Add("MAXIMUMLOT"   , Type.GetType("System.Int32"));
            dt.Columns.Add("TIP"          , Type.GetType("System.String"));
            dt.Columns.Add("INITIAL_MARKET_SESSION_DATE" , Type.GetType("System.String"));
            dt.Columns.Add("ENDING_MARKET_SESSION_DATE"  , Type.GetType("System.String"));
            dt.Columns.Add("SETTLEMENT_DATE"             , Type.GetType("System.String"));

            dt.Columns.Add("INITIAL_MARKET_SESSION_SEL", Type.GetType("System.Int32"));
            dt.Columns.Add("ENDING_MARKET_SESSION_SEL", Type.GetType("System.Int32"));
            dt.Columns.Add("LAK", Type.GetType("System.String"));

            gridControl1.DataSource = dt;

            gridView1.Columns[0].Caption = "Saat";          //* kolon header bilgilerini değiştirelim.
            gridView1.Columns[1].Caption = "Hesap";        
            gridView1.Columns[2].Caption = "Ad Soyad";     
            gridView1.Columns[3].Caption = "Menkul";        
            gridView1.Columns[4].Caption = "Al/Sat";
            gridView1.Columns[5].Caption = "Lot";
            gridView1.Columns[6].Caption = "Fiyat";
            gridView1.Columns[7].Caption = "Ordino Durumu";
            gridView1.Columns[7].Visible = false;

            gridView1.Columns[8].Caption = "Ordino Durumu"; //* Emir Nerede ? "İşlem Yapılmadı" , "Borsaya İletildi"
            gridView1.Columns[9].Caption = "Gir.Seans";
            gridView1.Columns[9].Visible = false;

            gridView1.Columns[10].Caption  = "Geçerlilik";
            gridView1.Columns[11].Caption = "Dan.Kd";

            gridView1.Columns[12].Caption = "Transaction Id";
            gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Caption = "FinInst Id";
            gridView1.Columns[13].Visible = false;
            gridView1.Columns[14].Caption = "Customer Id";
            gridView1.Columns[14].Visible = false;
            gridView1.Columns[15].Caption = "Account Id";
            gridView1.Columns[15].Visible = false;
            gridView1.Columns[16].Caption = "MergeTransaction Id";
            gridView1.Columns[16].Visible = false;
            gridView1.Columns[17].Caption = "Maksimum Lot";
            gridView1.Columns[17].Visible = false;
            gridView1.Columns[18].Caption = "Tip";
            gridView1.Columns[18].Visible = true;

            gridView1.Columns[19].Caption = "Initial Market Session Date";
            gridView1.Columns[19].Visible = false;

            gridView1.Columns[20].Caption = "Ending Market Session Date";
            gridView1.Columns[20].Visible = false;

            gridView1.Columns[21].Caption = "Settlement Date";
            gridView1.Columns[21].Visible = false;

            gridView1.Columns[22].Caption = "Initial Market Session Sel";
            gridView1.Columns[22].Visible = false;

            gridView1.Columns[23].Caption = "Ending Market Session Sel";
            gridView1.Columns[23].Visible = false;

            gridView1.Columns[24].Caption = "Lak";
            gridView1.Columns[24].Visible = false;

            /*
               gridView1.Columns[0].Width = 30;                         // kolon size ını sabitleyelim.
               gridView1.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
               gridView1.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
               gridView1.Columns[1].Width = 30;                         // kolon size ını sabitleyelim.
               gridView1.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
               gridView1.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
               gridView1.Columns[10].Visible = false;
            */

            for (int i = 2; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

        }


        //********************************************************************************************************************


        public void Grid_Initialize2(ref GridControl gridControl2, ref GridView gridView2, Gtp.Framework.ControlLibrary.Form frm)
        {
            frmx = frm;

            DataTable dt = new DataTable();
            dt.Columns.Add("SAAT", Type.GetType("System.String"));
            dt.Columns.Add("HESAP", Type.GetType("System.String"));
            dt.Columns.Add("ADSOY", Type.GetType("System.String"));
            dt.Columns.Add("MENKUL", Type.GetType("System.String"));
            dt.Columns.Add("ALSAT", Type.GetType("System.String"));
            dt.Columns.Add("LOT", Type.GetType("System.Decimal"));
            dt.Columns.Add("FIYAT", Type.GetType("System.Decimal"));
            dt.Columns.Add("ORDINODURUMU", Type.GetType("System.String"));
            dt.Columns.Add("SEANS", Type.GetType("System.Int32"));
            dt.Columns.Add("DANISMANKODU", Type.GetType("System.String"));

            gridControl2.DataSource = dt;

            gridView2.Columns[0].Caption = "Saat";          //* kolon header bilgilerini değiştirelim.
            gridView2.Columns[1].Caption = "Hesap";
            gridView2.Columns[2].Caption = "Ad Soyad";
            gridView2.Columns[3].Caption = "Menkul";
            gridView2.Columns[4].Caption = "Al/Sat";
            gridView2.Columns[5].Caption = "Lot";
            gridView2.Columns[6].Caption = "Fiyat";
            gridView2.Columns[7].Caption = "Ordino Durumu";
            
            gridView2.Columns[8].Caption = "Seans";
            gridView2.Columns[8].Visible = false;
            
            gridView2.Columns[9].Caption = "Dan.Kd";
            /*
             gridView2.Columns[0].Width = 30;                         // kolon size ını sabitleyelim.
             gridView2.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
             gridView2.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
             gridView2.Columns[1].Width = 30;                         // kolon size ını sabitleyelim.
             gridView2.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
             gridView2.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
             gridView2.Columns[10].Visible = false;
            */

            for (int i = 2; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

        }

        
        //********************************************************************************************************************

        public void Grid_Initialize3(ref GridControl gridControl3, ref GridView gridView3)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("SAAT", Type.GetType("System.String"));
            dt.Columns.Add("HESAP", Type.GetType("System.String"));
            dt.Columns.Add("ADSOY", Type.GetType("System.String"));
            dt.Columns.Add("MENKUL", Type.GetType("System.String"));
            dt.Columns.Add("ALSAT", Type.GetType("System.String"));
            dt.Columns.Add("LOT", Type.GetType("System.Decimal"));
            dt.Columns.Add("FIYAT", Type.GetType("System.Decimal"));
            dt.Columns.Add("ORDINODURUMU", Type.GetType("System.String"));
            dt.Columns.Add("SEANS", Type.GetType("System.Int32"));
            dt.Columns.Add("DANISMANKODU", Type.GetType("System.String"));

            gridControl3.DataSource = dt;

            gridView3.Columns[0].Caption = "Saat";          //* kolon header bilgilerini değiştirelim.
            gridView3.Columns[1].Caption = "Hesap";
            gridView3.Columns[2].Caption = "Ad Soyad";
            gridView3.Columns[3].Caption = "Menkul";
            gridView3.Columns[4].Caption = "Al/Sat";
            gridView3.Columns[5].Caption = "Lot";
            gridView3.Columns[6].Caption = "Fiyat";
            gridView3.Columns[7].Caption = "Ordino Durumu";
            
            gridView3.Columns[8].Caption = "Seans";
            gridView3.Columns[8].Visible = false;
            
            gridView3.Columns[9].Caption = "Dan.Kd";
            /*
             gridView3.Columns[0].Width = 30;                         // kolon size ını sabitleyelim.
             gridView3.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
             gridView3.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
             gridView3.Columns[1].Width = 30;                         // kolon size ını sabitleyelim.
             gridView3.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
             gridView3.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
             gridView3.Columns[10].Visible = false;
            */

            for (int i = 2; i < gridView3.Columns.Count; i++)
            {
                gridView3.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

        }



        //********************************************************************************************************************

        public void Grid_Initialize4(ref GridControl gridControl4, ref GridView gridView4, Gtp.Framework.ControlLibrary.Form frm)
        {
            frmx = frm;

            DataTable dt = new DataTable();
            dt.Columns.Add("SAAT", Type.GetType("System.String"));
            dt.Columns.Add("HESAP", Type.GetType("System.String"));
            dt.Columns.Add("ADSOY", Type.GetType("System.String"));
            dt.Columns.Add("MENKUL", Type.GetType("System.String"));
            dt.Columns.Add("ALSAT", Type.GetType("System.String"));
            dt.Columns.Add("LOT", Type.GetType("System.Decimal"));
            dt.Columns.Add("FIYAT", Type.GetType("System.Decimal"));
            dt.Columns.Add("ORDINODURUMU", Type.GetType("System.String"));
            dt.Columns.Add("SEANS", Type.GetType("System.Int32"));
            dt.Columns.Add("DANISMANKODU", Type.GetType("System.String"));

            gridControl4.DataSource = dt;

            gridView4.Columns[0].Caption = "Saat";          //* kolon header bilgilerini değiştirelim.
            gridView4.Columns[1].Caption = "Hesap";
            gridView4.Columns[2].Caption = "Ad Soyad";
            gridView4.Columns[3].Caption = "Menkul";
            gridView4.Columns[4].Caption = "Al/Sat";
            gridView4.Columns[5].Caption = "Lot";
            gridView4.Columns[6].Caption = "Fiyat";
            gridView4.Columns[7].Caption = "Ordino Durumu";
            gridView4.Columns[8].Caption = "Seans";
            gridView4.Columns[9].Caption = "Dan.Kd";
            /*
             gridView2.Columns[0].Width = 30;                         // kolon size ını sabitleyelim.
             gridView2.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
             gridView2.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
             gridView2.Columns[1].Width = 30;                         // kolon size ını sabitleyelim.
             gridView2.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
             gridView2.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
             gridView2.Columns[10].Visible = false;
            */

            for (int i = 2; i < gridView4.Columns.Count; i++)
            {
                gridView4.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

        }

        
        //********************************************************************************************************************

        public void Grid_Initialize1_Replacement(ref GridControl gridControl1, ref GridView gridView1, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit  chkedt , Gtp.Framework.ControlLibrary.Form frm)
        {
            frmx = frm;

            DataTable dt = new DataTable();
            dt.Columns.Add("CHECK", Type.GetType("System.Boolean"));
            dt.Columns.Add("HESAP", Type.GetType("System.String"));
            dt.Columns.Add("MENKUL", Type.GetType("System.String"));
            dt.Columns.Add("LOT", Type.GetType("System.Decimal"));
            dt.Columns.Add("FIYAT", Type.GetType("System.Decimal"));
            dt.Columns.Add("SAAT", Type.GetType("System.String"));

            dt.Columns.Add("ALSAT", Type.GetType("System.String"));
            dt.Columns.Add("SEANS", Type.GetType("System.Int32"));
            dt.Columns.Add("GECERLILIK", Type.GetType("System.String"));
            dt.Columns.Add("TRANSACTIONID", Type.GetType("System.String"));
            dt.Columns.Add("FININSTID", Type.GetType("System.String"));
            dt.Columns.Add("CUSTOMERID", Type.GetType("System.String"));
            dt.Columns.Add("ACCOUNTID", Type.GetType("System.String"));
            dt.Columns.Add("MAXIMUMLOT", Type.GetType("System.Int32"));
            dt.Columns.Add("TIP", Type.GetType("System.String"));
            dt.Columns.Add("INITIAL_MARKET_SESSION_DATE", Type.GetType("System.String"));
            dt.Columns.Add("ENDING_MARKET_SESSION_DATE", Type.GetType("System.String"));
            dt.Columns.Add("SETTLEMENT_DATE", Type.GetType("System.String"));

            dt.Columns.Add("INITIAL_MARKET_SESSION_SEL" , Type.GetType("System.Int32"));
            dt.Columns.Add("ENDING_MARKET_SESSION_SEL"  , Type.GetType("System.Int32"));
            dt.Columns.Add("LAK", Type.GetType("System.String"));

            gridControl1.DataSource = dt;

            gridView1.Columns[0].ColumnEdit = chkedt;
            gridView1.Columns[0].Width = 10;                         // kolon size ını sabitleyelim.
            gridView1.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
            gridView1.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.

            gridView1.Columns[1].Caption = "Hesap";
            gridView1.Columns[2].Caption = "Menkul";
            gridView1.Columns[3].Caption = "Lot";
            gridView1.Columns[4].Caption = "Fiyat";
            gridView1.Columns[5].Caption = "Saat";       

            gridView1.Columns[6].Caption = "Al/Sat";
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Caption = "Gir.Seans";
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Caption = "Geçerlilik";
            gridView1.Columns[8].Visible = false;
            gridView1.Columns[9].Caption = "Transaction Id";
            gridView1.Columns[9].Visible = false;
            gridView1.Columns[10].Caption = "FinInst Id";
            gridView1.Columns[10].Visible = false;
            gridView1.Columns[11].Caption = "Customer Id";
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Caption = "Account Id";
            gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Caption = "Maksimum Lot";
            gridView1.Columns[13].Visible = false;
            gridView1.Columns[14].Caption = "Tip";
            gridView1.Columns[14].Visible = false;

            gridView1.Columns[15].Caption = "Initial Market Session Date";
            gridView1.Columns[15].Visible = false;

            gridView1.Columns[16].Caption = "Ending Market Session Date";
            gridView1.Columns[16].Visible = false;

            gridView1.Columns[17].Caption = "Settlement Date";
            gridView1.Columns[17].Visible = false;

            gridView1.Columns[18].Caption = "Initial Market Session Sel";
            gridView1.Columns[18].Visible = false;

            gridView1.Columns[19].Caption = "Ending Market Session Sel";
            gridView1.Columns[19].Visible = false;

            gridView1.Columns[20].Caption = "laK";
            gridView1.Columns[20].Visible = false;

            /*
               gridView1.Columns[0].Width = 30;                         // kolon size ını sabitleyelim.
               gridView1.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
               gridView1.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
               gridView1.Columns[1].Width = 30;                         // kolon size ını sabitleyelim.
               gridView1.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
               gridView1.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
               gridView1.Columns[10].Visible = false;
            */

            for (int i = 2; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

        }


        //********************************************************************************************************************


        public void Grid_Initialize_Basket(ref GridControl gridControl1, ref GridView gridView1)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MENKUL", Type.GetType("System.String"));
            dt.Columns.Add("ALSAT" , Type.GetType("System.String"));
            dt.Columns.Add("ADET"  , Type.GetType("System.String"));
            dt.Columns.Add("AKIBET", Type.GetType("System.String"));

            gridControl1.DataSource = dt;

            gridView1.Columns[0].Caption = "Menkul";          //* kolon header bilgilerini değiştirelim.
            gridView1.Columns[1].Caption = "B:Buy/S:Sell";
            gridView1.Columns[2].Caption = "Adet";
            gridView1.Columns[3].Caption = "Akibet";

            for (int i = 2; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

            gridView1.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView1.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gridView1.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

        }


   //********************************************************************************************************************

        public void Grid_Initialize_AlgoTopluSecim(ref GridControl gridControl2, ref GridView gridView2)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MENKUL", Type.GetType("System.String"));
            dt.Columns.Add("ALSAT", Type.GetType("System.String"));
            dt.Columns.Add("ADET", Type.GetType("System.String"));
            dt.Columns.Add("AKIBET", Type.GetType("System.String"));

            gridControl2.DataSource = dt;

            gridView2.Columns[0].Caption = "Menkul";          //* kolon header bilgilerini değiştirelim.
            gridView2.Columns[1].Caption = "B:Buy/S:Sell";
            gridView2.Columns[2].Caption = "Adet";
            gridView2.Columns[3].Caption = "Akibet";

            for (int i = 2; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }

            gridView2.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView2.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView2.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gridView2.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

        }


        //********************************************************************************************************************

        public DataTable MusteriListesi_Al(Gtp.Framework.ControlLibrary.Form frm, bool hepsidahil)
        {
            frmx = frm;
            string str = "";
            DataTable Tablo1;

            //* Select ACCOUNT_ID from POSITION_ACCOUNT_VIEW Where POSTN_ID=ApplicationBrowser.ActiveUser.PositionId

            if (hepsidahil == true)
            {
                str = "SELECT '000' AS ID , 'HEPSİ' AS NAME ";
                str += " UNION ";
            }
            str += " Select A.ACCOUNT_EXT_ID as ID , C.FULL_NAME as NAME ";
            str += " from GTPFSI_CUSTOMER_VIEW C,GTPFSI_ACCOUNT_VIEW A Where C.CUSTOMER_EXT_ID in ";
            str += " ('7190000',	'7240000',	'7400000',	'7480000',	'7590000',	'7660000',	";
            str += " '7700000',	'7720000',	'7870000',	'7890000',	'7960000',	'7970000',	";
            str += " '7990000',	'7141000',	'7179000',	'7202000',	'7210000',	'7294000',	";
            str += " '7295000',	'7393000',	'7444000',	'7901000',	'7907000',	'7936000','1000','112542')";
            str += " AND A.CUSTOMER_ID=C.CUSTOMER_ID AND A.DEFAULT_ACCOUNT='TRUE' ";

            //*  str += " SELECT ACCOUNT_EXT_ID AS ID , FULL_NAME AS NAME  from GTPFSI_ACCOUNT_VIEW where DIV_ID =  '0000-00001G-DIV' AND STATUS=1 AND DEFAULT_ACCOUNT = 'TRUE' ORDER BY ID";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            return Tablo1;
        }


        //********************************************************************************************************************


        public void MusteriBilgisi_Detay(Gtp.Framework.ControlLibrary.Form frm, string AccExtId, ref string CustId, ref string AccId)
        {
            frmx = frm;
            string str = "";
            /*
            select C.CUSTOMER_ID CustId, A.ACCOUNT_ID AccId 
            from GTPFSI_CUSTOMER_VIEW C,GTPFSI_ACCOUNT_VIEW A
            where A.ACCOUNT_EXT_ID ='112542-103'
            AND A.CUSTOMER_ID=C.CUSTOMER_ID AND A.DEFAULT_ACCOUNT='TRUE' 
            */
            str += " select C.CUSTOMER_ID CustId, A.ACCOUNT_ID AccId  ";
            str += " from GTPFSI_CUSTOMER_VIEW C,GTPFSI_ACCOUNT_VIEW A ";
            str += " where A.ACCOUNT_EXT_ID ='" + AccExtId + "'";
            str += "  AND A.CUSTOMER_ID=C.CUSTOMER_ID AND A.DEFAULT_ACCOUNT='TRUE' ";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                CustId = Convert.ToString(Tablo1.Rows[0]["CustId"]);
                AccId  = Convert.ToString(Tablo1.Rows[0]["AccId"]);
            }
           
        }


        //********************************************************************************************************************

        public DataTable MenkulListesi_Al(Gtp.Framework.ControlLibrary.Form frm)
        {
            frmx = frm;
            DataTable Tablo1;

            //** string str = "select NAME from GTPFSI_FINANCIAL_INSTRUMENTS where STATUS=1 AND FIN_INST_TYPE_ID = '0000-000001-FIT' order by NAME";
            string str = "select I.NAME AS NAME ";
            str += "from GTPFSI_FINANCIAL_INSTRUMENTS I INNER JOIN GTPFSI_FI_RELATIONS R ON (I.FIN_INST_ID = R.ORIGIN_FIN_INST_ID)";
            str += " where I.STATUS = 1 AND I.FIN_INST_TYPE_ID = '0000-000001-FIT' and R.DESTINATION_FIN_INST_ID = '0000-000001-FIN'";
            str += " AND NAME not LIKE ('%.B') and NAME not LIKE ('%.R') and NAME not LIKE ('%.M') and NAME not LIKE ('%y') ";
           // str += " order by I.NAME";
            str += " UNION ";
            str += " SELECT 'DGGYO' AS NAME ";     

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            return Tablo1;

        }


        //********************************************************************************************************************

        public void TarihBilgileri_Al(Gtp.Framework.ControlLibrary.Form frm, ref string BugunkuTarih, ref string TakasTarihi)
        {
            frmx = frm;
            DataTable Tablo1;
            //** string str = "SELECT DATE,SETTLEMENT_DATE FROM GTPFSI_CALENDARS WHERE DATE= cast(floor(cast(GETDATE() as float)) as datetime) and MARKET_PLACE_ID='0000-000001-MPL'";
            string str = "SELECT DATE BugunkuTarih , SETTLEMENT_DATE TakasTarihi ";
            str += " FROM GTPFSI_CALENDARS ";
            str += " WHERE DATE= cast(floor(cast(GETDATE() as float)) as datetime) and MARKET_PLACE_ID='0000-000001-MPL'";
            
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {
                BugunkuTarih =  String.Format("{0:yyyy-MM-dd}", Tablo1.Rows[i]["BugunkuTarih"]);
                TakasTarihi  =  String.Format("{0:yyyy-MM-dd}" , Tablo1.Rows[i]["TakasTarihi"]);

               // BugunkuTarih = Convert.ToString(Convert.ToDateTime(Tablo1.Rows[i]["BugunkuTarih"]));
               // TakasTarihi  = Convert.ToString(Convert.ToDateTime(Tablo1.Rows[i]["TakasTarihi"]));
            }
        }

        //********************************************************************************************************************


        public DataTable FiyatKademeListesi_Al(Gtp.Framework.ControlLibrary.Form frm, string menkul)
        {
            frmx = frm;
            DataTable Tablo1;

            string str = "SELECT V.*  , I.NAME , I.DESCRIPTION , C.MAX_LOT ";
            str += "FROM GTPFSI_FI_VALUES V ";
            str += "Left Join GTPFSI_FINANCIAL_INSTRUMENTS I on I.FIN_INST_ID=V.FIN_INST_ID ";
            str += "Left Join GTPFSI_EQ_CHTS C on I.FIN_INST_ID=C.FIN_INST_ID ";
            str += "WHERE VALUE_DATE = cast(floor(cast(GETDATE() as float)) as datetime)  ";   //*serverdan tarih kontrolü
            //* str += "YEAR(VALUE_DATE)='" + yil + "' and MONTH(VALUE_DATE)='" + ay + "' and DAY(VALUE_DATE)='" + gun + "'";
            str += "AND I.NAME = '" + menkul + "' ";
            str += "ORDER BY SESSION_NO desc";         // en son seans numarasını alsın. Bu select çift record döndürüyor 1. ve 2. seans için row lar.

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            return Tablo1;
        }

        //********************************************************************************************************************

        public DataTable MenkulDetay(Gtp.Framework.ControlLibrary.Form frm, string menkul)
        {
            frmx = frm;
            DataTable Tablo1;

            string str = "SELECT  I.NAME, I.FIN_INST_ID , C.MAX_LOT ";
                    str+= "FROM GTPFSI_FINANCIAL_INSTRUMENTS I ";
                    str += "Left Join GTPFSI_EQ_CHTS C on I.FIN_INST_ID=C.FIN_INST_ID ";
                    str += "WHERE I.NAME = '" + menkul + "' ";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            return Tablo1;
        }



        //********************************************************************************************************************

        public string HangiSeanstayiz()
        {
            decimal Baszaman_Seans1=0, Bitzaman_Seans1=0, Baszaman_Seans=0, Bitzaman_Seans2=0;
            string seans;
            decimal zaman=0;

            BorsaZamanlari(ref Baszaman_Seans1, ref Bitzaman_Seans1, ref Baszaman_Seans, ref Bitzaman_Seans2);

            string str = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS Zaman";
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                zaman = Convert.ToDecimal( Convert.ToString( Tablo1.Rows[0]["Zaman"] ).Replace(":", ""));
            }

            if (zaman < Bitzaman_Seans1)  //* Bitzaman_Seans1=130000
                seans = "1";
            else
                seans = "2";

            return seans;
        }


        //********************************************************************************************************************

        public decimal GetServerTime()
        {
            string seans;
            decimal zaman = 0;
            string str = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS Zaman";
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                zaman = Convert.ToDecimal(Convert.ToString(Tablo1.Rows[0]["Zaman"]).Replace(":", ""));
            }

            return zaman;
        }


        //********************************************************************************************************************
        public string GetServerTime2()
        {
            string seans;
            string zaman = "";
            string str = "SELECT CONVERT(VARCHAR(14),GETDATE(),114) AS Zaman";
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                zaman = Convert.ToString(Tablo1.Rows[0]["Zaman"].ToString());
            }

            return zaman;
        }


        //********************************************************************************************************************


        public void BorsaZamanlari(ref decimal Baszaman_Seans1, ref decimal Bitzaman_Seans1, ref decimal Baszaman_Seans2, ref decimal Bitzaman_Seans2)
        {
            string str = "SELECT * FROM NETMESAJ.dbo.IcmBorsaSeansSaatleri ";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                Baszaman_Seans1 = Convert.ToDecimal(Convert.ToString(Tablo1.Rows[0]["Baszaman_Seans1"]));
                Bitzaman_Seans1 = Convert.ToDecimal(Convert.ToString(Tablo1.Rows[0]["Bitzaman_Seans1"]));
                Baszaman_Seans2 = Convert.ToDecimal(Convert.ToString(Tablo1.Rows[0]["Baszaman_Seans2"]));
                Bitzaman_Seans2 = Convert.ToDecimal(Convert.ToString(Tablo1.Rows[0]["Bitzaman_Seans2"]));
            }

            return;
        }

        //********************************************************************************************************************

        public string GetDateTime()
        {
            string sonan = " ";
            string str = "SELECT GETDATE() AS Sonan";
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                sonan = String.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", Tablo1.Rows[0]["Sonan"]);
            }

            return sonan;
        }


        //********************************************************************************************************************

        public decimal YeniRefNoGetir()
        {
            decimal refno = 0;
            string str = "USE NETMESAJ ";
            str += "EXECUTE sp_IcmGetReferansNo";
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
            {
                refno = Convert.ToDecimal(Convert.ToSingle(Tablo1.Rows[0]["referansno"]));
            }


            return refno;
        }

        //********************************************************************************************************************

        public bool OrdinoBorsayaIletildimi(string transactionid)
        {
            string brokername="";
            bool donus=false;
            /*
                 ****BROKER_NAME alanı boş ise bekliyor, aksi halde borsaya iletildi demektir...
                
                select t.TRANSACTION_ID, ISNULL(BRK.NAME,'') BROKER_NAME  from GTPBR_EQ_TRANS t
                LEFT JOIN GTPBS_POSITIONS BRK (NOLOCK) ON BRK.POSTN_ID = t.DEST_RECON_POS_ID
                Left Join GTPFSI_FINANCIAL_INSTRUMENTS h on h.FIN_INST_ID=t.FIN_INST_ID  
                where t.TRANSACTION_ID='0000-019GK6-IET' 
            */

            string str  = "select t.TRANSACTION_ID, ISNULL(BRK.NAME,'') BROKER_NAME  from GTPBR_EQ_TRANS t ";
                   str += " LEFT JOIN GTPBS_POSITIONS BRK (NOLOCK) ON BRK.POSTN_ID = t.DEST_RECON_POS_ID ";
                   str += " Left Join GTPFSI_FINANCIAL_INSTRUMENTS h on h.FIN_INST_ID=t.FIN_INST_ID  ";
                   str += " WHERE t.TRANSACTION_ID='" + transactionid + "'";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
                brokername = Convert.ToString(Tablo1.Rows[0]["BROKER_NAME"]);

            if (brokername.Trim().Length > 0)
                donus = true;
            else
                donus = false;


            return donus;
        }


        //********************************************************************************************************************

        public string GetSessionDate()
        {
            string sessiondate="" ;
            string str = "SELECT SESSION_DATE FROM GTPBR_EQ_SESSION (NOLOCK)  WHERE ORG_ID = '0000-000001-ORG' AND STATUS = '1' ";

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            DataTable Tablo1 = x.Tables[0];

            if (Tablo1.Rows.Count > 0)
                sessiondate = Convert.ToDateTime( Convert.ToString(Tablo1.Rows[0]["SESSION_DATE"]) ).ToString("yyyy-MM-dd");

            return sessiondate;
        }

        //********************************************************************************************************************

        public OrderList BekleyenEmirler(string musteri,string danisman,string menkul, string alissatis,string seans,string sessiondate)
        {
            OrderList lst = new OrderList();
            Order ord;
            DataTable Tablo1;

            string str = Bekleyen_Str(musteri,danisman,menkul,alissatis,seans,sessiondate);
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {
                ord = new Order();
                ord.Saat   = Convert.ToDateTime(Tablo1.Rows[i]["Zaman"]).ToString("HH:mm:ss tt"); 
                ord.Hesap  = Convert.ToString(Tablo1.Rows[i]["Hesap"]);
                ord.Adsoy  = Convert.ToString(Tablo1.Rows[i]["Unvan"]);
                ord.Menkul = Convert.ToString(Tablo1.Rows[i]["Menkul"]);
                ord.Alsat  = Convert.ToString(Tablo1.Rows[i]["AlisSatis"]);
                ord.Lot    = Convert.ToDecimal( string.Format("{0:0,0}",  Convert.ToDecimal(Tablo1.Rows[i]["Lot"])) );
                ord.Fiyat  = Convert.ToDecimal( string.Format("{0:0,0.000}", Convert.ToDecimal(Tablo1.Rows[i]["Fiyat"])) );
                ord.Ordinodurumu  = Convert.ToString(Tablo1.Rows[i]["Durum"]);
                ord.Ordino_uzun_durumu = Convert.ToString(Tablo1.Rows[i]["Durum"]) + "(" +Convert.ToString(Tablo1.Rows[i]["Trnsts"]) + ")";
                ord.Emirnerede    = Convert.ToString(Tablo1.Rows[i]["EmirNerede"]);
                ord.Seans         = Convert.ToInt32(Tablo1.Rows[i]["Seans"]);
                ord.Danismankodu  = Convert.ToString(Tablo1.Rows[i]["EmriVeren"]);
                ord.Transactionid = Convert.ToString(Tablo1.Rows[i]["TransactionId"]);
                ord.FinInstid     = Convert.ToString(Tablo1.Rows[i]["FinInstId"]);
                ord.Customerid    = Convert.ToString(Tablo1.Rows[i]["CustomerId"]);
                ord.Accountid     = Convert.ToString(Tablo1.Rows[i]["AccountId"]);
                ord.Gecerlilik    = Convert.ToString(Tablo1.Rows[i]["Gecerlilik"]);
                ord.Maximumlot    = Convert.ToInt32(Tablo1.Rows[i]["MaximumLot"]);
                ord.Tip           = Convert.ToString(Tablo1.Rows[i]["Tip"]);
                ord.Initialmarketsessiondate = Convert.ToDateTime(Tablo1.Rows[i]["InitialMarketSessionDate"]);
                ord.Initialmarketsessionsel  = Convert.ToInt32(Tablo1.Rows[i]["InitialMarketSessionSel"]);
                ord.Endingmarketsessiondate  = Convert.ToDateTime(Tablo1.Rows[i]["EndingMarketSessionDate"]);
                ord.Endingmarketsessionsel   = Convert.ToInt32(Tablo1.Rows[i]["EndingMarketSessionSel"]);
                ord.Settlementdate           = Convert.ToDateTime(Tablo1.Rows[i]["SettlementDate"]);
                ord.Lak                      = Convert.ToString(Tablo1.Rows[i]["Lak"]);
                
                lst.Resultlist.Add(ord);
            }

            return lst;
        }


        //********************************************************************************************************************

        public OrderList GerceklesenEmirler(string musteri, string danisman, string menkul, string alissatis, string seans, string sessiondate)
        {
            OrderList lst = new OrderList();
            Order ord;
            DataTable Tablo1;

            string str = Gerceklesen_Str(musteri, danisman, menkul, alissatis, seans, sessiondate);
            try
            {
                var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
                Tablo1 = x.Tables[0];

              for (int i = 0; i < Tablo1.Rows.Count; i++)
              {
                ord = new Order();
                ord.Saat = Convert.ToDateTime(Tablo1.Rows[i]["Zaman"]).ToString("HH:mm:ss tt");
                ord.Hesap = Convert.ToString(Tablo1.Rows[i]["Hesap"]);
                ord.Adsoy = Convert.ToString(Tablo1.Rows[i]["Unvan"]);
                ord.Menkul = Convert.ToString(Tablo1.Rows[i]["Menkul"]);
                ord.Alsat = Convert.ToString(Tablo1.Rows[i]["AlisSatis"]);
                ord.Lot = Convert.ToDecimal(string.Format("{0:0,0}", Convert.ToDecimal(Tablo1.Rows[i]["Lot"])));
                ord.Fiyat = Convert.ToDecimal(string.Format("{0:0,0.000}", Convert.ToDecimal(Tablo1.Rows[i]["Fiyat"])));
                ord.Ordinodurumu = Convert.ToString(Tablo1.Rows[i]["Durum"]);
                ord.Seans = Convert.ToInt32(Tablo1.Rows[i]["Seans"]);
                ord.Danismankodu = Convert.ToString(Tablo1.Rows[i]["EmriVeren"]);
                lst.Resultlist.Add(ord);
              }
            }
            catch (Exception ex)
            {
                TopMostMessageBox.Show(ex.ToString());
                //* MessageBox.Show(ex.ToString());
            }

            return lst;
        }



        //********************************************************************************************************************

        public OrderList GecmisTarihliEmirler(string musteri, string danisman, string menkul, string alissatis, string seans, string tarih)
        {
            OrderList lst = new OrderList();
            Order ord;
            DataTable Tablo1;

            string str = GecmisTarihli_Str(musteri, danisman, menkul, alissatis, seans,tarih);
            try
            {
                var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
                Tablo1 = x.Tables[0];

              for (int i = 0; i < Tablo1.Rows.Count; i++)
              {
                ord = new Order();
                ord.Saat = Convert.ToDateTime(Tablo1.Rows[i]["Zaman"]).ToString("HH:mm:ss tt");
                ord.Hesap = Convert.ToString(Tablo1.Rows[i]["Hesap"]);
                ord.Adsoy = Convert.ToString(Tablo1.Rows[i]["Unvan"]);
                ord.Menkul = Convert.ToString(Tablo1.Rows[i]["Menkul"]);
                ord.Alsat = Convert.ToString(Tablo1.Rows[i]["AlisSatis"]);
                ord.Lot = Convert.ToDecimal(string.Format("{0:0,0}", Convert.ToDecimal(Tablo1.Rows[i]["Lot"])));
                ord.Fiyat = Convert.ToDecimal(string.Format("{0:0,0.000}", Convert.ToDecimal(Tablo1.Rows[i]["Fiyat"])));
                ord.Ordinodurumu = Convert.ToString(Tablo1.Rows[i]["Durum"]);
                ord.Seans = Convert.ToInt32(Tablo1.Rows[i]["Seans"]);
                ord.Danismankodu = Convert.ToString(Tablo1.Rows[i]["EmriVeren"]);
                lst.Resultlist.Add(ord);
              }
            }
            catch (Exception ex)
            {
                TopMostMessageBox.Show(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }

            return lst;
        }


        GtpXml ExecCustomSQL(string SQLText)
        {

            GtpXml g = new GtpXml("GLB_CRM_EXEC_CUSTOM_SQL", "1.0");
            g.AddParameter("SQLText", SQLText);
            return frmx.RbmInvoke(g);
        }



        //********************************************************************************************************************

        private string Bekleyen_Str(string musteri,string danisman,string menkul,string alissatis,string seans,string sessiondate)
        {
          DateTime dt1 = System.DateTime.Now.Date;
          string gun   = Convert.ToString(System.DateTime.Now.Day);
          string ay    = Convert.ToString(System.DateTime.Now.Month);
          string yil   = Convert.ToString(System.DateTime.Now.Year);

                         //* DECLARE @TAR datetime='2015-12-03 00:00:00.000'
          string str =   " DECLARE @TAR datetime='" + sessiondate + "'";
                 str+=   " Select ";
                 str+=   " Case When ISNULL(BRK.NAME,'') ='' Then 'İşlem Yapılmadı' Else 'Borsaya İletildi' End EmirNerede , ";
                 str+=   " t.CREATED Zaman,";
                 str+=   " a.ACCOUNT_EXT_ID Hesap,";
                 str+=   " a.FULL_NAME Unvan,";
                 str+=   " h.NAME Menkul ,";
                 str+=   " Case When t.DEBIT_CREDIT='DEBIT' Then 'S' Else 'A' End AlisSatis , ";
                 str+=   " t.UNITS-isNull(t.REALIZED_UNITS,0) Lot, ";
                 str+=   " t.PRICE Fiyat, ";
                 str+=   " r.NAME Lak, ";
                 str+=   " t.TRANSACTION_STATUS_ID,";
                 str+=   " s.NAME Trnsts, s.DESCRIPTION,";
                 str+=   " Case When ";
                 str+=   " t.UNITS-isNull(t.REALIZED_UNITS,0)>0 And ";
                 str+=   " t.TRANSACTION_STATUS_ID in ( ";
                 str+=   " '0000-000001-ESD', ";  //-- ACTIVE
                 str+=   " '0000-000002-ESD', ";  //-- HOLD
                 str+=   " '0000-00000F-ESD', ";  //-- 2.seans için geçersiz kıl. 
                 str+=   " '0000-000005-ESD', ";  //-- Improve Demand
                 str+=   " '0000-000006-ESD') ";  //-- Improve Order
                 str+=   " Then 'Bekleyen' ";
                 str+=   " Else 'İptal' End Durum,";
                 str+=   " p.NAME EmriVeren,";
                 str+=   " Convert(int,t.INITIAL_MARKET_SESSION_SEL) Seans,";
                 str+=   " t.TRANSACTION_EXT_ID EmirRef, ";
                 str+=   " t.TRANSACTION_ID TransactionId,";

                 str+=   " Case When  t.TRANSACTION_TYPE_ID='0000-000001-ETT' then 'Limit' ";
                 str+=   " when  t.TRANSACTION_TYPE_ID='0000-000010-ETT' then 'Piyasa' ";
                 str+=   " when  t.TRANSACTION_TYPE_ID='0000-000011-ETT' then 'Piyasadan Limite' ";
                 str+=   " when  t.TRANSACTION_TYPE_ID='0000-000012-ETT' then 'Denge' End Tip, ";
                 str+=   " Case When t.TIME_IN_FORCE='0' Then 'Gün'  When t.TIME_IN_FORCE='3' Then 'KİE'  When t.TIME_IN_FORCE='9' Then 'EFG' End Gecerlilik , "; 

                 str+=   " t.INITIAL_MARKET_SESSION_DATE InitialMarketSessionDate, ";
                 str+=   " t.ENDING_MARKET_SESSION_DATE  EndingMarketSessionDate, ";

                 str += " t.INITIAL_MARKET_SESSION_SEL  InitialMarketSessionSel, ";
                 str += " t.ENDING_MARKET_SESSION_SEL  EndingMarketSessionSel, ";

                 str+=   " t.SETTLEMENT_DATE SettlementDate, ";

                 str+=   " h.FIN_INST_ID FinInstId,";         
                 str+=   " t.CUSTOMER_ID CustomerId,";
                 str+=   " t.ACCOUNT_ID AccountId,";
                 str+=   " C.MAX_LOT MaximumLot ";
                 //* str+=   " Case When ENDING_MARKET_SESSION_SEL-INITIAL_MARKET_SESSION_SEL = 0 Then 'Seanslık' Else 'Günlük' End Gecerlilik";
                 str+=   " from GTPBR_EQ_TRANS t " ;
                 str+=   " Left Join GTPFSI_FINANCIAL_INSTRUMENTS h on h.FIN_INST_ID=t.FIN_INST_ID ";
                 str+=   " Left Join GTPFSI_EQ_CHTS C on h.FIN_INST_ID=C.FIN_INST_ID ";
                 str+=   " left join GTPBS_POSITIONS BRK (NOLOCK) ON BRK.POSTN_ID = t.DEST_RECON_POS_ID";
                 str+=   " Left Join GTPFSI_ACCOUNT_VIEW a on a.ACCOUNT_ID=t.ACCOUNT_ID";
                 str+=   " left Join GTPBS_PARTIES p On p.PARTY_ID=t.CREATED_BY ";
                 str+=   " Left Join GTPBR_EQ_TRANS_STATUS_DEFS s on s.TRANSACTION_STATUS_ID=t.TRANSACTION_STATUS_ID ";
                 str+=   " INNER JOIN GTPBR_EQ_TRANS_TYPES (NOLOCK) r ON r.TRANSACTION_TYPE_ID = t.TRANSACTION_TYPE_ID ";

                 str +=  " Where t.ACCOUNT_ID in ( "; // -- Kullanicinin görme yettikisi olan hesaplar

                 //*** str += " Select ACCOUNT_ID from POSITION_ACCOUNT_VIEW Where POSTN_ID='0000-00017V-POS' ";
                 str +=  " Select A.ACCOUNT_ID ";
                 str +=  " from GTPFSI_CUSTOMER_VIEW C,GTPFSI_ACCOUNT_VIEW A Where C.CUSTOMER_EXT_ID in ";
                 str +=  " ('7190000',	'7240000',	'7400000',	'7480000',	'7590000',	'7660000',	";
                 str +=  " '7700000',	'7720000',	'7870000',	'7890000',	'7960000',	'7970000',	";
                 str +=  " '7990000',	'7141000',	'7179000',	'7202000',	'7210000',	'7294000',	";
                 str += " '7295000',	'7393000',	'7444000',	'7901000',	'7907000',	'7936000','1000','112542') ";
                 str += " AND A.CUSTOMER_ID=C.CUSTOMER_ID AND A.DEFAULT_ACCOUNT='TRUE' ";
                 str += ") "; //-- Kullanici  pos id 
                 str +=  " AND t.UNITS-isNull(t.REALIZED_UNITS,0)>0 ";  //-- KALANI OLAN EMIRLER !!
                 str +=  " AND t.TRANSACTION_STATUS_ID <> '0000-000004-ESD' ";  //-- MAX_LOT DIVIDENED 

                 str += " AND INITIAL_MARKET_SESSION_DATE>= cast(floor(cast(@TAR as float)) as datetime) ";
                 //*** str+= " AND YEAR(INITIAL_MARKET_SESSION_DATE)='" + yil + "' AND MONTH(INITIAL_MARKET_SESSION_DATE)='" + ay + "' AND DAY(INITIAL_MARKET_SESSION_DATE)='" + gun + "'";
                 //*** str+=   " AND INITIAL_MARKET_SESSION_DATE>= cast(floor(cast(GETDATE() as float)) as datetime) ";
                 //*** str += " And t.INITIAL_MARKET_SESSION_DATE='20151007' ";  //-- TARIH


                 if(menkul.Trim().Length>0)
                     str += " AND h.NAME='" + menkul + "' ";  
                        
                 if (alissatis == "S")
                    str += " AND t.DEBIT_CREDIT='DEBIT' ";   //*Satış İşlemi...
                 else if (alissatis == "A")
                     str += " AND t.DEBIT_CREDIT<>'DEBIT' "; //*Alış İşlemi...


                    if (seans == "1")
                         str += " AND Convert(int,t.INITIAL_MARKET_SESSION_SEL)=1 ";     //* 1. seans
                    else if (seans == "2")
                         str += " AND Convert(int,t.INITIAL_MARKET_SESSION_SEL)=2 ";     //  2. seasn

            
                 if( (danisman.Trim().Length !=0) && (danisman!="000") )  //* HEPSİ değil ise,,,
                     str += " AND t.CREATED_BY='" + danisman + "' ";

                 if ((musteri.Trim().Length != 0) && (musteri != "000"))  //* HEPSİ değil ise,,,
                     str += " AND a.ACCOUNT_EXT_ID ='" + musteri + "' ";


                 return str;
        }



        //********************************************************************************************************************

        private string Gerceklesen_Str(string musteri, string danisman, string menkul, string alissatis, string seans, string sessiondate)
        {

            //* DECLARE @TAR datetime='2015-12-03 00:00:00.000'
            string str = " DECLARE @TAR datetime='" + sessiondate + "'";
                   str+= " Select ";
                   str+= "f.CREATED Zaman, ";
                   str+= "a.ACCOUNT_EXT_ID Hesap,";
                   str+= "a.FULL_NAME Unvan,";
                   str+= "h.NAME Menkul , ";
                   str+= "Case When t.DEBIT_CREDIT='DEBIT' Then 'S' Else 'A' End AlisSatis , ";
                   str+= "f.FILL_UNITS Lot, ";
                   str+= "f.FILL_PRICE Fiyat, ";
                   str+= "'Gerçekleşti' Durum, ";
                   str+= "p.NAME EmriVeren ,";
                   str+= "Convert(int,f.FILL_SESSION) Seans, ";
                   str+= "t.TRANSACTION_EXT_ID EmirRef ";
                   str+= "from GTPBR_EQ_TRANS t ";
                   str+= "Left Join GTPFSI_FINANCIAL_INSTRUMENTS h on h.FIN_INST_ID=t.FIN_INST_ID ";
                   str+= "Left Join GTPFSI_ACCOUNT_VIEW a on a.ACCOUNT_ID=t.ACCOUNT_ID ";
                   str+= "Right Join GTPBR_EQ_TRANS_FILLS f on f.TRANSACTION_ID=t.TRANSACTION_ID ";
                   str+= "left Join GTPBS_PARTIES p On p.PARTY_ID=t.CREATED_BY ";
                   str+= "Where t.ACCOUNT_ID in ( ";
                   //** str+= "Select ACCOUNT_ID from POSITION_ACCOUNT_VIEW Where POSTN_ID='0000-00017V-POS' ";
                   str += " Select A.ACCOUNT_ID ";
                   str += " from GTPFSI_CUSTOMER_VIEW C,GTPFSI_ACCOUNT_VIEW A Where C.CUSTOMER_EXT_ID in ";
                   str += " ('7190000',	'7240000',	'7400000',	'7480000',	'7590000',	'7660000',	";
                   str += " '7700000',	'7720000',	'7870000',	'7890000',	'7960000',	'7970000',	";
                   str += " '7990000',	'7141000',	'7179000',	'7202000',	'7210000',	'7294000',	";
                   str += " '7295000',	'7393000',	'7444000',	'7901000',	'7907000',	'7936000','1000','112542') ";
                   str += " AND A.CUSTOMER_ID=C.CUSTOMER_ID AND A.DEFAULT_ACCOUNT='TRUE' ";
                   str+= ") ";
                   str += " AND INITIAL_MARKET_SESSION_DATE>= cast(floor(cast(@TAR as float)) as datetime) ";
                   // str += " and INITIAL_MARKET_SESSION_DATE= cast(floor(cast(GETDATE() as float)) as datetime) ";



                   if (menkul.Trim().Length > 0)
                       str += " AND h.NAME='" + menkul + "' ";

                   if (alissatis == "S")
                       str += " AND t.DEBIT_CREDIT='DEBIT' ";   //*Satış İşlemi...
                   else if (alissatis == "A")
                       str += " AND t.DEBIT_CREDIT<>'DEBIT' "; //*Alış İşlemi...

                   if (seans == "1")
                       str += " AND Convert(int,f.FILL_SESSION)=1 ";     //* 1. seans
                   else if (seans == "2")
                       str += " AND Convert(int,f.FILL_SESSION)=2 ";     //2. seasn


                   if ((danisman.Trim().Length != 0) && (danisman != "000"))  //* HEPSİ değil ise,,,
                       str += " AND t.CREATED_BY='" + danisman + "' ";

                   if ((musteri.Trim().Length != 0) && (musteri != "000"))  //* HEPSİ değil ise,,,
                       str += " AND a.ACCOUNT_EXT_ID ='" + musteri + "' ";


            return str;
        }


        //********************************************************************************************************************

        private string GecmisTarihli_Str(string musteri, string danisman, string menkul, string alissatis, string seans,string tarih)
        {

            string str = "Select ";
            str += "f.CREATED Zaman, ";
            str += "a.ACCOUNT_EXT_ID Hesap,";
            str += "a.FULL_NAME Unvan,";
            str += "h.NAME Menkul , ";
            str += "Case When t.DEBIT_CREDIT='DEBIT' Then 'S' Else 'A' End AlisSatis , ";
            str += "f.FILL_UNITS Lot, ";
            str += "f.FILL_PRICE Fiyat, ";
            str += "'Gerçekleşti' Durum, ";
            str += "p.NAME EmriVeren ,";
            str += "Convert(int,f.FILL_SESSION) Seans, ";
            str += "t.TRANSACTION_EXT_ID EmirRef ";
            str += "from GTPBR_EQ_TRANS t ";
            str += "Left Join GTPFSI_FINANCIAL_INSTRUMENTS h on h.FIN_INST_ID=t.FIN_INST_ID ";
            str += "Left Join GTPFSI_ACCOUNT_VIEW a on a.ACCOUNT_ID=t.ACCOUNT_ID ";
            str += "Right Join GTPBR_EQ_TRANS_FILLS f on f.TRANSACTION_ID=t.TRANSACTION_ID ";
            str += "left Join GTPBS_PARTIES p On p.PARTY_ID=t.CREATED_BY ";
            str += "Where t.ACCOUNT_ID in ( ";
            //*** str += "Select ACCOUNT_ID from POSITION_ACCOUNT_VIEW Where POSTN_ID='0000-00017V-POS' ";
            str += " Select A.ACCOUNT_ID ";
            str += " from GTPFSI_CUSTOMER_VIEW C,GTPFSI_ACCOUNT_VIEW A Where C.CUSTOMER_EXT_ID in ";
            str += " ('7190000',	'7240000',	'7400000',	'7480000',	'7590000',	'7660000',	";
            str += " '7700000',	'7720000',	'7870000',	'7890000',	'7960000',	'7970000',	";
            str += " '7990000',	'7141000',	'7179000',	'7202000',	'7210000',	'7294000',	";
            str += " '7295000',	'7393000',	'7444000',	'7901000',	'7907000',	'7936000','1000','112542') ";
            str += " AND A.CUSTOMER_ID=C.CUSTOMER_ID AND A.DEFAULT_ACCOUNT='TRUE' ";
            str += ") ";
            str += " And INITIAL_MARKET_SESSION_DATE='" + tarih + "' ";  //-- 20151007  TARIH
            //***  str += " and INITIAL_MARKET_SESSION_DATE= cast(floor(cast(GETDATE() as float)) as datetime) ";



            if (menkul.Trim().Length > 0)
                str += " AND h.NAME='" + menkul + "' ";

            if (alissatis == "S")
                str += " AND t.DEBIT_CREDIT='DEBIT' ";   //*Satış İşlemi...
            else if (alissatis == "A")
                str += " AND t.DEBIT_CREDIT<>'DEBIT' "; //*Alış İşlemi...

            if (seans == "1")
                str += " AND Convert(int,f.FILL_SESSION)=1 ";     //* 1. seans
            else if (seans == "2")
                str += " AND Convert(int,f.FILL_SESSION)=2 ";   //2. seasn


            if ((danisman.Trim().Length != 0) && (danisman != "000"))  //* HEPSİ değil ise,,,
                str += " AND t.CREATED_BY='" + danisman + "' ";

            if ((musteri.Trim().Length != 0) && (musteri != "000"))  //* HEPSİ değil ise,,,
                str += " AND a.ACCOUNT_EXT_ID ='" + musteri + "' ";


            return str;
        }


        //********************************************************************************************************************

        public string Save_Equity_Order(string BugunkuTarih, string TakasTarihi, string finInstId, string alsat, string custId, string accId, Int32 maxlot, decimal fiyat, decimal miktar, Int32 InitialMarketSessionSel, Int32 EndingMarketSessionSel, string EquityTransactionTypeId,string OrderType,String TimeInForce)
        {
            string Error;
            int OrderEntryStatus;

            DataTable Tablo1;
            GtpXml g = new GtpXml("SAVE_EQUITY_ORDER", "1.0");

            g.AddParameter("orderDate"                 , "System.DateTime"  , Convert.ToDateTime(BugunkuTarih));
            g.AddParameter("transactionExtId"          , "System.String"    , " ");
            g.AddParameter("transactionStatus"         , "System.String"    , "ACTIVE");
            g.AddParameter("customerId"                , "System.String"    , custId);
            g.AddParameter("accountId"                 , "System.String"    , accId);
            g.AddParameter("equityTransactionTypeId"   , "System.String"    , EquityTransactionTypeId);
            g.AddParameter("debitCredit"               , "System.String"    , alsat);
            g.AddParameter("finInstId"                 , "System.String"    , finInstId);
            g.AddParameter("units"                     , "System.Decimal"   , miktar);
            g.AddParameter("orderPrice"                , "System.Decimal"   , fiyat);
            g.AddParameter("orderMaxLot"               , "System.Int32"     , maxlot);
            g.AddParameter("intLotCount"               , "System.Int32"     , Convert.ToInt32(1));
            g.AddParameter("SettlementDate"            , "System.DateTime"  , Convert.ToDateTime(TakasTarihi));
            g.AddParameter("initialMarketSessionDate"  , "System.DateTime"  , Convert.ToDateTime(BugunkuTarih));
            g.AddParameter("initialMarketSessionSel"   , "System.Int32"     , InitialMarketSessionSel);
            g.AddParameter("endingMarketSessionDate"   , "System.DateTime"  , Convert.ToDateTime(BugunkuTarih));
            g.AddParameter("endingMarketSessionSel"    , "System.Int32"     , EndingMarketSessionSel);
            g.AddParameter("holdType"                  , "System.String"    , "NONE");
            g.AddParameter("orderType"                 , "System.String"    , OrderType);
            g.AddParameter("timeInForce"               , "System.String"    , TimeInForce);
            g.AddParameter("limitControl"              , "System.String"    , "N");
            g.AddParameter("routeTypeId"               , "System.String"    , "0000-000001-TRT");
            g.AddParameter("routeIntSysId"             , "System.String"    , " ");
            g.AddParameter("routeCollPosId"            , "System.String"    , "0000-000002-POS");
            g.AddParameter("destReconPosId"            , "System.String"    , " ");
            g.AddParameter("left"                      , "System.Decimal"   , miktar);
            g.AddParameter("shortfall"                 , "System.Int32"     , Convert.ToDecimal(0));
            g.AddParameter("oldeAmount"                , "System.Decimal"   , Convert.ToDecimal(0));
            g.AddParameter("marketType"                , "System.String"    , " ");
            g.AddParameter("verbalOrder"               , "System.String"    , "0");
            g.AddParameter("SMSFillNotification"       , "bool"             , Convert.ToBoolean("False"));
            g.AddParameter("EmailFillNotification"     , "bool"             , Convert.ToBoolean("False"));
            g.AddParameter("equityTransfer"            , "System.String"    , "0");


            GtpXml res = new GtpXml(frmx.RbmInvoke(g.Xml));
            try
            {  Error = res.GetNodeContent("request-broker-message\\response\\error"); }
            catch { 
                Error = null; }


            if (Error == null)  Error = res.GetResponseOutput("Error");
            if (Error != null)  Error += ":" + res.GetResponseOutput("RealError");

            if (Error == null)
            {
                decimal OrderAmount = Convert.ToDecimal(res.GetResponseOutput("OrderAmount"));

                var x = res.GetResponseOutputDataSet("orders");
                Tablo1 = x.Tables[0];
                string transaction_id;
                for (int i = 0; i < Tablo1.Rows.Count; i++)
                {
                    transaction_id = Tablo1.Rows[i]["TRANSACTION_ID"].ToString();
                }
                OrderEntryStatus = Convert.ToInt32(res.GetResponseOutput("OrderEntryStatus"));
                
            }else
            {
                Error = "HATA : " + Error;
            }

            if (Error == null) Error = "";


            return Error;
        }


        //********************************************************************************************************************


        public string Update_Equity_Order(string TransactionId, string debitcredit, string fininstid, decimal units, decimal price, string customerid, string accountid, string valuedate, string initialmarketdate, int initialMarketSessionSel, int EndingMarketSessionSel, int orderMaxlot, string tip, string gecerlilik, string lak)
        {
            string Error;
            string orderId = "", orderType , newTIF = "0", newExpireDate;
            bool result=false;

            orderType = lak;
 
            if (gecerlilik == "Gün")
                newTIF = "0";
            else if (gecerlilik == "KİE")
                newTIF = "3";

            DateTime dt = Convert.ToDateTime(initialmarketdate);
            newExpireDate = dt.ToString("yyyyMMdd");


            GtpXml g = new GtpXml("UPDATE_EQUITY_ORDER", "1.0");
            /*
              g.AddParameter("CheckLimits"         , "System.Boolean" , Convert.ToBoolean("True"));
              g.AddParameter("IsPastDateOrder"     , "System.Boolean" , Convert.ToBoolean("False"));
              g.AddParameter("CanUpdatePriceUnit"  , "System.String"  , "0");
              g.AddParameter("TransactionId"       , "string"         , "0000-017I6V-IET");
              g.AddParameter("LimitControl"        , "System.String"  , "N");
              g.AddParameter("DebitCredit"         , "string"         , "CREDIT");
              g.AddParameter("FinInstId"           , "System.String"  , "0000-0001MH-FIN");
              g.AddParameter("Units"               , "System.Decimal" , Convert.ToDecimal(1));
              g.AddParameter("Price"               , "System.Decimal" , Convert.ToDecimal(7.420000));
              g.AddParameter("BrokerPasor"         , "string"         , "PASOR");
              g.AddParameter("PasorId"             , "string"         , "0000-000002-POS");
              g.AddParameter("BrokerId"            , "string"         , " ");
              g.AddParameter("CustomerId"          , "string"         , "0000-001PAL-CUS");
              g.AddParameter("AccountId"           , "string"         ,  "0000-003AJ4-ACC");
              g.AddParameter("valueDate"           , "System.DateTime", Convert.ToDateTime("2015-11-09 00:00:00.000"));
              g.AddParameter("InitialMarketDate"   , "System.DateTime", Convert.ToDateTime("2015-11-05 00:00:00.000"));
              g.AddParameter("InitialMarketSessionSel", "System.String", "1");
              g.AddParameter("EndingMarketSessionSel" , "System.String", "1");
              g.AddParameter("orderMaxLot"            , "System.Int32", Convert.ToInt32(25000));
              g.AddParameter("OrderType"              , "System.String", "LOT");
            */

            g.AddParameter("CheckLimits", "System.Boolean", Convert.ToBoolean("True"));
            g.AddParameter("IsPastDateOrder", "System.Boolean", Convert.ToBoolean("False"));
            g.AddParameter("CanUpdatePriceUnit", "System.String", "0");
            g.AddParameter("TransactionId", "string", TransactionId);
            g.AddParameter("LimitControl", "System.String", "N");
            g.AddParameter("DebitCredit", "string", debitcredit);
            g.AddParameter("FinInstId", "System.String", fininstid);
            g.AddParameter("Units", "System.Decimal", units);
            g.AddParameter("Price", "System.Decimal", price);
            g.AddParameter("BrokerPasor", "string", "PASOR");
            g.AddParameter("PasorId", "string", "0000-000002-POS");
            g.AddParameter("BrokerId", "string", " ");
            g.AddParameter("CustomerId", "string", customerid);
            g.AddParameter("AccountId", "string", accountid);

            g.AddParameter("valueDate", "System.DateTime", valuedate);
            //g.AddParameter("valueDate", "System.DateTime", "2015-11-10 00:00:00.000");
           
            g.AddParameter("InitialMarketDate", "System.DateTime", initialmarketdate);
            //g.AddParameter("InitialMarketDate", "System.DateTime", "2015-11-06 00:00:00.000");
            
            
            g.AddParameter("InitialMarketSessionSel", "System.String", initialMarketSessionSel);
            g.AddParameter("EndingMarketSessionSel", "System.String" , EndingMarketSessionSel);
            g.AddParameter("orderMaxLot", "System.Int32", orderMaxlot);
            //* g.AddParameter("OrderType", "System.String", tip);
            g.AddParameter("OrderType", "System.String", orderType);
            g.AddParameter("newTIF", "System.String", newTIF);
            g.AddParameter("newExpireDate", "System.String", newExpireDate);

            GtpXml res = new GtpXml(frmx.RbmInvoke(g.Xml));
            try
            { Error = res.GetNodeContent("request-broker-message\\response\\error"); }
            catch
            {
                Error = null;
            }


            if (Error == null) Error = res.GetResponseOutput("Error");
            if (Error != null) Error += ":" + res.GetResponseOutput("RealError");

            if (Error == null)
            {
                orderId = Convert.ToString(res.GetResponseOutput("orderId"));
                result = Convert.ToBoolean(res.GetResponseOutput("Result"));
            }
            else
            {
                Error = "HATA : " + Error;
            }

            if (Error == null) Error = "";


            return Error;
        }


        //********************************************************************************************************************

        public string Save_Improve_Order(string TransactionId, decimal improveprice, decimal improveunits, decimal oldunits, string gecerlilik, string initialmarketdate)
        {
            string Error;
            string orderId = "", oldTIF="0" , newTIF="0" , newExpireDate ;
            bool result = false;

            if (gecerlilik == "Gün")
                newTIF = "0";
            else if (gecerlilik == "KİE")
                newTIF = "3";

            // newExpireDate = initialmarketdate;
            DateTime dt = Convert.ToDateTime(initialmarketdate);
            newExpireDate = dt.ToString("yyyyMMdd");


            GtpXml g = new GtpXml("SAVE_IMPROVE_ORDER", "1.0");

            g.AddParameter("orderId"       , "System.String"   , TransactionId);
            g.AddParameter("improvePrice"  , "System.Decimal"  , improveprice);
            g.AddParameter("improveUnits"  , "System.Decimal"  , improveunits);
            g.AddParameter("oldUnits"      , "System.Decimal"  , oldunits);
            g.AddParameter("oldTIF"        , "System.String"   , oldTIF);
            g.AddParameter("newTIF"        , "System.String"   , newTIF);
            g.AddParameter("newExpireDate" , "System.String"   , newExpireDate);
            g.AddParameter("RejectIfOrderChanged", "bool"      , true);

         
            GtpXml res = new GtpXml(frmx.RbmInvoke(g.Xml));
            try
            { Error = res.GetNodeContent("request-broker-message\\response\\error"); }
            catch
            {
                Error = null;
            }

            if (Error == null) Error = res.GetResponseOutput("Error");
            if (Error != null) Error += ":" + res.GetResponseOutput("RealError");

            if (Error == null)
            {
                orderId = Convert.ToString(res.GetResponseOutput("orderId"));
            }
            else
            {
                Error = "HATA : " + Error;
            }

            if (Error == null) Error = "";


            return Error;
        }


        //********************************************************************************************************************


        public string Delete_Equity_Order(string TransactionId)
        {
            string Error;
            string orderId = "";
            bool result = false;

            GtpXml g = new GtpXml("DELETE_EQUITY_ORDER", "1.0");
            g.AddParameter("orderId", "System.String", TransactionId);


            GtpXml res = new GtpXml(frmx.RbmInvoke(g.Xml));
            try
            {   Error = res.GetNodeContent("request-broker-message\\response\\error"); }
            catch
            {
                Error = null;
            }


            if (Error == null) Error = res.GetResponseOutput("Error");
            if (Error != null) Error += ":" + res.GetResponseOutput("RealError");
            if (Error == null) Error = "";

            return Error;
        }


        //********************************************************************************************************************

        public void Algo_Grid_Initialize1(ref GridControl gridControl1, ref GridView gridView1, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkedt, DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit btn_detay, Gtp.Framework.ControlLibrary.Form frm)
        {
            frmx = frm;

            DataTable dt = new DataTable();
            dt.Columns.Add("CHECK", Type.GetType("System.Boolean"));
            dt.Columns.Add("ONAY",  Type.GetType("System.Boolean"));

            dt.Columns.Add("REFERANSID" , Type.GetType("System.Decimal"));
            dt.Columns.Add("HESAPNO"    , Type.GetType("System.String"));
            dt.Columns.Add("EMIRTIPI"   , Type.GetType("System.String"));
            dt.Columns.Add("MENKUL"     , Type.GetType("System.String"));
            dt.Columns.Add("ALSAT"      , Type.GetType("System.String"));
            dt.Columns.Add("LOT"        , Type.GetType("System.Decimal"));
            dt.Columns.Add("YUZDE"      , Type.GetType("System.Int32"));
            dt.Columns.Add("TETIKZAMAN" , Type.GetType("System.String"));
            dt.Columns.Add("STATU"      , Type.GetType("System.String"));

            dt.Columns.Add("TETIKSAAT", Type.GetType("System.Int32"));
            dt.Columns.Add("TETIKDAKIKA", Type.GetType("System.Int32"));
            dt.Columns.Add("TETIKSANIYE", Type.GetType("System.Int32"));
            dt.Columns.Add("CUSTID"     , Type.GetType("System.String"));
            dt.Columns.Add("FININSTID"  , Type.GetType("System.String"));
            dt.Columns.Add("ISLEMTARIHI", Type.GetType("System.String"));
            dt.Columns.Add("TAKASTARIHI", Type.GetType("System.String"));
            dt.Columns.Add("ACCID"      , Type.GetType("System.String"));
            dt.Columns.Add("MAXLOT"     , Type.GetType("System.Decimal"));
            dt.Columns.Add("SONFIYAT"   , Type.GetType("System.Decimal"));
            dt.Columns.Add("HESAPLANANEMIRFIYATI", Type.GetType("System.Decimal"));
            dt.Columns.Add("USERINF"    , Type.GetType("System.String"));

            dt.Columns.Add("GIB_BAS_ZMN", Type.GetType("System.String"));
            dt.Columns.Add("GIB_BIT_ZMN", Type.GetType("System.String"));
            dt.Columns.Add("GIB_PARCASAYISI", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_ENSONAKTIFOLANPARCA", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_AKTIFLESMESEKLI", Type.GetType("System.String"));
          
            dt.Columns.Add("GIB_YUZDE1", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_YUZDE2", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_BAS_ZMN1_SAAT", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_BAS_ZMN1_DAKIKA", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_BAS_ZMN1_SANIYE", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_BIT_ZMN1_SAAT", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_BIT_ZMN1_DAKIKA", Type.GetType("System.Int32"));
            dt.Columns.Add("GIB_BIT_ZMN1_SANIYE", Type.GetType("System.Int32"));
            dt.Columns.Add("MANUELAKTIVASYON", Type.GetType("System.String"));



            gridControl1.DataSource = dt;

            gridView1.Columns[0].ColumnEdit = chkedt;
            gridView1.Columns[0].Width = 20;                         // kolon size ını sabitleyelim.
            gridView1.Columns[0].MaxWidth = 20;  
            gridView1.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
            gridView1.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.

            gridView1.Columns[1].ColumnEdit = btn_detay;
            gridView1.Columns[1].MinWidth = 45;                      // kolon size ını sabitleyelim.
            gridView1.Columns[1].MaxWidth = 45;  
            gridView1.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
            gridView1.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.

            gridView1.Columns[2].Caption = "Ref.No";          //* kolon header bilgilerini değiştirelim.
            gridView1.Columns[2].Visible = false;

            gridView1.Columns[3].Caption = "Hesap";
            gridView1.Columns[3].MinWidth = 75;
            gridView1.Columns[3].MaxWidth = 75;   

            gridView1.Columns[4].Caption = "E.Tip";
            gridView1.Columns[4].MinWidth = 35;
            gridView1.Columns[4].MaxWidth = 35;   

            gridView1.Columns[5].Caption = "Menkul";
            gridView1.Columns[5].MinWidth = 55;
            gridView1.Columns[5].MaxWidth = 55;   

            gridView1.Columns[6].Caption = "Al/Sat";
            gridView1.Columns[6].MinWidth = 45;
            gridView1.Columns[6].MaxWidth = 45;   

            gridView1.Columns[7].Caption = "Lot";
            gridView1.Columns[7].MaxWidth = 85;
            gridView1.Columns[7].MinWidth = 85;   

            gridView1.Columns[8].Caption = "Mrj.%";
            gridView1.Columns[8].MinWidth = 40;
            gridView1.Columns[8].MaxWidth = 40;   

            gridView1.Columns[9].Caption = "Zaman";
            gridView1.Columns[9].MinWidth = 55;
            gridView1.Columns[9].MaxWidth = 55;   

            gridView1.Columns[10].Caption= "Statü";
            gridView1.Columns[10].MinWidth = 70;
            gridView1.Columns[10].MaxWidth = 70;   

            gridView1.Columns[11].Caption  = "Saat";
            gridView1.Columns[11].Visible  = false;
            gridView1.Columns[12].Caption = "Dakika";
            gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Caption = "Saniye";
            gridView1.Columns[13].Visible = false;
            gridView1.Columns[14].Caption = "Custid";
            gridView1.Columns[14].Visible = false;
            gridView1.Columns[15].Caption = "Fininstid";
            gridView1.Columns[15].Visible = false;
            gridView1.Columns[16].Caption = "Islemtarihi";
            gridView1.Columns[16].Visible = false;
            gridView1.Columns[17].Caption = "Takastarihi";
            gridView1.Columns[17].Visible = false;
            gridView1.Columns[18].Caption = "Accid";
            gridView1.Columns[18].Visible = false;
            gridView1.Columns[19].Caption = "Maxlot";
            gridView1.Columns[19].Visible = false;
            gridView1.Columns[20].Caption = "Sonfiyat";
            gridView1.Columns[20].Visible = false;
            gridView1.Columns[21].Caption = "Hesaplananemirfiyati";
            gridView1.Columns[21].Visible = false;
            gridView1.Columns[22].Caption = "Userinf";
            gridView1.Columns[22].Visible = false;

            gridView1.Columns[23].Caption = "Gib Bşl.Zmn";
            gridView1.Columns[23].MinWidth = 65;
            gridView1.Columns[23].MaxWidth = 65;   

            gridView1.Columns[24].Caption = "Gib Bit.Zmn";
            gridView1.Columns[24].MinWidth = 65;
            gridView1.Columns[24].MaxWidth = 65;   

            gridView1.Columns[25].Caption = "Gib Prç.Sayı";
            gridView1.Columns[25].MinWidth = 65;
            gridView1.Columns[25].MaxWidth = 65;   

            gridView1.Columns[26].Caption = "Gib Son Akt";  //*En son aktive olan parça sıra numarası.
            gridView1.Columns[25].MinWidth = 65;
            gridView1.Columns[25].MaxWidth = 65;   

            gridView1.Columns[27].Caption = "Borsa Statüsü";
            gridView1.Columns[27].MaxWidth = 205;
            gridView1.Columns[27].MinWidth = 205;   

            gridView1.Columns[28].Caption = "Gib Yüzde1";
            gridView1.Columns[28].Visible = false;
            gridView1.Columns[29].Caption = "Gib Yüzde2";
            gridView1.Columns[29].Visible = false;
            gridView1.Columns[30].Caption = "Gib Bşl.Zmn.Sa";
            gridView1.Columns[30].Visible = false;
            gridView1.Columns[31].Caption = "Gib Bşl.Zmn.Dk";
            gridView1.Columns[31].Visible = false;
            gridView1.Columns[32].Caption = "Gib Bşl.Zmn.Sn";
            gridView1.Columns[32].Visible = false;
            gridView1.Columns[33].Caption = "Gib Bit.Zmn.Sa";
            gridView1.Columns[33].Visible = false;
            gridView1.Columns[34].Caption = "Gib Bit.Zmn.Dk";
            gridView1.Columns[34].Visible = false;
            gridView1.Columns[35].Caption = "Gib Bit.Zmn.Sn";
            gridView1.Columns[35].Visible = false;

            gridView1.Columns[36].Caption = "Açıklama";
            gridView1.Columns[36].MaxWidth = 185;
            gridView1.Columns[36].MinWidth = 185;   


            /*
               gridView1.Columns[0].Width = 30;                         // kolon size ını sabitleyelim.
               gridView1.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
               gridView1.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
               gridView1.Columns[1].Width = 30;                         // kolon size ını sabitleyelim.
               gridView1.Columns[1].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
               gridView1.Columns[1].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.
               gridView1.Columns[10].Visible = false;
            */

            /*
            for (int i = 2; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
            }
            */
        }


        //********************************************************************************************************************


        public void AlgoBekleyenEmirler_Al(ref GridControl gridControl1, ref GridView gridView1, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkedt, DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit btn_detay, Gtp.Framework.ControlLibrary.Form frm)
        {

            DataTable Tablo1;
            Algo_Grid_Initialize1(ref gridControl1, ref gridView1, chkedt, btn_detay, frm);
            (gridControl1.DataSource as DataTable).Clear();  //* tabloyu temizler.

           

              string str=  "DECLARE @Tarih datetime = cast(floor(cast(GETDATE() as float)) as datetime)  ";
              str+= "SELECT FILL_PRICE GerceklesmeFiyati , ";
              
              str += " Case When (t.UNITS-isNull(t.REALIZED_UNITS,0)=0) then 'Gerçekleşti - (' + CONVERT(varchar, CAST(f.FILL_PRICE AS money), 1) + ";
              str += "' TL , ' +  CONVERT(varchar, CAST(f.FILL_UNITS AS money), 1) + ' Lot)' ";
              

              str+= "When t.UNITS-isNull(t.REALIZED_UNITS,0)>0 And t.TRANSACTION_STATUS_ID not in ('0000-000001-ESD','0000-000002-ESD','0000-00000F-ESD', '0000-000005-ESD','0000-000006-ESD') Then 'İPTAL EDİLDİ (' + s.NAME  + ')'";
              str += " else s.NAME End BorsaDurum,";
              
              str+=" d.* ";
              str+= "FROM NETMESAJ.dbo.IcmAlgoEmirler d ";
              str+= "Left Join [gtpbrdb].[dbo].GTPBR_EQ_TRANS t on d.TransactionId collate Latin1_General_CI_AS=t.TRANSACTION_ID  AND (t.INITIAL_MARKET_SESSION_DATE= @Tarih ) ";
              str+= "Left Join [gtpbrdb].[dbo].GTPBR_EQ_TRANS_FILLS f on f.TRANSACTION_ID=t.TRANSACTION_ID ";
              str += "Left Join [gtpbrdb].[dbo].GTPBR_EQ_TRANS_STATUS_DEFS s  on s.TRANSACTION_STATUS_ID=t.TRANSACTION_STATUS_ID ";  
               
              str+= "WHERE d.IslemTarihi = cast(floor(cast(GETDATE() as float)) as datetime) ";
              str+= "AND d.HesapNo='112542-103'";       //* ICM için bu satırı KAPATALIM.....
              // str+= "AND d.HesapNo<>'112542-103'";   //* ICM için bu satırı açalım.....
            

            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];
           
            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {
                DataRow newrow = (gridControl1.DataSource as DataTable).NewRow();
                newrow["CHECK"] = false;

                if (Convert.ToString(Tablo1.Rows[i]["EmirTipi"]) == "G.I.B")
                {
                    newrow["ONAY"] = true;
                    newrow["GIB_BAS_ZMN"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BasZmn1"]),2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BasZmn2"]),2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BasZmn3"]),2);
                    newrow["GIB_BIT_ZMN"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BitZmn1"]),2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BitZmn2"]),2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BitZmn3"]),2);
                    newrow["GIB_PARCASAYISI"] = Convert.ToString(Tablo1.Rows[i]["Gib_ParcaSayisi"]);
                    newrow["GIB_ENSONAKTIFOLANPARCA"] = Convert.ToString(Tablo1.Rows[i]["Gib_Enson_AktifOlanParca"]);
                     
                    newrow["GIB_AKTIFLESMESEKLI"] = Convert.ToString(Tablo1.Rows[i]["AktiflesmeSekli"]);
                      

                    newrow["GIB_YUZDE1"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_Yuzde1"]),2);
                    newrow["GIB_YUZDE2"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_Yuzde2"]), 2);
                    newrow["GIB_BAS_ZMN1_SAAT"]   = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BasZmn1"]), 2);
                    newrow["GIB_BAS_ZMN1_DAKIKA"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BasZmn2"]), 2);
                    newrow["GIB_BAS_ZMN1_SANIYE"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BasZmn3"]), 2);

                    newrow["GIB_BIT_ZMN1_SAAT"]   = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BitZmn1"]), 2);
                    newrow["GIB_BIT_ZMN1_DAKIKA"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BitZmn2"]), 2);
                    newrow["GIB_BIT_ZMN1_SANIYE"] = SifirKoy(Convert.ToString(Tablo1.Rows[i]["Gib_BitZmn3"]), 2);

                }
                else if (Convert.ToString(Tablo1.Rows[i]["EmirTipi"]) == "Z.A")
                {
                   // newrow["ONAY"] = false;
                    newrow["YUZDE"]       = Convert.ToInt32(Tablo1.Rows[i]["MarjYuzde"]);
                    newrow["TETIKZAMAN"]  = SifirKoy(Convert.ToString(Tablo1.Rows[i]["TetikSaat"]), 2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["TetikDakika"]), 2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["TetikSaniye"]), 2);
                    newrow["TETIKSAAT"]   = Convert.ToInt32(Tablo1.Rows[i]["TetikSaat"]);
                    newrow["TETIKDAKIKA"] = Convert.ToInt32(Tablo1.Rows[i]["TetikDakika"]);
                    newrow["TETIKSANIYE"] = Convert.ToInt32(Tablo1.Rows[i]["TetikSaniye"]);
                    newrow["SONFIYAT"]    = Convert.ToDecimal(Tablo1.Rows[i]["SonFiyat"]);
                    newrow["HESAPLANANEMIRFIYATI"] = Convert.ToDecimal(Tablo1.Rows[i]["HesaplananEmirFiyati"]);

                   // if (Tablo1.Rows[i]["GerceklesmeFiyati"].ToString().Length > 0)
                         newrow["GIB_AKTIFLESMESEKLI"]  = Convert.ToString(Tablo1.Rows[i]["BorsaDurum"]);
                }

                newrow["REFERANSID"]  = Convert.ToDecimal(Tablo1.Rows[i]["ReferansId"]);
                newrow["HESAPNO"]     = Convert.ToString(Tablo1.Rows[i]["HesapNo"]);
                newrow["EMIRTIPI"]    = Convert.ToString(Tablo1.Rows[i]["EmirTipi"]);
                newrow["MENKUL"]      = Convert.ToString(Tablo1.Rows[i]["Menkul"]);
                newrow["ALSAT"]       = Convert.ToString(Tablo1.Rows[i]["AlSat"]);
                newrow["LOT"]         = Convert.ToDecimal(Tablo1.Rows[i]["Lot"]);

                if (Convert.ToString(Tablo1.Rows[i]["Statu"]) == "1")
                    newrow["STATU"] = "Bekliyor";
                else if (Convert.ToString(Tablo1.Rows[i]["Statu"]) == "2")
                    newrow["STATU"] = "Emir İletildi";
                else if (Convert.ToString(Tablo1.Rows[i]["Statu"]) == "3")
                    newrow["STATU"] = "İptal Edildi";

                newrow["CUSTID"]      = Convert.ToString(Tablo1.Rows[i]["CustId"]);
                newrow["FININSTID"]   = Convert.ToString(Tablo1.Rows[i]["Fin_Inst_Id"]);
                newrow["ISLEMTARIHI"] = Convert.ToDateTime(Tablo1.Rows[i]["IslemTarihi"]);
                newrow["TAKASTARIHI"] = Convert.ToDateTime(Tablo1.Rows[i]["TakasTarihi"]);
                newrow["ACCID"]       = Convert.ToString(Tablo1.Rows[i]["AccId"]);
                newrow["MAXLOT"]      = Convert.ToDecimal(Tablo1.Rows[i]["Maxlot"]);
                newrow["USERINF"]     = Convert.ToString(Tablo1.Rows[i]["Userinf"]);
                newrow["MANUELAKTIVASYON"] = Convert.ToString(Tablo1.Rows[i]["ManuelAktivasyonAciklama"]);
              

                (gridControl1.DataSource as DataTable).Rows.Add(newrow);

            }

            gridControl1.Refresh();
                
            return;
        }


        //********************************************************************************************************************

        public AlgoOrderList Gib_AnaEmir_Bilgileri_Al(Gtp.Framework.ControlLibrary.Form frm, decimal RefId)
        {
            string aktsek;
            AlgoOrderList lst = new AlgoOrderList();
            AlgoOrder ord;
            DataTable Tablo1;

            frmx = frm ;

            string str = "SELECT * ";
            str += "FROM NETMESAJ.dbo.IcmAlgoEmirler ";
            str += "WHERE IslemTarihi = cast(floor(cast(GETDATE() as float)) as datetime)  ";
            str += " AND ReferansId=" + Convert.ToString(RefId); 
            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {
                ord = new AlgoOrder();
                ord.Emirtipi   = Convert.ToString(Tablo1.Rows[i]["EmirTipi"]);
                ord.Referansid = Convert.ToDecimal(Tablo1.Rows[i]["ReferansId"]);
                ord.Hesapno    = Convert.ToString(Tablo1.Rows[i]["HesapNo"]);
                ord.Menkul     = Convert.ToString(Tablo1.Rows[i]["Menkul"]);
                ord.Alsat      = Convert.ToString(Tablo1.Rows[i]["AlSat"]);
                ord.Lot        = Convert.ToDecimal(string.Format("{0:0,0}", Convert.ToDecimal(Tablo1.Rows[i]["Lot"])));
                ord.Gib_yuzde1 = Convert.ToInt32(Tablo1.Rows[i]["Gib_Yuzde1"]);
                ord.Gib_yuzde2 = Convert.ToInt32(Tablo1.Rows[i]["Gib_Yuzde2"]);
                
                ord.Gib_bas_saat   = Convert.ToInt32(Tablo1.Rows[i]["Gib_BasZmn1"]);
                ord.Gib_bas_dakika = Convert.ToInt32(Tablo1.Rows[i]["Gib_BasZmn2"]);
                ord.Gib_bas_saniye = Convert.ToInt32(Tablo1.Rows[i]["Gib_BasZmn3"]);
                ord.Gib_bit_saat   = Convert.ToInt32(Tablo1.Rows[i]["Gib_BitZmn1"]);
                ord.Gib_bit_dakika = Convert.ToInt32(Tablo1.Rows[i]["Gib_BitZmn2"]);
                ord.Gib_bit_saniye = Convert.ToInt32(Tablo1.Rows[i]["Gib_BitZmn3"]);
                ord.Gib_parcasayisi = Convert.ToInt32(Tablo1.Rows[i]["Gib_ParcaSayisi"]);
                ord.Aktiflesmesekli = Convert.ToString(Tablo1.Rows[i]["AktiflesmeSekli"]);

                lst.Resultlist.Add(ord);
            }

            return lst;
        }

        //********************************************************************************************************************

        public void Gib_Algo_Grid_Initialize1(ref GridControl gridControl1, ref GridView gridView1, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkedt)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("CHECK", Type.GetType("System.Boolean"));

            dt.Columns.Add("SIRA", Type.GetType("System.Int32"));
            dt.Columns.Add("LOT", Type.GetType("System.Decimal"));
            dt.Columns.Add("FIYAT", Type.GetType("System.Decimal"));
            dt.Columns.Add("GERFIYAT", Type.GetType("System.Decimal"));
            dt.Columns.Add("SEANSAGRORT", Type.GetType("System.Decimal"));

            dt.Columns.Add("ZAMAN", Type.GetType("System.String"));
            dt.Columns.Add("AKTIFLESMESEKLI", Type.GetType("System.String"));
            dt.Columns.Add("STATU", Type.GetType("System.String"));
            dt.Columns.Add("TRANSACTIONID", Type.GetType("System.String"));

            dt.Columns.Add("TETIKSAAT", Type.GetType("System.Int32"));
            dt.Columns.Add("TETIKDAKIKA", Type.GetType("System.Int32"));
            dt.Columns.Add("TETIKSANIYE", Type.GetType("System.Int32"));
            dt.Columns.Add("BORSADURUM", Type.GetType("System.String"));
            dt.Columns.Add("MANUELAKTIVASYON", Type.GetType("System.String"));

            gridControl1.DataSource = dt;

            gridView1.Columns[0].ColumnEdit = chkedt;
            gridView1.Columns[0].Width = 7;                         // kolon size ını sabitleyelim.
            gridView1.Columns[0].OptionsColumn.AllowSize = false;    // kolon size ını değiştiremesin.
            gridView1.Columns[0].OptionsColumn.ShowCaption = false;  // kolon başlığını göstermez.

            gridView1.Columns[1].Caption = "Sira";
            gridView1.Columns[1].MaxWidth = 30;
            gridView1.Columns[1].MinWidth = 30;  
            gridView1.Columns[1].OptionsColumn.AllowSize = false;

            gridView1.Columns[2].Caption = "Lot";
            gridView1.Columns[2].MaxWidth = 70;
            gridView1.Columns[2].MinWidth = 70;  
            gridView1.Columns[2].OptionsColumn.AllowSize = false;

            gridView1.Columns[3].Caption = "Emir Fyt.";
            gridView1.Columns[3].MaxWidth = 65;
            gridView1.Columns[3].MinWidth = 65;  
            gridView1.Columns[3].OptionsColumn.AllowSize = false;

            gridView1.Columns[4].Caption = "Ger.Fyt.";
            gridView1.Columns[4].MaxWidth = 65;
            gridView1.Columns[4].MinWidth = 65;
            gridView1.Columns[4].OptionsColumn.AllowSize = false;

            gridView1.Columns[5].Caption = "Ağr.Ort.";
            gridView1.Columns[5].MaxWidth = 65;
            gridView1.Columns[5].MinWidth = 65;
            gridView1.Columns[5].OptionsColumn.AllowSize = false;


            gridView1.Columns[6].Caption = "Zaman";
            gridView1.Columns[6].MaxWidth = 55;
            gridView1.Columns[6].MinWidth = 55;
            gridView1.Columns[6].OptionsColumn.AllowSize = false;

            gridView1.Columns[7].Caption = "Aktv.Şekli";
            gridView1.Columns[7].MaxWidth = 100;
            gridView1.Columns[7].MinWidth = 100;  

            gridView1.Columns[8].Caption = "Emir Durumu";

            gridView1.Columns[9].Caption  = "TransId";
            gridView1.Columns[9].Visible  = false;
            gridView1.Columns[10].Caption  = "Saat";
            gridView1.Columns[10].Visible  = false;
            gridView1.Columns[11].Caption = "Dakika";
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Caption = "Saniye";
            gridView1.Columns[12].Visible = false;

            gridView1.Columns[13].Caption = "Borsa Statüsü";

            gridView1.Columns[14].Caption = "Açıklama";
            gridView1.Columns[14].MaxWidth = 200;
            gridView1.Columns[14].MinWidth = 200;   

        }

        //********************************************************************************************************************

        public void Gib_EmirParcalarini_Tabloya_Doldur(ref GridControl gridControl1, ref GridView gridView1, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkedt,  decimal RefId)
        {
            DataTable Tablo1;
            Gib_Algo_Grid_Initialize1(ref gridControl1, ref gridView1, chkedt);
            (gridControl1.DataSource as DataTable).Clear();  //* tabloyu temizler.
            /*
            string str = "SELECT * ";
            str += "FROM NETMESAJ.dbo.IcmAlgoEmirler_GibEmirDetay ";
            str += "WHERE IslemTarihi = cast(floor(cast(GETDATE() as float)) as datetime)  ";
            str += " AND ReferansId=" + Convert.ToString(RefId); 
            */

       

            string  str= " DECLARE @Tarih datetime = cast(floor(cast(GETDATE() as float)) as datetime) ";
                   str+= " SELECT  d.TransactionId, d.*,  Case  When (d.TransactionId is Null) Then '-' ";
                   str+= " When (t.UNITS-isNull(t.REALIZED_UNITS,0)>0) And ( t.TRANSACTION_STATUS_ID in ( '0000-000001-ESD',  '0000-000002-ESD',  '0000-00000F-ESD',  '0000-000005-ESD',  '0000-000006-ESD')) Then 'Borsaya İletildi' ";
                   str+= " When (t.UNITS-isNull(t.REALIZED_UNITS,0)=0) then 'GERÇEKLEŞTİ' ";
                   str+= " Else 'İptal Edildi (' + s.NAME + ')' End BorsaDurum ,";
                   str+= " f.FILL_PRICE GerceklesmeFiyati ";
                   str+= " FROM [NETMESAJ].[dbo].IcmAlgoEmirler_GibEmirDetay d ";
                   str+= " Left Join [gtpbrdb].[dbo].GTPBR_EQ_TRANS t on d.TransactionId collate Latin1_General_CI_AS=t.TRANSACTION_ID " ;
                   str += " AND (t.INITIAL_MARKET_SESSION_DATE= @Tarih ) ";
                   str+= " Left Join [gtpbrdb].[dbo].GTPBR_EQ_TRANS_FILLS f on f.TRANSACTION_ID=t.TRANSACTION_ID ";
                   str += "Left Join [gtpbrdb].[dbo].GTPBR_EQ_TRANS_STATUS_DEFS s  on s.TRANSACTION_STATUS_ID=t.TRANSACTION_STATUS_ID ";  
                   str+= " WHERE (d.IslemTarihi= @Tarih ) AND (d.ReferansId=" + Convert.ToString(RefId) + ")"; 


            var x = ExecCustomSQL(str).GetResponseOutputDataSet("Result");
            Tablo1 = x.Tables[0];

            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {

                DataRow newrow        = (gridControl1.DataSource as DataTable).NewRow();
                newrow["CHECK"]       = false;
                newrow["SIRA"]        = Convert.ToInt32(Tablo1.Rows[i]["Sira"]);
                newrow["LOT"]         = Convert.ToDecimal(Tablo1.Rows[i]["Lot"]);
                newrow["FIYAT"]       = Convert.ToDecimal(Tablo1.Rows[i]["Fiyat"]);
                if (Tablo1.Rows[i]["GerceklesmeFiyati"].ToString().Length == 0)
                    newrow["GERFIYAT"] = 0;
                else 
                    newrow["GERFIYAT"]    = Convert.ToDecimal(Tablo1.Rows[i]["GerceklesmeFiyati"]);
                newrow["SEANSAGRORT"] = Convert.ToDecimal(Tablo1.Rows[i]["HesaplananAgirlikliOrtalamaFiyati"]);
                newrow["ZAMAN"]       = SifirKoy(Convert.ToString(Tablo1.Rows[i]["TetikSaat"]),2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["TetikDakika"]),2) + ":" + SifirKoy(Convert.ToString(Tablo1.Rows[i]["TetikSaniye"]),2);
                newrow["TETIKSAAT"]   = Convert.ToDecimal(Tablo1.Rows[i]["TetikSaat"]);
                newrow["TETIKDAKIKA"] = Convert.ToDecimal(Tablo1.Rows[i]["TetikDakika"]);
                newrow["TETIKSANIYE"] = Convert.ToDecimal(Tablo1.Rows[i]["TetikSaniye"]);
                newrow["AKTIFLESMESEKLI"] = Convert.ToString(Tablo1.Rows[i]["AktiflesmeSekli"]);
                newrow["TRANSACTIONID"]   = Convert.ToString(Tablo1.Rows[i]["TransactionId"]);
                newrow["BORSADURUM"]      = Convert.ToString(Tablo1.Rows[i]["BorsaDurum"]);

                if (Convert.ToString(Tablo1.Rows[i]["Statu"]) == "1")
                    newrow["STATU"] = "Bekliyor";
                else if (Convert.ToString(Tablo1.Rows[i]["Statu"]) == "2")
                    newrow["STATU"] = "Emir İletildi";
                else if (Convert.ToString(Tablo1.Rows[i]["Statu"]) == "3")
                    newrow["STATU"] = "İptal Edildi";

                newrow["MANUELAKTIVASYON"] = Convert.ToString(Tablo1.Rows[i]["ManuelAktivasyonAciklama"]);

                (gridControl1.DataSource as DataTable).Rows.Add(newrow);
            }

                gridControl1.Refresh();

                return;

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
    

    }


}
