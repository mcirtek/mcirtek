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
    public partial class EmirAzaltma : Form
    {
        Gtp.Global.ICM.Ui.OrderManagement frmana;

        public EmirAzaltma()
        {
            InitializeComponent();
        }

        private void EmirAzaltma_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        }

        public EmirAzaltma(Gtp.Global.ICM.Ui.OrderManagement frm)
        {
            InitializeComponent();
            frmana = frm;
        }



        //********************************************************************************************************************

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            decimal ekrlot, eskimiktar;
            eskimiktar = Convert.ToDecimal(Elot.Text);

            if ((yenilot.Text == null) || (yenilot.Text.Trim().Length == 0))
                ekrlot = 0;
            else
                ekrlot = Convert.ToDecimal(yenilot.Text);

            OrderOperations op = new OrderOperations(frmana);

            if ((ekrlot > 0) && (ekrlot < eskimiktar))
            {

                bool OrdinoBorsayaIletildimi = op.OrdinoBorsayaIletildimi(Etransactionid.Text); //* Sistemde bekliyor , yada borsada...
                if (OrdinoBorsayaIletildimi == false)
                    frmana.Sistemden_Iyilestir(Etransactionid.Text, Edebitcredit.Text, Efininstid.Text, ekrlot, Convert.ToDecimal(Eprice.Text), Ecustomerid.Text, Eaccountid.Text, Evaluedate.Text, Einitialmarketdate.Text, Convert.ToInt32(EinitialMarketSessionSel.Text), Convert.ToInt32(Eendingmarketsessionsel.Text), Convert.ToInt32(Eordermaxlot.Text), Etip.Text,Egecerlilik.Text,Elak.Text);
                else
                    frmana.Borsadan_Iyilestir(Etransactionid.Text, Convert.ToDecimal(Eprice.Text), ekrlot, Convert.ToDecimal(Elot.Text), Egecerlilik.Text, Einitialmarketdate.Text);  

               
                frmana.HisseAlSat_Ekran_Tazele();
                Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Lot alanına uygun bir değer giriniz.");
                yenilot.EditValue = 0;
                return;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Close();
        }


        //********************************************************************************************************************
    }
}
