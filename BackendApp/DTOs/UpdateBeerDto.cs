﻿namespace BackendApp.DTOs
{
    public class UpdateBeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public decimal Alcohol { get; set; }
    }
}
