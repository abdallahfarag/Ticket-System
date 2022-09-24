using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Application.Wrappers
{
    public class PagedResponse<T> where T : class
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious { get; private set; }
        public bool HasNext { get; private set; }
        public List<T> Items { get; private set; }

        public PagedResponse(int currentPage, int totalPages, int pageSize, int totalCount, bool hasPrevious, bool hasNext, List<T> items)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;
            HasPrevious = hasPrevious;
            HasNext = hasNext;
            Items = items;
        }
    }
}
