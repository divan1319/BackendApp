namespace BackendApp.DTOs
{
    public class CreateBeerDto
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public decimal Alcohol { get; set; }
    }
}
