
using Microsoft.AspNetCore.Components;

namespace YoiBlazor
{
    /// <summary>
    /// 定义组件具有任意 UI 内容的参数。
    /// </summary>
    public interface IHasChildContent
    {
        /// <summary>
        /// 设置组件的一段 UI 内容。
        /// </summary>
        RenderFragment ChildContent { get; set; }
    }

    /// <summary>
    /// 定义组件具有指定 <typeparamref name="TValue"/> 类型的任意 UI 内容的参数。
    /// </summary>
    /// <typeparam name="TValue">对象的类型。</typeparam>
    public interface IHasChildContent<TValue>
    {
        /// <summary>
        /// 设置类型为 <typeparamref name="TValue"/> 的对象的一段 UI 内容。
        /// </summary>
        RenderFragment<TValue> ChildContent { get; set; }
    }
}
