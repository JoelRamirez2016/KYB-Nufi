namespace Nufi.kyb.v2.Models
{
	public class AntecedentesPMNRequest
	{
        public int code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public AntecedentesPMN[] data { get; set; }
	}
}
