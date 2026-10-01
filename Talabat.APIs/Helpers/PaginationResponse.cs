using Talabat.Domain.Entities;

namespace Talabat.APIs.Helpers
{
    public class PaginationResponse<T> 
    {
         public int PageIndex { get; set; }
         public int pageSize { get; set; }
         public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }

    }
}
