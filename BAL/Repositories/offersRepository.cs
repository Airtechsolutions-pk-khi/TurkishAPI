using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BAL.Repositories
{

    public class offersRepository : BaseRepository
    {

        public offersRepository()
            : base()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();

        }

        public offersRepository(db_a8354f_turkishpizzaEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }

        public List<RspOffers> GetOffers(int brandID)
        {
            var rsp = new List<RspOffers>();

            try
            {
                var lstLoc = new List<LocationsBLL>();
                var listLocations = DBContext.sp_getLocations_api().ToList();

                var list = new List<Offer>();
                if (brandID >0)
                {
                    list = DBContext.Offers.Where(x => x.StatusID == 1 && x.BrandID == brandID).ToList();
                }
                else
                {
                    list = DBContext.Offers.Where(x => x.StatusID == 1).ToList();
                }

                foreach (var i in list)
                {
                    int locid = 0;
                    try
                    {
                        locid = i.Brand.Locations.FirstOrDefault().LocationID;
                    }
                    catch (Exception)
                    {
                        locid = 0;
                    }

                    lstLoc = new List<LocationsBLL>();
                    foreach (var j in listLocations.Where(x => x.StatusID == 1 && x.BrandID == i.BrandID))
                    {
                        lstLoc.Add(new LocationsBLL
                        {
                            ContactNo = j.ContactNo,
                            BrandID = j.BrandID,
                            Name = j.Name,
                            ImageURL = j.ImageURL,
                            Email = j.Email,
                            Address = j.Address,
                            StatusID = j.StatusID,
                            DeliveryCharges = j.DeliveryCharges,
                            DeliveryTime = j.DeliveryTime,
                            MinOrderAmount = j.MinOrderAmount,
                            DeliveryServices = j.DeliveryServices,
                            LocationID = j.LocationID,
                            Latitude = j.Latitude,
                            Longitude = j.Longitude
                        });
                    }
                    rsp.Add(new RspOffers
                    {
                        OfferID = i.OfferID,
                        Description = i.Description,
                        Name = i.Name,
                        BrandName = i.Brand.Name,
                        FromDate = i.FromDate,
                        ItemName = i.Item == null ? "":i.Item.Name,
                        Calories = i.Item == null ? "" : i.Item.Calories.ToString(),
                        ItemID = i.Item == null ? 0 : i.ItemID,
                        ToDate = i.ToDate,
                        LocationID = locid,
                        BrandID = i.BrandID,
                        Image = i.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + i.Image,
                        BrandLogo = i.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + i.Brand.Image,
                        StatusID = i.StatusID,
                        Locations = lstLoc
                    });
                }
                return rsp;
            }
            catch (Exception ex)
            {
                return new List<RspOffers>();
            }
        }
    }
}
