namespace Nufi.kyb.v2.Models
{
	public class FieldForm
	{
		public FieldForm (string label, string id)
		{
			this.label = label;
			this.id = id;
		}
		public string label { get; set; }
		public string id { get; set; }
	}
}
