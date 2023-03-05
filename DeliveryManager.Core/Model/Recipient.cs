namespace DeliveryManager.Core.Model
{
    public class Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Package>? Packages { get; set; }
    }
}
