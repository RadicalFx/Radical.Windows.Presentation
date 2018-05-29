using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Messaging;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace Topics.Radical.Windows.Presentation.Messaging.Handlers
{
    class ShowViewRequestHandler : AbstractMessageHandler<ShowViewRequest>, INeedSafeSubscription
    {
        readonly IViewResolver viewResolver;

        public ShowViewRequestHandler(IViewResolver viewResolver)
        {
            this.viewResolver = viewResolver;
        }

        public override void Handle( object sender, ShowViewRequest message )
        {
            var view = message.Resolve(viewResolver);
            var dialogRequest = message as ShowDialogViewRequest;
            if (dialogRequest != null)
            {
                var result = view.ShowDialog();
                dialogRequest.DialogClosed?.Invoke(result);
            }
            else
            {
                view.Show();
            }
        }
    }


}
