
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

namespace YoiBlazor.BlazorServer.Components
{
    [HtmlTag("button")]
    [CssClass("btn")]
    public class Button : BlazorComponentBase, IHasChildContent
    {
        [Parameter]
        [CssClass("btn-")]
        public Color Color { get; set; } = Color.Primary;
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter]
        [HtmlTagProperty("title")]
        public string Name { get; set; }
    }


    public enum Color
    {
        [CssClass("primary")]
        Primary,
        [CssClass("secondary")]
        Secondary,
        [CssClass("info")]
        Info,
        [CssClass("warning")]
        Warning,
        [CssClass("success")]
        Success,
        [CssClass("danger")]
        Danger,
        [CssClass("light")]
        Light,
        [CssClass("dark")]
        Dark
    }
}
