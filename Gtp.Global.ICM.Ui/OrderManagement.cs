using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using DevExpress.XtraGrid.Views.Grid;
using System.Threading;
using System.Globalization;
using System.Messaging;


// using System.Windows.Forms;
using Gtp.Framework;
using Gtp.Framework.ControlLibrary;
using Gtp.Global.ICM.Ui;

namespace Gtp.Global.ICM.Ui
{
    public partial class OrderManagement : Form
    {
        int ROW;
        decimal T_E1, T_E11, T_E111, T_E2, T_E22, T_E222, T_E3, T_E33, T_E333, T_E5, T_E55, T_E555, T_E6, T_E66, T_E666;
        decimal T_F1, T_F11, T_F111, T_F2, T_F22, T_F222, T_F3, T_F33, T_F333, T_F5, T_F55, T_F555, T_F6, T_F66, T_F666;
        decimal T_FF1, T_FF11, T_FF111, T_FF2, T_FF22, T_FF222, T_FF3, T_FF33, T_FF333, T_FF5, T_FF55, T_FF555, T_FF6, T_FF66, T_FF666;
        decimal T_P1, T_P11, T_P111, T_P2, T_P22, T_P222, T_P3, T_P33, T_P333, T_P5, T_P55, T_P555, T_P6, T_P66, T_P666;
       public string SESSIONDATE;

        public OrderManagement()
        {
            InitializeComponent();
        }

   //********************************************************************************************************************


        private void OrderManagement_Load(object sender, EventArgs e)
        {
            //**Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
            Menkul_Doldur();
            Musteri_Doldur();
            Danisman_Doldur();
            Ekran_ToplamAlanlari_Bosalt();
            PasiftekiEmirler_Islem();
            GerceklesenEmirler_Islem();
            //GecmisTarihliEmirler_Islem();
            dateEdit1.DateTime = DateTime.Now.Date;
            xtraTabControl1.SelectedTabPageIndex = 1;
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
            decimal degfiyat, tavan, taban, kademe;

            OrderOperations op = new OrderOperations();
            Tablo1 = op.FiyatKademeListesi_Al(this, menkul.Text);

            fiyat.Text = "";
            fiyat.Properties.Items.Clear();

            for (int i = 0; i < Tablo1.Rows.Count; i++)
            {
                taban = Convert.ToDecimal(Tablo1.Rows[i]["VALUE10"]);
                tavan = Convert.ToDecimal(Tablo1.Rows[i]["VALUE11"]);
                kademe = Convert.ToDecimal(Tablo1.Rows[i]["VALUE12"]);

                degfiyat = taban;
                while (degfiyat <= tavan)
                {
                    fiyat.Properties.Items.Add(string.Format("{0:#,0.000}", degfiyat));
                    degfiyat += kademe;
                }
            }

        }

   //********************************************************************************************************************


        public void Musteri_Doldur()
        {
            DataTable Tablo1;

            bool hepsidahil = true;
            OrderOperations op = new OrderOperations();
            Tablo1 = op.MusteriListesi_Al(this, hepsidahil);
 
            musteri.Properties.DataSource = Tablo1;
            musteri.Properties.DisplayMember = "ID";
            musteri.Properties.ValueMember = "NAME";
            musteri.ItemIndex = 0;
        }


        //********************************************************************************************************************

    
        public void Danisman_Doldur()
        {
            DataTable Tablo1;
            string str  = " SELECT 'HEPSİ' AS NAME , '000' AS ID";
                   str += " UNION ";
                   str += " SELECT 'FIX Kullanıcısı' AS NAME , '0000-00001N-PTY' AS ID";
                   str += " UNION ";
                   str += " SELECT 'GTP Administrator' AS NAME , '0000-000002-PTY' AS ID";   
                   str += " UNION ";

                   str += " SELECT P.NAME AS NAME, PARTY_ID AS ID  from GTPBS_POSITIONS P, GTPBS_EMPLOYEE_POSITIONS E, GTPBS_PARTIES PT WHERE PT.ENTITY_ID = E.EMP_ID AND PARTY_TYPE = 'EMPLOYEE' AND (P.DIV_ID = '0000-00001G-DIV' or P.NAME='FIX Kullanıcısı') AND PT.STATUS = 1 AND P.STATUS = 1 AND E.STATUS=1 And E.POSTN_ID=P.POSTN_ID ORDER BY ID";

                   //** str += " SELECT P.NAME AS NAME, PT.PARTY_ID AS ID from GTPBS_POSITIONS P, GTPBS_PARTIES PT WHERE PT.ENTITY_ID = P.POSTN_ID AND PARTY_TYPE = 'POSITION' AND DIV_ID = '0000-00001G-DIV' AND PT.STATUS = 1 AND P.STATUS = 1 order by ID";

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
            string alissatis="H" , seans="0" , hisse="" ;
            try
            {
                OrderOperations isl = new OrderOperations();
                isl.Grid_Initialize1(ref gridControl1, ref gridView1, this);
                isl.Grid_Initialize3(ref gridControl3, ref gridView3);

               //*BISTECH_KAPA string hangiseanstayiz = isl.HangiSeanstayiz();

                if (menkul.EditValue == null) hisse = "";   else  hisse = menkul.EditValue.ToString();

                if(radioButton1.Checked==true) alissatis="A";
                else if (radioButton2.Checked==true)  alissatis="S";
                else                                  alissatis="H";

                seans = "0";

                SESSIONDATE = isl.GetSessionDate();
                OrderList beklist = isl.BekleyenEmirler(musteri.Text.ToString(), danisman.EditValue.ToString(), hisse, alissatis, seans, SESSIONDATE);
                List<Order> lst = beklist.Resultlist;
                (gridControl1.DataSource as DataTable).Clear();  //* tabloyu temizler.

                foreach (Order a in lst)
                {
                    if ( (fiyat.Text!=null) && (fiyat.Text.Trim().Length > 0) )
                        if( (a.Fiyat != Convert.ToDecimal(fiyat.Text.ToString())))
                          continue;

                    //*BISTECH_KAPA   if ( (hangiseanstayiz=="2") && (a.Gecerlilik=="Seanslık") && (a.Seans==1))  //*2. seanstaysak : 1.seansta girilen Seanslık emirleri 
                    //*BISTECH_KAPA        continue;                                                              //*atlatalım. Çünkü 1.seansta tahtada kaldı onlar.

                       if (a.Ordinodurumu == "İptal")
                       {
                            if ((a.Seans == 0) || (a.Seans == 1))
                            {
                                T_P1++;
                                if (a.Alsat == "A")
                                {
                                    T_P2 += a.Lot;
                                    T_P5 += (a.Lot * a.Fiyat);
                                }
                                else if (a.Alsat == "S")
                                {
                                    T_P3 += a.Lot;
                                    T_P6 += (a.Lot * a.Fiyat);
                                }
                            }
                            else if (a.Seans == 2)
                            {
                                T_P11++;
                                if (a.Alsat == "A")
                                {
                                    T_P22 += a.Lot;
                                    T_P55 += (a.Lot * a.Fiyat);
                                }
                                else if (a.Alsat == "S")
                                {
                                    T_P33 += a.Lot;
                                    T_P66 += (a.Lot * a.Fiyat);
                                }
                            }
                            T_P111++;
                            if (a.Alsat == "A")
                            {
                                T_P222 += a.Lot;
                                T_P555 += (a.Lot * a.Fiyat);
                            }
                            else if (a.Alsat == "S")
                            {
                                T_P333 += a.Lot;
                                T_P666 += (a.Lot * a.Fiyat);
                            }

                       }
                       else if(a.Ordinodurumu == "Bekleyen")
                       {
                            if( (a.Seans==0) || (a.Seans==1) )
                            {
                                T_E1++;
                                if(a.Alsat=="A")
                                {
                                    T_E2 += a.Lot ;
                                    T_E5 += (a.Lot * a.Fiyat) ;
                                } else if(a.Alsat=="S")
                                {
                                    T_E3 += a.Lot ;
                                    T_E6 += (a.Lot *  a.Fiyat) ;
                                }
                            }else if(a.Seans==2)
                            {
                                T_E11++;
                                if(a.Alsat=="A")
                                {
                                    T_E22 += a.Lot ;
                                    T_E55 += (a.Lot * a.Fiyat) ;
                                } else if(a.Alsat=="S")
                                {
                                    T_E33 += a.Lot ;
                                    T_E66 += (a.Lot * a.Fiyat) ;
                                }

                            }
                            //* Günlük Emirler ...
                                T_E111++;
                                if(a.Alsat=="A")
                                {
                                    T_E222 += a.Lot ;
                                    T_E555 += (a.Lot *  a.Fiyat) ;
                                } else if(a.Alsat=="S")
                                {
                                    T_E333 += a.Lot ;
                                    T_E666 += (a.Lot * a.Fiyat) ;
                                }
                       }



                       PasiftekiEmirler_Doldur(a.Saat, a.Hesap, a.Adsoy, a.Menkul, a.Alsat, a.Lot, a.Fiyat, a.Ordinodurumu, a.Seans, a.Danismankodu,a.Transactionid,a.FinInstid,a.Customerid,a.Accountid,a.Gecerlilik,a.Maximumlot,a.Tip, a.Initialmarketsessiondate,a.Endingmarketsessiondate,a.Settlementdate,a.Initialmarketsessionsel,a.Endingmarketsessionsel,a.Emirnerede,a.Lak,a.Ordino_uzun_durumu);
                    }





                   PasiftekiEmirler_DipToplamlar_Yaz();
                   isl = null;
                   beklist = null;
                   lst = null;
            }
            catch (Exception ex) { }

            }


    //********************************************************************************************************************


        public void PasiftekiEmirler_Doldur(string saat, string hesap, string adsoy, string menkul, string alsat, decimal lot, decimal fiyat, string ordinodurumu, int seans, string danismankodu,string transactionid,string fininstid,string customerid,string accountid,string gecerlilik,int maximumlot,string tip,DateTime initialmarketsessiondate,DateTime endingmarketsessiondate,DateTime settlementdate , int initialmarketsessionsel,int endingmarketsessionsel,string emirnerede,string lak, string ordino_uzun_durumu)
        {
            string grd_hesapno, grd_menkul, grd_alsat, grd_ordinodurumu ,grd_transactionid;
            decimal grd_fiyat , grd_lot=0;

            if (checkBox2.Checked == true)  //*konsolide.
            {
                if (ordinodurumu == "İptal")
                {
                    PasiftekiEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu, transactionid, fininstid, customerid, accountid, gecerlilik, maximumlot, tip, initialmarketsessiondate, endingmarketsessiondate, settlementdate, initialmarketsessionsel, endingmarketsessionsel, emirnerede, lak, ordino_uzun_durumu);
                    return;
                }

                bool bulundu = false;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    grd_hesapno = Convert.ToString(gridView1.GetRowCellValue(i, "HESAP"));
                    grd_menkul = Convert.ToString(gridView1.GetRowCellValue(i, "MENKUL"));
                    grd_alsat = Convert.ToString(gridView1.GetRowCellValue(i, "ALSAT"));
                    grd_ordinodurumu = Convert.ToString(gridView1.GetRowCellValue(i, "ORDINODURUMU"));
                    grd_fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(i, "FIYAT"));
                    grd_lot = Convert.ToDecimal(gridView1.GetRowCellValue(i, "LOT"));
                    grd_transactionid = Convert.ToString(gridView1.GetRowCellValue(i, "TRANSACTIONID"));

                    if ((menkul == grd_menkul) && (hesap == grd_hesapno) && (alsat == grd_alsat) && (fiyat == grd_fiyat) && (ordinodurumu == grd_ordinodurumu))
                    {
                        grd_lot = grd_lot + lot;
                        gridView1.SetRowCellValue(i, "LOT", grd_lot);   //* üzerine günceller ...
                        MergeOrdinoNumber2(i, transactionid);
                        bulundu = true;
                        break;
                    }
                }

                if (bulundu == false)
                    PasiftekiEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu, transactionid, fininstid, customerid, accountid, gecerlilik, maximumlot, tip, initialmarketsessiondate, endingmarketsessiondate, settlementdate, initialmarketsessionsel, endingmarketsessionsel, emirnerede, lak, ordino_uzun_durumu);

                gridView1.Columns[18].Visible = false;
            }
            else
            {
                PasiftekiEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu, transactionid, fininstid, customerid, accountid, gecerlilik, maximumlot, tip, initialmarketsessiondate, endingmarketsessiondate, settlementdate, initialmarketsessionsel, endingmarketsessionsel, emirnerede, lak, ordino_uzun_durumu);
                gridView1.Columns[18].Visible = true;
            
            }

        }


     //********************************************************************************************************************


        public void PasiftekiEmirler_Gride_Yaz(string saat, string hesap, string adsoy, string menkul, string alsat, decimal lot, decimal fiyat, string ordinodurumu, int seans, string danismankodu, string transactionid, string fininstid, string customerid, string accountid, string gecerlilik, int maximumlot, string tip, DateTime initialmarketsessiondate, DateTime endingmarketsessiondate, DateTime settlementdate, int initialmarketsessionsel, int endingmarketsessionsel, string emirnerede,string lak, string ordino_uzun_durumu)
        {
            if (ordinodurumu == "İptal")
            {
                DataRow newrow = (gridControl3.DataSource as DataTable).NewRow();
                newrow["SAAT"] = saat;
                newrow["HESAP"] = hesap;
                newrow["ADSOY"] = adsoy;
                newrow["MENKUL"] = menkul;
                newrow["ALSAT"] = alsat;
                newrow["LOT"] = lot;
                newrow["FIYAT"] = fiyat;
               // newrow["ORDINODURUMU"] = ordinodurumu;
                newrow["ORDINODURUMU"] = ordino_uzun_durumu;
                newrow["SEANS"] = seans;
                newrow["DANISMANKODU"] = danismankodu;

                (gridControl3.DataSource as DataTable).Rows.Add(newrow);
            }
            else //*Bekleyenler
            {
                DataRow newrow = (gridControl1.DataSource as DataTable).NewRow();
                newrow["SAAT"]   = saat;
                newrow["HESAP"]  = hesap;
                newrow["ADSOY"]  = adsoy;
                newrow["MENKUL"] = menkul;
                newrow["ALSAT"]  = alsat;
                newrow["LOT"]    = lot;
                newrow["FIYAT"]  = fiyat;
                newrow["ORDINODURUMU"] = ordinodurumu;
                newrow["EMIRNEREDE"] = emirnerede;
                newrow["SEANS"]  = seans;
                newrow["DANISMANKODU"] = danismankodu;
                newrow["TRANSACTIONID"] = transactionid;
                newrow["FININSTID"]     = fininstid;
                newrow["CUSTOMERID"]    = customerid;
                newrow["ACCOUNTID"]     = accountid;
                newrow["MERGE_TRANSACTIONID"] = transactionid + "|";
                newrow["GECERLILIK"] = gecerlilik;
                newrow["MAXIMUMLOT"] = maximumlot;
                newrow["TIP"] = tip;                                                //*2015-11-10 00:00:00.000
                newrow["INITIAL_MARKET_SESSION_DATE"] = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", initialmarketsessiondate);    // String.Format("{0:yyyy-MM-dd hh:mm:ss.fff}", initialmarketsessiondate); 
                newrow["ENDING_MARKET_SESSION_DATE"]  = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", endingmarketsessiondate);     // Convert.ToString(endingmarketsessiondate);
                newrow["SETTLEMENT_DATE"]             = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", settlementdate);             // Convert.ToString(settlementdate);
                newrow["INITIAL_MARKET_SESSION_SEL"]  = initialmarketsessionsel;
                newrow["ENDING_MARKET_SESSION_SEL"]   = endingmarketsessionsel;
                newrow["LAK"] = lak;
                (gridControl1.DataSource as DataTable).Rows.Add(newrow);
                
            }

        }

    //********************************************************************************************************************

        public void MergeOrdinoNumber2(int row, string transactionid)
        {
            string tut = Convert.ToString(gridView1.GetRowCellValue(row, "MERGE_TRANSACTIONID"));
            tut = tut + transactionid + "|";
            gridView1.SetRowCellValue(row, "MERGE_TRANSACTIONID", tut);
        }

   //********************************************************************************************************************

        public void PasiftekiEmirler_DipToplamlar_Yaz()
        {
            decimal degtut;

            //***** bekleyenler ...

            E1.Text   = Convert.ToString(T_E1);
            E11.Text  = Convert.ToString(T_E11);
            E111.Text = Convert.ToString(T_E111);

            E2.Text = Convert.ToString(T_E2);
            E22.Text = Convert.ToString(T_E22);
            E222.Text = Convert.ToString(T_E222);

            E3.Text = Convert.ToString(T_E3);
            E33.Text = Convert.ToString(T_E33);
            E333.Text = Convert.ToString(T_E333);



            if ((T_E2 - T_E3) < 0)
            {
                degtut = (T_E2 - T_E3);
                E4.ForeColor= Color.Red;
            }
            else
            {
                degtut = (T_E2 - T_E3);
                E4.ForeColor = Color.Green;
            }
            E4.Text = Convert.ToString(degtut);


            if ((T_E22 - T_E33) < 0)
            {
                degtut = (T_E22 - T_E33);
                E44.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_E22 - T_E33);
                E44.ForeColor = Color.Green;
            }
            E44.Text = Convert.ToString(degtut);


            if ((T_E222 - T_E333) < 0)
            {
                degtut = (T_E222 - T_E333);
                E444.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_E222 - T_E333);
                E444.ForeColor = Color.Green;
            }
            E444.Text = Convert.ToString(degtut);


            E5.Text   = string.Format("{0:#,0.00}", T_E5);
            E55.Text  = string.Format("{0:#,0.00}", T_E55);
            E555.Text = string.Format("{0:#,0.00}", T_E555);

            E6.Text   = string.Format("{0:#,0.00}", T_E6);
            E66.Text  = string.Format("{0:#,0.00}", T_E66);
            E666.Text = string.Format("{0:#,0.00}", T_E666);

            E7.Text   = string.Format("{0:#,0.00}", T_E5 + T_E6);
            E77.Text  = string.Format("{0:#,0.00}", T_E55 + T_E66);
            E777.Text = string.Format("{0:#,0.00}", T_E555 + T_E666); 


            if ((T_E5 - T_E6) < 0)
            {
                degtut = (T_E5 - T_E6);
                E8.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_E5 - T_E6);
                E8.ForeColor = Color.Green;
            }
            E8.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_E55 - T_E66) < 0)
            {
                degtut = (T_E55 - T_E66);
                E88.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_E55 - T_E66);
                E88.ForeColor = Color.Green;
            }
            E88.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_E555 - T_E666) < 0)
            {
                degtut = (T_E555 - T_E666);
                E888.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_E555 - T_E666);
                E888.ForeColor = Color.Green;
            }
            E888.Text = string.Format("{0:#,0.00}", degtut);


            if (T_E2 > 0)
                  E9.Text = string.Format("{0:#,0.0000}", (T_E5 / T_E2));
            else  E9.Text = "0.0000";

            if (T_E22 > 0)
                  E99.Text = string.Format("{0:#,0.0000}", (T_E55 / T_E22));
            else  E99.Text = "0.0000";

            if (T_E222 > 0)
                  E999.Text = string.Format("{0:#,0.0000}", (T_E555 / T_E222));
            else  E999.Text = "0.0000";


            if (T_E3 > 0)
                  E10.Text = string.Format("{0:#,0.0000}", (T_E6 / T_E3));
            else  E10.Text = "0.0000";

            if (T_E33 > 0)
                  E1010.Text = string.Format("{0:#,0.0000}", (T_E66 / T_E33));
            else  E1010.Text = "0.0000";


            if (T_E333 > 0)
                  E101010.Text = string.Format("{0:#,0.0000}", (T_E666 / T_E333));
            else  E101010.Text = "0.0000";



            if ((T_E2-T_E3)!=0)
            {
                if (((T_E5 - T_E6) / (T_E2 - T_E3)) < 0) degtut = ((T_E5 - T_E6) / (T_E2 - T_E3)) * (-1); else degtut = (T_E5 - T_E6) / (T_E2 - T_E3);
                E100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else E100.Text = "0.0000";



            if ((T_E22 - T_E33) != 0)
            {
                if (((T_E55 - T_E66) / (T_E22 - T_E33)) < 0) degtut = ((T_E55 - T_E66) / (T_E22 - T_E33)) * (-1); else degtut = (T_E55 - T_E66) / (T_E22 - T_E33);
                E100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else E100100.Text = "0.0000";


            if ((T_E222 - T_E333) != 0)
            {
                if (((T_E555 - T_E666) / (T_E222 - T_E333)) < 0) degtut = ((T_E555 - T_E666) / (T_E222 - T_E333)) * (-1); else degtut = (T_E555 - T_E666) / (T_E222 - T_E333);
                E100100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else E100100100.Text = "0.0000";

            //***** iptal edilenler...
            //******************************************************************************


            P1.Text = Convert.ToString(T_P1);
            P11.Text = Convert.ToString(T_P11);
            P111.Text = Convert.ToString(T_P111);

            P2.Text = Convert.ToString(T_P2);
            P22.Text = Convert.ToString(T_P22);
            P222.Text = Convert.ToString(T_P222);

            P3.Text = Convert.ToString(T_P3);
            P33.Text = Convert.ToString(T_P33);
            P333.Text = Convert.ToString(T_P333);


            if ((T_P2 - T_P3) < 0)
            {
                degtut = (T_P2 - T_P3);
                P4.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_P2 - T_P3);
                P4.ForeColor = Color.Green;
            }
            P4.Text = Convert.ToString(degtut);


            if ((T_P22 - T_P33) < 0)
            {
                degtut = (T_P22 - T_P33);
                P44.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_P22 - T_P33);
                P44.ForeColor = Color.Green;
            }
            P44.Text = Convert.ToString(degtut);


            if ((T_P222 - T_P333) < 0)
            {
                degtut = (T_P222 - T_P333);
                P444.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_P222 - T_P333);
                P444.ForeColor = Color.Green;
            }
            P444.Text = Convert.ToString(degtut);


            P5.Text = string.Format("{0:#,0.00}", T_P5);
            P55.Text = string.Format("{0:#,0.00}", T_P55);
            P555.Text = string.Format("{0:#,0.00}", T_P555);

            P6.Text = string.Format("{0:#,0.00}", T_P6);
            P66.Text = string.Format("{0:#,0.00}", T_P66);
            P666.Text = string.Format("{0:#,0.00}", T_P666);

            P7.Text = string.Format("{0:#,0.00}", T_P5 + T_P6);
            P77.Text = string.Format("{0:#,0.00}", T_P55 + T_P66);
            P777.Text = string.Format("{0:#,0.00}", T_P555 + T_P666);


            if ((T_P5 - T_P6) < 0)
            {
                degtut = (T_P5 - T_P6);
                P8.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_P5 - T_P6);
                P8.ForeColor = Color.Green;
            }
            P8.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_P55 - T_P66) < 0)
            {
                degtut = (T_P55 - T_P66);
                P88.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_P55 - T_P66);
                P88.ForeColor = Color.Green;
            }
            P88.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_P555 - T_P666) < 0)
            {
                degtut = (T_P555 - T_P666);
                P888.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_P555 - T_P666);
                P888.ForeColor = Color.Green;
            }
            P888.Text = string.Format("{0:#,0.00}", degtut);


            if (T_P2 > 0)
                P9.Text = string.Format("{0:#,0.0000}", (T_P5 / T_P2));
            else P9.Text = "0.0000";

            if (T_P22 > 0)
                P99.Text = string.Format("{0:#,0.0000}", (T_P55 / T_P22));
            else P99.Text = "0.0000";

            if (T_P222 > 0)
                P999.Text = string.Format("{0:#,0.0000}", (T_P555 / T_P222));
            else P999.Text = "0.0000";


            if (T_P3 > 0)
                P10.Text = string.Format("{0:#,0.0000}", (T_P6 / T_P3));
            else P10.Text = "0.0000";

            if (T_P33 > 0)
                P1010.Text = string.Format("{0:#,0.0000}", (T_P66 / T_P33));
            else P1010.Text = "0.0000";


            if (T_P333 > 0)
                P101010.Text = string.Format("{0:#,0.0000}", (T_P666 / T_P333));
            else P101010.Text = "0.0000";



            if ((T_P2 - T_P3) != 0)
            {
                if (((T_P5 - T_P6) / (T_P2 - T_P3)) < 0) degtut = ((T_P5 - T_P6) / (T_P2 - T_P3)) * (-1); else degtut = (T_P5 - T_P6) / (T_P2 - T_P3);
                P100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else P100.Text = "0.0000";



            if ((T_P22 - T_P33) != 0)
            {
                if (((T_P55 - T_P66) / (T_P22 - T_P33)) < 0) degtut = ((T_P55 - T_P66) / (T_P22 - T_P33)) * (-1); else degtut = (T_P55 - T_P66) / (T_P22 - T_P33);
                P100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else P100100.Text = "0.0000";


            if ((T_P222 - T_P333) != 0)
            {
                if (((T_P555 - T_P666) / (T_P222 - T_P333)) < 0) degtut = ((T_P555 - T_P666) / (T_P222 - T_P333)) * (-1); else degtut = (T_P555 - T_P666) / (T_P222 - T_P333);
                P100100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else P100100100.Text = "0.0000";

        }



     //********************************************************************************************************************


        internal GtpXml ExecCustomSQL(string SQLText)
        {
            GtpXml g = new GtpXml("GLB_CRM_EXEC_CUSTOM_SQL", "1.0");
            g.AddParameter("SQLText", SQLText);
            return RbmInvoke(g);
        }

     //********************************************************************************************************************


        private void button1_Click(object sender, EventArgs e)
        {
            HisseAlSat_Ekran_Tazele();
        }

     //********************************************************************************************************************

        public void HisseAlSat_Ekran_Tazele()
        {
            if (checkBox2.Checked == true) label6.Visible = true; else label6.Visible = false;
            if (checkBox1.Checked == true) label7.Visible = true; else label7.Visible = false;

            Ekran_ToplamAlanlari_Bosalt();
            PasiftekiEmirler_Islem();
            GerceklesenEmirler_Islem();

            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                if (checkBox1.Checked == true) label8.Visible = true; else label8.Visible = false;

                string tarih = dateEdit1.DateTime.ToString("dd/MM/yyyy");
                label8.Text = tarih + " -    KONSOLIDE  GÖSTERİM . . .  ";
                GecmisTarihliEmirler_Islem();
            }
        }

        //********************************************************************************************************************

        public void Ekran_ToplamAlanlari_Bosalt()
        {
            T_E1 = 0;  T_E11 = 0;  T_E111 = 0; T_E2 = 0; T_E22 = 0; T_E222 = 0; T_E3 = 0; T_E33 = 0; T_E333 = 0; T_E5 = 0; T_E55 = 0; T_E555 = 0; T_E6 = 0; T_E66 = 0; T_E666 = 0;
            T_F1 = 0;  T_F11 = 0;  T_F111 = 0; T_F2 = 0; T_F22 = 0; T_F222 = 0; T_F3 = 0; T_F33 = 0; T_F333 = 0; T_F5 = 0; T_F55 = 0; T_F555 = 0; T_F6 = 0; T_F66 = 0; T_F666 = 0;
            T_FF1 = 0; T_FF11 = 0; T_FF111 = 0; T_FF2 = 0; T_FF22 = 0; T_FF222 = 0; T_FF3 = 0; T_FF33 = 0; T_FF333 = 0; T_FF5 = 0; T_FF55 = 0; T_FF555 = 0; T_FF6 = 0; T_FF66 = 0; T_FF666 = 0;
            T_P1 = 0;  T_P11 = 0;  T_P111 = 0; T_P2 = 0; T_P22 = 0; T_P222 = 0; T_P3 = 0; T_P33 = 0; T_P333 = 0; T_P5 = 0; T_P55 = 0; T_P555 = 0; T_P6 = 0; T_P66 = 0; T_P666 = 0;
        }


     //********************************************************************************************************************


        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.Column.OptionsColumn.AllowEdit = false;
            e.Column.OptionsColumn.AllowFocus = false;
        }


        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.Column.OptionsColumn.AllowEdit = false;
            e.Column.OptionsColumn.AllowFocus = false;
        }

        private void gridView3_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.Column.OptionsColumn.AllowEdit = false;
            e.Column.OptionsColumn.AllowFocus = false;

        }

        private void gridView4_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.Column.OptionsColumn.AllowEdit = false;
            e.Column.OptionsColumn.AllowFocus = false;

        }

    //********************************************************************************************************************


        public void GerceklesenEmirler_Islem()
        {
            string alissatis = "H", seans = "0", hisse = "";
            try
            {
                OrderOperations isl = new OrderOperations();
                isl.Grid_Initialize2(ref gridControl2, ref gridView2,this);


                if (menkul.EditValue == null) hisse = ""; else hisse = menkul.EditValue.ToString();

                if (radioButton1.Checked == true) alissatis = "A";
                else if (radioButton2.Checked == true) alissatis = "S";
                else alissatis = "H";

                seans = "0";
                
                OrderList gerlist = isl.GerceklesenEmirler(musteri.Text.ToString(), danisman.EditValue.ToString(), hisse, alissatis, seans,SESSIONDATE);
                List<Order> lst = gerlist.Resultlist;
                (gridControl2.DataSource as DataTable).Clear();  //* tabloyu temizler.

                foreach (Order a in lst)
                {
                    if ((fiyat.Text != null) && (fiyat.Text.Trim().Length > 0))
                        if ((a.Fiyat != Convert.ToDecimal(fiyat.Text.ToString())))
                            continue;

                        if ((a.Seans == 0) || (a.Seans == 1))
                        {
                            T_F1++;
                            if (a.Alsat == "A")
                            {
                                T_F2 += a.Lot;
                                T_F5 += (a.Lot * a.Fiyat);
                            }
                            else if (a.Alsat == "S")
                            {
                                T_F3 += a.Lot;
                                T_F6 += (a.Lot * a.Fiyat);
                            }
                        }
                        else if (a.Seans == 2)
                        {
                            T_F11++;
                            if (a.Alsat == "A")
                            {
                                T_F22 += a.Lot;
                                T_F55 += (a.Lot * a.Fiyat);
                            }
                            else if (a.Alsat == "S")
                            {
                                T_F33 += a.Lot;
                                T_F66 += (a.Lot * a.Fiyat);
                            }

                        }
                        //* Günlük Emirler ...
                        T_F111++;
                        if (a.Alsat == "A")
                        {
                            T_F222 += a.Lot;
                            T_F555 += (a.Lot * a.Fiyat);
                        }
                        else if (a.Alsat == "S")
                        {
                            T_F333 += a.Lot;
                            T_F666 += (a.Lot * a.Fiyat);
                        }



                    GerceklesenEmirler_Doldur(a.Saat, a.Hesap, a.Adsoy, a.Menkul, a.Alsat, a.Lot, a.Fiyat, a.Ordinodurumu, a.Seans, a.Danismankodu);
                }


                GerceklesenEmirler_DipToplamlar_Yaz();
                isl = null;
                gerlist = null;
                lst = null;
            }
            catch (Exception ex) { }

        }


        //********************************************************************************************************************


        public void GerceklesenEmirler_DipToplamlar_Yaz()
        {
            decimal degtut;

            //***** bekleyenler ...

            F1.Text = Convert.ToString(T_F1);
            F11.Text = Convert.ToString(T_F11);
            F111.Text = Convert.ToString(T_F111);
            
            F2.Text = Convert.ToString(T_F2);
            F22.Text = Convert.ToString(T_F22);
            F222.Text = Convert.ToString(T_F222);

            F3.Text = Convert.ToString(T_F3);
            F33.Text = Convert.ToString(T_F33);
            F333.Text = Convert.ToString(T_F333);


            if ((T_F2 - T_F3) < 0)
            {
                degtut = (T_F2 - T_F3);
                F4.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_F2 - T_F3);
                F4.ForeColor = Color.Green;
            }
            F4.Text = Convert.ToString(degtut);


            if ((T_F22 - T_F33) < 0)
            {
                degtut = (T_F22 - T_F33);
                F44.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_F22 - T_F33);
                F44.ForeColor = Color.Green;
            }
            F44.Text = Convert.ToString(degtut);


            if ((T_F222 - T_F333) < 0)
            {
                degtut = (T_F222 - T_F333);
                F444.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_F222 - T_F333);
                F444.ForeColor = Color.Green;
            }
            F444.Text = Convert.ToString(degtut);


            F5.Text = string.Format("{0:#,0.00}", T_F5);
            F55.Text = string.Format("{0:#,0.00}", T_F55);
            F555.Text = string.Format("{0:#,0.00}", T_F555);

            F6.Text = string.Format("{0:#,0.00}", T_F6);
            F66.Text = string.Format("{0:#,0.00}", T_F66);
            F666.Text = string.Format("{0:#,0.00}", T_F666);

            F7.Text = string.Format("{0:#,0.00}", T_F5 + T_F6);
            F77.Text = string.Format("{0:#,0.00}", T_F55 + T_F66);
            F777.Text = string.Format("{0:#,0.00}", T_F555 + T_F666);


            if ((T_F5 - T_F6) < 0)
            {
                degtut = (T_F5 - T_F6);
                F8.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_F5 - T_F6);
                F8.ForeColor = Color.Green;
            }
            F8.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_F55 - T_F66) < 0)
            {
                degtut = (T_F55 - T_F66);
                F88.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_F55 - T_F66);
                F88.ForeColor = Color.Green;
            }
            F88.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_F555 - T_F666) < 0)
            {
                degtut = (T_F555 - T_F666);
                F888.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_F555 - T_F666);
                F888.ForeColor = Color.Green;
            }
            F888.Text = string.Format("{0:#,0.00}", degtut);


            if (T_F2 > 0)
                F9.Text = string.Format("{0:#,0.0000}", (T_F5 / T_F2));
            else F9.Text = "0.0000";

            if (T_F22 > 0)
                F99.Text = string.Format("{0:#,0.0000}", (T_F55 / T_F22));
            else F99.Text = "0.0000";

            if (T_F222 > 0)
                F999.Text = string.Format("{0:#,0.0000}", (T_F555 / T_F222));
            else F999.Text = "0.0000";


            if (T_F3 > 0)
                F10.Text = string.Format("{0:#,0.0000}", (T_F6 / T_F3));
            else F10.Text = "0.0000";

            if (T_F33 > 0)
                F1010.Text = string.Format("{0:#,0.0000}", (T_F66 / T_F33));
            else F1010.Text = "0.0000";


            if (T_F333 > 0)
                F101010.Text = string.Format("{0:#,0.0000}", (T_F666 / T_F333));
            else F101010.Text = "0.0000";



            if ((T_F2 - T_F3) != 0)
            {
                if (((T_F5 - T_F6) / (T_F2 - T_F3)) < 0) degtut = ((T_F5 - T_F6) / (T_F2 - T_F3)) * (-1); else degtut = (T_F5 - T_F6) / (T_F2 - T_F3);
                F100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else F100.Text = "0.0000";



            if ((T_F22 - T_F33) != 0)
            {
                if (((T_F55 - T_F66) / (T_F22 - T_F33)) < 0) degtut = ((T_F55 - T_F66) / (T_F22 - T_F33)) * (-1); else degtut = (T_F55 - T_F66) / (T_F22 - T_F33);
                F100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else F100100.Text = "0.0000";


            if ((T_F222 - T_F333) != 0)
            {
                if (((T_F555 - T_F666) / (T_F222 - T_F333)) < 0) degtut = ((T_F555 - T_F666) / (T_F222 - T_F333)) * (-1); else degtut = (T_F555 - T_F666) / (T_F222 - T_F333);
                F100100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else F100100100.Text = "0.0000";
        }


   //********************************************************************************************************************


        public void GerceklesenEmirler_Doldur(string saat, string hesap, string adsoy, string menkul, string alsat, decimal lot, decimal fiyat, string ordinodurumu, int seans, string danismankodu)
        {
            string grd_hesapno, grd_menkul, grd_alsat, grd_ordinodurumu;
            decimal grd_fiyat, grd_lot = 0;

            if (checkBox1.Checked == true)  //*konsolide.
            {

                bool bulundu = false;
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    grd_hesapno = Convert.ToString(gridView2.GetRowCellValue(i, "HESAP"));
                    grd_menkul  = Convert.ToString(gridView2.GetRowCellValue(i, "MENKUL"));
                    grd_alsat   = Convert.ToString(gridView2.GetRowCellValue(i, "ALSAT"));
                    grd_ordinodurumu = Convert.ToString(gridView2.GetRowCellValue(i, "ORDINODURUMU"));
                    grd_fiyat = Convert.ToDecimal(gridView2.GetRowCellValue(i, "FIYAT"));
                    grd_lot   = Convert.ToDecimal(gridView2.GetRowCellValue(i, "LOT"));

                    if ((menkul == grd_menkul) && (hesap == grd_hesapno) && (alsat == grd_alsat) && (fiyat == grd_fiyat) && (ordinodurumu == grd_ordinodurumu))
                    {
                        grd_lot = grd_lot + lot;
                        gridView2.SetRowCellValue(i, "LOT", grd_lot);   //* üzerine günceller ...
                        bulundu = true;
                        break;
                    }
                }

                if (bulundu == false)
                    GerceklesenEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu);
            }
            else
            {
                GerceklesenEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu);
            }




        }


   //********************************************************************************************************************


        public void GerceklesenEmirler_Gride_Yaz(string saat, string hesap, string adsoy, string menkul, string alsat, decimal lot, decimal fiyat, string ordinodurumu, int seans, string danismankodu)
        {
                DataRow newrow = (gridControl2.DataSource as DataTable).NewRow();
                newrow["SAAT"] = saat;
                newrow["HESAP"] = hesap;
                newrow["ADSOY"] = adsoy;
                newrow["MENKUL"] = menkul;
                newrow["ALSAT"] = alsat;
                newrow["LOT"] = lot;
                newrow["FIYAT"] = fiyat;
                newrow["ORDINODURUMU"] = ordinodurumu;
                newrow["SEANS"] = seans;
                newrow["DANISMANKODU"] = danismankodu;

                (gridControl2.DataSource as DataTable).Rows.Add(newrow);

        }

     //********************************************************************************************************************

        public void GecmisTarihliEmirler_Islem()
        {
            string alissatis = "H", seans = "0", hisse = "";
            try
            {
                OrderOperations isl = new OrderOperations();
                isl.Grid_Initialize4(ref gridControl4, ref gridView4,this);
                

                if (menkul.EditValue == null) hisse = ""; else hisse = menkul.EditValue.ToString();

                if (radioButton1.Checked == true) alissatis = "A";
                else if (radioButton2.Checked == true) alissatis = "S";
                else alissatis = "H";

                seans = "0";

                string tarih = dateEdit1.DateTime.ToString("yyyyMMdd");

                OrderList tarlist = isl.GecmisTarihliEmirler(musteri.Text.ToString(), danisman.EditValue.ToString(), hisse, alissatis, seans,tarih);
                List<Order> lst = tarlist.Resultlist;
                (gridControl4.DataSource as DataTable).Clear();  //* tabloyu temizler.

                foreach (Order a in lst)
                {
                    if ((fiyat.Text != null) && (fiyat.Text.Trim().Length > 0))
                        if ((a.Fiyat != Convert.ToDecimal(fiyat.Text.ToString())))
                            continue;

                        if ((a.Seans == 0) || (a.Seans == 1))
                        {
                            T_FF1++;
                            if (a.Alsat == "A")
                            {
                                T_FF2 += a.Lot;
                                T_FF5 += (a.Lot * a.Fiyat);
                            }
                            else if (a.Alsat == "S")
                            {
                                T_FF3 += a.Lot;
                                T_FF6 += (a.Lot * a.Fiyat);
                            }
                        }
                        else if (a.Seans == 2)
                        {
                            T_FF11++;
                            if (a.Alsat == "A")
                            {
                                T_FF22 += a.Lot;
                                T_FF55 += (a.Lot * a.Fiyat);
                            }
                            else if (a.Alsat == "S")
                            {
                                T_FF33 += a.Lot;
                                T_FF66 += (a.Lot * a.Fiyat);
                            }

                        }
                        //* Günlük Emirler ...
                        T_FF111++;
                        if (a.Alsat == "A")
                        {
                            T_FF222 += a.Lot;
                            T_FF555 += (a.Lot * a.Fiyat);
                        }
                        else if (a.Alsat == "S")
                        {
                            T_FF333 += a.Lot;
                            T_FF666 += (a.Lot * a.Fiyat);
                        }



                    GecmisTarihliEmirler_Doldur(a.Saat, a.Hesap, a.Adsoy, a.Menkul, a.Alsat, a.Lot, a.Fiyat, a.Ordinodurumu, a.Seans, a.Danismankodu);
                }


                GecmisTarihliEmirler_DipToplamlar_Yaz();
                isl = null;
                tarlist = null;
                lst = null;
            }
            catch (Exception ex) { }

        }


        //********************************************************************************************************************

        public void GecmisTarihliEmirler_DipToplamlar_Yaz()
        {
            decimal degtut;

            //***** bekleyenler ...

            FF1.Text = Convert.ToString(T_FF1);
            FF11.Text = Convert.ToString(T_FF11);
            FF111.Text = Convert.ToString(T_FF111);

            FF2.Text = Convert.ToString(T_FF2);
            FF22.Text = Convert.ToString(T_FF22);
            FF222.Text = Convert.ToString(T_FF222);

            FF3.Text = Convert.ToString(T_FF3);
            FF33.Text = Convert.ToString(T_FF33);
            FF333.Text = Convert.ToString(T_FF333);


            if ((T_FF2 - T_FF3) < 0)
            {
                degtut = (T_FF2 - T_FF3);
                FF4.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_FF2 - T_FF3);
                FF4.ForeColor = Color.Green;
            }
            FF4.Text = Convert.ToString(degtut);


            if ((T_FF22 - T_FF33) < 0)
            {
                degtut = (T_FF22 - T_FF33);
                FF44.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_FF22 - T_FF33);
                FF44.ForeColor = Color.Green;
            }
            FF44.Text = Convert.ToString(degtut);


            if ((T_FF222 - T_FF333) < 0)
            {
                degtut = (T_FF222 - T_FF333);
                FF444.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_FF222 - T_FF333);
                FF444.ForeColor = Color.Green;
            }
            FF444.Text = Convert.ToString(degtut);


            FF5.Text = string.Format("{0:#,0.00}", T_FF5);
            FF55.Text = string.Format("{0:#,0.00}", T_FF55);
            FF555.Text = string.Format("{0:#,0.00}", T_FF555);

            FF6.Text = string.Format("{0:#,0.00}", T_FF6);
            FF66.Text = string.Format("{0:#,0.00}", T_FF66);
            FF666.Text = string.Format("{0:#,0.00}", T_FF666);

            FF7.Text = string.Format("{0:#,0.00}", T_FF5 + T_FF6);
            FF77.Text = string.Format("{0:#,0.00}", T_FF55 + T_FF66);
            FF777.Text = string.Format("{0:#,0.00}", T_FF555 + T_FF666);


            if ((T_FF5 - T_FF6) < 0)
            {
                degtut = (T_FF5 - T_FF6);
                FF8.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_FF5 - T_FF6);
                FF8.ForeColor = Color.Green;
            }
            FF8.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_FF55 - T_FF66) < 0)
            {
                degtut = (T_FF55 - T_FF66);
                FF88.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_FF55 - T_FF66);
                FF88.ForeColor = Color.Green;
            }
            FF88.Text = string.Format("{0:#,0.00}", degtut);


            if ((T_FF555 - T_FF666) < 0)
            {
                degtut = (T_FF555 - T_FF666);
                FF888.ForeColor = Color.Red;
            }
            else
            {
                degtut = (T_FF555 - T_FF666);
                FF888.ForeColor = Color.Green;
            }
            FF888.Text = string.Format("{0:#,0.00}", degtut);


            if (T_FF2 > 0)
                FF9.Text = string.Format("{0:#,0.0000}", (T_FF5 / T_FF2));
            else FF9.Text = "0.0000";

            if (T_FF22 > 0)
                FF99.Text = string.Format("{0:#,0.0000}", (T_FF55 / T_FF22));
            else FF99.Text = "0.0000";

            if (T_FF222 > 0)
                FF999.Text = string.Format("{0:#,0.0000}", (T_FF555 / T_FF222));
            else FF999.Text = "0.0000";


            if (T_FF3 > 0)
                FF10.Text = string.Format("{0:#,0.0000}", (T_FF6 / T_FF3));
            else FF10.Text = "0.0000";

            if (T_FF33 > 0)
                FF1010.Text = string.Format("{0:#,0.0000}", (T_FF66 / T_FF33));
            else FF1010.Text = "0.0000";


            if (T_FF333 > 0)
                FF101010.Text = string.Format("{0:#,0.0000}", (T_FF666 / T_FF333));
            else FF101010.Text = "0.0000";



            if ((T_FF2 - T_FF3) != 0)
            {
                if (((T_FF5 - T_FF6) / (T_FF2 - T_FF3)) < 0) degtut = ((T_FF5 - T_FF6) / (T_FF2 - T_FF3)) * (-1); else degtut = (T_FF5 - T_FF6) / (T_FF2 - T_FF3);
                FF100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else FF100.Text = "0.0000";



            if ((T_FF22 - T_FF33) != 0)
            {
                if (((T_FF55 - T_FF66) / (T_FF22 - T_FF33)) < 0) degtut = ((T_FF55 - T_FF66) / (T_FF22 - T_FF33)) * (-1); else degtut = (T_FF55 - T_FF66) / (T_FF22 - T_FF33);
                FF100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else FF100100.Text = "0.0000";


            if ((T_FF222 - T_FF333) != 0)
            {
                if (((T_FF555 - T_FF666) / (T_FF222 - T_FF333)) < 0) degtut = ((T_FF555 - T_FF666) / (T_FF222 - T_FF333)) * (-1); else degtut = (T_FF555 - T_FF666) / (T_FF222 - T_FF333);
                FF100100100.Text = string.Format("{0:#,0.0000}", degtut);
            }
            else FF100100100.Text = "0.0000";
        }


        //********************************************************************************************************************

        
        public void GecmisTarihliEmirler_Doldur(string saat, string hesap, string adsoy, string menkul, string alsat, decimal lot, decimal fiyat, string ordinodurumu, int seans, string danismankodu)
        {
            string grd_hesapno, grd_menkul, grd_alsat, grd_ordinodurumu;
            decimal grd_fiyat, grd_lot = 0;

            if (checkBox1.Checked == true)  //*konsolide.
            {

                bool bulundu = false;
                for (int i = 0; i < gridView4.RowCount; i++)
                {
                    grd_hesapno = Convert.ToString(gridView4.GetRowCellValue(i, "HESAP"));
                    grd_menkul = Convert.ToString(gridView4.GetRowCellValue(i, "MENKUL"));
                    grd_alsat = Convert.ToString(gridView4.GetRowCellValue(i, "ALSAT"));
                    grd_ordinodurumu = Convert.ToString(gridView4.GetRowCellValue(i, "ORDINODURUMU"));
                    grd_fiyat = Convert.ToDecimal(gridView4.GetRowCellValue(i, "FIYAT"));
                    grd_lot = Convert.ToDecimal(gridView4.GetRowCellValue(i, "LOT"));

                    if ((menkul == grd_menkul) && (hesap == grd_hesapno) && (alsat == grd_alsat) && (fiyat == grd_fiyat) && (ordinodurumu == grd_ordinodurumu))
                    {
                        grd_lot = grd_lot + lot;
                        gridView4.SetRowCellValue(i, "LOT", grd_lot);   //* üzerine günceller ...
                        bulundu = true;
                        break;
                    }
                }

                if (bulundu == false)
                    GecmisTarihliEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu);
            }
            else
            {
                GecmisTarihliEmirler_Gride_Yaz(saat, hesap, adsoy, menkul, alsat, lot, fiyat, ordinodurumu, seans, danismankodu);
            }


        }


        //********************************************************************************************************************

        public void GecmisTarihliEmirler_Gride_Yaz(string saat, string hesap, string adsoy, string menkul, string alsat, decimal lot, decimal fiyat, string ordinodurumu, int seans, string danismankodu)
        {
            DataRow newrow = (gridControl4.DataSource as DataTable).NewRow();
            newrow["SAAT"] = saat;
            newrow["HESAP"] = hesap;
            newrow["ADSOY"] = adsoy;
            newrow["MENKUL"] = menkul;
            newrow["ALSAT"] = alsat;
            newrow["LOT"] = lot;
            newrow["FIYAT"] = fiyat;
            newrow["ORDINODURUMU"] = ordinodurumu;
            newrow["SEANS"] = seans;
            newrow["DANISMANKODU"] = danismankodu;

            (gridControl4.DataSource as DataTable).Rows.Add(newrow);
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView currentview = sender as GridView;

                string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ALSAT"));
                if (tut == "A")
                    e.Appearance.ForeColor = Color.Green;
                else if (tut == "S")
                    e.Appearance.ForeColor = Color.Red;
        }

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView currentview = sender as GridView;

            string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ALSAT"));
            if (tut == "A")
                e.Appearance.ForeColor = Color.Green;
            else if (tut == "S")
                e.Appearance.ForeColor = Color.Red;

        }

        private void gridView3_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView currentview = sender as GridView;

            string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ALSAT"));
            if (tut == "A")
                e.Appearance.ForeColor = Color.Green;
            else if (tut == "S")
                e.Appearance.ForeColor = Color.Red;

        }

        private void gridView4_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView currentview = sender as GridView;

            string tut = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "ALSAT"));
            if (tut == "A")
                e.Appearance.ForeColor = Color.Green;
            else if (tut == "S")
                e.Appearance.ForeColor = Color.Red;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlSat frm = new AlSat(this);
            frm.tabControl1.SelectedIndex = 0;
            frm.panel3.BackColor = Color.MediumSeaGreen;
            frm.Text = "ALIŞ                         ";
            frm.ISLEM = "A";
            frm.BaseFormInit(this.ApplicationBrowser); 
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AlSat frm = new AlSat(this);
            frm.tabControl1.SelectedIndex = 1;
            frm.panel3.BackColor = Color.Red; 
            frm.Text = "SATIŞ                        ";
            frm.ISLEM = "S";
            frm.BaseFormInit(this.ApplicationBrowser);
            frm.Show();
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


        public void Sistemden_Iyilestir(string TransactionId, string debitcredit, string fininstid, decimal units, decimal price, string customerid, string accountid, string valuedate, string initialmarketdate, int initialMarketSessionSel, int EndingMarketSessionSel, int orderMaxlot, string tip, string gecerlilik, string lak)
        {
            OrderOperations op = new OrderOperations(this);
            string donus = op.Update_Equity_Order(TransactionId, debitcredit, fininstid, units, price, customerid, accountid, valuedate, initialmarketdate, initialMarketSessionSel, EndingMarketSessionSel, orderMaxlot, tip,gecerlilik,lak);

            if (donus.Trim().Length > 0)
            {
                TopMostMessageBox.Show("İşleme Konulmadı." + donus);
                // System.Windows.Forms.MessageBox.Show("İşleme Konulmadı. " + donus);
                return;
            }
            else
            {
                System.Threading.Thread.Sleep(500);  //* Gridde hemen gözükmüyor, bir zamana ihtiyacı var... 
                HisseAlSat_Ekran_Tazele();
            }
        }

        //********************************************************************************************************************


        public void Borsadan_Iyilestir(string TransactionId, decimal improveprice, decimal improveunits, decimal oldunits,string gecerlilik,string initialmarketdate)
        {
            OrderOperations op = new OrderOperations(this);
            string donus = op.Save_Improve_Order(TransactionId, improveprice, improveunits, oldunits, gecerlilik, initialmarketdate);
            System.Threading.Thread.Sleep(500);     //* Gridde hemen gözükmüyor, bir zamana ihtiyacı var... 
            HisseAlSat_Ekran_Tazele();

        }

        //********************************************************************************************************************


        private void topluİyileştirSeçilenMüşteriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowc = gridView1.RowCount;
            if (rowc > 0)
            {
                if (gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString().Length > 0)
                {
                    TopluIyilestirme frm = new TopluIyilestirme(this);
                    frm.Ehesapno.Text = gridView1.GetRowCellValue(ROW, "HESAP").ToString();
                    frm.Ecustomerid.Text = gridView1.GetRowCellValue(ROW, "CUSTOMERID").ToString();
                    frm.Eaccountid.Text = gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString();
                    frm.Efininstid.Text = gridView1.GetRowCellValue(ROW, "FININSTID").ToString();
                    frm.Emenkul.Text = gridView1.GetRowCellValue(ROW, "MENKUL").ToString();
                    frm.Ealsat.Text = gridView1.GetRowCellValue(ROW, "ALSAT").ToString();
                    frm.FIYAT = Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "FIYAT").ToString());
                    frm.Danisman_Doldur();

                    int ind = danisman.ItemIndex;
                    frm.danisman.EditValue = danisman.Properties.GetDataSourceValue("ID", ind);

                    frm.SECIMDURUMU = 1;
                    frm.BaseFormInit(this.ApplicationBrowser);
                    frm.Show();
                }
            }
        }

        //********************************************************************************************************************
  

        private void topluİyileştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowc = gridView1.RowCount;
            if (rowc > 0)
            {
                if (gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString().Length > 0)
                {
                    TopluIyilestirme frm = new TopluIyilestirme(this);
                    frm.Ehesapno.Text = gridView1.GetRowCellValue(ROW, "HESAP").ToString();
                    frm.Ecustomerid.Text = gridView1.GetRowCellValue(ROW, "CUSTOMERID").ToString();
                    frm.Eaccountid.Text = gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString();
                    frm.Efininstid.Text = gridView1.GetRowCellValue(ROW, "FININSTID").ToString();
                    frm.Emenkul.Text = gridView1.GetRowCellValue(ROW, "MENKUL").ToString();
                    frm.Ealsat.Text = gridView1.GetRowCellValue(ROW, "ALSAT").ToString();
                    frm.FIYAT = Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "FIYAT").ToString());
                    frm.Danisman_Doldur();

                    int ind = danisman.ItemIndex;
                    frm.danisman.EditValue = danisman.Properties.GetDataSourceValue("ID", ind);

                    frm.SECIMDURUMU = 2;
                    frm.BaseFormInit(this.ApplicationBrowser);
                    frm.Show();
                }
            }
        }


        //********************************************************************************************************************


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int rowc = gridView1.RowCount;
            if (rowc > 0)
            {
                if (gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString().Length > 0)
                {
                    TopluIyilestirme frm = new TopluIyilestirme(this);
                    frm.Ehesapno.Text = gridView1.GetRowCellValue(ROW, "HESAP").ToString();
                    frm.Ecustomerid.Text = gridView1.GetRowCellValue(ROW, "CUSTOMERID").ToString();
                    frm.Eaccountid.Text = gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString();
                    frm.Efininstid.Text = gridView1.GetRowCellValue(ROW, "FININSTID").ToString();
                    frm.Emenkul.Text = gridView1.GetRowCellValue(ROW, "MENKUL").ToString();
                    frm.Ealsat.Text = gridView1.GetRowCellValue(ROW, "ALSAT").ToString();
                    frm.FIYAT = Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "FIYAT").ToString());
                    frm.Danisman_Doldur();

                    int ind = danisman.ItemIndex;
                    frm.danisman.EditValue = danisman.Properties.GetDataSourceValue("ID", ind);

                    frm.SECIMDURUMU = 3;
                    frm.BaseFormInit(this.ApplicationBrowser);
                    frm.Show();
                }
            }
        }


    //********************************************************************************************************************

        private void emirAzaltmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowc = gridView1.RowCount;
            if (rowc > 0)
            {
                if (gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString().Length > 0)
                {
                    if (label6.Visible == true)
                    {
                        TopMostMessageBox.Show("Bu İşlemi yapabilmek için Konsolide Modundan çıkınız !");
                        // System.Windows.Forms.MessageBox.Show("Bu İşlemi yapabilmek için Konsolide Modundan çıkınız !");
                        return;
                    }

                    EmirAzaltma frm = new EmirAzaltma(this);
                    frm.Etransactionid.Text = gridView1.GetRowCellValue(ROW, "TRANSACTIONID").ToString();

                    if (gridView1.GetRowCellValue(ROW, "ALSAT").ToString() == "A")
                        frm.Edebitcredit.Text = "CREDIT";
                    else if (gridView1.GetRowCellValue(ROW, "ALSAT").ToString() == "S")
                        frm.Edebitcredit.Text = "DEBIT";

                    frm.Efininstid.Text = gridView1.GetRowCellValue(ROW, "FININSTID").ToString();
                    frm.Eunits.Text = gridView1.GetRowCellValue(ROW, "LOT").ToString();
                    frm.Eprice.Text = gridView1.GetRowCellValue(ROW, "FIYAT").ToString();
                    frm.Ecustomerid.Text = gridView1.GetRowCellValue(ROW, "CUSTOMERID").ToString();
                    frm.Eaccountid.Text = gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString();
                    frm.Evaluedate.Text = gridView1.GetRowCellValue(ROW, "SETTLEMENT_DATE").ToString();
                    frm.Einitialmarketdate.Text = gridView1.GetRowCellValue(ROW, "INITIAL_MARKET_SESSION_DATE").ToString();
                    frm.EinitialMarketSessionSel.Text = gridView1.GetRowCellValue(ROW, "INITIAL_MARKET_SESSION_SEL").ToString();
                    frm.Eendingmarketsessionsel.Text = gridView1.GetRowCellValue(ROW, "ENDING_MARKET_SESSION_SEL").ToString();
                    frm.Eordermaxlot.Text = gridView1.GetRowCellValue(ROW, "MAXIMUMLOT").ToString();
                    frm.Etip.Text = gridView1.GetRowCellValue(ROW, "TIP").ToString();

                    frm.Ehesapno.Text = gridView1.GetRowCellValue(ROW, "HESAP").ToString();
                    frm.Emenkul.Text = gridView1.GetRowCellValue(ROW, "MENKUL").ToString();
                    frm.Ealsat.Text = gridView1.GetRowCellValue(ROW, "ALSAT").ToString();
                    frm.Elot.Text = gridView1.GetRowCellValue(ROW, "LOT").ToString();
                    frm.Efiyat.Text = gridView1.GetRowCellValue(ROW, "FIYAT").ToString();
                    frm.Egecerlilik.Text = gridView1.GetRowCellValue(ROW, "GECERLILIK").ToString();
                    frm.Elak.Text = gridView1.GetRowCellValue(ROW, "LAK").ToString();
                    frm.BaseFormInit(this.ApplicationBrowser);
                    frm.ShowDialog();
                }
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
                    System.Windows.Forms.DialogResult dialogResult = TopMostMessageBox.Show("Emir İptalini Onaylıyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                    //* System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Emir İptalini Onaylıyormusunuz ?", "Onay", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.No)
                        return;

                    OrderOperations op = new OrderOperations(this);
                    string merge_transaction = gridView1.GetRowCellValue(ROW, "MERGE_TRANSACTIONID").ToString();
                    string[] trans = merge_transaction.Split('|');

                    foreach (string id in trans)
                    {
                        if (id.Trim().Length > 0)
                        {
                            string donus = op.Delete_Equity_Order(id);
                            if (donus.Trim().Length > 0)
                            {
                                TopMostMessageBox.Show(id + " Nolu Emir İptal Edilemedi. " + donus);
                                // System.Windows.Forms.MessageBox.Show(id + " Nolu Emir İptal Edilemedi. " + donus);
                            }
                        }
                    }

                    HisseAlSat_Ekran_Tazele();
                }
                else
                {
                    TopMostMessageBox.Show("İptal Edilecek Emri Seçiniz !");
                    //* System.Windows.Forms.MessageBox.Show("İptal Edilecek Emri Seçiniz !");
                    return;
                }
            }
        }

        //********************************************************************************************************************


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString().Length > 0)
            {
                TopluIyilestirme frm = new TopluIyilestirme(this);
                frm.Ehesapno.Text = gridView1.GetRowCellValue(ROW, "HESAP").ToString();
                frm.Ecustomerid.Text = gridView1.GetRowCellValue(ROW, "CUSTOMERID").ToString();
                frm.Eaccountid.Text = gridView1.GetRowCellValue(ROW, "ACCOUNTID").ToString();
                frm.Efininstid.Text = gridView1.GetRowCellValue(ROW, "FININSTID").ToString();
                frm.Emenkul.Text = gridView1.GetRowCellValue(ROW, "MENKUL").ToString();
                frm.Ealsat.Text = gridView1.GetRowCellValue(ROW, "ALSAT").ToString();
                frm.FIYAT = Convert.ToDecimal(gridView1.GetRowCellValue(ROW, "FIYAT").ToString());
                frm.Danisman_Doldur();

                int ind = danisman.ItemIndex;
                frm.danisman.EditValue = danisman.Properties.GetDataSourceValue("ID", ind);

                frm.SECIMDURUMU = 2;
                frm.BaseFormInit(this.ApplicationBrowser);
                frm.Show();
            }
        }

        //********************************************************************************************************************

        private void button4_Click(object sender, EventArgs e)
        {
            BasketOrders frm = new BasketOrders(this);
            frm.BaseFormInit(this.ApplicationBrowser);
            frm.Show();

        }

        //********************************************************************************************************************

        private void button5_Click(object sender, EventArgs e)
        {
            string filename="";
            System.Windows.Forms.SaveFileDialog SAveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
                gridControl2.ExportToExcelOld(filename);
                TopMostMessageBox.Show("Excel dosyasına save edildi.");
                // System.Windows.Forms.MessageBox.Show("Excel dosyasına save edildi.");               
            }


        }

        //********************************************************************************************************************

        private void button6_Click(object sender, EventArgs e)
        {
            string filename = "";
            System.Windows.Forms.SaveFileDialog SAveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
                gridControl4.ExportToExcelOld(filename);
                TopMostMessageBox.Show("Excel dosyasına save edildi.");
                //System.Windows.Forms.MessageBox.Show("Excel dosyasına save edildi.");
            }
        }

        //********************************************************************************************************************

        private void button7_Click(object sender, EventArgs e)
        {
            AlgoritmicTrade frm = new AlgoritmicTrade(this);
            frm.BaseFormInit(this.ApplicationBrowser);
            frm.Show();
        }


   //********************************************************************************************************************


    }
}
