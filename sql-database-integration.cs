using IronPdf;
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Activate your license
        IronPdf.License.LicenseKey = "YOUR_LICENSE_KEY";

        // Connect to SQL database
        string connectionString = "your_connection_string_here";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT Name, Amount FROM InvoiceTable", conn);
            SqlDataReader reader = command.ExecuteReader();

            // HTML template for the PDF
            string htmlContent = "<html><body><h1>Invoice Report</h1><table><tr><th>Name</th><th>Amount</th></tr>";

            while (reader.Read())
            {
                string name = reader["Name"].ToString();
                string amount = reader["Amount"].ToString();
                htmlContent += $"<tr><td>{name}</td><td>{amount}</td></tr>";
            }

            htmlContent += "</table></body></html>";

            // Generate the PDF
            var Renderer = new ChromePdfRenderer();
            var pdfDocument = Renderer.RenderHtmlAsPdf(htmlContent);

            // Save the PDF
            pdfDocument.SaveAs("InvoiceReport.pdf");
        }
    }
}
