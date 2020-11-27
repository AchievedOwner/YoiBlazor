using System;

namespace YoiBlazor
{
    /// <summary>
    /// 表示当参数的布尔值为 <c>false</c> 需要应用的 CSS 类名称。
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class BooleanCssClassAttribute : CssClassAttribute
    {
        /// <summary>
        /// 初始化 <see cref="BooleanCssClassAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="trueCssClass">当参数的值是 <c>true</c> 时应用的 CSS 名称。</param>
        /// <param name="falseCssClass">当参数的值是 <c>false</c> 时应用的 CSS 名称。</param>
        public BooleanCssClassAttribute(string trueCssClass,string falseCssClass):base(trueCssClass)
        {
            FaleCssClass = falseCssClass;
        }

        /// <summary>
        /// 获取参数是 <c>false</c> 时的 CSS 类名称。
        /// </summary>
        public string FaleCssClass { get; }
    }
}
