namespace ValidKitchenControl.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string Area { get; set; }
    }
}
