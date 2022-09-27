using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TurkishAPI.Controllers
{
    public class TestController : ApiController
    {



        public IHttpActionResult GetAllData() {


            CustomerRepository data = new CustomerRepository();


            DataSet dataTable = new DataSet();
            dataTable = data.GetAllRecord();


            return Ok(dataTable);

   
        }

        [HttpPost]
        public IHttpActionResult SaveRecord(Customer customer)
        {


            CustomerRepository data = new CustomerRepository();


            int result;
            result = data.saveRecord(customer);


            return Ok(result);


        }




    }
}
