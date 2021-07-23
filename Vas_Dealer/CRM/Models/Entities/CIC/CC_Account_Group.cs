namespace VAS.Dealer.Models.Entities.CIC
{
    public class CC_Account_Group
    {
        public int GroupId { get; set; }
        public int AccountId { get; set; }
        public virtual CC_Group CC_Group { get; set; }
    }
}