using CK.Models.TopSoft;

namespace CK.ViewModel
{
    public class SalesOrderViewModel
    {
        public HsalesCode Header { get; set; }
        public List<DsalesCode> Details { get; set; }
    }
}
