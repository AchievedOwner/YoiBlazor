using System;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the CSS class name while value is <c>null</c>.
    /// </summary>
    /// <seealso cref="YoiBlazor.CssClassAttribute" />
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class NullCssClassAttribute : CssClassAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCssClassAttribute"/> class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        public NullCssClassAttribute(string cssClass):base(cssClass)
        {
        }
    }
}
