using System;
using System.Windows;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Messaging;
using Topics.Radical.Validation;

namespace Topics.Radical.Windows.Presentation.Messaging.Handlers
{
    /// <summary>
    /// Handle the ShowMessageBoxRequest message
    /// </summary>
    public class ShowMessageBoxRequestHandler : AbstractMessageHandler<ShowMessageBoxRequest>, INeedSafeSubscription
    {
        private readonly IMessageBroker broker;

        /// <summary>
        /// ShowMessageBoxRequestHandler costructor
        /// </summary>
        /// <param name="broker"></param>
        public ShowMessageBoxRequestHandler(IMessageBroker broker)
        {
            Ensure.That(broker).Named(() => broker).IsNotNull();
            this.broker = broker;
        }

        /// <summary>
        /// Handle the ShowMessageBoxRequest message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public override void Handle(object sender, ShowMessageBoxRequest message)
        {
            var icon = message.Icon == null ? MessageBoxImage.None : (MessageBoxImage)message.Icon;
            var button = message.Button == null ? MessageBoxButton.OK : (MessageBoxButton)message.Button;
            var defaultResult = message.DefaultResult == null ? MessageBoxResult.None : (MessageBoxResult)message.DefaultResult;
            var options = message.Options == null ? MessageBoxOptions.None : (MessageBoxOptions)message.Options;

            var result = MessageBox.Show(message.MessageBoxText, message.Caption, button,
                icon, defaultResult, options);

            broker.Broadcast(this, new ShowMessageBoxResult(result));
        }
    }
}