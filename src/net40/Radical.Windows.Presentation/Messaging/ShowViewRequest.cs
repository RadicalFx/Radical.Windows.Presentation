using System;
using System.Windows;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace Topics.Radical.Windows.Presentation.Messaging
{
    /// <summary>
    /// Allows any ViewModel to open views
    /// </summary>
    public abstract class ShowViewRequest
    {
        internal abstract Window Resolve(IViewResolver viewResolver);
    }

    /// <summary>
    /// Allows any ViewModel to open views
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class ShowViewRequest<TView> : ShowViewRequest where TView : Window
    {
        internal override Window Resolve(IViewResolver viewResolver)
        {
            var view = viewResolver.GetView<TView>(vm =>
            {
                ViewModelInterceptor?.Invoke(vm);
            });

            return view;
        }

        /// <summary>
        /// Defines the interceptor that will be called before the ViewModel is bound to the View
        /// </summary>
        public Action<object> ViewModelInterceptor { get; set; }
    }

    /// <summary>
    /// Allows any ViewModel to open views
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public class ShowViewRequest<TView, TViewModel> : ShowViewRequest where TView : Window
    {
        internal override Window Resolve(IViewResolver viewResolver)
        {
            var view = viewResolver.GetView<TView, TViewModel>(vm =>
            {
                ViewModelInterceptor?.Invoke(vm);
            });

            return view;
        }

        /// <summary>
        /// Defines the interceptor that will be called before the ViewModel is bound to the View
        /// </summary>
        public Action<TViewModel> ViewModelInterceptor { get; set; }
    }
}
