using Microsoft.AspNetCore.Mvc.Rendering;

namespace VAS.Dealer.Models.CRM
{
    public class CustomerIndexModel
    {
        public SelectList Areas { get; set; }
    }


    public class CustomerSearchRequest
    {
        /// <summary>
        /// Loại khách hàng
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Khu vực
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// Tỉnh
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public string District { get; set; }

    }
}
