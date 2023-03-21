using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using WebAPICode.Helpers;

namespace BAL.Repositories
{
    public class deliveryboyRepository : BaseRepository
    {

        public deliveryboyRepository()
            : base()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();

        }

        public deliveryboyRepository(db_a8354f_turkishpizzaEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }
        public RspDeliveryBoy GetDeliveryBoy()
        {
            var rsp = new RspDeliveryBoy();
            try
            {
                var deliveryBoy = GetDeliveryBoy_ADO();
                rsp.DeliveryBoy = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(deliveryBoy.Tables[0])).ToObject<List<DeliveryBoyBLL>>();

                rsp.description = "Success";
                return rsp;
            }
            catch (Exception ex)
            {
                rsp.status = 0;
                rsp.description = "Failed";
                return rsp;
            }
        }
        public DataSet GetDeliveryBoy_ADO()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[0];

                ds = (new DBHelper().GetDatasetFromSP)("sp_GetDeliveryBoy_api", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}

