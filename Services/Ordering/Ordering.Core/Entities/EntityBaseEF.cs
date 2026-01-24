namespace Ordering.Core.Entities
{
    public abstract class EntityBaseEF
    {
        //use this in derived class
        // public int Id { get; protected set; } //EF Use Case
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
