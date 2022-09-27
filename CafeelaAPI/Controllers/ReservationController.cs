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
    public class ReservationController : ApiController
    {
        ReservationRepository repo;
        /// <summary>
        /// 
        /// </summary>
        public ReservationController()
        {
            repo = new ReservationRepository(new db_a8354f_turkishpizzaEntities());
        }

       
        [HttpPost]
        [Route("reservation/post")]
        public HttpResponseMessage PostOrder(ReservationBLL obj)
        {
            RspReservation rsp = new RspReservation();
            try
            {
                rsp = repo.PostReservation(obj);
            }
            catch (Exception ex)
            {
                rsp = new RspReservation();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
                rsp.ReservationID = 0;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }

       
    }
}
