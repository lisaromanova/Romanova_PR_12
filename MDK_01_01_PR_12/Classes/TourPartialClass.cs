﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MDK_01_01_PR_12
{
    public partial class Tour
    {
        public string PriceRub
        {
            get
            {
                int t = Convert.ToInt32(Math.Floor(Price / 1000));
                int r = Convert.ToInt32(Price % 1000);
                return t.ToString() + "," + r.ToString() + " РУБ";
            }
        }
        public string ActualString
        {
            get
            {
                if (IsActual)
                {
                    return "Актуален";
                }
                else
                {
                    return "Не актуален";
                }
            }
        }
        public SolidColorBrush ActualColor
        {
            get
            {
                if (IsActual)
                {
                    return Brushes.Green;
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }
    }
}
