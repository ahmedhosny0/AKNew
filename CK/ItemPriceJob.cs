using CK.Models;
using Microsoft.Data.SqlClient;
using MimeKit;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
namespace CK.Services
{
    public static class ItemPriceJob
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
                try
                {
                using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
                {
                    using (SqlCommand command = new SqlCommand(@"delete from axdbtest.dbo.ItemPrice
insert into axdbtest.dbo.ItemPrice
select * from (
select distinct It.Name ItemName,
                            pr.Amount Price,
                            pr.Itemrelation ItemLookupCode,
							case when Inv.Physicalinvent>7 then 1 else 0 end as Qty
							,store.username StoreName
							 from AXDB.dbo.Inventsum Inv
left join AXDB.dbo.Inventtable ca on Inv.Itemid = ca.Itemid   
left join AXDB.dbo.Pricedisctable pr on Inv.Itemid = pr.Itemrelation
 left join AXDB.dbo.Ecoresproducttranslation It on ca.Product = It.Product
	left join AXDB.dbo.Ecoresproductcategory Re on It.Product = Re.Product
	left join AXDB.dbo.Ecorescategory CateN on Re.Category = CateN.Recid
					inner join AXDBTest.dbo.Storeusers store
  on store.Inventlocation collate SQL_Latin1_General_CP1_CI_AS = convert(nvarchar,Inv.InventlocationId )
                        where cast (pr.Todate as date) = '1900-01-01' and pr.Accountrelation = 'Retail'
and store.username in ('Zayed','ALMAZA', 'MANSOURA', 'VOD-2', 'TRUMPH', 'STRIP', 'SODIC', 'SHATBY', 'ROSHDY', 'REHAB2', 'AL-REHAB', 'Nahas1', 'Mivida', 'Midor', 'Hbrkat', 'GLEEM', 'Dunes', 'CONCORD', 'Sadat', 'korba', 'SAFIR', 'MSYC')
					    --and
                        --(Inv.Inventlocationid = '112-ALMAZA' or Inv.Inventlocationid = '69-CONCORD' or Inv.Inventlocationid = '02-GALAA2' or
						--Inv.Inventlocationid = '118-Nahas1' or Inv.Inventlocationid = '117-Hbrkat' or Inv.Inventlocationid = '1-MAADI' or
						--Inv.Inventlocationid = '23-Mivida' or Inv.Inventlocationid = '128-REHAB2' or Inv.Inventlocationid = '32-Zayed' or
						--Inv.Inventlocationid = '43-VOD-2' or Inv.Inventlocationid = '2-GLEEM' or Inv.Inventlocationid = '14-AZARITA' or
						--Inv.Inventlocationid = '26-SHATBY' or Inv.Inventlocationid = '27-ROSHDY' or Inv.Inventlocationid = '108-MANS3'
						--or Inv.Inventlocationid = '111-MANS4' or Inv.Inventlocationid = '5-SODIC' or Inv.Inventlocationid = '25-STRIP'
                        --or Inv.Inventlocationid = '133-KORBA' or Inv.Inventlocationid = '159-SAFIR' or Inv.Inventlocationid = '143-TRUMPH' or
						--Inv.Inventlocationid = '161-MSYC')
						--and CateN.Name IN ('Smoking Accessories', 'Shawarma', 'Service Charge', 'Salty Snacks', 'Salad', 'Rich Cut & Fries', 'Rich Cut', 'Protein Bar', 'POP Corn & Cotton Candy', 'Pizza', 'Phone Accessories', 'Package Sweet Snacks', 'Package Ice Cream', 'Package Beverage', 'Other Tobacco', 'Oriental Pastry', 'Offers Rich Cut', 'Offers - Coffe & Croissant', 'Offers', 'Non Edible Grocery', 'Kahwetek', 'Juice', 'Hot Meal', 'Healthy Beauty Care', 'General Merchandise', 'Fresh Fruits', 'French Fries', 'Fountain', 'Feteer', 'Edible Grocery', 'Dounts', 'Delivery', 'Crispy Sandwich', 'Crispy Chicken', 'Corn Dog', 'Cookies', 'Cold Cut', 'Coffee', 'Ck Ice Cream', 'Cigarettes', 'Candy', 'Burger Offer', 'Burger', 'BreakFast', 'Bakery', 'AutoMotive')
						)s ", connection))
                    {
                        connection.Open(); // Open the connection
                        var test = command.ExecuteReader();
                    }
                }
                using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
                    {
                        connection.Open();
                        string SQL = @"select distinct
 Rm.Description collate SQL_Latin1_General_CP1_CI_AS ItemName,
                             Rm.Price,
                             Rm.ItemLookupCode collate SQL_Latin1_General_CP1_CI_AS ItemLookupCode,
							 case when Rm.Quantity>0 then 1 else 0 end as Qty,
							store.username collate SQL_Latin1_General_CP1_CI_AS StoreName
from DATA_CENTER.dbo.Item Rm
                         inner join DATA_CENTER.dbo.Department cate on Rm.departmentid=cate.Id and  Rm.storeid = cate.Storeid 
						  inner join CkproUsers.dbo.Storeuser store on store.RMSstoNumber=convert(nvarchar,Rm.storeid)
                         where 
store.username in ( 'Maadimobil','AL-REHAB', 'Midor', 'Dunes','Sadat')
--(Rm.storeid = 83 or Rm.storeid = 218 or Rm.storeid = 194 or Rm.storeid = 112 or Rm.storeid = 55 or Rm.storeid = 173 or Rm.storeid = 33) and (cate.Storeid = 83 or cate.Storeid = 218 or cate.Storeid = 194 or cate.Storeid = 112 or cate.Storeid = 55 or cate.Storeid = 173 or cate.Storeid = 33)
                        -- and
                            -- cate.Name IN ('Smoking Accessories', 'Shawarma', 'Service Charge', 'Salty Snacks', 'Salad', 'Rich Cut & Fries', 'Rich Cut', 'Protein Bar', 'POP Corn & Cotton Candy', 'Pizza', 'Phone Accessories', 'Package Sweet Snacks', 'Package Ice Cream', 'Package Beverage', 'Other Tobacco', 'Oriental Pastry', 'Offers Rich Cut', 'Offers - Coffe & Croissant', 'Offers', 'Non Edible Grocery', 'Kahwetek', 'Juice', 'Hot Meal', 'Healthy Beauty Care', 'General Merchandise', 'Fresh Fruits', 'French Fries', 'Fountain', 'Feteer', 'Edible Grocery', 'Dounts', 'Delivery', 'Crispy Sandwich', 'Crispy Chicken', 'Corn Dog', 'Cookies', 'Cold Cut', 'Coffee', 'Ck Ice Cream', 'Cigarettes', 'Candy', 'Burger Offer', 'Burger', 'BreakFast', 'Bakery', 'AutoMotive')
						  union all 
						  select * from [192.168.1.210].axdbtest.dbo.ItemPrice
";
                        using (SqlCommand command = new SqlCommand(SQL, connection))
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                using (var package = new ExcelPackage())
                                {
                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AKItemsPricesReport");
                                    int row = 2; // Start from row 2 to leave space for headers
                                    int sheetIndex = 1; // Start with the first sheet
                                    int columnCount = 1;
                                    // Add header row
                                    void AddHeaderRow(ExcelWorksheet ws, int columnCount)
                                    {
                                        int column = 1;
                                        ws.Cells[1, column++].Value = "Site Name";
                                        ws.Cells[1, column++].Value = "International barcode";
                                        ws.Cells[1, column++].Value = "Product description";
                                        ws.Cells[1, column++].Value = "RSP(retail selling price)";
                                        ws.Cells[1, column++].Value = "Original price(if there is a discount)";
                                        ws.Cells[1, column++].Value = "Stock indicator";
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
                                        worksheet.Cells[row, columnCount++].Value = reader["StoreName"];
                                        worksheet.Cells[row, columnCount++].Value = reader["ItemLookupCode"];
                                        worksheet.Cells[row, columnCount++].Value = reader["ItemName"];
                                        worksheet.Cells[row, columnCount++].Value = reader["Price"];
                                        worksheet.Cells[row, columnCount++].Value = 0;
                                        worksheet.Cells[row, columnCount++].Value = reader["Qty"];
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
                                    }
                                    worksheet.Cells.AutoFitColumns();
                                    var stream = new MemoryStream(package.GetAsByteArray());
                                    var stream2 = new MemoryStream(package.GetAsByteArray());
                                    var stream3 = new MemoryStream(package.GetAsByteArray());
                                    var stream4 = new MemoryStream(package.GetAsByteArray());
                                    var stream5 = new MemoryStream(package.GetAsByteArray());
                                    SendEmailWithAttachmentInsta(stream, username);
                                    SendEmailWithAttachmentGehad(stream5, username);
                                    SendEmailWithAttachmentAK(stream2, username);
                                    SendEmailWithAttachmentSaleh(stream3, username);
                                    SendEmailWithAttachmentYoussef(stream4, username);
                                }
                            }
                        }
                        catch (Exception ex) { 
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception details...
                }
            }
        private static void SendEmailWithAttachmentInsta(MemoryStream stream, string username)
        {
            var message = new MimeMessage();
            // message.From.Add(new MailboxAddress("Fouad Esmail", "f.laz@circlekegypt.com"));
            message.From.Add(new MailboxAddress("CircleK Egypt", "circlek1.eg@gmail.com"));
           // message.To.Add(new MailboxAddress("CircleK Egypt", "circlek.eg@outlook.com"));
            //message.From.Add(new MailboxAddress("Ahmed Hosny", "AhmedHosny1340@gmail.com"));
            // Updated the recipient's email address here
            // message.To.Add(new MailboxAddress("Fouad", "F.laz@circlekegypt.com")); 
             message.To.Add(new MailboxAddress("PriceBooks", "Pricebooks@instashop.ae"));
            //message.To.Add(new MailboxAddress("Yousef", "youssef.saber@instashop.ae"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "s.sharaf@circlekegypt.com"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "saleh20000@gmail.com"));
            //message.To.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            // message.To.Add(new MailboxAddress("AK Soft", "f.laz@circlekegypt.com"));
            //message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
            message.Subject = "أسعار الأصناف لجميع الفروع المحدده";

            var bodyBuilder = new BodyBuilder { TextBody = "أسعار الأصناف لجميع الفروع المحدده" };
            bodyBuilder.Attachments.Add("AKItemsPricesReport.xlsx", stream, new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            message.Body = bodyBuilder.ToMessageBody();
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 1000000000; // Set timeout to 10 seconds or longer if needed 

                    //client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    // client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Note: only needed if the SMTP server requires authentication   

                    client.Authenticate("circlek1.eg@gmail.com", "yvdqnqpwphjafhvq");
                    //client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");
                    //client.Authenticate("circlek1.eg@gmail.com", "q0sHxkdsZbms");
                    //client.Authenticate("circlek1.eg@gmail.com", "yvdqnqpwphjafhvq");
                    // client.Authenticate("f.laz@circlekegypt.com", "dizfXNJK3XqG");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private static void SendEmailWithAttachmentAK(MemoryStream stream, string username)
        {
            var message = new MimeMessage();
            // message.From.Add(new MailboxAddress("Fouad Esmail", "f.laz@circlekegypt.com"));
            message.From.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            // message.From.Add(new MailboxAddress("Ahmed Hosny", "AhmedHosny1340@gmail.com"));
            // Updated the recipient's email address here
            // message.To.Add(new MailboxAddress("Fouad", "F.laz@circlekegypt.com")); 
            //message.To.Add(new MailboxAddress("PriceBooks", "Pricebooks@instashop.ae"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "s.sharaf@circlekegypt.com"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "saleh20000@gmail.com"));
            message.To.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            // message.To.Add(new MailboxAddress("AK Soft", "f.laz@circlekegypt.com"));
            //message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
            message.Subject = "أسعار الأصناف لجميع الفروع المحدده";

            var bodyBuilder = new BodyBuilder { TextBody = "أسعار الأصناف لجميع الفروع المحدده" };
            bodyBuilder.Attachments.Add("AKItemsPricesReport.xlsx", stream, new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            message.Body = bodyBuilder.ToMessageBody();
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 1000000000; // Set timeout to 10 seconds or longer if needed
                                                 // client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Note: only needed if the SMTP server requires authentication  

                    //client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");
                    //client.Authenticate("circlek1.eg@gmail.com", "q0sHxkdsZbms");
                    client.Authenticate("circlek1.eg@gmail.com", "yvdqnqpwphjafhvq");
                    // client.Authenticate("f.laz@circlekegypt.com", "dizfXNJK3XqG");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private static void SendEmailWithAttachmentSaleh(MemoryStream stream, string username)
        {
            var message = new MimeMessage();
            // message.From.Add(new MailboxAddress("Fouad Esmail", "f.laz@circlekegypt.com"));
            message.From.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            //message.To.Add(new MailboxAddress("Ahmed Hosny", "AhmedHosny1340@gmail.com"));
            // Updated the recipient's email address here
            // message.To.Add(new MailboxAddress("Fouad", "F.laz@circlekegypt.com")); 
            //message.To.Add(new MailboxAddress("PriceBooks", "Pricebooks@instashop.ae"));
            message.To.Add(new MailboxAddress("Saleh Sharaf", "s.sharaf@circlekegypt.com"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "saleh20000@gmail.com"));
            //message.To.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            // message.To.Add(new MailboxAddress("AK Soft", "f.laz@circlekegypt.com"));
            //message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
            message.Subject = "أسعار الأصناف لجميع الفروع المحدده";

            var bodyBuilder = new BodyBuilder { TextBody = "أسعار الأصناف لجميع الفروع المحدده" };
            bodyBuilder.Attachments.Add("AKItemsPricesReport.xlsx", stream, new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            message.Body = bodyBuilder.ToMessageBody();
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 1000000000; // Set timeout to 10 seconds or longer if needed
                                                 // client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Note: only needed if the SMTP server requires authentication  

                    //client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");
                    //client.Authenticate("circlek1.eg@gmail.com", "q0sHxkdsZbms");
                    client.Authenticate("circlek1.eg@gmail.com", "yvdqnqpwphjafhvq");
                    // client.Authenticate("f.laz@circlekegypt.com", "dizfXNJK3XqG");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private static void SendEmailWithAttachmentYoussef(MemoryStream stream, string username)
        {
            var message = new MimeMessage();
            // message.From.Add(new MailboxAddress("Fouad Esmail", "f.laz@circlekegypt.com"));
            message.From.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            message.To.Add(new MailboxAddress("Youssef Saber", "youssef.saber@instashop.ae"));
            // Updated the recipient's email address here
            // message.To.Add(new MailboxAddress("Fouad", "F.laz@circlekegypt.com")); 
            //message.To.Add(new MailboxAddress("PriceBooks", "Pricebooks@instashop.ae"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "s.sharaf@circlekegypt.com"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "saleh20000@gmail.com"));
            //message.To.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            // message.To.Add(new MailboxAddress("AK Soft", "f.laz@circlekegypt.com"));
            //message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
            message.Subject = "أسعار الأصناف لجميع الفروع المحدده";

            var bodyBuilder = new BodyBuilder { TextBody = "أسعار الأصناف لجميع الفروع المحدده" };
            bodyBuilder.Attachments.Add("AKItemsPricesReport.xlsx", stream, new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            message.Body = bodyBuilder.ToMessageBody();
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 1000000000; // Set timeout to 10 seconds or longer if needed
                                                 // client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Note: only needed if the SMTP server requires authentication  

                    //client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");
                    //client.Authenticate("circlek1.eg@gmail.com", "q0sHxkdsZbms"); 
                    client.Authenticate("circlek1.eg@gmail.com", "yvdqnqpwphjafhvq");
                    // client.Authenticate("f.laz@circlekegypt.com", "dizfXNJK3XqG");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private static void SendEmailWithAttachmentGehad(MemoryStream stream, string username)
        {
            var message = new MimeMessage();
            // message.From.Add(new MailboxAddress("Fouad Esmail", "f.laz@circlekegypt.com"));
            message.From.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            message.To.Add(new MailboxAddress("Gehad", "g.mostafa@circlekegypt.com"));
            // Updated the recipient's email address here
            // message.To.Add(new MailboxAddress("Fouad", "F.laz@circlekegypt.com")); 
            //message.To.Add(new MailboxAddress("PriceBooks", "Pricebooks@instashop.ae"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "s.sharaf@circlekegypt.com"));
            //message.To.Add(new MailboxAddress("Saleh Sharaf", "saleh20000@gmail.com"));
            //message.To.Add(new MailboxAddress("Ahmed Hosny", "circlek1.eg@gmail.com"));
            // message.To.Add(new MailboxAddress("AK Soft", "f.laz@circlekegypt.com"));
            //message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
            message.Subject = "أسعار الأصناف لجميع الفروع المحدده";

            var bodyBuilder = new BodyBuilder { TextBody = "أسعار الأصناف لجميع الفروع المحدده" };
            bodyBuilder.Attachments.Add("AKItemsPricesReport.xlsx", stream, new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            message.Body = bodyBuilder.ToMessageBody();
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 1000000000; // Set timeout to 10 seconds or longer if needed
                                                 // client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Note: only needed if the SMTP server requires authentication  

                    //client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");
                    //client.Authenticate("circlek1.eg@gmail.com", "q0sHxkdsZbms"); 
                    client.Authenticate("circlek1.eg@gmail.com", "yvdqnqpwphjafhvq");
                    // client.Authenticate("f.laz@circlekegypt.com", "dizfXNJK3XqG");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
