using IronPdf;

class Program
{
    static void Main()
    {
        // Activate your license
        IronPdf.License.LicenseKey = "YOUR_LICENSE_KEY";

        // Create a new PDF renderer
        var renderer = new ChromePdfRenderer();

        // Define custom headers and footers with HTML
        renderer.RenderingOptions.TextHeader = new TextHeaderFooter()
        {
            CenterText = "Document Title",
            RightText = "{date}",
            DrawDividerLine = true
        };

        renderer.RenderingOptions.TextFooter = new TextHeaderFooter()
        {
            CenterText = "Page {page} of {total-pages}",
            DrawDividerLine = true
        };

        // Enable pagination by ensuring the content fits and breaks correctly across pages
        renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;

        // Render HTML content to PDF
        var htmlContent = @"
        <html>
            <head>
                <style>
                    body { font-family: Arial, sans-serif; }
                    h1 { page-break-before: always; }
                </style>
            </head>
            <body>
                <h1>Page 1</h1>
                <p>This is the content for the first page.</p>
                <h1>Page 2</h1>
                <p>This content starts on the second page.</p>
            </body>
        </html>";

        // Render the HTML to PDF
        var pdf = renderer.RenderHtmlAsPdf(htmlContent);

        // Save the generated PDF
        pdf.SaveAs("PaginatedDocument.pdf");
    }
}
