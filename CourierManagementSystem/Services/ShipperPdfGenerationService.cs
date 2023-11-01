using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using CourierManagementSystem.Models.Entities;

namespace CourierManagementSystem.Web.Services
{
    public class ShipperPdfGenerationService
    {
        public byte[] GeneratePdfReport(IEnumerable<Shipper> shippers)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                writer.PageEvent = new MyHeaderEvent(); // Remove footer

                document.Open();
                document.AddTitle("Shipper Report");

                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HeaderRows = 1;

                Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD); // Header font

                PdfPCell headerCell = new PdfPCell(new Phrase("Company Name", headerFont));
                table.AddCell(headerCell);

                headerCell = new PdfPCell(new Phrase("Contact Person Name", headerFont));
                table.AddCell(headerCell);

                headerCell = new PdfPCell(new Phrase("Contact Person Phone", headerFont));
                table.AddCell(headerCell);

                foreach (var shipper in shippers)
                {
                    table.AddCell(new PdfPCell(new Phrase(shipper.CompanyName)));
                    table.AddCell(new PdfPCell(new Phrase(shipper.ContactPersonName)));
                    table.AddCell(new PdfPCell(new Phrase(shipper.ContactPersonPhone)));
                }

                // Add the table to the document after populating it for all shippers
                document.Add(table);

                document.Close();
                return ms.ToArray();
            }
        }

        class MyHeaderEvent : PdfPageEventHelper
        {
            Font FONT = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD);

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte canvas = writer.DirectContent;

                // Get the width of the page
                float pageWidth = document.PageSize.Width;

                // Calculate the X-coordinate to center-align the title
                float titleX = (pageWidth - FONT.GetCalculatedBaseFont(true).GetWidthPoint("Shipper Report", 18)) / 2;

                ColumnText.ShowTextAligned(
                    canvas, Element.ALIGN_CENTER, // Center-align the title
                    new Phrase("Shipper Report", FONT), titleX, 810, 0
                );
            }
        }
    }
}
