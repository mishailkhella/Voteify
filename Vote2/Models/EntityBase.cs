namespace Vote2.Models
{
    public class EntityBase
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set;}
        public bool Cancelled { get; set; }
    }
}
