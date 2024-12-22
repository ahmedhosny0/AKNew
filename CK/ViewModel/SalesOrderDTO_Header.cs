using CK.Models.TopSoft;

namespace CK.ViewModel
{
    public class SalesOrderDTO_Header
    {
        public List<CK.DTO.SalesOrderDTO> SalesOrder { get; set; }
        public HsalesCode Header { get; set; } = new HsalesCode();

    }
}
