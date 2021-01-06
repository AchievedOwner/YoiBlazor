using System;
using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// Repersents the style builder.
    /// </summary>
    public sealed class Style
    {
        private readonly Dictionary<string, string> _styleBuilder;
        /// <summary>
        /// Prevents a default instance of the <see cref="Style"/> class from being created.
        /// </summary>
        private Style()
        {
            _styleBuilder = new Dictionary<string, string>();
        }
        /// <summary>
        /// Gets the create.
        /// </summary>
        /// <value>
        /// The create.
        /// </value>
        public static Style Create => new Style();

        /// <summary>
        /// Adds the style by specified name and value.
        /// </summary>
        /// <param name="name">The name of style.</param>
        /// <param name="value">The value of the name.</param>
        public Style Add(string name,string value)
        {
            if (!_styleBuilder.ContainsKey(name))
            {
                _styleBuilder.Add(name, value);
            }
            else
            {
                _styleBuilder[name] = value;
            }
            return this;
        }

        /// <summary>
        /// Adds the style by specified name and value that satisfied given condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="name">The name of style.</param>
        /// <param name="value">The value of the name.</param>
        public Style Add(bool condition,string name,string value)
        {
            if (condition)
            {
                Add(name, value);
            }
            return this;
        }

        /// <summary>
        /// Adds the style by specified <see cref="StyleCollection"/> instance that satisfied condition.
        /// </summary>
        /// <param name="condition">The condition should be satisfiled.</param>
        /// <param name="styles">The instance of <see cref="StyleCollection"/> class.</param>
        public Style Add(bool condition,StyleCollection styles)
        {
            if (styles is null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            foreach (var item in styles.Styles)
            {
                Add(condition, item.Key, item.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds the style by specified <see cref="StyleCollection"/> instance.
        /// </summary>
        /// <param name="styles">The instance of <see cref="StyleCollection"/> class.</param>
        public Style Add(StyleCollection styles)
            => Add(true, styles);

        /// <summary>
        /// Gets the styles.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Styles => _styleBuilder;
        /// <summary>
        /// Performs an implicit conversion from <see cref="Style"/> to <see cref="StyleCollection"/>.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator StyleCollection(Style style) => new StyleCollection(style.Styles);
        /// <summary>
        /// Converts to string seperated by semicolon for each item.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var result= string.Join(";", Styles.Select(m => $"{m.Key}:{m.Value}"));
            _styleBuilder.Clear();
            return result;
        }
    }
}
