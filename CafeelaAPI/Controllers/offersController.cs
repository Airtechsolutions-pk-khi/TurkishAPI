using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TurkishAPI.Controllers
{
    [RoutePrefix("api")]
    public class offersController : ApiController
    {
        offersRepository repo;
        /// <summary>
        /// 
        /// </summary>
        public offersController()
        {
            repo = new offersRepository(new db_a8354f_turkishpizzaEntities());

        }        

        /// <summary>
        /// Get list for dashboard w.r.t brands selected
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("offers/all/{brandID}")]
        public List<RspOffers> GetOffers(int brandID)
        {
            return repo.GetOffers(brandID);
        }
    }
}
