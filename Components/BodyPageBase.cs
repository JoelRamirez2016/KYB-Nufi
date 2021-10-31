using Microsoft.AspNetCore.Components;

using Nufi.kyb.v2.Models;


namespace Nufi.kyb.v2.Components
{
    public class BodyPageBase: ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public SuperSeccion[] SuperSections { get; set; }

        public string createId(string a, string b)
        {
            return a + b;
        }
    }

}

