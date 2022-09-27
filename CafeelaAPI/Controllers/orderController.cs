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
    public class orderController : ApiController
    {
        orderRepository repo;
        /// <summary>
        /// 
        /// </summary>
        public orderController()
        {
            repo = new orderRepository(new db_a8354f_turkishpizzaEntities());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="customerid"></param>
        /// <returns></returns>
        [Route("orders/customer/{customerid}/{orderid}")]
        public Rsp GetOrdersCustomer(int orderid,int customerid)
        {
            return repo.GetOrderCustomersV2(orderid,customerid);
        }
        
        /// <summary>
        /// List of AdminOrders
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        /// 
        [Route("orders/admin/{locationID}")]
        public Rsp GetOrdersAdmin(string locationID)
        {
            return repo.GetOrdersAdminV2(int.Parse(locationID));
        }

        [Route("orders/admin/{locationid}/{startdate}/{enddate}/{search}")]
        public Rsp GetOrdersAdmin(int locationid, string startdate, string enddate, string search)
        {
            return repo.GetOrdersAdminV2(locationid, startdate, enddate, search);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("orders/new")]
        public HttpResponseMessage PostOrder(OrdersBLL obj)
        {
            RspOrderPunch rsp = new RspOrderPunch();
            try
            {
                rsp = repo.OrderPunch(obj);
            }
            catch (Exception ex)
            {
                rsp = new RspOrderPunch();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
                rsp.OrderID = 0;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("orders/update/cancel")]
        public HttpResponseMessage PostOrderCancel(OrdersBLL obj)
        {
            Rsp rsp = new Rsp();
            try
            {
                rsp = repo.CancelOrder(obj);
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

        [HttpPost]
        [Route("orders/transfer")]
        public HttpResponseMessage PostOrderCancel(TransferOrderBLL obj)
        {
            Rsp rsp = new Rsp();
            try
            {
                rsp = repo.TransferOrder(obj.OrderID,obj.FromLocationID,obj.ToLocationID);
            }
            catch (Exception ex)
            {
                rsp = new Rsp();
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

        /// <summary>
        /// update order for admin 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orders/admin/update/{orderid}/{statusid}")]
        public HttpResponseMessage GetOrderUpdateAdmin(int orderid,int statusid)
        {
            Rsp rsp = new Rsp();
            try
            {
                rsp = repo.UpdateOrderAdmin(orderid,statusid);
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
    }
}
