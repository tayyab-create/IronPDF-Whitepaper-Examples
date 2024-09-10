using System;
using System.IO;
using IronPdf;
using IronQr;
using IronSoftware.Drawing;

class CertificateGenerator
{
    private const string CertificateTemplatePath = "certificateTemplate.html";
    private const string QRImagePath = "qr_code.png";
    private const string PdfFileExtension = "_Certificate.pdf";

    static void Main()
    {
        try
        {
            // Activate licenses for IronPDF and IronQR
            InitializeLicenses();

            // Get user input for certificate details
            var studentName = GetInput("Enter Student Name");
            var courseTitle = GetInput("Enter Course Title");
            var completionDate = GetInput("Enter Completion Date (MM/DD/YYYY)");

            // Generate QR Code
            string qrFilePath = GenerateQRCode(studentName, courseTitle, completionDate);

            // Generate PDF certificate
            string certificatePath = GenerateCertificate(studentName, courseTitle, completionDate, qrFilePath);

            Console.WriteLine($"Certificate generated successfully: {certificatePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void InitializeLicenses()
    {
        // Replace these keys with your actual IronPDF and IronQR licenses
        IronPdf.License.LicenseKey = "YOUR_IRONPDF_LICENSE_KEY";
        IronQr.License.LicenseKey = "YOUR_IRONQR_LICENSE_KEY";
    }

    private static string GetInput(string prompt)
    {
        string input;
        do
        {
            Console.Write($"{prompt}: ");
            input = Console.ReadLine();
        }
        while (string.IsNullOrEmpty(input));  // Ensure valid input
        return input;
    }

    private static string GenerateQRCode(string studentName, string courseTitle, string completionDate)
    {
        string verificationUrl = $"https://example.com/verify?student={studentName}&course={courseTitle}&date={completionDate}";

        try
        {
            QrCode qrCode = QrWriter.Write(verificationUrl);
            AnyBitmap qrImage = qrCode.Save();
            qrImage.SaveAs(QRImagePath);
            return QRImagePath;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating QR code.", ex);
        }
    }

    private static string GenerateCertificate(string studentName, string courseTitle, string completionDate, string qrFilePath)
    {
        try
        {
            // Load and fill the HTML template
            string htmlTemplate = File.ReadAllText(CertificateTemplatePath);
            string filledHtml = FillTemplate(htmlTemplate, studentName, courseTitle, completionDate, qrFilePath);

            // Generate PDF using IronPDF
            var Renderer = new ChromePdfRenderer();
            var pdfDocument = Renderer.RenderHtmlAsPdf(filledHtml);

            // Save the PDF to a file
            string pdfFilePath = $"{studentName}{PdfFileExtension}";
            pdfDocument.SaveAs(pdfFilePath);
            return pdfFilePath;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating PDF certificate.", ex);
        }
    }

    private static string FillTemplate(string template, string studentName, string courseTitle, string completionDate, string qrFilePath)
    {
        return template.Replace("{{STUDENT_NAME}}", studentName)
                       .Replace("{{COURSE_TITLE}}", courseTitle)
                       .Replace("{{COMPLETION_DATE}}", completionDate)
                       .Replace("{{QR_CODE}}", qrFilePath);
    }
}
