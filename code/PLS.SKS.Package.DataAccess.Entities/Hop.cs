namespace PLS.SKS.Package.DataAccess.Entities
{
    public abstract class Hop
    {
	    protected Hop() { }

	    protected Hop(string code, decimal duration)
        {
            Code = code;
            Duration = duration;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Duration { get; set; }
    }
}
