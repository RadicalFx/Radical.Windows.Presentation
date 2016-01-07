using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Windows;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace Topics.Radical
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationBootstrapper : Topics.Radical.Windows.Presentation.Boot.ApplicationBootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBootstrapper"/> class.
        /// </summary>
        public ApplicationBootstrapper()
        {
            this.UsingAsContainerBootstrapper(new Windows.Presentation.Boot.PuzzleContainerBootstrapper());
        }

        /// <summary>
        /// Gets the container bootstrapper.
        /// </summary>
        /// <value>
        /// The container bootstrapper.
        /// </value>
        public IContainerBootstrapper ContainerBootstrapper { get; private set; }

        /// <summary>
        /// Usings as container bootstrapper.
        /// </summary>
        /// <param name="containerBootstrapper">The container bootstrapper.</param>
        /// <returns></returns>
        public virtual ApplicationBootstrapper UsingAsContainerBootstrapper(IContainerBootstrapper containerBootstrapper)
        {
            this.ContainerBootstrapper = containerBootstrapper;

            return this;
        }

        /// <summary>
        /// Composes the composition container.
        /// </summary>
        /// <param name="compositionContainer">The composition container.</param>
        /// <param name="catalog">The catalog.</param>
        /// <param name="serviceProvider">The service provider.</param>
        protected override void ComposeCompositionContainer(CompositionContainer compositionContainer, AggregateCatalog catalog, IServiceProvider serviceProvider)
        {
            base.ComposeCompositionContainer(compositionContainer, catalog, serviceProvider);

            compositionContainer.ComposeParts(this.ContainerBootstrapper);
        }

        /// <summary>
        /// Called when the composition container has been composed.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="serviceProvider">The service provider.</param>
        protected override void OnCompositionContainerComposed(CompositionContainer container, IServiceProvider serviceProvider)
        {
            base.OnCompositionContainerComposed(container, serviceProvider);

            this.ContainerBootstrapper.OnCompositionContainerComposed(container, serviceProvider);
        }

        /// <summary>
        /// Creates the IoC service provider.
        /// </summary>
        /// <returns>
        /// The IoC service provider.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override IServiceProvider CreateServiceProvider()
        {
            return this.ContainerBootstrapper.CreateServiceProvider(this);
        }

        /// <summary>
        /// Called to ask to the concrete container to resolve all the registered components of type T.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>
        /// A list of resolved types.
        /// </returns>
        protected override IEnumerable<T> ResolveAll<T>()
        {
            return this.ContainerBootstrapper.ResolveAll<T>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TShellView">The type of the shell view.</typeparam>
    public class ApplicationBootstrapper<TShellView> :
        ApplicationBootstrapper
        where TShellView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBootstrapper{TShellView}"/> class.
        /// </summary>
        public ApplicationBootstrapper()
        {
            this.UsingAsShell<TShellView>();
        }
    }
}
