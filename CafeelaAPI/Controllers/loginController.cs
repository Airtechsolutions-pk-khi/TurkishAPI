using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
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
    public class loginController : ApiController
    {
        loginRepository loginRepo;
        /// <summary>
        /// 
        /// </summary>
        public loginController()
        {
            loginRepo = new loginRepository(new db_a8354f_turkishpizzaEntities());

        }

        /// <summary>
        /// Login Admin location users
        /// </summary>
        /// <param name="passcode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("login/admin/{passcode}")]
        public RspAdminLogin GetLoginAdmin(string passcode)
        {
            return loginRepo.GetLoginAdmin(passcode);

        }


        /// <summary>
        /// Insert Push token
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login/insert/token")]
        public HttpResponseMessage PostInsertToken(TokenBLL obj)
        {
            Rsp rsp = loginRepo.InsertToken(obj);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }

    }
}
