using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Producer
{
    internal interface IMQTTProducer
    {
        public Task Produce(IServiceProvider serviceProvider);
    }
}
