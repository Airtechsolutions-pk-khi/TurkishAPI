using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("login/post/odooAuth")]
        public  async Task<OdooRsp> posttest(authBLL obj)
        {
             
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ConfigurationManager.AppSettings["AuthPath"]}");
           var py = new authBLL();
            {
                py.url = obj.url;
                py.db = obj.db;
                py.username = obj.username;
                py.password = obj.password;
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(py));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await SendClientRequest(request);
            var resultJSON = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OdooRsp>(resultJSON);
            return result;
        }
       
        [HttpPost]
        [Route("login/post/Invoice")]
        public async Task<HttpResponseMessage> PostInvoice(invoiceBLL obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ConfigurationManager.AppSettings["InvoicePath"]}");
            var invoice = new invoiceBLL();
            {
                invoice.url = obj.url;
                invoice.db = obj.db;
                invoice.token = obj.token;
                invoice.password = obj.password;
                invoice.partner_id = obj.partner_id;
                invoice.product_id = obj.product_id;
                invoice.quantity = obj.quantity;
                invoice.price_unit = obj.price_unit;
        
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(invoice));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await SendClientRequest(request);
            return response;
        }
        private async Task<HttpResponseMessage> SendClientRequest(HttpRequestMessage request)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.SendAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
