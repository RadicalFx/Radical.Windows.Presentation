using System.Windows;

namespace Topics.Radical.Windows.Presentation.Messaging
{
    /// <summary>
    /// Return value for the Message Box Open Request
    /// </summary>
    public class ShowMessageBoxResult
    {
        /// <summary>
        /// Value that specifies which message box button is clicked by the user.
        /// </summary>
        public MessageBoxResult Result { get; set; }

        /// <summary>
        /// Create a ShowMessageBoxResult message
        /// </summary>
        /// <param name="result"></param>
        public ShowMessageBoxResult(MessageBoxResult result)
        {
            this.Result = result;
        }

    }
}
