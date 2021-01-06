using System;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the CSS class name.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DefaultValueAttribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class CssClassAttribute : System.ComponentModel.DefaultValueAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssClassAttribute"/> class.
        /// </summary>
        public CssClassAttribute() : this(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CssClassAttribute"/> class by specified CSS class name.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        public CssClassAttribute(string cssClass) : base(cssClass)
        {
        }

        /// <summary>
        /// Gets or sets the order of the css name that sort from small to large while loading.
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CssClassAttribute"/> is suffix.
        /// </summary>
        /// <value>
        ///   <c>true</c> if suffix; otherwise, <c>false</c>.
        /// </value>
        public bool Suffix { get; set; }
    }
}
