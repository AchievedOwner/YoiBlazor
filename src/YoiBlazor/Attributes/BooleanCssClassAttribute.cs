using System;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the value of boolean argument which should apply the CSS name.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class BooleanCssClassAttribute : CssClassAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanCssClassAttribute"/> class.
        /// </summary>
        /// <param name="trueCssClass">The value is <c>true</c> to apply the name of CSS class.</param>
        /// <param name="falseCssClass">The value is <c>true</c> to apply the name of CSS class.</param>
        public BooleanCssClassAttribute(string trueCssClass, string falseCssClass) : base(trueCssClass)
        {
            FalseCssClass = falseCssClass;
        }
        /// <summary>
        /// Gets the CSS class name while value is <c>false</c>.
        /// </summary>
        public string FalseCssClass { get; }
    }
}
