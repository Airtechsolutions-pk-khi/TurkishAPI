//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DBEntities
{
    using System;
    
    public partial class sp_rptSalesDetailsReport_Result
    {
        public double GrandTotal { get; set; }
        public double Tax { get; set; }
        public double ServiceCharges { get; set; }
        public double AmountTotal { get; set; }
        public double DiscountAmount { get; set; }
        public int TotalReturn { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> TransactionNo { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public int OrderID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string LocationName { get; set; }
    }
}
