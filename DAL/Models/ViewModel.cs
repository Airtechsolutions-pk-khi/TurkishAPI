using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    class ViewModel
    {
    }


    public class Rsp
    {
        public string description { get; set; }
        public int status { get; set; }

    }
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
    public class RspBrandList : Rsp
    {
        public List<BrandsBLL> brands { get; set; }
    }
    public class RspAppSetting : Rsp
    {
        public AppInfoBLL AppInfo { get; set; }
        public List<AboutBLL> Timings { get; set; }
    }
    public class RspDeliveryAreaList : Rsp
    {
        public List<DeliveryAreaBLL> DeliveryArea{ get; set; }
    }
    public class RspAdminLogin : Rsp
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public int POSID { get; set; }
        public string USIN { get; set; }
        public DateTime DateTime { get; set; }
        public string BuyerName { get; set; }
        public string BuyerNTN { get; set; }
        public string BuyerCNIC { get; set; }
        public string BuyerPhoneNumber { get; set; }
        public double TotalSaleValue { get; set; }
        public double TotalTaxCharged { get; set; }
        public double TotalQuantity { get; set; }
        public double Discount { get; set; }
        public double FurtherTax { get; set; }
        public double TotalBillAmount { get; set; }
        public int PaymentMode { get; set; }

        public int InvoiceType { get; set; }
        public List<InvoiceItems> Items { get; set; }
    }
    public class InvoiceItems
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string PCTCode { get; set; }
        public double Quantity { get; set; }
        public float TaxRate { get; set; }
        public double SaleValue { get; set; }
        public double Discount { get; set; }
        public double FurtherTax { get; set; }
        public double TaxCharged { get; set; }
        public double TotalAmount { get; set; }
        public int InvoiceType { get; set; }
        public string RefUSIN { get; set; }

    }

    public class RspBanners
    {
        public int BannerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> BrandID { get; set; }

    }
    public class RspOffers
    {
        public int OfferID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> BrandID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string Image { get; set; }
        public string BrandLogo { get; set; }
        public string BrandName { get; set; }
        public string ItemName { get; set; }
        public string Calories { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> StatusID { get; set; }

        public List<LocationsBLL> Locations { get; set; }

    }
    public class RspMenu : Rsp
    {
        public IEnumerable<CategoryBLL> categories { get; set; }
    }

    public class RspReservationCustomer : Rsp
    {
        public List<ReservationBLL> Reservations { get; set; }
    }

    public class RspDeliveryBoy : Rsp
    {
        public List<DeliveryBoyBLL> DeliveryBoy { get; set; }
    }
    public class RspOrdersCustomer : Rsp
    {
        public List<OrdersBLL> Orders { get; set; }
        public List<ReservationBLL> Reservations { get; set; }
    }

    public class RspOrdersAdmin : Rsp
    {
        public List<OrdersBLL> Orders { get; set; }
    }
    public class RspCustomerLogin : Rsp
    {
        public CustomerBLL customer { get; set; }
    }
   
    public class RspOrderPunch : Rsp
    {
        public int? OrderNo { get; set; }
        public int OrderID { get; set; }
        
    }
    public class RspReservation : Rsp
    {        
        public int ReservationID { get; set; }

    }
    public class RspCustomerSignup : Rsp
    {
        public int CustomerID { get; set; }
    }
    public class RspCustomerAddress : Rsp
    {
        public int AddressID { get; set; }
    }
    public class RspCustomerPayment : Rsp
    {
        public int PaymentID { get; set; }
    }

    public class DeliveryAreaBLL
    {
        public int? BrandID{ get; set; }
        public int? DeliveryAreaID{ get; set; }
        public string Name{ get; set; }
        public double? Price{ get; set; }
    }
    public class AboutBLL
    {
       
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchTiming { get; set; }
        public int? Discount { get; set; }
        public string DeliveryNo { get; set; }
        public string DiscountDecription { get; set; }
        public string WhatsappNo { get; set; }

    }
    public class AppInfoBLL
    {
        public string AppDescription { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }        
    }
    public class BrandsBLL
    {
        public int BrandID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyURl { get; set; }
        public string Address { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Currency { get; set; }
        public double? Tax { get; set; }
        public double? DeliveryCharges { get; set; }
        public Nullable<long> BusinessKey { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public double? DiscountApplied { get; set; }
        public List<LocationsBLL> Locations { get; set; }
        public List<DeliveryAreaBLL> DeliveryAreas { get; set; }
        //public List<AboutBLL> AppSettings { get; set; }
        //public List<AppInfoBLL> ApplicationInfo { get; set; }
    }

    public class LocationsBLL
    {
        public Nullable<double> Discounts { get; set; }
        public Nullable<double> Tax { get; set; }
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int LicenseID { get; set; }
        public Nullable<bool> DeliveryServices { get; set; }
        public Nullable<double> DeliveryCharges { get; set; }
        public string DeliveryTime { get; set; }
        public Nullable<double> MinOrderAmount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string ImageURL { get; set; }
        public Nullable<int> BrandID { get; set; }

    }

    public class CategoryBLL
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int DisplayOrder { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> BrandID { get; set; }

        public List<ItemBLL> items { get; set; }
    }
    public class ItemBLL
    {
        public int ItemID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Barcode { get; set; }
        public string SKU { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public string ItemType { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<double> Calories { get; set; }

        public Nullable<bool> IsSoldOut { get; set; }
        public List<ModifierBLL> modifiers { get; set; }
    }
    public class ModifierBLL
    {
        public int ItemID { get; set; }
        public int ModifierID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<double> Price { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> BrandID { get; set; }
    }

    public class CustomerBLL
    {
        public float DiscountApplied { get; set; }
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> BrandID { get; set; }

        public CustomerDetailBLL details { get; set; }

        public List<CustomerAddressBLL> address { get; set; }
        public List<CustomerPaymentBLL> payment { get; set; }
    }

    public class CustomerDetailBLL
    {
        public int CustomerDetailID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ZipCode { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }
    public class CustomerAddressBLL
    {

        public string placeID { get; set; }
        public int addressID { get; set; }
        public string houseNumber { get; set; }
        public string landmark { get; set; }

        public string locationAddress { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string placeType { get; set; }
        public int customerID { get; set; }
        public Nullable<int> brandID { get; set; }
        public Nullable<int> statusID { get; set; }
    }
    public class CustomerPaymentBLL
    {
        public int PaymentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardExpire { get; set; }
        public string CVV { get; set; }
        public string CardTitle { get; set; }
        public Nullable<int> StatusID { get; set; }
        public int CustomerID { get; set; }
        public Nullable<int> BrandID { get; set; }
    }

    public class TokenBLL
    {
        public int Device { get; set; }
        public int TokenID { get; set; }
        public string Token { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> StatusID { get; set; }

    }
    public class OdooRsp
    {
        public string message { get; set; }
        public string status { get; set; }
        public int token { get; set; }
    }
    public class authBLL
    {
        public string url { get; set; }
        public string db { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int test { get; set; }
    }
    public class invoiceBLL
    {
        public string url { get; set; }
        public string db { get; set; }
        public int token { get; set; }
        public string password { get; set; }
        public int partner_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }

        public int price_unit { get; set; }
               
    }
    public class TransferOrderBLL
    {
        public int FromLocationID { get; set; }
        public int ToLocationID { get; set; }
        public int OrderID { get; set; }

    }
    public class ReservationBLL
    {
        public int? CustomerID { get; set; }
        public int? StatusID{ get; set; }
        public int ReservationID { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public int? BrandID { get; set; }
        public int? NumberPerson { get; set; }
        public string ReservationDate { get; set; }
        public string SittingArea { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? LocationID{ get; set; }
        public int? ReservationStatus { get; set; }
    }
    public class OrdersBLL
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public int? TransactionNo { get; set; }
        public int? OrderNo { get; set; }

        public string AppVersion { get; set; }  
        public string OrderType { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderDeliveryDate { get; set; }
        public Nullable<System.DateTime> OrderPreparedDate { get; set; }
        public Nullable<System.DateTime> OrderOFDDate { get; set; }
        public Nullable<System.DateTime> OrderDoneDate { get; set; }
        public int? StatusID { get; set; }
        public string SessionID { get; set; }
        public int? OrderTakerID { get; set; }
        public int? DeliverUserID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
        public int? LocationID { get; set; }
        public int? BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }
        public string Remarks { get; set; }
        public OrderCheckoutBLL OrderCheckouts { get; set; }
        public OrderCustomerBLL CustomerOrders { get; set; }
        public List<OrderDetailBLL> OrderDetails { get; set; }
        public List<ReservationBLL> Reservations { get; set; }
        public OrderStatusBLL OrderStatus { get; set; }
    }
    public class OrderStatusBLL
    {
        public OrderStatusChildBLL OrderConfirmed { get; set; }
        public OrderStatusChildBLL FoodPrepared { get; set; }
        public OrderStatusChildBLL DeliveryinProgress { get; set; }
    }
    public class OrderStatusChildBLL
    {
        public string Value { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
    }
    public class OrderDetailBLL
    {
        public int OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public int? ItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public string OrderMode { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }

        public List<OrderModifiersBLL> OrderDetailModifiers { get; set; }
    }
    public class OrderModifiersBLL
    {
        public int OrderDetailModifierID { get; set; }
        public int? OrderDetailID { get; set; }
        public int? ModifierID { get; set; }
        public string ModifierName { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public int? StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
    }
    public class OrderCheckoutBLL
    {
        public int OrderCheckoutID { get; set; }
        public int? OrderID { get; set; }
        public int? PaymentMode { get; set; }
        public Nullable<double> AmountPaid { get; set; }
        public Nullable<double> AmountTotal { get; set; }
        public Nullable<double> ServiceCharges { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<double> DiscountAmount { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public string SessionID { get; set; }
        public int? StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }
    public class OrderCustomerBLL
    {
        public int CustomerOrderID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LocationURL { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string AddressNickName { get; set; }
        public string AddressType { get; set; }
        public string TableNo { get; set; }
    }
    public class PushNoticationBLL
    {
        public string DeviceID { get; set; }
        public string Type { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
    }
    public class DeliveryBoyBLL
    {
        public int DeliveryBoyID { get; set;}
        public string DBName { get; set;}
        public string DBAddress { get; set;}
        public string DBContactNo { get; set;}
        public string DBVehicleNo { get; set;}
        public string DBCNICNo { get; set;}
        public string DBImage { get; set;}
        public int Amount { get; set;}
        public int StatusID { get; set;}
    }
}
