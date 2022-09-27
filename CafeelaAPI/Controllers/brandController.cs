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
    public class brandController : ApiController
    {
        loginRepository loginRepo;
        /// <summary>
        /// 
        /// </summary>
        public brandController()
        {
            loginRepo = new loginRepository(new db_a8354f_turkishpizzaEntities());

        }

        /// <summary>
        /// Get list of Brands and Locations are inherited in each brand model
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("brand/all")]
        public RspBrandList GetBrands()
        {
            return loginRepo.GetBrandInfo();

        }

        [HttpGet]
        [Route("brand/appsetting")]
        public RspAppSetting GetAppInfo()
        {
            return loginRepo.GetAppsetting();

        }

        /// <summary>
        /// Get list for dashboard w.r.t brands selected
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dashboard/banners/{brandID}")]
        public List<RspBanners> GetBanners(int brandID)
        {
            return loginRepo.GetBanners(brandID);
        }

        [HttpGet]
        [Route("deliveryarea/all/{BrandID}")]
        public RspDeliveryAreaList GetDeliveryArea(int? BrandID)
        {
            return loginRepo.GetDeliveryArea(BrandID);
        }

    }
}
