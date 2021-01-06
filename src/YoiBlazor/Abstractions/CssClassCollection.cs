using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// Represents a collection of CSS class string.
    /// </summary>
    public class CssClassCollection : IReadOnlyList<CssClass>, IReadOnlyCollection<CssClass>, IEnumerable<string>
    {
        private readonly List<CssClass> cssClassList = new List<CssClass>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CssClassCollection"/> class.
        /// </summary>
        /// <param name="cssClass">A group of <see cref="CssClass"/> class.</param>
        public CssClassCollection(IEnumerable<object> cssClass)
        {
            foreach (var item in cssClass)
            {
                cssClassList.Add(new CssClass(item));
            }
        }

        /// <summary>
        /// Gets the <see cref="CssClass"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="CssClass"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The instance of <see cref="CssClass"/>.</returns>
        public CssClass this[int index]
        {
            get
            {
                return cssClassList[index];
            }
        }

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public int Count => cssClassList.Count;


        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Converts to string seperated by space of each item.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var result= string.Join(" ", cssClassList.Select(m => m.ToString()));
            cssClassList.Clear();
            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
             return cssClassList.Select(m => m.ToString()).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<CssClass> GetEnumerator()
        {
            return cssClassList.GetEnumerator();
        }
        /// <summary>
        /// Gets the CSS classes.
        /// </summary>
        /// <value>
        /// The CSS classes.
        /// </value>
        public IEnumerable<CssClass> CssClasses => cssClassList;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        public void Add(CssClass item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            cssClassList.Add(item);
        }

        /// <summary>
        /// Adds the css class by specified condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="item">The item.</param>
        public void Add(bool condition, CssClass item)
        {
            if (condition)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Object[]"/> to <see cref="CssClassCollection"/>.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">cssClass</exception>
        public static implicit operator CssClassCollection(object[] cssClass)
        {
            if (cssClass is null)
            {
                throw new ArgumentNullException(nameof(cssClass));
            }

            return new CssClassCollection(cssClass);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="CssClassCollection"/>.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator CssClassCollection(string cssClass)
        {
            return new CssClassCollection(cssClass.Split(' '));
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CssClass[]"/> to <see cref="CssClassCollection"/>.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">cssClass</exception>
        public static implicit operator CssClassCollection(CssClass[] cssClass)
        {
            if (cssClass is null)
            {
                throw new ArgumentNullException(nameof(cssClass));
            }

            return new CssClassCollection(cssClass);
        }
        /// <summary>
        /// Performs an implicit conversion from <see cref="CssClass"/> to <see cref="CssClassCollection"/>.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">cssClass</exception>
        public static implicit operator CssClassCollection(CssClass cssClass)
        {
            if (cssClass is null)
            {
                throw new ArgumentNullException(nameof(cssClass));
            }

            return new CssClassCollection(new[] { cssClass });
        }
    }
}
