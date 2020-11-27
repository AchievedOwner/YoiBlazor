using System;

namespace YoiBlazor
{
    /// <summary>
    /// 表示当参数是 null 时需要应用的指定样式。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class NullCssClassAttribute : CssClassAttribute
    {
        /// <summary>
        /// 初始化 <see cref="NullCssClassAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="cssClass">CSS 类名称。</param>
        public NullCssClassAttribute(string cssClass):base(cssClass)
        {
        }
    }
}
