using CK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using MimeKit;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using Hangfire;
using Hangfire.SqlServer;
using System.Runtime;
namespace CK.Services
{
    public static class JobServices
    {
        public static void ExportExcel()
        {
            GenerateAndSendReport();
        }

        public static void GenerateAndSendReport()
        {
            SalesParameters Parobj = new SalesParameters();
            // Assuming session variables are replaced with parameters or configuration settings
            var username = "Ahmed Hosny"; // Example, replace with actual logic to obtain username
                                          // Other necessary variables...

            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    connection.Open();
                    string SQL = @"SELECT * FROM rptcars WHERE Remainingdays <= 30";
                    using (SqlCommand command = new SqlCommand(SQL, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        using (var package = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AKCarLicenseReport");
                            int row = 2; // Start from row 2 to leave space for headers
                            int sheetIndex = 1; // Start with the first sheet
                            int columnCount = 1;
                            // Add header row
                            void AddHeaderRow(ExcelWorksheet ws, int columnCount)
                            {
                                int column = 1;
                                ws.Cells[1, column++].Value = "DriverName";
                                ws.Cells[1, column++].Value = "CarNumber";
                                ws.Cells[1, column++].Value = "RemainingDays";
                                ws.Cells[1, column++].Value = "StartDate";
                                ws.Cells[1, column++].Value = "EndDate";
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
                            while (reader.Read())
                            {
                                columnCount = 1; // Reset column count for each row
                                worksheet.Cells[row, columnCount++].Value = reader["DriverName"];
                                worksheet.Cells[row, columnCount++].Value = reader["CarNumber"];
                                if (!Convert.IsDBNull(reader["RemainingDays"]))
                                    worksheet.Cells[row, columnCount++].Value = reader["RemainingDays"];
                                else worksheet.Cells[row, columnCount++].Value = 0;
                                worksheet.Cells[row, columnCount].Style.Numberformat.Format = "yyyy-MM-dd";
                                worksheet.Cells[row, columnCount++].Value = reader["StartDate"];
                                worksheet.Cells[row, columnCount].Style.Numberformat.Format = "yyyy-MM-dd";
                                worksheet.Cells[row, columnCount++].Value = reader["EndDate"];
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
                                            rowRange.Style.Fill.BackgroundColor.SetColor(Color.LightBlue); // Light gray for even rows
                                        }
                                        rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        rowRange.Style.Border.Top.Color.SetColor(Color.LightBlue); // Set border color to black
                                        rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        rowRange.Style.Border.Bottom.Color.SetColor(Color.LightBlue); // Set border color to black
                                        rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        rowRange.Style.Border.Left.Color.SetColor(Color.LightBlue); // Set border color to black
                                        rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        rowRange.Style.Border.Right.Color.SetColor(Color.LightBlue); // Set border color to black
                                    }
                                    row++;
                                }

                                if (row == 1000001)
                                {
                                    // Create a new worksheet and reset the row count
                                    worksheet = package.Workbook.Worksheets.Add($"AKCarLicenseReport{sheetIndex++}");
                                    // Re-add the header row to the new worksheet
                                    row = 2; // Reset row count for the new worksheet
                                    columnCount = 1; // Reset column count
                                                     // Re-add the header row to the new worksheet\
                                    AddHeaderRow(worksheet, columnCount);
                                    if (Parobj.VDateInTime)
                                        worksheet.Cells[row, columnCount++].Value = reader["DinTime"];
                                    if (Parobj.VStoreId)
                                        worksheet.Cells[row, columnCount++].Value = reader["StoreID"];
                                    if (Parobj.VStoreName)
                                        worksheet.Cells[row, columnCount++].Value = reader["Store Name"];
                                    if (Parobj.VTransactionNumber)
                                        worksheet.Cells[row, columnCount++].Value = reader["TransactionNumber"];
                                    if (Parobj.VItemLookupCode)
                                        worksheet.Cells[row, columnCount++].Value = reader["Itemlookupcode"];
                                    if (Parobj.VItemName)
                                        worksheet.Cells[row, columnCount++].Value = reader["ItemName"];
                                    if (Parobj.Vbatch)
                                        worksheet.Cells[row, columnCount++].Value = reader["Batchid"];
                                    if (Parobj.Vbatch)
                                        worksheet.Cells[row, columnCount++].Value = reader["Terminalid"];
                                    if (Parobj.Vbatch)
                                        worksheet.Cells[row, columnCount++].Value = reader["Paidtype"];
                                    if (Parobj.Vbatch)
                                        worksheet.Cells[row, columnCount++].Value = reader["TotalSales"];
                                    if (Parobj.Vbatch)
                                        worksheet.Cells[row, columnCount++].Value = reader["Startdate"];
                                    if (Parobj.Vbatch)
                                        worksheet.Cells[row, columnCount++].Value = reader["Closeddate"];
                                    if (Parobj.VPaidtype)
                                        worksheet.Cells[row, columnCount++].Value = reader["Paidtype"];
                                    worksheet.Cells[row, columnCount].Style.Numberformat.Format = "#,##0.00";
                                    if (Parobj.VTotalSales)
                                        worksheet.Cells[row, columnCount++].Value = reader["TotalSales"];
                                    worksheet.Cells[row, columnCount].Style.Numberformat.Format = "yyyy-MM-dd";
                                    if (Parobj.VPerDay)
                                        worksheet.Cells[row, columnCount++].Value = reader["Date"];
                                    if (columnCount <= 1)
                                    {
                                        Console.WriteLine("Error: columnCount is 0. No data to process.");
                                        // Optionally, throw an exception to halt execution
                                        // throw new InvalidOperationException("columnCount is 0. No data to process.");
                                    }
                                    else
                                    {
                                        // Apply styling to the row
                                        using (var rowRange = worksheet.Cells[row, 1, row, columnCount - 1])
                                        {
                                            rowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            if (row % 2 == 0) // Even row
                                            {
                                                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                rowRange.Style.Fill.BackgroundColor.SetColor(Color.LightBlue); // Light gray for even rows
                                            }
                                            rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            rowRange.Style.Border.Top.Color.SetColor(Color.LightBlue); // Set border color to black
                                            rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            rowRange.Style.Border.Bottom.Color.SetColor(Color.LightBlue); // Set border color to black
                                            rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            rowRange.Style.Border.Left.Color.SetColor(Color.LightBlue); // Set border color to black
                                            rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            rowRange.Style.Border.Right.Color.SetColor(Color.LightBlue); // Set border color to black
                                        }
                                    }
                                }
                            }
                            worksheet.Cells.AutoFitColumns();

                            var stream = new MemoryStream(package.GetAsByteArray());

                            // Email sending logic...
                            SendEmailWithAttachment(stream, username);

                            // Note: Instead of setting HttpContext.Session, consider logging or another form of status tracking
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception details...
            }
        }
        private static void SendEmailWithAttachment(MemoryStream stream, string username)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Fouad Esmail", "f.laz@circlekegypt.com"));
            //message.From.Add(new MailboxAddress("Ahmed Hosny", "a.hosny@circlekegypt.com"));
            //message.From.Add(new MailboxAddress("Ahmed Hosny", "AhmedHosny1340@gmail.com"));
            // Updated the recipient's email address here
            message.To.Add(new MailboxAddress("Fouad", "F.laz@circlekegypt.com"));
            //message.To.Add(new MailboxAddress("Ahmed", "ahmed.hosny68@yahoo.com"));
            //message.To.Add(new MailboxAddress("Ahmed", "ahmed.hosny.fci@gmail.com"));
            // message.To.Add(new MailboxAddress("AK Soft", "f.laz@circlekegypt.com"));
            message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
           // message.To.Add(new MailboxAddress("Saleh Sharaf", "saleh20000@gmail.com"));
            //message.To.Add(new MailboxAddress("Ahmed Hosny", "a.hosny@circlekegypt.com"));
            message.Subject = "رخص سيارات قاربت علي الانتهاء";

            var bodyBuilder = new BodyBuilder { TextBody = "يرجي مراجعه الرخص المرفقه التي قاربت علي الانتهاء" };
            bodyBuilder.Attachments.Add("AKCarLicenseReport.xlsx", stream, new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Timeout = 1000000; // Set timeout to 10 seconds or longer if needed
                // client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Connect("smtppro.zoho.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Note: only needed if the SMTP server requires authentication  

                //client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");
                //client.Authenticate("a.hosny@circlekegypt.com", "q0sHxkdsZbms");
                client.Authenticate("f.laz@circlekegypt.com", "dizfXNJK3XqG");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
