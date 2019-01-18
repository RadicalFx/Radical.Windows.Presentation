namespace Topics.Radical.Windows.Presentation.Messaging
{
    /// <summary>
    /// Issue a request to open a message box.
    /// </summary>
    public class ShowMessageBoxRequest
    {
        /// <summary>
        /// Specifies the icon that is displayed by a message box.
        /// </summary>
        public enum MessageBoxImage
        {
            /// <summary>No icon is displayed.</summary>
            None = 0,
            /// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
            Error = 16, // 0x00000010
            /// <summary>The message box contains a symbol consisting of a white X in a circle with a red background.</summary>
            Hand = 16, // 0x00000010
            /// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
            Stop = 16, // 0x00000010
            /// <summary>The message box contains a symbol consisting of a question mark in a circle.</summary>
            Question = 32, // 0x00000020
            /// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
            Exclamation = 48, // 0x00000030
            /// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
            Warning = 48, // 0x00000030
            /// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
            Asterisk = 64, // 0x00000040
            /// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
            Information = 64, // 0x00000040
        }
        /// <summary>
        /// Specifies the buttons that are displayed on a message box.
        /// </summary>
        public enum MessageBoxButton
        {
            /// <summary>The message box displays an OK button.</summary>
            OK = 0,
            /// <summary>The message box displays OK and Cancel buttons.</summary>
            OKCancel = 1,
            /// <summary>The message box displays Yes, No, and Cancel buttons.</summary>
            YesNoCancel = 3,
            /// <summary>The message box displays Yes and No buttons.</summary>
            YesNo = 4,
        }
        /// <summary>
        /// Specifies which message box button that a user clicks.
        /// </summary>
        public enum MessageBoxResult
        {
            /// <summary>The message box returns no result.</summary>
            None = 0,
            /// <summary>The result value of the message box is OK.</summary>
            OK = 1,
            /// <summary>The result value of the message box is Cancel.</summary>
            Cancel = 2,
            /// <summary>The result value of the message box is Yes.</summary>
            Yes = 6,
            /// <summary>The result value of the message box is No.</summary>
            No = 7,
        }
        /// <summary>
        /// Specifies special display options for a message box.
        /// </summary>
        public enum MessageBoxOptions
        {
            /// <summary>No options are set.</summary>
            None = 0,
            /// <summary>The message box is displayed on the currently active desktop even if a user is not logged on to the computer. Specifies that the message box is displayed from a Microsoft .NET Framework windows service application in order to notify the user of an event. </summary>
            ServiceNotification = 2097152, // 0x00200000
            /// <summary>The message box is displayed on the default desktop of the interactive window station. Specifies that the message box is displayed from a Microsoft .NET Framework windows service application in order to notify the user of an event. </summary>
            DefaultDesktopOnly = 131072, // 0x00020000
            /// <summary>The message box text and title bar caption are right-aligned.</summary>
            RightAlign = 524288, // 0x00080000
            /// <summary>All text, buttons, icons, and title bars are displayed right-to-left.</summary>
            RtlReading = 1048576, // 0x00100000
        }

        /// <summary>
        /// Specifies the title bar caption to display
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Specifies the text to display
        /// </summary>
        public string MessageBoxText { get; set; }
        /// <summary>
        /// Specifies the icon to display
        /// </summary>
        public MessageBoxImage? Image { get; set; }
        /// <summary>
        /// Specifies which button or buttons to display
        /// </summary>
        public MessageBoxButton? Button { get; set; }
        /// <summary>
        /// Specifies the default result of the message box
        /// </summary>
        public MessageBoxResult? DefaultResult { get; set; }
        /// <summary>
        /// Specifies the options
        /// </summary>
        public MessageBoxOptions? Options { get; set; }
    }
}
