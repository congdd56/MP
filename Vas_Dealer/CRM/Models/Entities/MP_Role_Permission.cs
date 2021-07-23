namespace VAS.Dealer.Models.Entities
{
    public class MP_Role_Permission
    {
        public int IdRole { get; set; }
        public int IdPermission { get; set; }
        public virtual MP_Role Role { get; set; }
        public virtual MP_Permission Permission { get; set; }
    }
}
