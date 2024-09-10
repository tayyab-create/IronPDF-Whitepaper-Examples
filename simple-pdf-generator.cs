using IronPdf;
using System;
using System.IO;

class SimplePdfGenerator
{
    public void GenerateSimplePdfExamples()
    {
        // Set the license key
        IronPdf.License.LicenseKey = "YOUR-LICENSE-KEY-HERE";

        // Common renderer
        var renderer = new ChromePdfRenderer();

        // 1. HTML String to PDF
        string htmlContent = "<h1>Hello, IronPDF!</h1><p>This is a simple HTML to PDF conversion.</p>";
        var pdfFromHtmlString = renderer.RenderHtmlAsPdf(htmlContent);
        pdfFromHtmlString.SaveAs("output_from_html_string.pdf");
        Console.WriteLine("PDF from HTML string created.");

        // 2. HTML File to PDF
        string htmlFilePath = "sample.html";
        File.WriteAllText(htmlFilePath, "<html><body><h1>HTML File to PDF</h1><p>Converting a local HTML file to PDF.</p></body></html>");
        var pdfFromHtmlFile = renderer.RenderHtmlFileAsPdf(htmlFilePath);
        pdfFromHtmlFile.SaveAs("output_from_html_file.pdf");
        Console.WriteLine("PDF from HTML file created.");

        // 3. URL to PDF
        string url = "https://www.example.com";
        var pdfFromUrl = renderer.RenderUrlAsPdf(url);
        pdfFromUrl.SaveAs("output_from_url.pdf");
        Console.WriteLine("PDF from URL created.");

        // Clean up temporary HTML file
        File.Delete(htmlFilePath);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var generator = new SimplePdfGenerator();
        generator.GenerateSimplePdfExamples();
    }
}
