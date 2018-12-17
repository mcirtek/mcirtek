using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtp.Global.ICM.Ui
{
    class Order
    {
   
        private string saat;

        public string Saat
        {
            get { return saat; }
            set { saat = value; }
        }
        private string hesap;

        public string Hesap
        {
            get { return hesap; }
            set { hesap = value; }
        }
        private string adsoy;

        public string Adsoy
        {
            get { return adsoy; }
            set { adsoy = value; }
        }
        private string menkul;

        public string Menkul
        {
            get { return menkul; }
            set { menkul = value; }
        }
        private string alsat;

        public string Alsat
        {
            get { return alsat; }
            set { alsat = value; }
        }
        private decimal lot;

        public decimal Lot
        {
            get { return lot; }
            set { lot = value; }
        }
        private decimal fiyat;

        public decimal Fiyat
        {
            get { return fiyat; }
            set { fiyat = value; }
        }
        private string ordinodurumu;

        public string Ordinodurumu
        {
            get { return ordinodurumu; }
            set { ordinodurumu = value; }
        }


        private string ordino_uzun_durumu;
        public string Ordino_uzun_durumu
        {
            get { return ordino_uzun_durumu; }
            set { ordino_uzun_durumu = value; }
        }


        private int seans;

        public int Seans
        {
            get { return seans; }
            set { seans = value; }
        }
        private string danismankodu;

        public string Danismankodu
        {
            get { return danismankodu; }
            set { danismankodu = value; }
        }

        private string transactionid;

        public string Transactionid
        {
            get { return transactionid; }
            set { transactionid = value; }
        }



        private string finInstid;

        public string FinInstid
        {
            get { return finInstid; }
            set { finInstid = value; }
        }



        private string customerid;

        public string Customerid
        {
            get { return customerid; }
            set { customerid = value; }
        }


        private string accountid;

        public string Accountid
        {
            get { return accountid; }
            set { accountid = value; }
        }

        private string gecerlilik;

        public string Gecerlilik
        {
            get { return gecerlilik; }
            set { gecerlilik = value; }
        }

        private Int32 maximumlot;

        public Int32 Maximumlot
        {
            get { return maximumlot; }
            set { maximumlot = value; }
        }

        private string tip;

        public string Tip
        {
            get { return tip; }
            set { tip = value; }
        }

        private DateTime initialmarketsessiondate;

        public DateTime Initialmarketsessiondate
        {
            get { return initialmarketsessiondate; }
            set { initialmarketsessiondate = value; }
        }

        private DateTime endingmarketsessiondate;

        public DateTime Endingmarketsessiondate
        {
            get { return endingmarketsessiondate; }
            set { endingmarketsessiondate = value; }
        }



        private DateTime settlementdate;

        public DateTime Settlementdate
        {
            get { return settlementdate; }
            set { settlementdate = value; }
        }



        private Int32 initialmarketsessionsel;

        public Int32 Initialmarketsessionsel
        {
            get { return initialmarketsessionsel; }
            set { initialmarketsessionsel = value; }
        }


            private Int32 endingmarketsessionsel;

            public Int32 Endingmarketsessionsel
            {
              get { return endingmarketsessionsel; }
              set { endingmarketsessionsel = value; }
            }

            private string emirnerede;

            public string Emirnerede
            {
                get { return emirnerede; }
                set { emirnerede = value; }
            }

            private string lak;

            public string Lak
            {
                get { return lak; }
                set { lak = value; }
            }
    }
}
