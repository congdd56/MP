namespace VAS.Dealer.Models.Entities
{
    public class MP_Account_Department
    {
        public int AccountId { get; set; }
        public int DepartmentId { get; set; }
        public virtual MP_Account Account { get; set; }
        public virtual MP_Department Department { get; set; }
    }
}
