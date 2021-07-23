using MP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VAS.Dealer.Models.CRM;

namespace VAS.Dealer.Models.VAS
{
    public class RegisteredModel
    {
        public int STT { get; set; }
        [Key]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Vendor { get; set; }
        public string Services { get; set; }
        public string Type { get; set; }
        public string TradeKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_103Full); }
        public int TotalRows { get; set; }
    }

    public class AllRegisteredModel
    {
        public int STT { get; set; }
        [Key]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Vendor { get; set; }
        public string Services { get; set; }
        public string CreatedBy { get; set; }
        public string Type { get; set; }
        public string TradeKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_103Full); }
        public int TotalRows { get; set; }
    }

    #region Phân trang

    public class PagingResultModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; } = 0;
        public int recordsFiltered { get; set; }
        public object data { get; set; }
    }
    public class PagingResultModel2
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; } = 0;
        public int recordsFiltered { get; set; }
        public List<AllRegisteredModel> data { get; set; }
    }

    public class PagingAllHisRegisRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string User { get; set; }
        public string Branch { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; } 
        public int IsExport { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }


    public class PagingCDRRequestModel
    {
        public string MPFromDate { get; set; }
        public string MPToDate { get; set; }
        public string User { get; set; }
        public string Branch { get; set; }
        public string Type { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }



    public class PagingRequestModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }


    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class HisRegisterExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<AllRegisteredModel> Details { get; set; }
    }
    #endregion

}
