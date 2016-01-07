using Topics.Radical.ComponentModel;
using Topics.Radical.Windows.Presentation.ComponentModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace Topics.Radical.Windows.Presentation.Boot
{
    class PuzzleContainerBootstrapper : IContainerBootstrapper
    {
        IPuzzleContainer container;
        Topics.Radical.ApplicationBootstrapper owner;

        [ImportMany]
        public IEnumerable<IPuzzleSetupDescriptor> Installers { get; set; }

        public IServiceProvider CreateServiceProvider(Topics.Radical.ApplicationBootstrapper owner)
        {
            this.container = new PuzzleContainer();
            this.owner = owner;
            var facade = new PuzzleContainerServiceProviderFacade(this.container);

            this.container.Register(EntryBuilder.For<Topics.Radical.ApplicationBootstrapper>()
                .UsingInstance(owner));
            this.container.Register(EntryBuilder.For<IPuzzleContainer>()
                .UsingInstance(this.container));
            this.container.Register(EntryBuilder.For<IServiceProvider>()
                .UsingInstance(facade));
            this.container.Register(EntryBuilder.For<BootstrapConventions>()
                .UsingInstance(new BootstrapConventions()));

            //var view = CoreApplication.GetCurrentView();
            //var dispatcher = view.CoreWindow.Dispatcher;

            //this.container.Register(
            //        EntryBuilder.For<CoreDispatcher>()
            //            .UsingInstance(dispatcher)
            //);

            this.container.AddFacility<SubscribeToMessageFacility>();

            return facade;
        }


        public void OnCompositionContainerComposed(CompositionContainer compositionContainer, IServiceProvider serviceProvider)
        {
            var conventions = this.container.Resolve<BootstrapConventions>();
            var allTypes = new HashSet<Type>(Assembly.GetEntryAssembly().GetTypes());
            foreach(var dll in Directory.EnumerateFiles(Helpers.EnvironmentHelper.GetCurrentDirectory(), "*.dll"))
            {
                var name = Path.GetFileNameWithoutExtension(dll);
                var a = Assembly.Load(name);
                if(conventions.IncludeAssemblyInContainerScan(a))
                {
                    var ts = a.GetTypes();
                    foreach(var t in ts)
                    {
                        allTypes.Add(t);
                    }
                }
            }

            this.container.SetupWith(() => allTypes, this.Installers.ToArray());
        }

        /// <summary>
        /// Called to ask to the concrete container to resolve all the registered components of type T.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>
        /// A list of resolved types.
        /// </returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return this.container.ResolveAll<T>();
        }
    }
}
