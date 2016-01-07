using Topics.Radical.Windows.Presentation.Boot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace Topics.Radical.Windows.Presentation.ComponentModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContainerBootstrapper
    {
        /// <summary>
        /// Creates the service provider.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        IServiceProvider CreateServiceProvider(Topics.Radical.ApplicationBootstrapper owner);
        /// <summary>
        /// Called when the composition container is composed.
        /// </summary>
        /// <param name="compositionContainer">The MEF container.</param>
        /// <param name="serviceProvider">The IoC container.</param>
        void OnCompositionContainerComposed(CompositionContainer compositionContainer, IServiceProvider serviceProvider);

        /// <summary>
        /// Called to ask to the concrete container to resolve all the registered components of type T.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>
        /// A list of resolved types.
        /// </returns>
        IEnumerable<T> ResolveAll<T>();
    }
}