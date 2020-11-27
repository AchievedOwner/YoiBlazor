using System;

namespace YoiBlazor
{
    /// <summary>
    /// 表示参数自动适应到 <c>style</c> 样式中。
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class StyleAttribute : Attribute
    {
        /// <summary>
        /// 初始化 <see cref="StyleAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="name">样式名称。</param>
        public StyleAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 获取样式名称。
        /// </summary>
        public string Name { get; }
    }
}
