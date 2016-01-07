using System;
using System.Linq;
using System.Collections.Generic;
using Topics.Radical.ComponentModel;
using Topics.Radical.Linq;
using System.ComponentModel.Composition;

namespace Topics.Radical.Windows.Presentation.Boot.Installers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Topics.Radical.ComponentModel.IPuzzleSetupDescriptor" />
    [Export( typeof( IPuzzleSetupDescriptor ) )]
	public class ServicesDescriptor : IPuzzleSetupDescriptor
	{
        /// <summary>
        /// Setups the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="knownTypesProvider">The known types provider.</param>
        public void Setup( IPuzzleContainer container, Func<IEnumerable<Type>> knownTypesProvider )
		{
            var conventions = container.Resolve<BootstrapConventions>();
            knownTypesProvider()
                .Where(t => conventions.IsService(t) && !conventions.IsExcluded(t))
                .Select(t => new
                {
                    Contracts = conventions.SelectServiceContracts(t),
                    Implementation = t
                })
                .ForEach(descriptor =>
                {
                    var entry = EntryBuilder.For(descriptor.Contracts.First())
                            .ImplementedBy(descriptor.Implementation)
                            .Overridable();

                    foreach(var fw in descriptor.Contracts.Skip(1))
                    {
                        entry = entry.Forward(fw);
                    }

                    container.Register(entry);
                });
        }
	}
}