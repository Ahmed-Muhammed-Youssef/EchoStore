using System;

namespace Core.Pagination
{
    public class PaginationInfo
    {
        private int _numberOfItems;
        private static int _maxPageSize = 20; 
        public int PageSize { get; private set; } 
        public int CurrentPageNumber { get; private set; } 
        public int LastPage { get; private set; }
        public int NumberOfItems {
            get
            {
                return _numberOfItems;
            }
            set {
                _numberOfItems = value;
                LastPage = (value - 1) / PageSize;
            } }
        public PaginationInfo(int numberOfItems = 4, int pageSize = 4, int currentPageNumber = 0)
        {
            if(pageSize < 1)
            {
                throw new ArgumentException("invalid items per page number");
            }
            PageSize = pageSize > _maxPageSize ? _maxPageSize : pageSize;                
            _numberOfItems = numberOfItems;
            CurrentPageNumber = currentPageNumber;
            LastPage = (numberOfItems-1)/pageSize;
        }
        public int GetFirstElementIndex()
        {
            return CurrentPageNumber * PageSize;
        }
    }
}
