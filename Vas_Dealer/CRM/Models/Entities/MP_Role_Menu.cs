namespace VAS.Dealer.Models.Entities
{
    public class MP_Role_Menu
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public virtual MP_Menu Menu { get; set; }
        public virtual MP_Role Role { get; set; }
    }
}
