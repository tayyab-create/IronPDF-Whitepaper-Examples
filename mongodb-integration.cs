using MongoDB.Driver;
using IronPdf;
using System;
using MongoDB.Bson;

class Program
{
    static void Main()
    {
        // Activate your IronPDF license
        IronPdf.License.LicenseKey = "YOUR_LICENSE_KEY";

        // MongoDB connection string
        string connectionString = "your_mongodb_connection_string";
        var client = new MongoClient(connectionString);

        // Connect to the MongoDB database and collection
        var database = client.GetDatabase("your_database_name");
        var collection = database.GetCollection<BsonDocument>("your_collection_name");

        // Query to retrieve data
        var documents = collection.Find(_ => true).ToList();

        // Prepare HTML content for the PDF
        string htmlContent = "<html><body><h1>Data Report</h1><table border='1'><tr><th>Name</th><th>Value</th></tr>";

        // Loop through the retrieved data and add it to the HTML
        foreach (var document in documents)
        {
            string name = document["Name"].AsString;
            string value = document["Value"].AsString;
            htmlContent += $"<tr><td>{name}</td><td>{value}</td></tr>";
        }

        htmlContent += "</table></body></html>";

        // Generate the PDF using IronPDF
        var Renderer = new ChromePdfRenderer();
        var pdfDocument = Renderer.RenderHtmlAsPdf(htmlContent);

        // Save the generated PDF
        pdfDocument.SaveAs("DataReport.pdf");
    }
}
