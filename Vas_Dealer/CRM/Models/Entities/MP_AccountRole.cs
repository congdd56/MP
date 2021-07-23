namespace VAS.Dealer.Models.Entities
{
    public class MP_AccountRole
    {
        public int AccId { get; set; }
        public int RoleId { get; set; }
        public bool Manager { get; set; }
        public virtual MP_Account Account { get; set; }
        public virtual MP_Role Role { get; set; }
    }
}
