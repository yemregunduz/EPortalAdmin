namespace EPortalAdmin.Core.Domain.Models
{
    public class PagingRequest
    {
        private int? _page;
        private int? _pageSize;
        public int Page
        {
            get
            {
                if (_page is null || _page <= 0)
                    return 0;
                return _page.Value;
            }
            set
            {
                _page = value;
            }
        }
        public int PageSize
        {
            get
            {
                if (_pageSize is null || _pageSize <= 0)
                    return int.MaxValue;
                return _pageSize.Value;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
