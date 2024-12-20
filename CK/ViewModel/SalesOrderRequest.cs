using CK.Models.TopSoft;

namespace CK.ViewModel
{
    public class SalesOrderRequest
    {
        public HsalesCode Header { get; set; } = new HsalesCode();
        public List<DsalesCode> Details { get; set; } = new List<DsalesCode>();
    }
}
