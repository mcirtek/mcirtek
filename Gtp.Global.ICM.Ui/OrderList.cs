﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtp.Global.ICM.Ui
{
    class OrderList
    {
        private List<Order> resultlist;

        internal List<Order> Resultlist
        {
            get
            {
                if (resultlist == null)
                { resultlist = new List<Order>(); }

                return resultlist;
            }

            set { resultlist = value; }
        }
    }
}
