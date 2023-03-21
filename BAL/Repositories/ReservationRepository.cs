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
                        var currDate = DateTime.UtcNow.AddMinutes(300);
                        //var isAllowReservation = false;

                        var settings = DBContext.Locations.Where(x => x.LocationID == obj.LocationID).FirstOrDefault();
                        //if (settings != null)
                        //{
                        //    try
                        //    {
                        //        if (settings.ReservationOpen != null && settings.ReservationClose != null)
                        //        {
                        //            var t1 = int.Parse(TimeSpan.Parse(settings.ReservationOpen).ToString("hhmm"));
                        //            var t2 = 2359;
                        //            var t3 = 0001;
                        //            var t4 = int.Parse(TimeSpan.Parse(settings.ReservationClose).ToString("hhmm"));
                        //            var currTimeint = int.Parse(Convert.ToDateTime(currDate).ToString("HHmm"));
                        //            isAllowReservation = (currTimeint > t1 && currTimeint < t2) && (currTimeint > t3 && currTimeint < t4) ? true : false;
                        //        }

                        //    }
                        //    catch { }
                        //}

                        //if (!isAllowReservation)
                        //{
                        //    rsp = new RspReservation();
                        //    rsp.status = (int)eStatus.Exception;
                        //    rsp.description = "Sorry, Reservation Time is from " + Convert.ToDateTime(settings.ReservationOpen).ToString("hh:mm tt") + " to " + Convert.ToDateTime(settings.ReservationClose).ToString("hh:mm tt");                            
                        //    return rsp;
                        //}

                        if (obj != null)
                        {
                            Reservation reservation = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(obj)).ToObject<Reservation>();
                            reservation.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
                            reservation.StatusID = 101;
                            reservation.ReservationStatus = 1;
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

        public Rsp UpdateReservation(int ReservationID, int StatusID, int iscustomercancel)
        {
            Rsp rsp = new Rsp();

            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    if (iscustomercancel == 1)
                    {
                        var obj = DBContext.Reservations.Where(x => x.ReservationID == ReservationID).FirstOrDefault();
                        if(obj.StatusID == 101)
                        {
                            var currDate = DateTime.UtcNow.AddMinutes(300);
                            obj.StatusID = StatusID;
                            obj.LastUpdatedDate = currDate;
                            DBContext.Reservations.AddOrUpdate(obj);
                            DBContext.SaveChanges();
                            dbContextTransaction.Commit();

                            rsp.status = (int)eStatus.Success;
                            rsp.description = "Reservation Cancelled Successfully";
                        }
                        else
                        {
                            rsp.description = "Sorry! Reservation cannot be cancelled.";
                        }

                    }
                    else
                    {
                        if (ReservationID == 0 || StatusID == 0)
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

        public RspReservationCustomer GetAdminReservations(int brandid, string startdate, string enddate)
        {
            var rsp = new RspReservationCustomer();
            try
            {
                var dataReservation = GetReservationsAdmin(brandid, startdate, enddate);
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
        public DataSet GetReservationsAdmin(int BrandID, string startdate, string enddate)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@BrandID", BrandID);
                p[1] = new SqlParameter("@StartDate", startdate);
                p[2] = new SqlParameter("@EndDate", enddate);

                ds = (new DBHelper().GetDatasetFromSP)("sp_GetAdminReservation_api", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Rsp CancelReservation(int ReservationID, int StatusID)
        {
            Rsp rsp = new Rsp();

            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    var obj = DBContext.Reservations.Where(x => x.ReservationID == ReservationID).FirstOrDefault();
                    if (obj.StatusID == 101)
                    {
                        obj.StatusID = StatusID;
                        var currDate = DateTime.UtcNow.AddMinutes(300);
                        obj.LastUpdatedDate = currDate;
                        DBContext.Reservations.AddOrUpdate(obj);
                        DBContext.SaveChanges();
                        dbContextTransaction.Commit();

                        rsp.status = (int)eStatus.Success;
                        rsp.description = "Reservation Cancelled";
                    }
                    else
                    {
                        rsp.description = "Sorry! Reservation cannot be cancelled at this time.";
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    rsp.status = (int)eStatus.Exception;
                    rsp.description = "Sorry! Reservation cannot be Cancelled.";
                }
            }

            return rsp;
        }
    }

}

