namespace Domain
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ErfSize { get; set; }
        public string FloorSize { get; set; }
        public decimal Price { get; set; }
        public decimal Levies { get; set; }
        public decimal Rates { get; set; }
        public string Address { get; set; }
        public bool PetsAllowed { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public int KitchenCount { get; set; }
        public int Louge { get; set; }
        public int Dining { get; set; }
        public DateTime ListDate { get; set; }

        public List<Image> Images { get; set; }

    }
}