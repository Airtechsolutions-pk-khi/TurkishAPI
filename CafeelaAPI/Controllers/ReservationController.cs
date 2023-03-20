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

        //status 300(pending),301(accepted),302(Completed),303(Cancelled)
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

        [HttpPost]
        [Route("reservation/admin/update/{reservationid}/{statusid}")]
        public HttpResponseMessage UpdateReservation(int reservationid, int statusid)
        {
            Rsp rsp = new Rsp();
            try
            {
                rsp = repo.UpdateReservation(reservationid, statusid);
            }
            catch (Exception ex)
            {
                rsp = new RspCustomerAddress();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }

        [Route("reservation/customer/{customerid}")]
        public RspReservationCustomer GetOrdersCustomer(int customerid)
        {
            return repo.GetCustReservations(customerid);
        }
        [Route("reservation/admin/{brandid}/{startdate}/{enddate}")]
        public RspReservationCustomer GetReservationAdmin(int brandid, string startdate, string enddate)
        {
            return repo.GetAdminReservations(brandid, startdate, enddate);
        }
    }
}
