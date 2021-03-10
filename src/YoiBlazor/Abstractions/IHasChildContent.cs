
using Microsoft.AspNetCore.Components;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the component could has the UI content of child.
    /// </summary>
    public interface IHasChildContent : IBlazorComponent
    {
        /// <summary>
        /// Sets the segment of UI content.
        /// </summary>
        RenderFragment ChildContent { get; set; }
    }

    /// <summary>
    /// Represents the component could has the UI content of child with type of <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="YoiBlazor.IBlazorComponent" />
    public interface IHasChildContent<TValue> : IBlazorComponent
    {
        /// <summary>
        /// Sets the segment of UI content by given <typeparamref name="TValue"/> type.
        /// </summary>
        RenderFragment<TValue> ChildContent { get; set; }
    }
}
