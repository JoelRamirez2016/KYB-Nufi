using System.Web;

namespace Nufi.kyb.v2.Models
{
	public class IndexPage
	{
		public IndexPage(FieldForm[] fields)
		{
			Fields = fields;
		}
		public FieldForm[] Fields { get; set; }
	}
}
