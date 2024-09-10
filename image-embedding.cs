using IronPdf;

class Program
{
    static void Main()
    {
        // Activate IronPDF License
        IronPdf.License.LicenseKey = "YOUR_LICENSE_KEY";

        // Initialize HTML to PDF Renderer
        var Renderer = new ChromePdfRenderer();

        // HTML content with embedded images
        string htmlContent = @"
        <html>
        <body>
            <h1>Image Embedding in PDF</h1>
            <p>Embedding a JPEG image below:</p>
            <img src='https://example.com/image.jpg' width='400' height='300'/>
            <p>Embedding a PNG image below:</p>
            <img src='https://example.com/image.png' width='400' height='300'/>
        </body>
        </html>";

        // Render the HTML to a PDF document
        var pdfDocument = Renderer.RenderHtmlAsPdf(htmlContent);

        // Save the generated PDF
        pdfDocument.SaveAs("EmbeddedImagesPDF.pdf");
    }
}
