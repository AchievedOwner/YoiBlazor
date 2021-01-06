using System;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the enumeration type of css class.
    /// </summary>
    public class CssClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssClass"/> class.
        /// </summary>
        protected CssClass()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CssClass"/> class.
        /// </summary>
        /// <param name="cssClass">The CSS class that only support the instance of <see cref="CssClass"/>, <see cref="CssClassCollection"/></param>
        public CssClass(object cssClass)
        {
            Name = cssClass;
        }

        /// <summary>
        /// Gets the name of css class.
        /// </summary>
        public object Name { get; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Name switch
        {
            Enum _ => Name.GetEnumCssClass(),
            _ => Name?.ToString(),
        };

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="CssClass"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator CssClass(string name)
            =>new CssClass(name);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Enum"/> to <see cref="CssClass"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">item</exception>
        public static implicit operator CssClass(Enum item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new CssClass(item);
        }
    }
}
