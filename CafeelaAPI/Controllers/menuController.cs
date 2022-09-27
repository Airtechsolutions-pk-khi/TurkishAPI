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
    public class menuController : ApiController
    {
        menuRepository repo;
        public menuController()
        {
            repo = new menuRepository(new db_a8354f_turkishpizzaEntities());
        }

        /// <summary>
        /// List of categories and for each category , item list is inherited
        /// </summary>
        /// <param name="brandID"></param>
        /// <returns></returns>
        /// 
        [Route("menu/{brandID}")]
        public RspMenu GetMenu(string brandID)
        {
            return repo.GetMenuV2(int.Parse(brandID));
        }
    }
}
