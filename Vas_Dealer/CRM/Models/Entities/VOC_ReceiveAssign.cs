using System;

namespace VAS.Dealer.Models.Entities
{
    public class VOC_ReceiveAssign
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime LastAssignDate { get; set; }
        public int AssignConter { get; set; }
    }

    public class VOC_StationAssign
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime LastAssignDate { get; set; }
        public int AssignConter { get; set; }
    }
}
