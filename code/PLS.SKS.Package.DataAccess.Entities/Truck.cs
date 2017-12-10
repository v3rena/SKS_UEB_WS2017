namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Truck : Hop
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

        public string NumberPlate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Radius { get; set; }
	}
}
