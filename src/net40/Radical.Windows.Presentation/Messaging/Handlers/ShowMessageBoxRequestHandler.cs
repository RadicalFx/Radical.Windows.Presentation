using System;
using System.Windows;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Messaging;

namespace Topics.Radical.Windows.Presentation.Messaging.Handlers
{
    /// <summary>
    /// Handle the ShowMessageBoxRequest message
    /// </summary>
    public class ShowMessageBoxRequestHandler: AbstractMessageHandler<ShowMessageBoxRequest>, INeedSafeSubscription
    {
        private readonly IMessageBroker broker;

        /// <summary>
        /// ShowMessageBoxRequestHandler costructor
        /// </summary>
        /// <param name="broker"></param>
        public ShowMessageBoxRequestHandler(IMessageBroker broker)
        {
            if (broker == null) throw new ArgumentNullException(nameof(broker));
            this.broker = broker;
        }

        /// <summary>
        /// Handle the ShowMessageBoxRequest message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public override void Handle(object sender, ShowMessageBoxRequest message)
        {
            MessageBoxImage icon = message.Icon == null ? MessageBoxImage.None : (MessageBoxImage) message.Icon;
            MessageBoxButton button = message.Button == null ? MessageBoxButton.OK : (MessageBoxButton) message.Button;
            MessageBoxResult defaultResult = message.DefaultResult == null ? MessageBoxResult.None : (MessageBoxResult) message.DefaultResult;
            MessageBoxOptions options = message.Options == null ? MessageBoxOptions.None : (MessageBoxOptions) message.Options;

            MessageBoxResult result = MessageBox.Show(message.MessageBoxText, message.Caption, button,
                icon, defaultResult, options);

            broker.Broadcast(this,new ShowMessageBoxResult(result));
        }
    }
}