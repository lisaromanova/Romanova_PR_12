//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MDK_01_01_PR_12
{
    using System;
    using System.Collections.Generic;
    
    public partial class HotelOfTour
    {
        public int HotelOfTourId { get; set; }
        public int HotelId { get; set; }
        public int TourId { get; set; }
    
        public virtual Hotel Hotel { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
