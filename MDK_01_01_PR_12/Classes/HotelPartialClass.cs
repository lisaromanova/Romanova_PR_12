using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDK_01_01_PR_12.Classes;

namespace MDK_01_01_PR_12
{
    public partial class Hotel
    {
        public int ToursHotel
        {
            get
            {
                List<HotelOfTour> list = DataBase.connect.HotelOfTour.Where(x => x.HotelId == Id).ToList();
                int i = 0;
                foreach(HotelOfTour hotelOfTour in list)
                {
                    i += hotelOfTour.Tour.TicketCount;
                }
                return i;
            }
        }
    }
}
