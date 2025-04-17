using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginationResponse<TEntity>
    {
        public PaginationResponse(int pageindex, int pagesize, int totalCount, IEnumerable<TEntity> data)
        {
            Pageindex = pageindex;
            Pagesize = pagesize;
            TotalCount = totalCount;
            Data = data;
        }

        public int Pageindex { get; set; }
        public int Pagesize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
