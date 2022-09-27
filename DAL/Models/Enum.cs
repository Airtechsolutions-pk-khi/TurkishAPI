using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum eStatus
    {
        InvalidSession = 1001,
        InvalidVinNo = 1002,
        AlreadyCheckout = 1003,
        ServiceOrderChanged = 1004,
        CheckoutOrderChanged = 1005,
        Success = 1,
        Failed = 0,
        Exception = 1000,
        AppVersion = 1006

    }
    public enum eItemType
    {
        oil = 1,
        oilfilter = 2
    }
}
