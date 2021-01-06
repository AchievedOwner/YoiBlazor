namespace YoiBlazor
{
    /// <summary>
    /// Provide the notification of the state of component.
    /// </summary>
    public interface IStateChangeHandler
    {
        /// <summary>
        /// Notifies the state of component has changed.
        /// </summary>
        void NotifyStateChanged();
    }
}
