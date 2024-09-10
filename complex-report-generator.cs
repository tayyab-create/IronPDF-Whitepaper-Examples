using IronPdf;
using System;
using System.Drawing;
using System.IO;

class Program
{
    static void Main()
    {
        // Activate IronPDF license if needed
        IronPdf.License.LicenseKey = "YOUR_LICENSE_KEY";

        // Step 1: Load and populate the HTML template
        string htmlTemplate = File.ReadAllText("reportTemplate.html");
        htmlTemplate = htmlTemplate.Replace("{{YEAR}}", DateTime.Now.Year.ToString())
                                   .Replace("{{INTRO_TEXT}}", "This report provides a comprehensive analysis of the data...");

        // Step 2: Generate a dynamic data table
        string dataTable = "<tr><td>Total Sales</td><td>$500,000</td></tr>" +
                           "<tr><td>New Customers</td><td>3,200</td></tr>";
        htmlTemplate = htmlTemplate.Replace("{{DATA_TABLE}}", dataTable);

        // Step 3: Create a chart and save it as an image
        string chartImagePath = GenerateChart();
        htmlTemplate = htmlTemplate.Replace("{{CHART_IMAGE}}", chartImagePath);

        // Step 4: Add image placeholders
        htmlTemplate = htmlTemplate.Replace("{{IMAGE1}}", "image1.png")
                                   .Replace("{{IMAGE2}}", "image2.png");

        // Step 5: Convert the HTML to PDF using IronPDF
        var Renderer = new ChromePdfRenderer();
        var pdfDocument = Renderer.RenderHtmlAsPdf(htmlTemplate);

        // Save the generated PDF
        pdfDocument.SaveAs("ComplexReport.pdf");

        Console.WriteLine("PDF report generated successfully.");
    }

    // A simple method to generate a placeholder chart and save it as a PNG
    static string GenerateChart()
    {
        string chartPath = "chart.png";

        using (Bitmap bmp = new Bitmap(500, 300))
        using (Graphics graph = Graphics.FromImage(bmp))
        {
            graph.Clear(Color.White);
            Pen pen = new Pen(Color.Blue, 2);
            graph.DrawLine(pen, 50, 50, 450, 250);
            graph.DrawRectangle(Pens.Black, 50, 50, 400, 200);
            bmp.Save(chartPath);
        }

        return chartPath;
    }
}
