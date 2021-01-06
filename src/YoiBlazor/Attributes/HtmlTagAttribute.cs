using System;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the component class could generate the HTML tag with specify name.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class HtmlTagAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTagAttribute"/> class by 'div' tag.
        /// </summary>
        public HtmlTagAttribute() : this("div")
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTagAttribute"/> class by specify tag name.
        /// </summary>
        /// <param name="tagName">Name of the HTML tag.</param>
        public HtmlTagAttribute(string tagName)
        {
            TagName = tagName;
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        public string TagName { get; }
    }
}
