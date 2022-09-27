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
    public class customerController : ApiController
    {
        customerRepository repo;
        /// <summary>
        /// 
        /// </summary>
        public customerController()
        {
            repo = new customerRepository(new db_a8354f_turkishpizzaEntities());
        }

        /// <summary>
        /// List of categories and for each category , item list is inherited
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("customer/login/{username}/{password}")]
        public RspCustomerLogin loginCustomer(string username, string password)
        {
            return repo.GetCustomerInfo(username, password);

        }

        [HttpGet]
        [Route("Customerlogin/{username}/{password}/{type}/{fullname}")]
        public RspCustomerLogin Customerlogin(string username, string password, string type,string fullname)
        {
            return repo.GetCustomerInfo(username, password, type, fullname);

        }

        /// <summary>
        /// Reset customer password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/forgetpassword/{email}")]
        public Rsp loginCustomer(string email)
        {
            return repo.ForgetPassword(email);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("customer/signup")]
        public HttpResponseMessage PostSignUp(CustomerBLL obj)
        {
            RspCustomerSignup rsp = new RspCustomerSignup();
            try
            {
                rsp = repo.Signup(obj);
            }
            catch (Exception ex)
            {
                rsp = new RspCustomerSignup();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
                rsp.CustomerID = 0;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }


        /// <summary>
        /// Customer Address Insert
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("customer/address/add")]
        public HttpResponseMessage PostCustomerAddress(CustomerAddressBLL obj)
        {
            RspCustomerAddress rsp = new RspCustomerAddress();
            try
            {
                rsp = repo.Insert(obj);
            }
            catch (Exception ex)
            {
                rsp = new RspCustomerAddress();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
                rsp.AddressID = 0;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }


       /// <summary>
       /// Customer Payment Add
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("customer/payment/add")]
        public HttpResponseMessage PostCustomerPayment(CustomerPaymentBLL obj)
        {
            RspCustomerPayment rsp = new RspCustomerPayment();
            try
            {
                rsp = repo.Insert(obj);
            }
            catch (Exception ex)
            {
                rsp = new RspCustomerPayment();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
                rsp.PaymentID = 0;
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rsp);
            json = Newtonsoft.Json.Linq.JObject.Parse(json).ToString();
            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "text/json")    //  RETURNING json
            };

        }

        [HttpPost]
        [Route("customer/profile/Update")]
        public HttpResponseMessage PostCustomerUpdate(CustomerBLL obj)
        {
            RspCustomerSignup rsp = new RspCustomerSignup();
            try
            {
                rsp = repo.CustomerUpdate(obj);
            }
            catch (Exception ex)
            {
                rsp = new RspCustomerSignup();
                rsp.status = (int)eStatus.Exception;
                rsp.description = ex.Message;
                rsp.CustomerID = 0;
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
    