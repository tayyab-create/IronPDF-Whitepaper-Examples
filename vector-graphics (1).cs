using IronPdf;

class Program
{
    static void Main()
    {
        // Activate your IronPDF license
        IronPdf.License.LicenseKey = "YOUR_LICENSE_KEY";

        // Create a new HtmlToPdf renderer
        var Renderer = new ChromePdfRenderer();

        // HTML content with an embedded SVG
        string htmlContent = @"
        <html>
        <body>
            <h1>Embedding SVG in PDF</h1>
            <p>Here is a vector-based logo:</p>
            <svg width='200' height='200' xmlns='http://www.w3.org/2000/svg'>
                <circle cx='100' cy='100' r='80' stroke='green' stroke-width='4' fill='yellow' />
                <text x='50%' y='50%' dominant-baseline='middle' text-anchor='middle' font-size='20'>Logo</text>
            </svg>
        </body>
        </html>";

        // Render the HTML to a PDF document
        var pdfDocument = Renderer.RenderHtmlAsPdf(htmlContent);

        // Save the PDF document
        pdfDocument.SaveAs("SvgEmbeddedPDF.pdf");
    }
}
