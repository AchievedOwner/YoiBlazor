using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the CSS class builder.
    /// </summary>
    public class Css
    {
        private readonly List<string> _cssBuilder = new List<string>();

        /// <summary>
        /// Gets the instance of <see cref="Css"/> class.
        /// </summary>
        public static Css Create => new Css();
            

        /// <summary>
        /// Prevents a default instance of the <see cref="Css"/> class from being created.
        /// </summary>
        private Css()
        {
            _cssBuilder = new List<string>();
        }

        /// <summary>
        /// Adds the specified <see cref="CssClass"/> instance.
        /// </summary>
        /// <param name="cssClass">The <see cref="CssClass"/> instance.</param>
        /// <exception cref="System.ArgumentNullException">cssClass</exception>
        public Css Add(CssClass cssClass)
        {
            if (cssClass is null)
            {
                throw new System.ArgumentNullException(nameof(cssClass));
            }

            return Add(true, cssClass);
        }

        /// <summary>
        /// Adds the specified <see cref="CssClass"/> that satisfied by specify condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> to add the <see cref="CssClass"/> instance.</param>
        /// <param name="cssClass">The <see cref="CssClass"/> instance.</param>
        public Css Add(bool condition, CssClass cssClass)
        {
            if (condition)
            {
                _cssBuilder.Add(cssClass.ToString());
            }
            return this;
        }

        /// <summary>
        /// Adds the specified <see cref="CssClassCollection"/> that satisfied by specify condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> to add the <see cref="CssClassCollection"/> instance.</param>
        /// <param name="cssClasses">The <see cref="CssClassCollection"/> instance.</param>
        public Css Add(bool condition, CssClassCollection cssClasses)
        {
            if (cssClasses is null)
            {
                throw new System.ArgumentNullException(nameof(cssClasses));
            }

            return Add(condition, cssClasses.CssClasses.Select(m => m.ToString()));
        }

        /// <summary>
        /// Adds the specified <see cref="CssClassCollection"/>.
        /// </summary>
        /// <param name="cssClasses">The <see cref="CssClassCollection"/> instance.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cssClasses"/> 是 <c>null</c>。</exception>
        public Css Add(CssClassCollection cssClasses) => Add(true, cssClasses);

        /// <summary>
        /// Adds the specified CSS classes that satisfied by specify condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> to add the css classes.</param>
        /// <param name="cssClasses">The css classes.</param>
        public Css Add(bool condition,IEnumerable<string> cssClasses)
        {
            if (cssClasses is null)
            {
                throw new System.ArgumentNullException(nameof(cssClasses));
            }

            foreach (var item in cssClasses)
            {
                Add(condition, item);
            }
            return this;
        }

        /// <summary>
        /// Adds the specified CSS classes.
        /// </summary>
        /// <param name="cssClasses">The css classes.</param>
        public Css Add(IEnumerable<string> cssClasses) => Add(true, cssClasses);

        /// <summary>
        /// Gets the CSS classes.
        /// </summary>
        public IEnumerable<string> CssClasses => _cssBuilder;

        /// <summary>
        /// Converts to string seperated by space for each items.
        /// </summary>
        public override string ToString()
        {
            var result= string.Join(" ", CssClasses.Distinct());
            _cssBuilder.Clear();
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Css"/> to <see cref="CssClassCollection"/>.
        /// </summary>
        /// <param name="css">The CSS.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator CssClassCollection(Css css)
            =>new CssClassCollection(css.CssClasses.Select(m=>m.ToString()));
    }
}
