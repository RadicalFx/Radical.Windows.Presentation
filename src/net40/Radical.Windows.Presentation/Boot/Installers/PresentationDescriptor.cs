using System.Linq;
using Topics.Radical.ComponentModel;
using Topics.Radical.Linq;
using System.Collections.Generic;
using System;
using System.ComponentModel.Composition;

namespace Topics.Radical.Windows.Presentation.Boot.Installers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Topics.Radical.ComponentModel.IPuzzleSetupDescriptor" />
    [Export( typeof( IPuzzleSetupDescriptor ) )]
	public class PresentationDescriptor : IPuzzleSetupDescriptor
	{
        /// <summary>
        /// Setups the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="knownTypesProvider">The known types provider.</param>
        public void Setup( IPuzzleContainer container, System.Func<IEnumerable<Type>> knownTypesProvider )
		{
            var conventions = container.Resolve<BootstrapConventions>();
            var allTypes = knownTypesProvider();

            allTypes.Where(t => conventions.IsViewModel(t) && !conventions.IsExcluded(t))
                .Select(t =>
                {
                    var contracts = conventions.SelectViewModelContracts(t);

                    return new
                    {
                        Contracts = conventions.SelectViewModelContracts(t),
                        Implementation = t,
                        Lifestyle = conventions.IsShellViewModel(contracts, t) ?
                           Lifestyle.Singleton :
                           Lifestyle.Transient
                    };
                })
                .ForEach(descriptor =>
                {
                    var builder = EntryBuilder.For(descriptor.Contracts.First())
                        .WithLifestyle(descriptor.Lifestyle);

                    foreach(var c in descriptor.Contracts.Skip(1))
                    {
                        builder = builder.Forward(c);
                    }

                    container.Register(builder);
                });

            allTypes.Where(t => conventions.IsView(t) && !conventions.IsExcluded(t))
                .Select(t =>
                {
                    var contracts = conventions.SelectViewContracts(t);

                    return new
                    {
                        Contracts = contracts,
                        Implementation = t,
                        Lifestyle = conventions.IsShellView(contracts, t) ?
                         Lifestyle.Singleton :
                         Lifestyle.Transient
                    };
                })
                .ForEach(descriptor => 
                {
                    var builder = EntryBuilder.For(descriptor.Contracts.First())
                        .WithLifestyle(descriptor.Lifestyle);

                    foreach(var c in descriptor.Contracts.Skip(1))
                    {
                        builder = builder.Forward(c);
                    }

                    container.Register(builder);
                });
        }
	}
}