using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace TurkishAPI.Controllers
{
    [RoutePrefix("api")]
    public class DeliveryBoyController : ApiController
    {
        deliveryboyRepository repo;
        /// <summary>
        /// 
        /// </summary>
        public DeliveryBoyController()
        {
            repo = new deliveryboyRepository(new db_a8354f_turkishpizzaEntities());
        }
        [Route("deliveryboy")]
        public RspDeliveryBoy GetDeliveryBoy()
        {
            return repo.GetDeliveryBoy();
        }
    }
}
