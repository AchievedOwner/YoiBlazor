using System;
using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// 表示用于快速构造样式表。
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
        /// 构造新的样式表。
        /// </summary>
        public static Style Create => new Style();

        /// <summary>
        /// 添加指定样式名称的值。
        /// </summary>
        /// <param name="name">样式名称。</param>
        /// <param name="value">样式的值。</param>
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
        /// 添加满足指定条件的样式名称的值。
        /// </summary>
        /// <param name="condition"><c>true</c> 表示满足条件。</param>
        /// <param name="name">样式名称。</param>
        /// <param name="value">样式的值。</param>
        /// <returns></returns>
        public Style Add(bool condition,string name,string value)
        {
            if (condition)
            {
                Add(name, value);
            }
            return this;
        }

        /// <summary>
        /// 添加满足指定条件的 <see cref="StyleCollection"/> 实例。
        /// </summary>
        /// <param name="condition"><c>true</c> 表示满足条件。</param>
        /// <param name="styles"><see cref="StyleCollection"/> 实例。</param>
        /// <exception cref="ArgumentNullException"><paramref name="styles"/> 是 <c>null</c>。</exception>
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
        /// 添加指定 <see cref="StyleCollection"/> 实例。
        /// </summary>
        /// <param name="styles"><see cref="StyleCollection"/> 实例。</param>
        public Style Add(StyleCollection styles)
            => Add(true, styles);

        /// <summary>
        /// 获取所有的样式表。
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Styles => _styleBuilder;

        /// <summary>
        /// 执行 <see cref="Style"/> 到 <see cref="StyleCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="style"><see cref="Style"/> 实例。</param>
        public static implicit operator StyleCollection(Style style)
        {
            return new StyleCollection(style.Styles);
        }

        /// <summary>
        /// 转换成 style 构成的样式字符串。
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
