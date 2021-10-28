using System.Web;

namespace Nufi.kyb.v2.Models
{
	public class IndexPage : Page
	{
		public IndexPage(FieldForm[] fields) : base(null)
		{
			Fields = fields;
		}
		public FieldForm[] Fields { get; set; }
	}
}
