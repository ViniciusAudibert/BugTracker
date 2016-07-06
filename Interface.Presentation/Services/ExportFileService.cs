using Interface.Presentation.Extensions;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Interface.Presentation.Services
{
    public static class ExportFileService
    {
        public static MemoryStream TableHtmlToPdf(ICollection<BugTracker.Domain.Entity.BugTracker> bugTrackers)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var documentation = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(documentation, memoryStream))
                    {
                        documentation.Open();

                        var html = RazorViewToString.TableTrackingToString(bugTrackers.FromModel());

                        using (var htmlWorker = new HTMLWorker(documentation))
                        {
                            using (var sr = new StringReader(html))
                            {
                                htmlWorker.Parse(sr);
                            }
                        }

                        documentation.Close();
                    }
                }
                return memoryStream;
            }
        }

        public static Stream TableHtmlToTxt(ICollection<BugTracker.Domain.Entity.BugTracker> bugTrackers)
        {
            MemoryStream memoryStream = TableHtmlToPdf(bugTrackers);

            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(memoryStream.ToArray()))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);
                    string[] theLines = thePage.Split('\n');
                    foreach (var theLine in theLines)
                    {
                        text.AppendLine(theLine);
                    }
                }

                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);

                writer.Write(text.ToString());

                writer.Flush();

                stream.Position = 0;

                return stream;
            }
        }
    }
}