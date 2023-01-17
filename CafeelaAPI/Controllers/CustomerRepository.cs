using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TurkishAPI.Controllers
{
    public class CustomerRepository
    {
        public DataSet GetAllRecord() {

			string connetionString = null;
			SqlConnection connection;
			SqlDataAdapter adapter;
			SqlCommand command = new SqlCommand();
			DataSet ds = new DataSet();

			int i = 0;

			connetionString = "Data Source=sql5107.site4now.net;Initial Catalog=db_a8354f_turkishpizza;User ID=db_a8354f_turkishpizza_admin;Password=Tech@123;MultipleActiveResultSets=True;Application Name=EntityFramework";
			connection = new SqlConnection(connetionString);

			connection.Open();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "GetCustomerDeatil";
			adapter = new SqlDataAdapter(command);
			adapter.Fill(ds);
			return ds;
		}
		public int saveRecord(Customer cus)
		{
			SqlConnection con = new 
				SqlConnection(@"Data Source=sql5107.site4now.net;Initial Catalog=db_a8354f_turkishpizza;User ID=db_a8354f_turkishpizza_admin;Password=Tech@123;MultipleActiveResultSets=True;Application Name=EntityFr");
			SqlCommand cmd = new SqlCommand("spSaveCustomer", con);
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.Parameters.AddWithValue("CusromerName", cus.CusromerName);
			cmd.Parameters.AddWithValue("Mobile", cus.Mobile);
			cmd.Parameters.AddWithValue("@Email", cus.Email);
			cmd.Parameters.AddWithValue("image", cus.image);
			cmd.Parameters.AddWithValue("StatusID", cus.StatusID);

			//cmd.Parameters.AddWithValue("LastUpdatedBy", cus.LastUpdatedBy);
			//cmd.Parameters.AddWithValue("LastudateDate", cus.LastudateDate);
			cmd.Parameters.AddWithValue("LocationID", cus.LocationID);
			cmd.Parameters.AddWithValue("BrandId", cus.BrandId);

			con.Open();
			int k = cmd.ExecuteNonQuery();
			return k;
		}
	}
}