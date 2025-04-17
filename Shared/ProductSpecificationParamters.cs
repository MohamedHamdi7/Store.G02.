using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParamters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        //fullprop
        private int _Pageindex=1;
        private int _Pagesize=5;

        public int Pagesize
        {
            get { return _Pagesize; }
            set { _Pagesize = value; }
        }


        public int Pageindex
        {
            get { return _Pageindex; }
            set { _Pageindex= value; }
        }

    }
}
