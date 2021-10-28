using Microsoft.AspNetCore.Components;

namespace Nufi.kyb.v2.Components
{
	public class TabsBase : ComponentBase
	{
		[Parameter]
		public int tabSelected { get; set; }
	}
}
