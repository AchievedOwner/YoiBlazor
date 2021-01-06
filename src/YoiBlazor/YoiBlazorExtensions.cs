using System;
using System.ComponentModel;
using System.Reflection;

namespace YoiBlazor
{
    /// <summary>
    /// The extensions of YoiBlazor.
    /// </summary>
    public static class YoiBlazorExtensions
    {
        /// <summary>
        /// Gets the enum member value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="enumeration">The enumeration.</param>
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
        /// Gets the <see cref="CssClassAttribute"/> value that defines in items of enum.
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns></returns>
        public static string GetEnumCssClass(this object enumeration)
			=> GetEnumMemberValue<CssClassAttribute>(enumeration)?.ToString();
	}
}
