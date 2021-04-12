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

			using (FileStream pdfDest = File.Open("output.pdf", FileMode.OpenOrCreate))
			{
				ConverterProperties converterProperties = new ConverterProperties();
				HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
			}
		}
	}
}
