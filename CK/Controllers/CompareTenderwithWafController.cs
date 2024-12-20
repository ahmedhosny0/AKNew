using CK.Models.AXDBTest;
using CK.Repo;
using CK.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CK.Controllers
{
    public class CompareTenderwithWafController : Controller
    {
        private readonly ICompareTenderRepo _CompareTenderRepo;
        private readonly TenderData _TenderData;
        public CompareTenderwithWafController(ICompareTenderRepo CompareTenderRepo, TenderData tenderData)
        {
            _CompareTenderRepo = CompareTenderRepo;
            _TenderData = tenderData;
        }
        public async Task<IActionResult> CompareData()
        {
            var data= _CompareTenderRepo.GetDatafromExcel();

            _CompareTenderRepo.InsertDataIntoDB();
           string  Conn = _TenderData.TopSoftConnection;
            SqlCommand command = _CompareTenderRepo.ExecuteQuery(Conn, _TenderData);
            var stream = _CompareTenderRepo.ExportExcel(command, _TenderData, Conn);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AKSalesWafrhaVSCKReport.xlsx");
            return View();
        }
    }
}
