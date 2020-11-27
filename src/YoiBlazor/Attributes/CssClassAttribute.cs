using System;

namespace YoiBlazor
{
    /// <summary>
    /// 定义组件的参数使用的 CSS 类名称。
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class CssClassAttribute : System.ComponentModel.DefaultValueAttribute
    {
        /// <summary>
        /// 初始化 <see cref="CssClassAttribute"/> 类的新实例。
        /// </summary>
        public CssClassAttribute() : this(string.Empty)
        {

        }

        /// <summary>
        /// 初始化 <see cref="CssClassAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="cssClass">Css 类名称。</param>
        public CssClassAttribute(string cssClass) : base(cssClass)
        {
        }

        /// <summary>
        /// 获取或设置 CSS 特性的加载顺序，数字越小排列越靠前。
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 设置 CSS 类名称作为值的后缀。
        /// </summary>
        public bool Suffix { get; set; }
    }
}
