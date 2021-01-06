using System;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the name of style that could apply after render.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class StyleAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of style.</param>
        public StyleAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of style.
        /// </summary>
        public string Name { get; }
    }
}
