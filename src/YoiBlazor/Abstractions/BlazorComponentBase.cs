using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace YoiBlazor
{
    /// <summary>
    /// 表示 Blazor 组件的基类。这是一个抽象类。
    /// </summary>
    public abstract class BlazorComponentBase : ComponentBase, IBlazorComponent, IStateChangeHandler
    {
        /// <summary>
        /// 初始化 <see cref="BlazorComponentBase"/> 类的新实例。
        /// </summary>
        protected BlazorComponentBase()
        {
            _cssBuilder = Css.Create;
            _styleBuilder = Style.Create;
            AutoElementReferece = true;
        }

        private readonly Css _cssBuilder;
        private readonly Style _styleBuilder;

        #region 参数
        /// <summary>
        /// 设置在组件当前所有类样式的基础上进行追加的样式。
        /// </summary>
        [Parameter]public CssClassCollection AdditionalCssClass { get; set; }

        /// <summary>
        /// 设置组件的 class 类名称并覆盖组件的所有样式。
        /// </summary>
        [Parameter] public CssClassCollection CssClass { get; set; }

        /// <summary>
        /// 设置追加的样式，不会覆盖内置样式。
        /// </summary>
        [Parameter]public StyleCollection AdditionalStyles { get; set; }

        /// <summary>
        /// 设置自定义的内联样式，并覆盖其他的内置样式。
        /// </summary>
        [Parameter] public StyleCollection Styles { get; set; }

        /// <summary>
        /// 设置将该控件或元素中出现的属性进行合并。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

        #endregion

        /// <summary>
        /// 获取 <see cref="IJSRuntime"/> 的服务。
        /// </summary>
        [Inject] protected IJSRuntime JS { get; set; }

        private ElementReference _elementRef;
        /// <summary>
        /// 获取当前组件的元素引用。
        /// </summary>
        public ElementReference ElementRef
        {
            get => GetElementRef != null ? GetElementRef() : _elementRef;
            protected set => _elementRef = value;
        }

        private Func<ElementReference> GetElementRef { get; set; }

        /// <summary>
        /// 获取或设置是否自动加载元素的引用。
        /// </summary>
        protected bool AutoElementReferece { get; set; }

        /// <summary>
        /// 创建组件所需要的 class 类。
        /// </summary>
        /// <param name="css"><see cref="Css"/> 实例。</param>
        protected virtual void CreateComponentCssClass(Css css) { }

        /// <summary>
        /// 创建组件所需要的 style 样式。
        /// </summary>
        /// <param name="style"><see cref="Style"/> 实例。</param>
        protected virtual void CreateComponentStyle(Style style) { }

        /// <summary>
        /// 构造组件的 class 样式名称并用空格连接的字符串。
        /// </summary>
        /// <returns>用空格分割的样式字符串。</returns>
        public virtual string BuildCssClassString()
        {
            if (AdditionalAttributes.TryGetValue("class", out object cssClass))
            {
                return cssClass.ToString();
            }

            if (CssClass != null && CssClass.Count > 0)
            {
                return CssClass.ToString();
            }

            GetCssClassFromAttribute();

            CreateComponentCssClass(_cssBuilder);

            if (AdditionalCssClass!=null && AdditionalCssClass.Count > 0)
            {
                _cssBuilder.Add(AdditionalCssClass);
            }

            if (!_cssBuilder.CssClasses.Any())
            {
                return null;
            }
            return _cssBuilder.ToString();

        }

        /// <summary>
        /// 构造组件的 style 的值并用“;”连接。
        /// </summary>
        /// <returns>用分号隔开的 style 样式。</returns>
        public virtual string BuildStylesString()
        {
            if (AdditionalAttributes.TryGetValue("style", out object style))
            {
                return style.ToString();
            }
            if (Styles != null && Styles.Count > 0)
            {
                return Styles.ToString();
            }

            ResolveStyleAttribute();
            CreateComponentStyle(_styleBuilder);

            if (AdditionalStyles!=null && AdditionalStyles.Count>0)
            {
                _styleBuilder.Add(AdditionalStyles);
            }

            if (!_styleBuilder.Styles.Any())
            {
                return null;
            }
            return _styleBuilder.ToString();
        }
        #region 子类用的方法定义

        /// <summary>
        /// 添加在 <see cref="RenderTreeBuilder"/> 中构造元素的 class 属性。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">系列号。</param>
        protected virtual void AddCssClassAttribute(RenderTreeBuilder builder, int sequence = 999990)
        {
            var cssClass = BuildCssClassString();
            if (!string.IsNullOrEmpty(cssClass))
            {
                builder.AddAttribute(sequence, "class", cssClass);
            }
        }

        /// <summary>
        /// 添加在 <see cref="RenderTreeBuilder"/> 中构造元素的 style 属性。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">系列号。</param>
        protected virtual void AddStyleAttribute(RenderTreeBuilder builder, int sequence = 999991)
        {
            var styles = BuildStylesString();
            if (!string.IsNullOrEmpty(styles))
            {
                builder.AddAttribute(sequence, "style", styles);
            }
        }

        /// <summary>
        /// 添加在 <see cref="RenderTreeBuilder"/> 中构造元素未被明确定义的其他属性。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">系列号。</param>
        protected virtual void AddAddtionalAttributes(RenderTreeBuilder builder, int sequence = 99992)
        {
            builder.AddMultipleAttributes(sequence, AdditionalAttributes);
        }

        /// <summary>
        /// 添加在 <see cref="RenderTreeBuilder"/> 中对元素的引用。
        /// <para>
        /// <see cref="ElementRef"/> 在调用方法后可获取值。
        /// </para>
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">系列号。</param>
        protected virtual void AddElementReference(RenderTreeBuilder builder, int sequence = 99993) 
            => builder.AddElementReferenceCapture(sequence, el => _elementRef = el);

        /// <summary>
        /// 添加在 <see cref="RenderBatchBuilder"/> 中对组件的元素引用。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">系列号。</param>
        protected virtual void AddComponentReference(RenderTreeBuilder builder, int sequence = 99994)
        {
            builder.AddComponentReferenceCapture(sequence, component =>
            {
                if (component is BlazorComponentBase blazorComponent)
                {
                    GetElementRef = () => blazorComponent.ElementRef;
                }
            });
        }

        /// <summary>
        /// 添加构造 <see cref="RenderTreeBuilder"/> 的公共的属性，包括 class、style、id 和 AdditionalAttributes。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        protected virtual void AddCommonAttributes(RenderTreeBuilder builder)
        {
            AddCssClassAttribute(builder);
            AddStyleAttribute(builder);
            AddAddtionalAttributes(builder);
            AddExtraCommonAttributes(builder);
        }

        /// <summary>
        /// 添加通用的其他特性。会在 <see cref="AddCommonAttributes(RenderTreeBuilder)"/> 调用。
        /// <para>
        /// 可以通过重写该方法来添加额外的特性。
        /// </para>
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">序列号。</param>
        protected virtual void AddExtraCommonAttributes(RenderTreeBuilder builder, int sequence = 99500) { }

        /// <summary>
        /// 添加 <see cref="IHasChildContent.ChildContent"/> 的参数。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        /// <param name="sequence">序列号。</param>
        protected virtual void AddChildContent(RenderTreeBuilder builder,int sequence=100)
        {
            if(this is IHasChildContent childContent)
            {
                builder.AddContent(sequence, childContent.ChildContent);
            }
        }

        /// <summary>
        /// 添加标记了 <see cref="HtmlTagPropertyAttribute"/> 的属性。
        /// </summary>
        /// <param name="builder"><see cref="RenderTreeBuilder"/> 实例。</param>
        protected virtual void AddHtmlTagProperties(RenderTreeBuilder builder)
        {
            var properties = GetType().GetProperties().Where(m => m.CanRead);

            var index = 1000;
            foreach (var property in properties.Where(m=>m.IsDefined(typeof(HtmlTagPropertyAttribute))))
            {
                var elementAttr = property.GetCustomAttribute<HtmlTagPropertyAttribute>(true);
                if (elementAttr != null)
                {
                    var name = elementAttr.Value ?? property.Name.ToLower();
                    var value = property.GetValue(this);
                    if (value != null)
                    {
                        if (value.GetType().BaseType == typeof(Enum))
                        {
                            var elemebtTagAttrValue= value.GetEnumMemberValue<DefaultValueAttribute>();
                            value = elemebtTagAttrValue;

                        }
                        builder.AddAttribute(++index, name.ToString(), value);

                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Renders the component to the supplied <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" />.
        /// </summary>
        /// <param name="builder">A <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" /> that will receive the render output.</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            var componentType = GetType();

            var element = componentType.GetCustomAttribute<HtmlTagAttribute>(true);
            if (element != null)
            {
                builder.OpenElement(0, element.ElementName);
                AddCommonAttributes(builder);
                AddHtmlTagProperties(builder);
                AddChildContent(builder, 888);

                builder.CloseElement();
            }
        }

        /// <summary>
        /// 获取从接口中定义了 <see cref="CssClassAttribute"/> 特性的参数。
        /// </summary>
        /// <returns></returns>
        protected virtual void GetCssClassFromAttribute()
        {
            var cssClassDic = new Dictionary<string, int>();// class 名称, 顺序

            //获取接口
            var interfaces = this.GetType().GetInterfaces();

            //接口定义了 CssClassAttribute
            interfaces.Where(instance => instance.IsDefined(typeof(CssClassAttribute), true))
                .Select(m => m.GetCustomAttribute<CssClassAttribute>(true))
                .OrderBy(instance => instance.Order)
                .ToList()
                .ForEach(item =>
                {
                    TryAddCssClassDic(cssClassDic, item);
                });

            //类定义了 CssClassAttribute
            if (this.GetType().IsDefined(typeof(CssClassAttribute)))
            {
                var classCssClassAttribute = this.GetType().GetCustomAttribute<CssClassAttribute>();
                TryAddCssClassDic(cssClassDic, classCssClassAttribute);
            }


            //接口的属性
            var interfaceProperties = interfaces.SelectMany(type => type.GetProperties());

            // 类的属性有 CssClassAttribute
            var classProperties = this.GetType().GetProperties();

            var properties = new List<PropertyInfo>();

            foreach (var classProp in classProperties)
            {
                if (IdentifyCssClassProperty(classProp)) //类的属性定义了 CssClassAttribute 则忽略接口的 CssClassAttribute
                {
                    properties.Add(classProp);
                }
                else
                {
                    var interfaceProperty = interfaceProperties.LastOrDefault(m => m.Name == classProp.Name);
                    if (interfaceProperty != null)
                    {
                        properties.Add(interfaceProperty);
                    }
                }
            }

            ResolveCssClassProperties(properties, cssClassDic);

            //排序 去重
            var cssClassArray = cssClassDic.OrderBy(m => m.Value).Select(m => m.Key).Distinct();

            _cssBuilder.Add(cssClassArray.ToArray());
        }

        /// <summary>
        /// Resolves the property to CSS class dic.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="cssClassDic">The CSS class dic.</param>
        void ResolveCssClassProperties(IEnumerable<PropertyInfo> properties, Dictionary<string, int> cssClassDic)
        {
            foreach (var property in properties.Where(property=>IdentifyCssClassProperty(property)))
            {
                if (!property.CanRead) //必须要有 get
                {
                    return;
                }

                var value = property.GetValue(this);//属性的值

                if (!TryGetCssClassAttribute(property, out CssClassAttribute cssClassAttribute))
                {
                    return;
                }

                var cssClassName = cssClassAttribute.Value?.ToString();

                if (value == null) //若属性是 null，判断是否有 NullCssClassAttribute
                {
                    if (TryGetCssClassAttribute(property, out NullCssClassAttribute nullCssClassAttribute))
                    {
                        TryAddCssClassDic(cssClassDic, nullCssClassAttribute);
                    }
                }
                else // 属性有值
                {

                    if (value.GetType() == typeof(bool)) //布尔值，则会选择使用 CssClassAttrbute 或  BooleanCssClassAttribute
                    {
                        var boolValue = (bool)value;

                        //识别 BooleanCssClassAttibute
                        if (TryGetCssClassAttribute(property, out BooleanCssClassAttribute boolCssClassAttribute))
                        {
                            if (boolValue)
                            {
                                TryAddCssClassDic(cssClassDic, boolCssClassAttribute);
                            }
                            else
                            {
                                TryAddCssClassDic(cssClassDic, boolCssClassAttribute.FaleCssClass);
                            }
                        }
                        else if (boolValue) //兼容 CssClassAttribute 但只有 true 时有效
                        {
                            TryAddCssClassDic(cssClassDic, cssClassAttribute);
                        }
                    }
                    else // CssClassAttribute 将会连接属性的值一起组成新的 css 名称
                    {
                        // CssClassCollection 类型，结合 CssClassAttribute，并为每一个 CssClassCollection 的值都追加 CssClass 的值。
                        if (value.GetType() == typeof(CssClassCollection) || value.GetType().BaseType == typeof(CssClassCollection)) 
                        {
                            var cssCollection = value as CssClassCollection;
                            if (cssClassAttribute.Suffix)
                            {
                                cssClassName = string.Join(" ", cssCollection.CssClasses.Select(m=>$"{m}{cssClassName}"));
                            }
                            else
                            {
                                cssClassName = string.Join(" ", cssCollection.CssClasses.Select(m => $"{cssClassName}{m}"));
                            }
                        }
                        else
                        {
                            if (value.GetType().BaseType == typeof(Enum))//如果是枚举，要将枚举项和 CssClass 连起来使用
                            {
                                var enumFieldCssClass = value.GetEnumCssClass();
                                value = enumFieldCssClass;
                            }
                            else if (typeof(CssClass).IsAssignableFrom(value.GetType())) // 是 CssClass 类型
                            {
                                value = (value as CssClass)?.ToString();
                            }

                            if (cssClassAttribute.Suffix) //后缀属性的 CssClassAttrbute
                            {
                                cssClassName = $"{value}{cssClassName}";
                            }
                            else
                            {
                                cssClassName = $"{cssClassName}{value}";
                            }
                        }

                        TryAddCssClassDic(cssClassDic, cssClassName, cssClassAttribute.Order);
                    }
                }
            }
        }

        /// <summary>
        /// 尝试从反射的属性中获取指定的特性类型。
        /// </summary>
        /// <typeparam name="TAttribute">特性类型。</typeparam>
        /// <param name="property">属性对象。</param>
        /// <param name="attribute">返回获取到的特性类型。</param>
        /// <returns>若获取成功，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
        private static bool TryGetCssClassAttribute<TAttribute>(PropertyInfo property, out TAttribute attribute)
            where TAttribute : CssClassAttribute
        {
            attribute = default;
            if (!property.IsDefined(typeof(TAttribute)))
            {
                return false;
            }

            attribute = property.GetCustomAttribute<TAttribute>();

            return attribute != null;
        }

        /// <summary>
        /// Tries the add CSS class dic.
        /// </summary>
        /// <param name="cssClassDic">The CSS class dic.</param>
        /// <param name="cssClassAttribute">The CSS class attribute.</param>
        /// <returns></returns>
        private static bool TryAddCssClassDic(Dictionary<string, int> cssClassDic, CssClassAttribute cssClassAttribute)
        {
            if (cssClassAttribute == null)
            {
                return false;
            }

            var name = cssClassAttribute.Value?.ToString();
            var order = cssClassAttribute.Order;

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return TryAddCssClassDic(cssClassDic, name, order);
        }

        /// <summary>
        /// Tries the add CSS class dic.
        /// </summary>
        /// <param name="cssClassDic">The CSS class dic.</param>
        /// <param name="name">The name.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        private static bool TryAddCssClassDic(Dictionary<string, int> cssClassDic, string name, int order = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (cssClassDic.ContainsKey(name))
            {

                cssClassDic[name] = order;
            }
            else
            {
                cssClassDic.Add(name, order);
            }
            return true;
        }

        /// <summary>
        /// 解析具有 <see cref="StyleAttribute"/> 特性的参数。
        /// </summary>
        protected virtual void ResolveStyleAttribute()
        {
            GetType().GetProperties()
                .Where(m => m.IsDefined(typeof(StyleAttribute), true))
                .ToList()
                .ForEach(property=>
                {
                    if (!property.CanRead)
                    {
                        return;
                    }
                    var styleAttribute= property.GetCustomAttribute<StyleAttribute>();
                    if (styleAttribute != null)
                    {
                        var value = property.GetValue(this);
                        if (value != null)
                        {
                            _styleBuilder.Add(styleAttribute.Name, value.ToString());
                        }
                    }
                });
        }

        /// <summary>
        /// 识别属性是内部支持的 CSS 属性。
        /// </summary>
        /// <param name="property">要识别的属性。</param>
        /// <returns></returns>
        private bool IdentifyCssClassProperty(PropertyInfo property)
        {
            return property.IsDefined(typeof(CssClassAttribute), true);
        }


        /// <summary>
        /// 通知组件其状态已更改。在适用的情况下，这将导致组件被重新呈现。
        /// </summary>
        public virtual void NotifyStateChanged() => StateHasChanged();

        
    }
}
