using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BAL.Repositories
{

    public class customerRepository : BaseRepository
    {

        public customerRepository()
            : base()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();

        }

        public customerRepository(db_a8354f_turkishpizzaEntities contextDB)
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
                                Image = k.Image,
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
                            Image = j.Image,
                            ItemID = j.ItemID,
                            ItemType = j.ItemType,
                            LastUpdatedBy = j.LastUpdatedBy,
                            LastUpdatedDate = j.LastUpdatedDate,
                            Price = j.Price,
                            SKU = j.SKU,
                            UnitID = j.UnitID,
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
        public RspCustomerLogin GetCustomerInfo(string username, string password, string type,string fullname)
        {
            var bll = new CustomerBLL();
            var objCustomerDetail = new CustomerDetailBLL();
            var lstAddress = new List<CustomerAddressBLL>();
            var lstPayment = new List<CustomerPaymentBLL>();
            var rsp = new RspCustomerLogin();
            try
            {
                var customerInfo = new Customer();
                if (type == "sm")
                {
                    customerInfo = DBContext.Customers.Where(x => x.Email == username).FirstOrDefault();
                    password = "social";//to get value from sp with using email only 
                    if (customerInfo == null)
                    {
                        customerInfo = new Customer();
                        customerInfo.Email = username;
                        customerInfo.FullName= fullname;
                        customerInfo.LastUpdatedDate = DateTime.Now;
                        customerInfo.StatusID = 1;
                        customerInfo = DBContext.Customers.Add(customerInfo);
                        DBContext.SaveChanges();

                    }
                }
                lstAddress = new List<CustomerAddressBLL>();
                var addHome = customerInfo.CustomerAddresses.Where(x => x.StatusID == 1 && x.Type == "Home").OrderByDescending(x => x.AddressID).FirstOrDefault();
                if (addHome != null)
                {
                    var k = addHome;
                    lstAddress.Add(new CustomerAddressBLL
                    {
                        statusID = k.StatusID,
                        brandID = k.BrandID,
                        addressID = k.AddressID,
                        landmark = k.Address,
                        placeType = k.Type,
                        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                        houseNumber = k.ShortName,
                        customerID = k.CustomerID,
                        placeID = k.PlaceID,
                        locationAddress = k.LocationAddress
                    });
                }

                var addWork = customerInfo.CustomerAddresses.Where(x => x.StatusID == 1 && x.Type == "Work").OrderByDescending(x => x.AddressID).FirstOrDefault();
                if (addWork != null)
                {
                    var k = addWork;
                    lstAddress.Add(new CustomerAddressBLL
                    {
                        statusID = k.StatusID,
                        brandID = k.BrandID,
                        addressID = k.AddressID,
                        landmark = k.Address,
                        placeType = k.Type,
                        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                        houseNumber = k.ShortName,
                        customerID = k.CustomerID,
                        placeID = k.PlaceID,
                        locationAddress = k.LocationAddress
                    });
                }
                var addOther = customerInfo.CustomerAddresses.Where(x => x.StatusID == 1 && x.Type == "Other").OrderByDescending(x => x.AddressID).FirstOrDefault();
                if (addOther != null)
                {
                    var k = addOther;
                    lstAddress.Add(new CustomerAddressBLL
                    {
                        statusID = k.StatusID,
                        brandID = k.BrandID,
                        addressID = k.AddressID,
                        landmark = k.Address,
                        placeType = k.Type,
                        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                        houseNumber = k.ShortName,
                        customerID = k.CustomerID,
                        placeID = k.PlaceID,
                        locationAddress = k.LocationAddress
                    });
                }
                //foreach (var k in result.CustomerAddresses.Where(x => x.StatusID == 1).OrderBy(x => x.Type))
                //{
                //    lstAddress.Add(new CustomerAddressBLL
                //    {
                //        statusID = k.StatusID,
                //        brandID = k.BrandID,
                //        addressID = k.AddressID,
                //        landmark = k.Address,
                //        placeType = k.Type,
                //        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                //        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                //        houseNumber = k.ShortName,
                //        customerID = k.CustomerID,
                //        placeID = k.PlaceID,
                //        locationAddress = k.LocationAddress
                //    });
                //}

                // address
                lstPayment = new List<CustomerPaymentBLL>();
                foreach (var k in customerInfo.CustomerPayments.Where(x => x.StatusID == 1))
                {
                    lstPayment.Add(new CustomerPaymentBLL
                    {
                        StatusID = k.StatusID,
                        BrandID = k.BrandID,
                        CustomerID = k.CustomerID,
                        CardExpire = k.CardExpire,
                        CardTitle = k.CardTitle,
                        CVV = k.CVV,
                        Description = k.Description,
                        Name = k.Name,
                        PaymentID = k.PaymentID,
                    });
                }

                var _customerDetail = customerInfo.CustomerDetails.FirstOrDefault();
                if (_customerDetail != null)
                {
                    objCustomerDetail = new CustomerDetailBLL
                    {
                        CustomerID = _customerDetail.CustomerID,
                        LastUpdatedBy = _customerDetail.LastUpdatedBy,
                        LastUpdatedDate = _customerDetail.LastUpdatedDate,
                        Address = _customerDetail.Address,
                        Contact = _customerDetail.Contact,
                        CustomerDetailID = _customerDetail.CustomerDetailID,
                        Latitude = _customerDetail.Latitude,
                        Longitude = _customerDetail.Longitude,
                        ZipCode = _customerDetail.ZipCode,
                        StatusID = _customerDetail.StatusID
                    };
                }
                else
                    objCustomerDetail = null;


                rsp.customer = new CustomerBLL
                {
                    address = lstAddress,
                    payment = lstPayment,
                    BrandID = customerInfo.BrandID,
                    CustomerID = customerInfo.CustomerID,
                    details = objCustomerDetail,
                    Email = customerInfo.Email,
                    FullName = customerInfo.FullName,
                    Image = customerInfo.Image,
                    LastUpdatedBy = customerInfo.LastUpdatedBy,
                    LastUpdatedDate = customerInfo.LastUpdatedDate,
                    LocationID = customerInfo.LocationID,
                    Mobile = customerInfo.Mobile,
                    StatusID = customerInfo.StatusID,
                    DiscountApplied=20,
                    Password= customerInfo.Password
                };

                rsp.status = 1;
                rsp.description = "Success";
                return rsp;
            }
            catch (Exception ex)
            {

                rsp.status = 0;
                rsp.description = "Failed to load data.";
            }
            return rsp;
        }
        public RspCustomerLogin GetCustomerInfo(string username, string password)
        {
            var bll = new CustomerBLL();
            var objCustomerDetail = new CustomerDetailBLL();
            var lstAddress = new List<CustomerAddressBLL>();
            var lstPayment = new List<CustomerPaymentBLL>();
            var rsp = new RspCustomerLogin();
            try
            {

                var result = DBContext.Customers.Where(x => x.StatusID == 1 && (x.Email == username || x.Mobile == username) && x.Password == password).FirstOrDefault();

                // addresshome

                lstAddress = new List<CustomerAddressBLL>();
                var addHome = result.CustomerAddresses.Where(x => x.StatusID == 1 && x.Type == "Home").OrderByDescending(x => x.AddressID).FirstOrDefault();
                if (addHome != null)
                {
                    var k = addHome;
                    lstAddress.Add(new CustomerAddressBLL
                    {
                        statusID = k.StatusID,
                        brandID = k.BrandID,
                        addressID = k.AddressID,
                        landmark = k.Address,
                        placeType = k.Type,
                        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                        houseNumber = k.ShortName,
                        customerID = k.CustomerID,
                        placeID = k.PlaceID,
                        locationAddress = k.LocationAddress
                    });
                }

                var addWork = result.CustomerAddresses.Where(x => x.StatusID == 1 && x.Type == "Work").OrderByDescending(x => x.AddressID).FirstOrDefault();
                if (addWork != null)
                {
                    var k = addWork;
                    lstAddress.Add(new CustomerAddressBLL
                    {
                        statusID = k.StatusID,
                        brandID = k.BrandID,
                        addressID = k.AddressID,
                        landmark = k.Address,
                        placeType = k.Type,
                        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                        houseNumber = k.ShortName,
                        customerID = k.CustomerID,
                        placeID = k.PlaceID,
                        locationAddress = k.LocationAddress
                    });
                }
                var addOther = result.CustomerAddresses.Where(x => x.StatusID == 1 && x.Type == "Other").OrderByDescending(x => x.AddressID).FirstOrDefault();
                if (addOther != null)
                {
                    var k = addOther;
                    lstAddress.Add(new CustomerAddressBLL
                    {
                        statusID = k.StatusID,
                        brandID = k.BrandID,
                        addressID = k.AddressID,
                        landmark = k.Address,
                        placeType = k.Type,
                        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                        houseNumber = k.ShortName,
                        customerID = k.CustomerID,
                        placeID = k.PlaceID,
                        locationAddress = k.LocationAddress
                    });
                }
                //foreach (var k in result.CustomerAddresses.Where(x => x.StatusID == 1).OrderBy(x => x.Type))
                //{
                //    lstAddress.Add(new CustomerAddressBLL
                //    {
                //        statusID = k.StatusID,
                //        brandID = k.BrandID,
                //        addressID = k.AddressID,
                //        landmark = k.Address,
                //        placeType = k.Type,
                //        latitude = k.Latitude == null ? 0 : float.Parse(k.Latitude),
                //        longitude = k.Longitude == null ? 0 : float.Parse(k.Longitude),
                //        houseNumber = k.ShortName,
                //        customerID = k.CustomerID,
                //        placeID = k.PlaceID,
                //        locationAddress = k.LocationAddress
                //    });
                //}

                // address
                lstPayment = new List<CustomerPaymentBLL>();
                foreach (var k in result.CustomerPayments.Where(x => x.StatusID == 1))
                {
                    lstPayment.Add(new CustomerPaymentBLL
                    {
                        StatusID = k.StatusID,
                        BrandID = k.BrandID,
                        CustomerID = k.CustomerID,
                        CardExpire = k.CardExpire,
                        CardTitle = k.CardTitle,
                        CVV = k.CVV,
                        Description = k.Description,
                        Name = k.Name,
                        PaymentID = k.PaymentID,
                    });
                }

                var _customerDetail = result.CustomerDetails.FirstOrDefault();
                if (_customerDetail != null)
                {
                    objCustomerDetail = new CustomerDetailBLL
                    {
                        CustomerID = _customerDetail.CustomerID,
                        LastUpdatedBy = _customerDetail.LastUpdatedBy,
                        LastUpdatedDate = _customerDetail.LastUpdatedDate,
                        Address = _customerDetail.Address,
                        Contact = _customerDetail.Contact,
                        CustomerDetailID = _customerDetail.CustomerDetailID,
                        Latitude = _customerDetail.Latitude,
                        Longitude = _customerDetail.Longitude,
                        ZipCode = _customerDetail.ZipCode,
                        StatusID = _customerDetail.StatusID
                    };
                }
                else
                    objCustomerDetail = null;

                rsp.customer = new CustomerBLL
                {
                    address = lstAddress,
                    payment = lstPayment,
                    BrandID = result.BrandID,
                    CustomerID = result.CustomerID,
                    details = objCustomerDetail,
                    Email = result.Email,
                    FullName = result.FullName,
                    Image = result.Image,
                    LastUpdatedBy = result.LastUpdatedBy,
                    LastUpdatedDate = result.LastUpdatedDate,
                    LocationID = result.LocationID,
                    Mobile = result.Mobile,
                    StatusID = result.StatusID,
                    Password=result.Password,
                    DiscountApplied=20,
                    
                };

                rsp.status = 1;
                rsp.description = "Success";
                return rsp;
            }
            catch (Exception ex)
            {
                rsp.customer = bll;
                rsp.status = 0;
                rsp.description = "Failed";
                return rsp;
            }
        }


        public Rsp ForgetPassword(string email)
        {
            var bll = new CustomerBLL();
            var rsp = new Rsp();
            try
            {

                var result = DBContext.Customers.Where(x => x.StatusID == 1 && x.Email == email).FirstOrDefault();

                result.Password = RandomString(10, false);
                DBContext.Customers.Attach(result);
                DBContext.UpdateOnly<SubUser>(
                    result, x => x.Password);

                DBContext.SaveChanges();

                try
                {
                    Email("LunchBox-APP",
                        "Your password has been reset successfully./n your new password is " + result.Password + ".",
                        email,
                        "",
                        "",
                        "",
                        0
                        );
                }
                catch
                {
                }

                rsp.status = 1;
                rsp.description = "Password reset successfully";


                return rsp;
            }
            catch (Exception ex)
            {
                rsp.status = 0;
                rsp.description = "Failed to ";
                return rsp;
            }
        }


        public RspCustomerSignup Signup(CustomerBLL obj)
        {
            RspCustomerSignup rsp;

            try
            {

                using (var dbContextTransaction = DBContext.Database.BeginTransaction())
                {
                    try
                    {
                        var chkCustomer = DBContext.Customers.Where(x => (x.Mobile == obj.Mobile || x.Email == obj.Email) && x.Password == obj.Password).Count();

                        if (chkCustomer == 0)
                        {
                            var currDate = DateTime.UtcNow.AddMinutes(300);

                            Customer customer = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(obj)).ToObject<Customer>();
                            customer.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
                            customer.StatusID = 1;
                            Customer data = DBContext.Customers.Add(customer);
                            DBContext.SaveChanges();
                            dbContextTransaction.Commit();

                            rsp = new RspCustomerSignup();
                            rsp.status = (int)eStatus.Success;
                            rsp.description = "Your account has been registered successfully.";
                            rsp.CustomerID = data.CustomerID;
                        }
                        else
                        {
                            rsp = new RspCustomerSignup();
                            rsp.status = (int)eStatus.Failed;
                            rsp.description = "Username or password is already registered";
                            rsp.CustomerID = 0;
                        }

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        rsp = new RspCustomerSignup();
                        rsp.status = (int)eStatus.Exception;
                        rsp.description = "Sorry, You cannot be able to signup now.";
                        rsp.CustomerID = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                rsp = new RspCustomerSignup();
                rsp.status = (int)eStatus.Exception;
                rsp.description = "Sorry, You cannot be able to signup now.";
                rsp.CustomerID = 0;
            }
            return rsp;
        }

        public RspCustomerSignup CustomerUpdate(CustomerBLL obj)
        {
            RspCustomerSignup rsp;
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    var customer = DBContext.Customers.Where(x => x.CustomerID == obj.CustomerID).FirstOrDefault();

                    if (customer != null)
                    {
                        customer.FullName = obj.FullName;
                        customer.Mobile = obj.Mobile;
                        customer.Password = obj.Password;
                        customer.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
                        customer.StatusID = 1;
                        DBContext.Customers.AddOrUpdate(customer);
                        DBContext.SaveChanges();
                        dbContextTransaction.Commit();

                        rsp = new RspCustomerSignup();
                        rsp.status = (int)eStatus.Success;
                        rsp.description = "Customer Profile Updated";
                        rsp.CustomerID = obj.CustomerID;
                    }
                    else
                    {
                        rsp = new RspCustomerSignup();
                        rsp.status = (int)eStatus.Failed;
                        rsp.description = "Customer Not Found.";
                        rsp.CustomerID = 0;
                    }

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    rsp = new RspCustomerSignup();
                    rsp.status = (int)eStatus.Exception;
                    rsp.description = "Sorry, You cannot be able to signup now.";
                    rsp.CustomerID = 0;
                }
            }
            return rsp;
        }
        public RspCustomerAddress Insert(CustomerAddressBLL obj)
        {
            RspCustomerAddress rsp;
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    CustomerAddress address = new CustomerAddress();
                    address.ShortName = obj.houseNumber;
                    address.Address = obj.landmark;
                    address.LocationAddress = obj.locationAddress;
                    address.CustomerID = obj.customerID;
                    address.Latitude = obj.latitude.ToString();
                    address.Longitude = obj.longitude.ToString();
                    address.StatusID = 1;
                    address.Type = obj.placeType;
                    address.BrandID = obj.brandID;
                    address.PlaceID = obj.placeID;
                    CustomerAddress data = DBContext.CustomerAddresses.Add(address);
                    DBContext.SaveChanges();

                    //foreach (var item in list)
                    //{
                    //    if (item.addressID == 0)
                    //    {
                    //        CustomerAddress address = new CustomerAddress();
                    //        address.ShortName = item.houseNumber;
                    //        address.Address = item.landmark;
                    //        address.LocationAddress = item.locationAddress;
                    //        address.CustomerID= item.customerID;
                    //        address.Latitude= item.latitude.ToString();
                    //        address.Longitude = item.longitude.ToString();
                    //        address.StatusID = 1;
                    //        address.Type= item.placeType;
                    //        address.BrandID= item.brandID;
                    //        address.PlaceID = item.placeID;
                    //        CustomerAddress data = DBContext.CustomerAddresses.Add(address);
                    //        DBContext.SaveChanges();
                    //    }
                    //    else
                    //    {
                    //        CustomerAddress address = new CustomerAddress();
                    //        address.AddressID= item.addressID;
                    //        address.ShortName = item.houseNumber;
                    //        address.Address = item.landmark;
                    //        address.LocationAddress = item.locationAddress;
                    //        address.CustomerID = item.customerID;
                    //        address.Latitude = item.latitude.ToString();
                    //        address.Longitude = item.longitude.ToString();
                    //        address.StatusID = 1;
                    //        address.Type = item.placeType;
                    //        address.PlaceID = item.placeID;
                    //        address.BrandID = item.brandID;

                    //        DBContext.CustomerAddresses.AddOrUpdate(address);
                    //        DBContext.SaveChanges();
                    //    }
                    //}
                    dbContextTransaction.Commit();
                    rsp = new RspCustomerAddress();
                    rsp.status = (int)eStatus.Success;
                    rsp.description = "Your addresses updated successfully.";
                    rsp.AddressID = 0;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    rsp = new RspCustomerAddress();
                    rsp.status = (int)eStatus.Exception;
                    rsp.description = "Your address doesnot added successfully.";
                    rsp.AddressID = 0;
                }
            }

            return rsp;
        }
        public RspCustomerPayment Insert(CustomerPaymentBLL obj)
        {
            RspCustomerPayment rsp;

            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    if (obj.PaymentID == 0)
                    {
                        var currDate = DateTime.UtcNow.AddMinutes(300);

                        CustomerPayment payment = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(obj)).ToObject<CustomerPayment>();
                        payment.StatusID = 1;
                        CustomerPayment data = DBContext.CustomerPayments.Add(payment);
                        DBContext.SaveChanges();
                        dbContextTransaction.Commit();

                        rsp = new RspCustomerPayment();
                        rsp.status = (int)eStatus.Success;
                        rsp.description = "Your payment added successfully.";
                        rsp.PaymentID = data.PaymentID;
                    }
                    else
                    {
                        var currDate = DateTime.UtcNow.AddMinutes(300);

                        CustomerPayment payment = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(obj)).ToObject<CustomerPayment>();
                        payment.StatusID = 1;
                        DBContext.CustomerPayments.AddOrUpdate(payment);
                        DBContext.SaveChanges();
                        dbContextTransaction.Commit();

                        rsp = new RspCustomerPayment();
                        rsp.status = (int)eStatus.Success;
                        rsp.description = "Your payment updated successfully.";
                        rsp.PaymentID = obj.PaymentID;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    rsp = new RspCustomerPayment();
                    rsp.status = (int)eStatus.Exception;
                    rsp.description = "Your address doesnot added successfully.";
                    rsp.PaymentID = 0;
                }
            }

            return rsp;
        }
    }
}
