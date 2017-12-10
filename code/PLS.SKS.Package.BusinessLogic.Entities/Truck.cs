namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Truck
    {
        public Truck() { }

        public Truck(string code, string numberPlate, decimal latitude, decimal longitude, decimal radius, decimal duration)
        {
            Code = code;
            NumberPlate = numberPlate;
            Latitude = latitude;
            Longitude = longitude;
            Radius = radius;
            Duration = duration;
        }

        public string Code { get; set; }
		public string NumberPlate { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public decimal Radius { get; set; }
		public decimal Duration { get; set; }
    }
}
