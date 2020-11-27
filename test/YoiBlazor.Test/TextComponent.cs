using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Components;

namespace YoiBlazor.Test
{
    [HtmlTag]
    public class TestComponent : BlazorComponentBase
    {
        [Parameter] [CssClass("m-")] public int? Margin { get; set; }

        [Parameter] [BooleanCssClass("text","text-0")] public bool? Text { get; set; }

        [Parameter][CssClass] public Color Color { get; set; }
        [Parameter][CssClass("bg-")] public Color BgColor { get; set; }

        [Parameter] [Style("max-height")] public int? MaxHeight { get; set; }
        [Parameter] [CssClass("text-")] public CssClass TextColor { get; set; }

        [Parameter] [CssClass("rounded-")] public CssClassCollection RounedStyles { get; set; }

    }

    public class Color : CssClass
    {
        protected Color(string name) : base(name)
        {
        }
        public static readonly Color Primary = new Color("primary");
    }

    public class BgColor : Color
    {
        protected BgColor(string name) : base(name)
        {
        }
    }

    public enum Size
    {
        [CssClass("sm")]SM,
        [CssClass("lg")]LG
    }
}
