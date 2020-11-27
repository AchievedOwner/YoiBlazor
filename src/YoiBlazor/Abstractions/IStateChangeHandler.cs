namespace YoiBlazor
{
    /// <summary>
    /// 提供对组件状态改变的通知。
    /// </summary>
    public interface IStateChangeHandler
    {
        /// <summary>
        /// 通知组件状态已更改。
        /// </summary>
        void NotifyStateChanged();
    }
}
