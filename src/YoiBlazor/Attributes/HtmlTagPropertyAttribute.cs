using System;
using System.ComponentModel;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the attribute in HTML tag after render.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DefaultValueAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class HtmlTagPropertyAttribute : DefaultValueAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTagPropertyAttribute"/> class using the same name of property.
        /// </summary>
        public HtmlTagPropertyAttribute() : this(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTagPropertyAttribute"/> class by given name.
        /// </summary>
        /// <param name="name">The name of attribute in HTML tag.</param>
        public HtmlTagPropertyAttribute(string name) : base(name)
        {

        }
    }
}
