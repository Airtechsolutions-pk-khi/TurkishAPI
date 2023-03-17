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
    public class ReservationRepository : BaseRepository
    {

        public ReservationRepository()
            : base()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();

        }

        public ReservationRepository(db_a8354f_turkishpizzaEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }

        public RspReservation PostReservation(ReservationBLL obj)
        {
            RspReservation rsp;
            try
            {
                using (var dbContextTransaction = DBContext.Database.BeginTransaction())
                {
                    try
                    {                        
                        if (obj != null)
                        {                           
                            Reservation reservation = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(obj)).ToObject<Reservation>();
                            reservation.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
                            reservation.StatusID = 300;
                            Reservation data = DBContext.Reservations.Add(reservation);
                            DBContext.SaveChanges();
                            dbContextTransaction.Commit();

                            rsp = new RspReservation();
                            rsp.status = (int)eStatus.Success;
                            rsp.description = "Reserved successfully.";
                            rsp.ReservationID = data.ReservationID;
                        }
                        else
                        {
                            rsp = new RspReservation();
                            rsp.status = (int)eStatus.Failed;
                            rsp.description = "Sorry, your Reservation Can not Successfully done";
                            rsp.ReservationID = 0;
                        }

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        rsp = new RspReservation();
                        rsp.status = (int)eStatus.Exception;
                        rsp.description = "Sorry, your Reservation Can not Successfully done";
                        rsp.ReservationID = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                rsp = new RspReservation();
                rsp.status = (int)eStatus.Exception;
                rsp.description = "Sorry, your Reservation Can not Successfully done";
                rsp.ReservationID = 0;
            }
            return rsp;
        }

        public Rsp UpdateReservation(int ReservationID, int StatusID)
        {
            Rsp rsp = new Rsp();

            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    if (ReservationID == 0 || ReservationID == 0)
                    {
                        rsp.status = (int)eStatus.Exception;
                        rsp.description = "Cannot be update due to invalid parameter";
                    }
                    else
                    {
                        var currDate = DateTime.UtcNow.AddMinutes(300);

                        var obj = DBContext.Reservations.Where(x => x.ReservationID == ReservationID).FirstOrDefault();
                        obj.StatusID = StatusID;
                        obj.LastUpdatedDate = currDate;
                        DBContext.Reservations.AddOrUpdate(obj);
                        DBContext.SaveChanges();
                        dbContextTransaction.Commit();

                        rsp.status = (int)eStatus.Success;
                        rsp.description = "Reservation Updated";
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    rsp.status = (int)eStatus.Exception;
                    rsp.description = "Sorry! Reservation cannot be updated.";
                }
            }

            return rsp;
        }

        public RspReservationCustomer GetCustReservations(int customerID)
        {
            var rsp = new RspReservationCustomer();
            try
            {
                var dataReservation = GetReservationsCustomer(customerID);
                rsp.Reservations = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(dataReservation.Tables[0])).ToObject<List<ReservationBLL>>();
                rsp.status = 1;
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

        public RspReservationCustomer GetAdminReservations(int brandid)
        {
            var rsp = new RspReservationCustomer();
            try
            {
                var dataReservation = GetReservationsAdmin(brandid);
                rsp.Reservations = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(dataReservation.Tables[0])).ToObject<List<ReservationBLL>>();
                rsp.status = 1;
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

        public DataSet GetReservationsCustomer(int CustomerID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@CustomerID", CustomerID);

                ds = (new DBHelper().GetDatasetFromSP)("sp_GetCustomerReservation_api", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetReservationsAdmin(int BrandID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@BrandID", BrandID);

                ds = (new DBHelper().GetDatasetFromSP)("sp_GetAdminReservation_api", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}

