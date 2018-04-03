namespace Synchronized.Model
{
    public class UserTag
    {
        public string UserId { get; set; }     
        public string TagId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Tag Tag { get; set; }
    }
}