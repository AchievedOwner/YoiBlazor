
using Microsoft.AspNetCore.Components;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the base class for blazor component that has segment of UI content of child.
    /// </summary>
    /// <seealso cref="YoiBlazor.BlazorComponentBase" />
    /// <seealso cref="YoiBlazor.IHasChildContent" />
    public abstract class BlazorChildContentComponentBase : BlazorComponentBase, IHasChildContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlazorChildContentComponentBase"/> class.
        /// </summary>
        protected BlazorChildContentComponentBase()
        {
        }

        /// <summary>
        /// Sets the segment of UI content.
        /// </summary>
        [Parameter]public RenderFragment ChildContent { get; set; }
    }
}
