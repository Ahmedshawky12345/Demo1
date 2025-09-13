namespace Demo1.Api.Common
{
    public class PagedResult<T> where T : class
    {
        public List<T>   Data { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCounts { get; set; }
        public int TotalPages { get; set; }
        public PagedResult(List<T> data,int pagesize,int pagenumber,int totalcount )
        {
            Data = data;
            PageSize = pagesize;
            PageNumber = pagenumber;
            TotalCounts = totalcount;
            TotalPages = (int)(Math.Ceiling((totalcount) / (double)pagesize));
        }
    }
}
