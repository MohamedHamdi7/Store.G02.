using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManger
    {
        public IProductService productService { get; }
        public IBasketServices basketServices { get; }

    }
}
