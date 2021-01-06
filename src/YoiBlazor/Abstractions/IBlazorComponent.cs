using Microsoft.AspNetCore.Components;

namespace YoiBlazor
{
    /// <summary>
    /// Repreesents the blazor component.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.IComponent" />
    public interface IBlazorComponent : IComponent
    {
        /// <summary>
        /// Builds the CSS class string.
        /// </summary>
        /// <returns>The string of CSS class seperated by space for each of items.</returns>
        string BuildCssClassString();
        /// <summary>
        /// Builds the styles string.
        /// </summary>
        /// <returns>The string of styles seperated by semicolon(;) for each of items.</returns>
        string BuildStylesString();
    }
}
