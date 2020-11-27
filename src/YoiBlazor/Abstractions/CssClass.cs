using System;
using System.Net.Http.Headers;

namespace YoiBlazor
{

    /// <summary>
    /// 表示一种具备 <c>class</c> 类样式名称的类型。
    /// </summary>
    public class CssClass
    {
        /// <summary>
        /// 初始化 <see cref="CssClass"/> 类的新实例。
        /// </summary>
        protected CssClass()
        {

        }

        /// <summary>
        /// 初始化 <see cref="CssClass"/> 类的新实例。
        /// </summary>
        /// <param name="cssClass">类样式。支持 <see cref="String"/>、<see cref="Enum"/> 或 <see cref="CssClass"/> 类型。</param>
        public CssClass(object cssClass)
        {
            Name = cssClass;
        }

        /// <summary>
        /// 获取类样式名称。
        /// </summary>
        public object Name { get; }

        /// <summary>
        /// 根据不同类型的 <see cref="Name"/> 转换成相应的字符串。
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
        /// 执行从 <see cref="System.String"/> 类型到 <see cref="CssClass"/> 的隐式转换。
        /// </summary>
        /// <param name="name">类样式名称。</param>
        /// <returns>
        /// 转换的结果。
        /// </returns>
        public static implicit operator CssClass(string name)
            =>new CssClass(name);

        /// <summary>
        /// 执行从 <see cref="Enum"/> 类型到 <see cref="CssClass"/> 的隐式转换。
        /// </summary>
        /// <param name="item">枚举项。</param>
        /// <returns>
        /// 转换的结果。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> 是 null。</exception>
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
