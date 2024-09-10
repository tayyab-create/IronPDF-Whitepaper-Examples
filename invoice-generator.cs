using System;
using System.IO;
using IronPdf;
using IronBarCode;

class InvoiceGenerator
{
    static void Main()
    {
        try
        {
            // Activate IronPDF and IronBarcode license
            IronPdf.License.LicenseKey = "YOUR_IRONPDF_LICENSE_KEY";
            IronBarCode.License.LicenseKey = "YOUR_IRONBARCODE_LICENSE_KEY";

            // Retrieve dynamic order details (for example purposes, hardcoded)
            string customerName = "John Doe";
            string orderDate = DateTime.Now.ToShortDateString();
            string orderDetails = "<tr><td>Product A</td><td>2</td><td>$50.00</td></tr>"
                                + "<tr><td>Product B</td><td>1</td><td>$25.00</td></tr>";
            decimal orderTotal = 125.00m;

            // Generate a unique barcode for the invoice
            string invoiceNumber = Guid.NewGuid().ToString();  // This should be the unique order/invoice number
            GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(invoiceNumber, BarcodeEncoding.Code128);
            string barcodePath = "barcode.png";
            barcode.SaveAsPng(barcodePath);

            // Load HTML template and replace placeholders with dynamic content
            string htmlTemplate = File.ReadAllText("invoiceTemplate.html");
            htmlTemplate = htmlTemplate.Replace("{{CUSTOMER_NAME}}", customerName)
                                       .Replace("{{ORDER_DATE}}", orderDate)
                                       .Replace("{{ORDER_DETAILS}}", orderDetails)
                                       .Replace("{{ORDER_TOTAL}}", orderTotal.ToString("F2"))
                                       .Replace("{{BARCODE_IMAGE}}", barcodePath);

            // Generate PDF using IronPDF
            var Renderer = new ChromePdfRenderer();
            var pdfDocument = Renderer.RenderHtmlAsPdf(htmlTemplate);

            // Save the final PDF invoice
            string pdfFilePath = $"Invoice_{invoiceNumber}.pdf";
            pdfDocument.SaveAs(pdfFilePath);

            Console.WriteLine($"Invoice generated successfully: {pdfFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
