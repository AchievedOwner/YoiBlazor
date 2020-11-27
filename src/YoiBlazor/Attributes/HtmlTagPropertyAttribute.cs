using System;
using System.ComponentModel;

namespace YoiBlazor
{
    /// <summary>
    /// 定义参数在渲染时的元素属性名称并将参数的值作为元素属性的值。
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class HtmlTagPropertyAttribute : DefaultValueAttribute
    {
        /// <summary>
        /// 使用当前参数的名称作为元素属性的名称初始化 <see cref="HtmlTagPropertyAttribute"/> 类的新实例。
        /// </summary>
        public HtmlTagPropertyAttribute() : this(null)
        {

        }
        /// <summary>
        /// 使用指定名称作为元素属性的名称初始化 <see cref="HtmlTagPropertyAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="name">元素属性的名称。</param>
        public HtmlTagPropertyAttribute(string name) : base(name)
        {

        }
    }
}
