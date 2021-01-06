
using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace YoiBlazor
{
    /// <summary>
    /// Represents the the child component class.
    /// </summary>
    /// <typeparam name="TParentComponent">The type of the parent component.</typeparam>
    /// <seealso cref="YoiBlazor.BlazorComponentBase" />
    /// <seealso cref="YoiBlazor.IHasChildContent" />
    public abstract class ChildBlazorComponentBase<TParentComponent> : BlazorComponentBase,IHasChildContent
        where TParentComponent : ParentBlazorComponentBase<TParentComponent>
    {
        /// <summary>
        /// Gets or sets the parent component.
        /// </summary>
        [CascadingParameter]protected virtual TParentComponent Parent { get; set; }

        /// <summary>
        /// Sets the segment of UI content.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        [Parameter] public virtual string ElementName { get; set; } = "div";

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            if (Parent == null)
            {
                throw new ArgumentException($"The child component '{GetType().Name}' must create inside of '{Parent.GetType().Name}' component");
            }

            base.OnInitialized();
            Parent.Add(this);
        }

        /// <summary>
        /// Renders the component to the supplied <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" />.
        /// </summary>
        /// <param name="builder">A <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" /> that will receive the render output.</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, ElementName);
            AddCommonAttributes(builder);
            builder.AddContent(1, ChildContent);
            builder.CloseElement();
        }
    }
}
