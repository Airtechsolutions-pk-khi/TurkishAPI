using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BAL.Repositories
{

    public class loginRepository : BaseRepository
    {
        public loginRepository()
            : base()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();

        }
        public loginRepository(db_a8354f_turkishpizzaEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }
        public RspBrandList GetBrandInfo()
        {
            var bll = new List<BrandsBLL>();
            var lstLoc = new List<LocationsBLL>();
            var lstDA = new List<DeliveryAreaBLL>();
            var lstdelivery = new List<DeliveryAreaBLL>();
            var rsp = new RspBrandList();
            var lstAS = new List<AboutBLL>();
            var lstAI = new List<AppInfoBLL>();
            try
            {
                var list = DBContext.sp_getBrands_api().ToList();
                var listLocations = DBContext.sp_getLocations_api().ToList();
                var listDA = DBContext.DeliveryAreaBrandJuncs.Where(x => x.Delivery.StatusID == 1).ToList();
                var listAS = DBContext.AppSettings.Where(x => x.StatusID == 1).ToList();
                var listAI = DBContext.ApplicationInfoes.ToList();


                foreach (var i in list)
                {
                    lstDA = new List<DeliveryAreaBLL>();
                    foreach (var j in listDA.Where(x => x.BrandID == i.BrandID).ToList())
                    {
                        lstDA.Add(new DeliveryAreaBLL
                        {
                            DeliveryAreaID = j.DeliveryAreaID,
                            BrandID = j.BrandID,
                            Name = j.Delivery.Name,
                            Price = j.Delivery.Amount,
                        });
                    }
                    lstAS = new List<AboutBLL>();
                    foreach (var j in listAS.ToList())
                    {
                        lstAS.Add(new AboutBLL
                        {
                            BranchName = j.BranchName,
                            BranchAddress = j.BranchAddress,
                            BranchTiming = j.BranchTiming,
                            DeliveryNo = j.DeliveryNo,
                            Discount = j.Discount,
                            DiscountDecription = j.Discountdescription,
                        });
                    }
                    lstAI = new List<AppInfoBLL>();
                    foreach (var j in listAI.ToList())
                    {
                        lstAI.Add(new AppInfoBLL
                        {
                            AppDescription = j.AppDescription,
                            Facebook = j.Facebook,
                            Twitter = j.Twitter,
                            Instagram = j.Instagram,
                        });
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
                            Longitude = j.Longitude,
                            Discounts = j.Discounts,
                            Tax = j.Tax,
                            Description = j.Description,

                        });
                    }

                    bll.Add(new BrandsBLL
                    {

                        BrandID = i.BrandID,
                        Username = i.Username,
                        Name = i.Name,
                        Image = i.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + i.Image,
                        Email = i.Email,
                        Password = i.Password,
                        Address = i.Address,
                        Currency = i.Currency,
                        BusinessKey = i.BusinessKey,
                        StatusID = i.StatusID,
                        Locations = lstLoc,
                        DeliveryAreas = lstDA,
                        //ApplicationInfo = lstAI,
                        //AppSettings = lstAS,
                        CompanyURl = i.CompanyURl == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + i.CompanyURl,
                        Tax = lstLoc.FirstOrDefault() == null ? 0 : lstLoc.FirstOrDefault().Tax == null ? 0 : lstLoc.FirstOrDefault().Tax,
                        DeliveryCharges = lstLoc.FirstOrDefault() == null ? 0 : lstLoc.FirstOrDefault().DeliveryCharges == null ? 0 : lstLoc.FirstOrDefault().DeliveryCharges,
                        DiscountApplied = lstLoc.FirstOrDefault() == null ? 0 : lstLoc.FirstOrDefault().Discounts == null ? 0 : lstLoc.FirstOrDefault().Discounts
                    });

                }
                rsp.brands = bll;
                rsp.status = 1;
                rsp.description = "Success";

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.brands = bll;
                rsp.status = 0;
                rsp.description = "Failed";
                return rsp;
            }
        }

        public RspAppSetting GetAppsetting()
        {
            var bll = new List<AboutBLL>();

            var rsp = new RspAppSetting();
            var lstAS = new List<AboutBLL>();
            var objAI = new AppInfoBLL();
            try
            {
                var listAS = DBContext.sp_getAppsetting_api().ToList();
                //var listAS = DBContext.AppSettings.Where(x => x.StatusID == 1).ToList();
                var appinfo = DBContext.ApplicationInfoes.FirstOrDefault();


                lstAS = new List<AboutBLL>();
                foreach (var j in listAS)
                {
                    lstAS.Add(new AboutBLL
                    {
                        BranchName = j.BranchName,
                        BranchAddress = j.BranchAddress,
                        BranchTiming = j.BranchTiming,
                        DeliveryNo = j.DeliveryNo,
                        Discount = j.Discount,
                        DiscountDecription = j.Discountdescription,
                        WhatsappNo = j.WhatsappNo
                    });
                }

                if (appinfo != null)
                {
                    objAI = new AppInfoBLL
                    {
                        AppDescription = appinfo.AppDescription,
                        Facebook = appinfo.Facebook,
                        Instagram = appinfo.Instagram,
                        Twitter = appinfo.Twitter
                    };
                }

                rsp.Timings = lstAS;
                rsp.AppInfo = objAI;
                rsp.status = 1;
                rsp.description = "Success";

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.Timings = new List<AboutBLL>();
                rsp.AppInfo = new AppInfoBLL();
                rsp.status = 0;
                rsp.description = "Failed";
                return rsp;
            }
        }


        public RspDeliveryAreaList GetDeliveryArea(int? BrandID)
        {
            var lst = new List<DeliveryAreaBLL>();
            var rsp = new RspDeliveryAreaList();
            try
            {
                var list = DBContext.DeliveryAreaBrandJuncs.Where(x => x.BrandID == BrandID).ToList();

                foreach (var i in list)
                {
                    lst.Add(new DeliveryAreaBLL
                    {
                        BrandID = i.BrandID,
                        Name = i.Delivery.Name,
                        Price = i.Delivery.Amount,
                    });
                }
                rsp.DeliveryArea = lst;
                rsp.status = 1;
                rsp.description = "Success";

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.DeliveryArea = lst;
                rsp.status = 0;
                rsp.description = "Failed";
                return rsp;
            }
        }
        public List<RspBanners> GetBanners(int brandID)
        {
            var rsp = new List<RspBanners>();
            try
            {
                var list = DBContext.Banners.Where(x => x.StatusID == 1 && x.BrandID == brandID).ToList();

                foreach (var i in list)
                {

                    rsp.Add(new RspBanners
                    {
                        BannerID = i.BannerID,
                        Description = i.Description,
                        Name = i.Name,
                        BrandID = i.BrandID,
                        Image = i.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + i.Image,
                        StatusID = i.StatusID
                    });
                }
                return rsp;
            }
            catch (Exception ex)
            {
                return new List<RspBanners>();
            }
        }
        public RspAdminLogin GetLoginAdmin(string passcode)
        {
            var rsp = new RspAdminLogin();
            try
            {
                var data = DBContext.Locations.Where(x => x.Passcode == passcode).FirstOrDefault();
                if (data != null)
                {
                    rsp.LocationID = data.LocationID;
                    rsp.Longitude = data.Longitude;
                    rsp.Latitude = data.Latitude;
                    rsp.ContactNo = data.ContactNo;
                    rsp.Name = data.Name;
                    rsp.status = 1;
                    rsp.description = "Login successfully.";
                }
                else
                {
                    rsp.status = 0;
                    rsp.description = "User not found.";
                }

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.status = 0;
                rsp.description = "Login failed.";
                return rsp;
            }
        }

        public Rsp InsertToken(TokenBLL obj)
        {
            Rsp rsp;
            try
            {
                PushToken token = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(obj)).ToObject<PushToken>();
                token.StatusID = 1;
                var chk = DBContext.PushTokens.Where(x => x.Token == obj.Token && x.LocationID == obj.LocationID).Count();
                if (chk == 0)
                {
                    PushToken data = DBContext.PushTokens.Add(token);
                    DBContext.SaveChanges();
                }


                rsp = new Rsp();
                rsp.status = (int)eStatus.Success;
                rsp.description = "Token Added";
            }
            catch (Exception ex)
            {
                rsp = new Rsp();
                rsp.status = (int)eStatus.Exception;
                rsp.description = "Failed to add token";
            }
            return rsp;
        }
        public Rsp InsertAuth(authBLL obj)
        {
            Rsp rsp;
            try
            {
                authBLL py = new authBLL();
                {
                    py.url = obj.url;
                    py.db = obj.db;
                    py.username = obj.username;
                    py.password = obj.password;
                }
                

                rsp = new Rsp();
                rsp.status = (int)eStatus.Success;
                rsp.description = "Authenticated";
            }
            catch (Exception ex)
            {
                rsp = new Rsp();
                rsp.status = (int)eStatus.Exception;
                rsp.description = "Failed Authenticated";
            }
            return rsp;
        }
    }
}
