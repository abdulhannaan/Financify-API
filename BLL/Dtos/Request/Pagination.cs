using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Request
{
    public class Pagination
    {
        public int PageNo { get; set; }

        public int RowPerPage { get; set; }
    }
}
