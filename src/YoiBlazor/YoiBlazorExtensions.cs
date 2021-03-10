using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

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



        /// <summary>
        /// Adds the CSS class attribute.
        /// </summary>
        /// <param name="component">The instance of <see cref="IBlazorComponent"/>.</param>
        /// <param name="builder">The UI tree builder.</param>
        /// <param name="sequence"> An integer that represents the position of the instruction in the source code.</param>
        public static void AddCssClassAttribute(this IBlazorComponent component, RenderTreeBuilder builder, int sequence = 999990)
        {
            var cssClass = component.BuildCssClassString();
            if (!string.IsNullOrEmpty(cssClass))
            {
                builder.AddAttribute(sequence, "class", cssClass);
            }
        }

        /// <summary>
        /// Adds the style attribute.
        /// </summary>
        /// <param name="component">The instance of <see cref="IBlazorComponent"/>.</param>
        /// <param name="builder">The UI tree builder.</param>
        /// <param name="sequence"> An integer that represents the position of the instruction in the source code.</param>
        public static void AddStyleAttribute(this IBlazorComponent component, RenderTreeBuilder builder, int sequence = 999991)
        {
            var styles = component.BuildStylesString();
            if (!string.IsNullOrEmpty(styles))
            {
                builder.AddAttribute(sequence, "style", styles);
            }
        }

        /// <summary>
        /// Add the <see cref="CascadingValue{TValue}"/> component with specified <typeparamref name="TValue"/> type in <see cref="RenderTreeBuilder"/> instance.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="value">The value of casecading parameter.</param>
        /// <param name="content">The UI content within casecading value.</param>
        /// <param name="name">The name of casecading parameter.</param>
        /// <param name="isFixed">If true, indicates that cascading value will not change.</param>
        /// <param name="sequence">An integer that represents the position of the instruction in the source code..</param>
        public static void BuildCascadingValueComponent<TValue>(this RenderTreeBuilder builder, object value, RenderFragment content, string name = default, bool isFixed = default, int sequence = 1000)
        {
            builder.OpenRegion(sequence);
            builder.OpenComponent<CascadingValue<TValue>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<TValue>.Value), value);
            builder.AddAttribute(2, nameof(CascadingValue<TValue>.ChildContent), content);
            builder.AddAttribute(3, nameof(CascadingValue<TValue>.Name), name);
            builder.AddAttribute(4, nameof(CascadingValue<TValue>.IsFixed), isFixed);
            builder.CloseComponent();
            builder.CloseRegion();
        }
    }
}
