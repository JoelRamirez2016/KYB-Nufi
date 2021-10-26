namespace Nufi.kyb.v2.Models
{
	public class NufiRequest
	{
        public int code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public SATData[] data { get; set; }
	}
}
