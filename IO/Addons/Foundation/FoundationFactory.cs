using IO.Addons.Foundation.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Addons.Foundation
{
    class FoundationFactory
    {
        public IAddonIO CreateAddonIO()
        {
            return new AddonIO();
        }

        public IObjectBuilder CreateObjectBuilder()
        {
            return new ObjectBuilder();
        }
    }
}
