using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using CourierManagementSystem.Models.Entities;

namespace CourierManagementSystem.Web.Services
{
    public class PdfGenerationService
    {
        public byte[] GeneratePdfReport(IEnumerable<Consignment> consignments)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                writer.PageEvent = new MyHeaderEvent(); // Remove footer

                document.Open();
                document.AddTitle("Consignment Report");

                PdfPTable table = new PdfPTable(5);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HeaderRows = 1;

                Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD); // Header font

                PdfPCell headerCell = new PdfPCell(new Phrase("Tracking Number", headerFont));
                table.AddCell(headerCell);

                headerCell = new PdfPCell(new Phrase("Recipient Name", headerFont));
                table.AddCell(headerCell);

                headerCell = new PdfPCell(new Phrase("Delivery Address", headerFont));
                table.AddCell(headerCell);

                headerCell = new PdfPCell(new Phrase("Estimated Delivery Date", headerFont));
                table.AddCell(headerCell);

                headerCell = new PdfPCell(new Phrase("Status", headerFont));
                table.AddCell(headerCell);

                foreach (var consignment in consignments)
                {
                    table.AddCell(new PdfPCell(new Phrase(consignment.TrackingNumber.ToString())));
                    table.AddCell(new PdfPCell(new Phrase(consignment.RecipientName.ToString())));
                    table.AddCell(new PdfPCell(new Phrase(consignment.DeliveryAddress.ToString())));
                    table.AddCell(new PdfPCell(new Phrase(consignment.EstimatedDeliveryDate.ToShortDateString() + " " + consignment.EstimatedDeliveryDate.ToShortTimeString())));
                    table.AddCell(new PdfPCell(new Phrase(consignment.Status.ToString())));
                }

                // Add the table to the document after populating it for all consignments
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
                float titleX = (pageWidth - FONT.GetCalculatedBaseFont(true).GetWidthPoint("Consignment Report", 18)) / 2;

                ColumnText.ShowTextAligned(
                    canvas, Element.ALIGN_CENTER, // Center-align the title
                    new Phrase("Consignment Report", FONT), titleX, 810, 0
                );
            }
        }
    }
}
