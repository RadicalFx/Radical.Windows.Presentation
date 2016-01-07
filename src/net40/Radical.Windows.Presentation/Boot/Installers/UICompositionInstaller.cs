using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Topics.Radical.ComponentModel;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Windows.Presentation.Regions;

namespace Topics.Radical.Windows.Presentation.Boot.Installers
{
    /// <summary>
    /// A Windsor installer.
    /// </summary>
    [Export(typeof(IPuzzleSetupDescriptor))]
    public class UICompositionInstaller : IPuzzleSetupDescriptor
    {
        /// <summary>
        /// Setups the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="knownTypesProvider">The known types provider.</param>
        public void Setup(IPuzzleContainer container, Func<IEnumerable<Type>> knownTypesProvider)
        {
            container.Register(
                EntryBuilder.For<IRegionManagerFactory>()
                    .ImplementedBy<RegionManagerFactory>()
                    .Overridable());

            container.Register(
                EntryBuilder.For<IRegionService>()
                    .ImplementedBy<RegionService>()
                    .Overridable());

            container.Register(
                EntryBuilder.For<IRegionManager>()
                    .ImplementedBy<RegionManager>()
                    .WithLifestyle(Lifestyle.Transient)
                    .Overridable());
        }
    }
}