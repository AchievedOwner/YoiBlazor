using System;

namespace YoiBlazor
{
    /// <summary>
    /// 定义组件构造为基本的内容型的元素。例如 div span。
    /// 必须实现 <see cref="IHasChildContent"/> 接口。
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class HtmlTagAttribute : Attribute
    {
        /// <summary>
        /// 使用 div 作为元素的名称初始化 <see cref="HtmlTagAttribute"/> 类的新实例。
        /// </summary>
        public HtmlTagAttribute() : this("div")
        {

        }

        /// <summary>
        /// 使用 html 的元素名称初始化 <see cref="HtmlTagAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="elementName">元素的名称。</param>
        public HtmlTagAttribute(string elementName)
        {
            ElementName = elementName;
        }

        /// <summary>
        /// 获取元素的名称。
        /// </summary>
        public string ElementName { get; }
    }
}
