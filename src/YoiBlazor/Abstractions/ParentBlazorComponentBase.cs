using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the base class of parent blazor component.
    /// </summary>
    /// <typeparam name="TParentComponent">The type of the parent component.</typeparam>
    /// <seealso cref="YoiBlazor.BlazorComponentBase" />
    /// <seealso cref="YoiBlazor.IHasChildContent" />
    public abstract class ParentBlazorComponentBase<TParentComponent> : BlazorComponentBase, IHasChildContent
        where TParentComponent: IComponent
    {
        /// <summary>
        /// Sets the segment of UI content.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// The child components
        /// </summary>
        private readonly List<IComponent> _childComponents = new List<IComponent>();

        /// <summary>
        /// Gets the child components.
        /// </summary>
        public IList<IComponent> ChildComponents => _childComponents;

        /// <summary>
        /// Gets the name of the element.
        /// </summary>
        protected virtual string ElementName => "div";

        /// <summary>
        /// Gets or sets the index of the actived.
        /// </summary>
        public int ActivedIndex { get; protected set; } = -1;

        /// <summary>
        /// Renders the component to the supplied <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" />.
        /// </summary>
        /// <param name="builder">A <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" /> that will receive the render output.</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, ElementName);
            AddCommonAttributes(builder);
            builder.OpenComponent<CascadingValue<TParentComponent>>(100);
            builder.AddAttribute(101, "Value", this);
            builder.AddAttribute(102, nameof(ChildContent), ChildContent);
            builder.CloseComponent();

            builder.CloseElement();
        }

        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public virtual void Add(IComponent component)
        {
            _childComponents.Add(component);
            if (_childComponents.Count == 1)
            {
                ActivedIndex = 0;
            }
            NotifyStateChanged();
        }
    }
}
