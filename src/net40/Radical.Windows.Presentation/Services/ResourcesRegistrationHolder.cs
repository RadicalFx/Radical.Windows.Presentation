using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Topics.Radical.Windows.Presentation.Services
{
    class ResourcesRegistrationHolder
    {
        public IDictionary<Type, HashSet<Type>> Registrations { get; set; }
    }
}
