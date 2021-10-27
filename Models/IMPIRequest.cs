namespace Nufi.kyb.v2.Models
{
	public class IMPIRequest
	{
        public int code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public IMPIData[] data { get; set; }
	}
}
