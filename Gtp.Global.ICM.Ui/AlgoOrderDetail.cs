using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Gtp.Framework.ControlLibrary;
using DevExpress.XtraGrid.Views.Grid;

namespace Gtp.Global.ICM.Ui
{
    public partial class AlgoOrderDetail : Form
    {
        public decimal REFERANSID;
        public AlgoOrderDetail()
        {
            InitializeComponent();
        }

        //********************************************************************************************************************

        private void AlgoOrderDetail_Load(object sender, EventArgs e)
        {
            EkranTazele();
        }

        //********************************************************************************************************************

        private void EkranTazele()
        {
            OrderOperations opr = new OrderOperations();
            AlgoOrderList beklst= opr.Gib_AnaEmir_Bilgileri_Al(this, REFERANSID);
            opr.Gib_EmirParcalarini_Tabloya_Doldur(ref gridControl1, ref gridView1, chkedt, REFERANSID);

            List<AlgoOrder> lst = beklst.Resultlist;
            foreach (AlgoOrder a in lst)
            {
                Eemirtipi.Text = a.Emirtipi;
                Erefid.Text    = Convert.ToString(a.Referansid);
                Emusteri.Text = a.Hesapno;
                Emenkul.Text = a.Menkul;
                Elot.Text = Convert.ToString(a.Lot);
                Egib_yuzde1.Text  = Convert.ToString(a.Gib_yuzde1);
                Egib_yuzde2.Text  = Convert.ToString(a.Gib_yuzde2);
                Egib_baszmn1.Text = SifirKoy(Convert.ToString(a.Gib_bas_saat),2);
                Egib_baszmn2.Text = SifirKoy(Convert.ToString(a.Gib_bas_dakika),2);
                Egib_baszmn3.Text = SifirKoy(Convert.ToString(a.Gib_bas_saniye),2);
                Egib_bitzmn1.Text = SifirKoy(Convert.ToString(a.Gib_bit_saat),2);
                Egib_bitzmn2.Text = SifirKoy(Convert.ToString(a.Gib_bit_dakika),2);
                Egib_bitzmn3.Text = SifirKoy(Convert.ToString(a.Gib_bit_saniye),2);
                Egib_parcasayisi.Text = Convert.ToString(a.Gib_parcasayisi);
                Eaktiflesmesekli.Text = a.Aktiflesmesekli;

                if (Convert.ToString(a.Alsat) == "CREDIT")
                {
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                }
                else
                {
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                }

            }

        }

        //********************************************************************************************************************

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            EkranTazele();
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
            else if ((e.Column.FieldName == "AKTIFLESMESEKLI") || (e.Column.FieldName == "STATU") || (e.Column.FieldName == "BORSADURUM") || (e.Column.FieldName == "MANUELAKTIVASYON"))
            {
                e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                e.Column.OptionsColumn.AllowEdit  = false;
                e.Column.OptionsColumn.AllowFocus = false;
            }
            else
            {
                e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Column.OptionsColumn.AllowEdit  = false;
                e.Column.OptionsColumn.AllowFocus = false;
            }
        }

        //********************************************************************************************************************

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView currentview = sender as GridView;

            string tut  = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "BORSADURUM"));
            string tut2 = Convert.ToString(currentview.GetRowCellValue(e.RowHandle, "STATU"));

            if ( (tut == "İptal Edildi") || ( (tut=="-")&&(tut2=="İptal Edildi") ) )
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.LightGray;
            }
            else if (tut == "GERÇEKLEŞTİ")
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Yellow;
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
    }
}
