using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// 表示用于快速构造 <see cref="CssClass"/> 的样式表。
    /// </summary>
    public class Css
    {
        private readonly List<string> _cssBuilder = new List<string>();

        /// <summary>
        /// 创建 <see cref="Css"/> 链表。
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
        /// 添加指定 <see cref="CssClass"/> 实例。
        /// </summary>
        /// <param name="cssClass"><see cref="CssClass"/> 实例。</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cssClass"/> 是 <c>null</c>。</exception>
        public Css Add(CssClass cssClass)
        {
            if (cssClass is null)
            {
                throw new System.ArgumentNullException(nameof(cssClass));
            }

            return Add(true, cssClass);
        }

        /// <summary>
        /// 添加满足指定条件的 <see cref="CssClass"/> 实例。
        /// </summary>
        /// <param name="condition"><c>true</c> 表示条件满足。</param>
        /// <param name="cssClass"><see cref="CssClass"/> 实例。</param>
        public Css Add(bool condition, CssClass cssClass)
        {
            if (condition)
            {
                _cssBuilder.Add(cssClass.ToString());
            }
            return this;
        }

        /// <summary>
        /// 添加满足指定条件的 <see cref="CssClassCollection"/> 实例。
        /// </summary>
        /// <param name="condition"><c>true</c> 表示条件满足。</param>
        /// <param name="cssClasses"><see cref="CssClassCollection"/> 实例。</param>
        public Css Add(bool condition, CssClassCollection cssClasses)
        {
            if (cssClasses is null)
            {
                throw new System.ArgumentNullException(nameof(cssClasses));
            }

            return Add(condition, cssClasses.CssClasses.Select(m => m.ToString()));
        }

        /// <summary>
        /// 添加指定 <see cref="CssClassCollection"/> 实例。
        /// </summary>
        /// <param name="cssClasses"><see cref="CssClassCollection"/> 实例。</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cssClasses"/> 是 <c>null</c>。</exception>
        public Css Add(CssClassCollection cssClasses) => Add(true, cssClasses);

        /// <summary>
        /// 添加满足条件的 CSS 的字符串数组。
        /// </summary>
        /// <param name="condition"><c>true</c> 表示条件满足。</param>
        /// <param name="cssClasses">CSS 的字符串数组。</param>
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
        /// 添加指定的 CSS 字符串数组。
        /// </summary>
        /// <param name="cssClasses">CSS 的字符串数组。</param>
        public Css Add(IEnumerable<string> cssClasses) => Add(true, cssClasses);

        /// <summary>
        /// 获取所有的 css 名称。
        /// </summary>
        public IEnumerable<string> CssClasses => _cssBuilder;

        /// <summary>
        /// 转换成以空格分隔的字符串。
        /// </summary>
        public override string ToString()
        {
            var result= string.Join(" ", CssClasses.Distinct());
            _cssBuilder.Clear();
            return result;
        }

        /// <summary>
        /// 执行从 <see cref="Css"/> 到 <see cref="CssClassCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="css"><see cref=" Css"/> 实例。</param>
        public static implicit operator CssClassCollection(Css css)
            =>new CssClassCollection(css.CssClasses.Select(m=>m.ToString()));
    }
}
