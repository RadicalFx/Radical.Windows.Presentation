using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Topics.Radical.ComponentModel;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Messaging;
using Topics.Radical.Linq;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using System.Diagnostics;
using System.Configuration;
using Topics.Radical.Windows.Threading;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace Topics.Radical.Windows.Presentation.Boot.Installers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Topics.Radical.ComponentModel.IPuzzleSetupDescriptor" />
    [Export(typeof(IPuzzleSetupDescriptor))]
    public class DefaultDescriptor : IPuzzleSetupDescriptor
    {
        /// <summary>
        /// Setups the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="knownTypesProvider">The known types provider.</param>
        public void Setup(IPuzzleContainer container, Func<IEnumerable<Type>> knownTypesProvider)
        {
            var conventions = container.Resolve<BootstrapConventions>();
            var allTypes = knownTypesProvider();

            allTypes.Where(t => conventions.IsMessageHandler(t) && !conventions.IsExcluded(t))
                .Select(t => new
                {
                    Contracts = conventions.SelectMessageHandlerContracts(t),
                    Implementation = t
                })
                .ForEach(descriptor =>
                {
                    var entry = EntryBuilder.For(descriptor.Contracts.First())
                            .ImplementedBy(descriptor.Implementation);

                    foreach(var fw in descriptor.Contracts.Skip(1))
                    {
                        entry = entry.Forward(fw);
                    }

                    container.Register(entry);
                });

            container.Register
            (
                EntryBuilder.For<TraceSource>()
                    .UsingFactory(() =>
                    {
                        var name = ConfigurationManager
                            .AppSettings[ "radical/windows/presentation/diagnostics/applicationTraceSourceName" ]
                            .Return(s => s, "default");

                        return new TraceSource(name);
                    })
                    .WithLifestyle(Lifestyle.Singleton)
            );


            container.Register(
                EntryBuilder.For<Dispatcher>()
                    .UsingFactory(() => Application.Current.Dispatcher)
                    .WithLifestyle(Lifestyle.Singleton)
            );

            container.Register(
                EntryBuilder.For<IDispatcher>()
                    .ImplementedBy<WpfDispatcher>()
                    .WithLifestyle(Lifestyle.Singleton)
                    .Overridable()
            );

            container.Register(
                EntryBuilder.For<Application>()
                    .UsingFactory(() => Application.Current)
                    .WithLifestyle(Lifestyle.Singleton)
            );

            container.Register(
                EntryBuilder.For<IMessageBroker>()
                    .ImplementedBy<MessageBroker>()
                    .WithLifestyle(Lifestyle.Singleton)
                    .Overridable()
            );

            container.Register(
                EntryBuilder.For<IReleaseComponents>()
                    .ImplementedBy<PuzzleComponentReleaser>()
                    .WithLifestyle(Lifestyle.Singleton)
                    .Overridable()
            );
        }
    }
}