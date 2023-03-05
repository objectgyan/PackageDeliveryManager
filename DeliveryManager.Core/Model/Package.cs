namespace DeliveryManager.Core.Model
{
    public class Package
    {
        public int Id { get; set; }
        public string PackageIdentifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? RecipientId { get; set; }
        public DateTime LastUpdated { get; set; }

        public Recipient? Recipient { get; set; }

        public string? CreatedBy { get; set; }
    }
}
