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
                            reservation.StatusID = 1;
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

    }

}

