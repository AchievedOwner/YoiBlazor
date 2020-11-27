using System;
using System.ComponentModel;
using System.Reflection;

namespace YoiBlazor
{
    /// <summary>
    /// YoiBlazor 的扩展。
    /// </summary>
    public static class YoiBlazorExtensions
    {
        /// <summary>
        /// 获取指定枚举项定义的指定特性。
        /// </summary>
        /// <typeparam name="TAttribute">特性类型。</typeparam>
        /// <param name="enumeration">枚举项。</param>
        /// <returns></returns>
        public static object GetEnumMemberValue<TAttribute>(this object enumeration) where TAttribute:DefaultValueAttribute
        {
			if (!(enumeration is Enum))
			{
				return null;
			}

			var enumType = enumeration.GetType();
			var enumName = enumeration.ToString().ToLower();
			var fieldInfo = enumType.GetTypeInfo().GetDeclaredField(enumeration.ToString());

			if (fieldInfo == null)
			{
				return enumName;
			}
			var attr = fieldInfo.GetCustomAttribute<TAttribute>();
            if (attr == null)
            {
				return enumName;
            }
			return attr.Value;
		}

		/// <summary>
		/// 获取枚举项定义了 <see cref="CssClassAttribute"/> 特性的 CSS 名称。若未定义 <see cref="CssClassAttribute"/> 特性，则使用枚举项的名称。
		/// </summary>
		/// <param name="enumeration">枚举类型。</param>
		public static string GetEnumCssClass(this object enumeration)
			=> GetEnumMemberValue<CssClassAttribute>(enumeration)?.ToString();
	}
}
