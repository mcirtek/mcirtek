using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtp.Global.ICM.Ui
{
    class AlgoOrderList
    {
        private List<AlgoOrder> resultlist;

        public List<AlgoOrder> Resultlist
        {
            get
            {
                if (resultlist == null)
                { resultlist = new List<AlgoOrder>(); }

                return resultlist;
            }

            set { resultlist = value; }
        }
    }
}
