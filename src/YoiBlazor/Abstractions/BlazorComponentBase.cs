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
    /// Represents the base class for blazor component.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <seealso cref="YoiBlazor.IBlazorComponent" />
    /// <seealso cref="YoiBlazor.IStateChangeHandler" />
    public abstract class BlazorComponentBase : ComponentBase, IBlazorComponent, IStateChangeHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlazorComponentBase"/> class.
        /// </summary>
        protected BlazorComponentBase()
        {
            _cssBuilder = Css.Create;
            _styleBuilder = Style.Create;
            AutoElementReferece = true;
        }

        private readonly Css _cssBuilder;
        private readonly Style _styleBuilder;

        #region Prameters        
        /// <summary>
        /// Gets or sets the additional CSS class.
        /// </summary>
        [Parameter]public CssClassCollection AdditionalCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [Parameter] public CssClassCollection CssClass { get; set; }

        /// <summary>
        /// Gets or sets the additional styles.
        /// </summary>
        [Parameter]public StyleCollection AdditionalStyles { get; set; }

        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        [Parameter] public StyleCollection Styles { get; set; }

        /// <summary>
        /// Gets or sets the additional attributes.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

        #endregion

        /// <summary>
        /// Gets or sets the instance of <see cref="IJSRuntime"/>.
        /// </summary>
        [Inject] protected IJSRuntime JS { get; set; }

        private ElementReference _elementRef;
        /// <summary>
        /// Gets or sets the element reference.
        /// </summary>
        public ElementReference ElementRef
        {
            get => GetElementRef != null ? GetElementRef() : _elementRef;
            protected set => _elementRef = value;
        }

        /// <summary>
        /// Gets or sets the get element reference.
        /// </summary>
        private Func<ElementReference> GetElementRef { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should set the <see cref="ElementRef"/> automaticly.
        /// </summary>
        protected bool AutoElementReferece { get; set; }

        #region Public
        /// <summary>
        /// Builds the CSS class string.
        /// </summary>
        /// <returns>
        /// The string of CSS class seperated by space for each of items.
        /// </returns>
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
        /// Builds the styles string.
        /// </summary>
        /// <returns>
        /// The string of styles seperated by semicolon(;) for each of items.
        /// </returns>
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

        /// <summary>
        /// Notifies the state of component has changed, When applicable, this will cause the component to be re-rendered.
        /// </summary>
        public virtual void NotifyStateChanged() => StateHasChanged();
        #endregion

        #region Protected
        /// <summary>
        /// Override to create the CSS class that component need.
        /// </summary>
        /// <param name="css">The instance of <see cref="Css"/> class.</param>
        protected virtual void CreateComponentCssClass(Css css) { }

        /// <summary>
        /// Override to create the CSS class that component need.
        /// </summary>
        /// <param name="style">The instance of <see cref="Style"/> class.</param>
        protected virtual void CreateComponentStyle(Style style) { }

        /// <summary>
        /// Adds the CSS class attribute.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
        protected virtual void AddCssClassAttribute(RenderTreeBuilder builder, int sequence = 999990)
        {
            var cssClass = BuildCssClassString();
            if (!string.IsNullOrEmpty(cssClass))
            {
                builder.AddAttribute(sequence, "class", cssClass);
            }
        }

        /// <summary>
        /// Adds the style attribute.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
        protected virtual void AddStyleAttribute(RenderTreeBuilder builder, int sequence = 999991)
        {
            var styles = BuildStylesString();
            if (!string.IsNullOrEmpty(styles))
            {
                builder.AddAttribute(sequence, "style", styles);
            }
        }

        /// <summary>
        /// Adds the addtional attributes.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
        protected virtual void AddAddtionalAttributes(RenderTreeBuilder builder, int sequence = 99992)
        {
            builder.AddMultipleAttributes(sequence, AdditionalAttributes);
        }

        /// <summary>
        /// Adds the element reference.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
        protected virtual void AddElementReference(RenderTreeBuilder builder, int sequence = 99993) 
            => builder.AddElementReferenceCapture(sequence, el => _elementRef = el);

        /// <summary>
        /// Adds the component reference.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
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
        /// Adds the common attributes including 'class' 'style' 'additinal atrributes' etc.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected virtual void AddCommonAttributes(RenderTreeBuilder builder)
        {
            AddCssClassAttribute(builder);
            AddStyleAttribute(builder);
            AddAddtionalAttributes(builder);
            AddExtraCommonAttributes(builder);
        }

        /// <summary>
        /// Adds the extra common attributes.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
        protected virtual void AddExtraCommonAttributes(RenderTreeBuilder builder, int sequence = 99500) { }

        /// <summary>
        /// Adds the content of the child that implemented by <see cref="IHasChildContent"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="sequence">The sequence.</param>
        protected virtual void AddChildContent(RenderTreeBuilder builder,int sequence=100)
        {
            if(this is IHasChildContent childContent)
            {
                builder.AddContent(sequence, childContent.ChildContent);
            }
        }

        /// <summary>
        /// Adds the HTML tag properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
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
                builder.OpenElement(0, element.TagName);
                AddCommonAttributes(builder);
                AddHtmlTagProperties(builder);
                AddChildContent(builder, 888);

                builder.CloseElement();
            }
        }

        /// <summary>
        /// Gets the CSS class from attribute.
        /// </summary>
        protected virtual void GetCssClassFromAttribute()
        {
            var cssClassDic = new Dictionary<string, int>();// class name, order

            //get all interfaces
            var interfaces = this.GetType().GetInterfaces();

            //find the interfaces that defined CssClassAttribute
            interfaces.Where(instance => instance.IsDefined(typeof(CssClassAttribute), true))
                .Select(m => m.GetCustomAttribute<CssClassAttribute>(true))
                .OrderBy(instance => instance.Order)
                .ToList()
                .ForEach(item =>
                {
                    TryAddCssClassDic(cssClassDic, item);
                });

            //class defined CssClassAttribute
            if (this.GetType().IsDefined(typeof(CssClassAttribute)))
            {
                var classCssClassAttribute = this.GetType().GetCustomAttribute<CssClassAttribute>();
                TryAddCssClassDic(cssClassDic, classCssClassAttribute);
            }


            //get the properties in interfaces
            var interfaceProperties = interfaces.SelectMany(type => type.GetProperties());

            // the properties of class defined CssClassAttribute
            var classProperties = this.GetType().GetProperties();

            var properties = new List<PropertyInfo>();

            foreach (var classProp in classProperties)
            {
                if (IdentifyCssClassProperty(classProp)) //if property in class defined CssClassAttribute should override the value of CssClassAttribute defined by interface.
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

            //reorder, distinction
            var cssClassArray = cssClassDic.OrderBy(m => m.Value).Select(m => m.Key).Distinct();

            _cssBuilder.Add(cssClassArray.ToArray());
        }

        #endregion
        /// <summary>
        /// Resolves the property to CSS class dic.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="cssClassDic">The CSS class dic.</param>
        void ResolveCssClassProperties(IEnumerable<PropertyInfo> properties, Dictionary<string, int> cssClassDic)
        {
            foreach (var property in properties.Where(property=>IdentifyCssClassProperty(property)))
            {
                if (!property.CanRead) //shold be get
                {
                    return;
                }

                var value = property.GetValue(this);//value of property

                if (!TryGetCssClassAttribute(property, out CssClassAttribute cssClassAttribute))
                {
                    return;
                }

                var cssClassName = cssClassAttribute.Value?.ToString();

                if (value == null) //null value，has NullCssClassAttribute
                {
                    if (TryGetCssClassAttribute(property, out NullCssClassAttribute nullCssClassAttribute))
                    {
                        TryAddCssClassDic(cssClassDic, nullCssClassAttribute);
                    }
                }
                else // not null value
                {

                    if (value.GetType() == typeof(bool)) //bool type，choose CssClassAttrbute or BooleanCssClassAttribute
                    {
                        var boolValue = (bool)value;

                        //identified BooleanCssClassAttibute
                        if (TryGetCssClassAttribute(property, out BooleanCssClassAttribute boolCssClassAttribute))
                        {
                            if (boolValue)
                            {
                                TryAddCssClassDic(cssClassDic, boolCssClassAttribute);
                            }
                            else
                            {
                                TryAddCssClassDic(cssClassDic, boolCssClassAttribute.FalseCssClass);
                            }
                        }
                        else if (boolValue) //if true by has CssClassAttribute
                        {
                            TryAddCssClassDic(cssClassDic, cssClassAttribute);
                        }
                    }
                    else // combine the value of CssClassAttribute and the value of property to be the CSS class
                    {
                        /* if property is CssClassCollection，and defined CssClassAttribute，so append the value of css class by each items.
                         *
                         */
                        if (value.GetType() == typeof(CssClassCollection) || value.GetType().BaseType == typeof(CssClassCollection)) 
                        {
                            var cssCollection = value as CssClassCollection;
                            if (cssClassAttribute.Suffix)//be suffix
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
                            if (value.GetType().BaseType == typeof(Enum))//enum，combine the item of enum and CssClassAttrbute
                            {
                                var enumFieldCssClass = value.GetEnumCssClass();
                                value = enumFieldCssClass;
                            }
                            else if (typeof(CssClass).IsAssignableFrom(value.GetType())) //  CssClass type
                            {
                                value = (value as CssClass)?.ToString();
                            }

                            if (cssClassAttribute.Suffix) //suffix CssClassAttrbute
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
        /// Tries the get CSS class attribute from property.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
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
        /// Resolves the style attribute.
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
        /// Identifies the CSS class property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private static bool IdentifyCssClassProperty(PropertyInfo property)
        {
            return property.IsDefined(typeof(CssClassAttribute), true);
        }


        
    }
}
