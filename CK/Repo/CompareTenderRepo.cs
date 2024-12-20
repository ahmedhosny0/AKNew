using CK.Models;
using CK.ViewModel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace CK.Repo
{
    public class CompareTenderRepo : ICompareTenderRepo
    {
        private readonly TenderData _TenderData;
        public CompareTenderRepo(TenderData tenderData)
        {
            _TenderData = tenderData;
        }
        public  List<TenderData> GetDatafromExcel()
        {
            string filePath = @"E:\CK Helper New Publish\WafarhaFile\data.xlsx"; 
            List<TenderData> tenderDataList = new List<TenderData>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file does not exist at the specified path.");
            }
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Get the first sheet

                // Loop through the rows, starting after the header (assuming headers are in the first row)
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Row 2 onwards
                {
                    // Map the Excel data to a TenderData object
                    var tender = new TenderData
                    {
                        Coupon = worksheet.Cells[row, 1].Text,                // Coupon
                        Price = double.TryParse(worksheet.Cells[row, 2].Text, out var price) ? price : 0, // Price
                        OfferId = worksheet.Cells[row, 3].Text,              // Offer Id
                        TypePriceId = worksheet.Cells[row, 4].Text,          // Type Price Id
                        ActivationDateTime = worksheet.Cells[row, 5].Value is DateTime dt
    ? dt
    : DateTime.TryParse(worksheet.Cells[row, 5].Text, out var date) ? date : (DateTime?)null,
                    ActivatedBy = worksheet.Cells[row, 6].Text,          // Activated By
                        BranchName = worksheet.Cells[row, 7].Text,           // Branch Name
                        WaffarhaCommission = double.TryParse(worksheet.Cells[row, 8].Text, out var commission) ? commission : 0, // Waffarha Commission
                        Vat = double.TryParse(worksheet.Cells[row, 9].Text, out var vat) ? vat : 0, // VAT
                        NetToMerchant = double.TryParse(worksheet.Cells[row, 10].Text, out var net) ? net : 0 // Net To Merchant
                    };

                    // Add the object to the list
                    tenderDataList.Add(tender);
                }
            }
            return tenderDataList;
        }
        public async Task<bool> InsertDataIntoDB()
        {
            var data=GetDatafromExcel();
            List<TenderData> tenderDataList= data;

            string connectionString = _TenderData.TopSoftConnection;
            string query2 = @"
Truncate table TenderData";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        await connection.OpenAsync();
                        command.ExecuteNonQuery(); // Execute the command
                    } 
                }
            string query = @"
        INSERT INTO TenderData (
            BranchName, Coupon, Price, OfferId, TypePriceId, ActivationDateTime, 
            NetToMerchant, Vat, WaffarhaCommission, ActivatedBy
        ) VALUES (
            @BranchName, @Coupon, @Price, @OfferId, @TypePriceId, @ActivationDateTime, 
            @NetToMerchant, @Vat, @WaffarhaCommission, @ActivatedBy
        )";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var tenderData in tenderDataList)
                    {
                        using (var command = new SqlCommand(query, connection))
                        {
                            // Add parameters
                            command.Parameters.AddWithValue("@BranchName", tenderData.BranchName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Coupon", tenderData.Coupon ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Price", tenderData.Price);
                            command.Parameters.AddWithValue("@OfferId", tenderData.OfferId ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@TypePriceId", tenderData.TypePriceId ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ActivationDateTime", tenderData.ActivationDateTime ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@NetToMerchant", tenderData.NetToMerchant);
                            command.Parameters.AddWithValue("@Vat", tenderData.Vat);
                            command.Parameters.AddWithValue("@WaffarhaCommission", tenderData.WaffarhaCommission);
                            command.Parameters.AddWithValue("@ActivatedBy", tenderData.ActivatedBy ?? (object)DBNull.Value);
                            command.ExecuteNonQuery(); // Execute the command
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public MemoryStream ExportExcel(SqlCommand command, TenderData _TenderData, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command.Connection = connection;
                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AKSalesWafrhaVSCKReport");
                    int row = 2; // Start from row 2 to leave space for headers
                    int sheetIndex = 1; // Start with the first sheet
                    int columnCount = 1;
                    // Add header row
                    void AddHeaderRow(ExcelWorksheet ws, int columnCount)
                    {
                        int column = 1;
                        ws.Cells[1, column++].Value = "Date";
                        ws.Cells[1, column++].Value = "Branch Name";
                            ws.Cells[1, column++].Value = "Total Wafrah";
                            ws.Cells[1, column++].Value = "Total CK";
                            ws.Cells[1, column++].Value = "Diff";
                        using (var headerRange = ws.Cells[1, 1, 1, column - 1])
                        {
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            headerRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            headerRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            headerRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);
                            ws.Cells[1, 1, 1, column - 1].AutoFilter = true;
                        }
                    }
                    AddHeaderRow(worksheet, columnCount);
                    //row = 2;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnCount = 1; // Reset column count for each row
                            {
                                worksheet.Cells[row, columnCount].Style.Numberformat.Format = "yyyy-MM-dd";
                                worksheet.Cells[row, columnCount++].Value = reader["Transdate"];
                                worksheet.Cells[row, columnCount].Style.Numberformat.Format = "yyyy-MM-dd";
                            }
                            worksheet.Cells[row, columnCount++].Value = reader["Storename"];
                            worksheet.Cells[row, columnCount++].Value = reader["TotalWafrah"];
                            worksheet.Cells[row, columnCount].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[row, columnCount++].Value = reader["TotalCK"];
                            worksheet.Cells[row, columnCount].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[row, columnCount++].Value = reader["Diff"];
                            worksheet.Cells[row, columnCount].Style.Numberformat.Format = "#,##0.00";
                            if (columnCount <= 1)
                            {
                                Console.WriteLine("Error: columnCount is 0. No data to process.");
                                // Optionally, throw an exception to halt execution
                                // throw new InvalidOperationException("columnCount is 0. No data to process.");
                            }
                            else
                            {
                                using (var rowRange = worksheet.Cells[row, 1, row, columnCount - 1])
                                {
                                    rowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                    if (row % 2 == 0) // Even row
                                    {
                                        rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        rowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue); // Light gray for even rows
                                    }
                                    rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    rowRange.Style.Border.Top.Color.SetColor(System.Drawing.Color.LightBlue); // Set border color to black
                                    rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    rowRange.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.LightBlue); // Set border color to black
                                    rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    rowRange.Style.Border.Left.Color.SetColor(System.Drawing.Color.LightBlue); // Set border color to black
                                    rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    rowRange.Style.Border.Right.Color.SetColor(System.Drawing.Color.LightBlue); // Set border color to black
                                }
                                row++;
                            }
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                    // Save the package to a MemoryStream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    // Reset the stream position to the beginning
                    stream.Position = 0;
                    return stream;
                }

            }
        }
        public SqlCommand ExecuteQuery(string connectionString, TenderData _TenderData)
        {

            string sqlQuery = "Select * from RptWafVSCKTender";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //string storedProcedureName = "R2"; // Replace with your actual stored procedure name
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    //command.CommandType = CommandType.StoredProcedure;
                    //sqlQuery = "SELECT sum(TotalSales) TotalSales FROM RptSales WHERE CAST(TransDate AS DATE) BETWEEN @fromDate AND @toDate";
                    command.CommandTimeout = 7200;
                    return command;
                }

            }
        }

    }
}
