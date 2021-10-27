namespace Nufi.kyb.v2.Models
{
	public class SATRequest
	{
        public int code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public SATData[] data { get; set; }
	}
}
