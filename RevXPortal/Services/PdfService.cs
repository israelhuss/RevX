using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Services
{
	public class PdfService : IPdfService
	{
		public void Write(string html)
		{
			//FileInfo file = new("Invoice");
			//PdfDocument pdf = new(new PdfWriter(file));
			//using (Document doc = new(pdf))
			//{
			//}
			//string path = Directory.GetCurrentDirectory();
			//Console.WriteLine(Directory.GetCurrentDirectory());
			//HtmlConverter.ConvertToPdf(html, new FileStream(file, FileMode.Create));
			MemoryStream ms = new();
			PdfWriter writer = new PdfWriter(ms);
			PdfDocument pdf = new(writer);
			HtmlConverter.ConvertToPdf(html, writer);

			var x = pdf.GetDocumentInfo();
			using (FileStream pdfDest = File.Open("output.pdf", FileMode.OpenOrCreate))
			{
				ConverterProperties converterProperties = new ConverterProperties();
				HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
			}

			//// create the HTML to PDF converter
			//HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
			//// set PDF page size and orientation
			//htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
			//htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation();
			//// set PDF page margins
			//htmlToPdfConverter.Document.Margins = new PdfMargins(0);
			//// convert HTML to 
			//PDFbyte[] pdfBuffer = null;
			//if (radioButtonConvertUrl.Checked)
			//{
			//	// convert URL to a PDF memory buffer
			//	string url = textBoxUrl.Text;
			//	pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
			//}
			//else
			//{
			//	// convert HTML code    
			//	string htmlCode = textBoxHtmlCode.Text;
			//	string baseUrl = textBoxBaseUrl.Text;
			//	// convert HTML code to a PDF memory buffer    
			//	pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
			//}
			//// inform the browser about the binary data 
			//formatHttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");
			//// set how to open the PDF document, attachment or inline, and the file name
			//HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}", checkBoxOpenInline.Checked ? "inline" : "attachment", pdfBuffer.Length.ToString()));
			//// write the PDF buffer to HTTP response
			//HttpContext.Current.Response.BinaryWrite(pdfBuffer);
			//// call End() method of HTTP response to stop ASP.NET page processing
			//HttpContext.Current.Response.End();
		}
	}
}
