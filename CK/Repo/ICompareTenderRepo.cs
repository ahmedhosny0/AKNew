using CK.Models;
using CK.ViewModel;
using Microsoft.Data.SqlClient;

namespace CK.Repo
{
    public interface ICompareTenderRepo
    {
        List<TenderData> GetDatafromExcel();
        Task<bool> InsertDataIntoDB();
        //Task<TenderData> GetDatafromDB();
        SqlCommand ExecuteQuery(string connectionString, TenderData _TenderData);
        MemoryStream ExportExcel(SqlCommand command, TenderData _TenderData, string connectionString);
    }
}
