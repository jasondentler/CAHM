using System;
using System.Collections.Generic;
using System.Linq;

namespace CAHM.ViewModels
{
    public class Page<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalResults { get; private set; }
        public int TotalPages { get { return TotalResults/PageSize + (TotalResults%PageSize == 0 ? 0 : 1); } }
        public bool MorePages { get { return PageNumber < TotalPages; } }

        public Page(IEnumerable<T> items, int pageSize, int pageNumber, int totalResults)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (pageSize == 0)
                throw new ArgumentOutOfRangeException("pageSize");

            Items = items as T[] ?? items as ICollection<T> ?? items.ToArray();
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalResults = totalResults;
        }
    }
}
