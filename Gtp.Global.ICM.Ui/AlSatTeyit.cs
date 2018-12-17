using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace Gtp.Global.ICM.Ui
{
    public partial class AlSatTeyit : Form
    {
        Gtp.Global.ICM.Ui.AlSat frmalsat;
        public string   BUGUNKUTARIH, TAKASTARIHI, FIN_INST_ID, alsat, CUSTID, ACCID, EquityTransactionTypeId;
        public int MAX_LOT, InitialMarketSessionSel, EndingMarketSessionSel;
        public decimal fyt, mik;

        public AlSatTeyit()
        {
            InitializeComponent();
        }

        private void AlSatTeyit_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        }

        public AlSatTeyit(Gtp.Global.ICM.Ui.AlSat frm)
        {
            InitializeComponent();
            frmalsat = frm;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmalsat.EMIRONAYLANDI = true;
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmalsat.EMIRONAYLANDI = false;
            this.Close();
        }

    }
}
