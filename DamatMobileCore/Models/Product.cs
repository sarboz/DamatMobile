namespace DamatMobile.Core.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string DiscountValue { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public string DetailText { get; set; }
    }
}