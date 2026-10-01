using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Specifications
{
    public class ProductSpecParams
    {
        public string? SortBy { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private int pageSize=10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 :value;
            }
        }

        public int  PageIndex{ get; set; } = 1;

       
        private string? search;
        public string ? Search 
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }



    }
}
