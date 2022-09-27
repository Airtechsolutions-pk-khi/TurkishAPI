using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishAPI.Controllers
{
    public class Customer
    {


        public int CusromerID { get; set; }

        public string CusromerName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string image { get; set; }

        public int StatusID { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastudateDate { get; set; }

        public int LocationID { get; set; }
        public int BrandId { get; set; }
        public string Password { get; set; }


    }
}