using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class menuRepository : BaseRepository
    {

        public menuRepository()
            : base()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();

        }

        public menuRepository(db_a8354f_turkishpizzaEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }
        public RspMenu GetMenu(int brandID)
        {
            var bll = new List<CategoryBLL>();
            var lstItem = new List<ItemBLL>();
            var lstModifier = new List<ModifierBLL>();
            var rsp = new RspMenu();
            try
            {
                var list = DBContext.Categories.Where(x => x.StatusID == 1 && x.BrandID == brandID).ToList();

                foreach (var i in list)
                {
                    lstItem = new List<ItemBLL>();
                    foreach (var j in i.Items.Where(x => x.StatusID == 1))
                    {
                        lstModifier = new List<ModifierBLL>();
                        foreach (var k in j.Modifiers.Where(x => x.StatusID == 1))
                        {
                            lstModifier.Add(new ModifierBLL
                            {
                                Name = k.Name,
                                StatusID = k.StatusID,
                                ArabicName = k.ArabicName,
                                Description = k.Description,
                                Image = k.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + k.Image,
                                LastUpdatedBy = k.LastUpdatedBy,
                                LastUpdatedDate = k.LastUpdatedDate,
                                Price = k.Price,
                                BrandID = k.BrandID,
                                ModifierID = k.ModifierID
                            });
                        }

                        lstItem.Add(new ItemBLL
                        {
                            Name = j.Name,
                            StatusID = j.StatusID,
                            ArabicName = j.ArabicName,
                            Barcode = j.Barcode,
                            CategoryID = j.CategoryID,
                            Cost = j.Cost,
                            Description = j.Description,
                            DisplayOrder = j.DisplayOrder,
                            Image = j.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + j.Image,
                            ItemID = j.ItemID,
                            ItemType = j.ItemType,
                            LastUpdatedBy = j.LastUpdatedBy,
                            LastUpdatedDate = j.LastUpdatedDate,
                            Price = j.Price,
                            SKU = j.SKU,
                            UnitID = j.UnitID,
                            Calories = j.Calories,
                            IsSoldOut = false,
                            modifiers = lstModifier
                        });
                    }
                    bll.Add(new CategoryBLL
                    {
                        BrandID = i.BrandID,
                        ArabicName = i.ArabicName,
                        LastUpdatedDate = i.LastUpdatedDate,
                        LastUpdatedBy = i.LastUpdatedBy,
                        CategoryID = i.CategoryID,
                        Description = i.Description,
                        DisplayOrder = i.DisplayOrder,
                        LocationID = i.BrandID,
                        Name = i.Name,
                        Image = i.Image,
                        StatusID = i.StatusID,
                        items = lstItem
                    });
                }
                rsp.categories = bll;
                rsp.status = 1;
                rsp.description = "Success";


                return rsp;
            }
            catch (Exception ex)
            {
                rsp.categories = bll;
                rsp.status = 0;
                rsp.description = "Failed";
                return rsp;
            }
        }
        public RspMenu GetMenuV2(int brandID)
        {
            var bll = new List<CategoryBLL>();
            var lstItem = new List<ItemBLL>();
            var lstModifier = new List<ModifierBLL>();
            var rsp = new RspMenu();
            try
            {
                var ds = GetMenu_ADO(brandID);
                var _dsCategory = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<CategoryBLL>>();
                var _dsItem = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<ItemBLL>>();
                var _dsModifier = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[2])).ToObject<List<ModifierBLL>>();

                foreach (var i in _dsCategory.OrderBy(x=>x.DisplayOrder))
                {
                    lstItem = new List<ItemBLL>();
                    
                    foreach (var j in _dsItem.Where(x => x.StatusID == 1 && x.CategoryID==i.CategoryID).OrderBy(x => x.DisplayOrder))
                    {
                        lstModifier = new List<ModifierBLL>();
                        foreach (var k in _dsModifier.Where(x => x.StatusID == 1 && x.ItemID == j.ItemID))
                        {
                            lstModifier.Add(new ModifierBLL
                            {
                                Name = k.Name,
                                StatusID = k.StatusID,
                                ArabicName = k.ArabicName,
                                Description = k.Description,
                                Image = k.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + k.Image,
                                LastUpdatedBy = k.LastUpdatedBy,
                                LastUpdatedDate = k.LastUpdatedDate,
                                Price = k.Price,
                                BrandID=k.BrandID,
                                ModifierID=k.ModifierID
                            });
                        }

                        lstItem.Add(new ItemBLL
                        {
                            Name = j.Name,
                            StatusID = j.StatusID,
                            ArabicName = j.ArabicName,
                            Barcode = j.Barcode,
                            CategoryID = j.CategoryID,
                            Cost = j.Cost,
                            Description = j.Description,
                            DisplayOrder = j.DisplayOrder,
                            Image = j.Image == null ? "" : ConfigurationManager.AppSettings["AdminURL"].ToString() + j.Image,
                            ItemID = j.ItemID,
                            ItemType = j.ItemType,
                            LastUpdatedBy = j.LastUpdatedBy,
                            LastUpdatedDate = j.LastUpdatedDate,
                            Price = j.Price,
                            SKU = j.SKU,
                            UnitID = j.UnitID,
                            Calories=j.Calories,
                            IsSoldOut=false,
                            modifiers =lstModifier
                        });
                    }
                    bll.Add(new CategoryBLL
                    {
                        BrandID = i.BrandID,
                        ArabicName = i.ArabicName,
                        LastUpdatedDate = i.LastUpdatedDate,
                        LastUpdatedBy = i.LastUpdatedBy,
                        CategoryID = i.CategoryID,
                        Description = i.Description,
                        DisplayOrder = i.DisplayOrder,
                        LocationID = i.BrandID,
                        Name = i.Name,
                        Image = i.Image,
                        StatusID = i.StatusID,
                        items = lstItem
                    });
                }

                rsp.categories = bll;
                rsp.status = 1;
                rsp.description = "Success";
            }
            catch (Exception ex)
            {
                rsp.categories = bll;
                rsp.status = 0;
                rsp.description = "Failed";
            }
            return rsp;
        }
        public DataSet GetMenu_ADO(int BrandID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@BrandID", BrandID);
                ds = (new DBHelper().GetDatasetFromSP)("sp_GetMenu_api", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
