
using Microsoft.AspNetCore.Components;

namespace YoiBlazor
{
    /// <summary>
    /// 表示自定义子内容的组件基类。
    /// </summary>
    public abstract class BlazorChildContentComponentBase : BlazorComponentBase, IHasChildContent
    {
        /// <summary>
        /// 设置组件的一段 UI 内容。
        /// </summary>
        [Parameter]public RenderFragment ChildContent { get; set; }
    }
}
