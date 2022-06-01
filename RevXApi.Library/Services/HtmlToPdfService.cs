using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelectPdf;

namespace RevXApi.Library.Services
{
	public class HtmlToPdfService
	{
		public byte[] GetPdfBytes(string value)
		{
			// instantiate the html to pdf converter
			HtmlToPdf converter = new();
			converter.Options.MarginBottom = 30;
			converter.Options.MarginTop = 30;
			// convert the url to pdf
			PdfDocument doc = converter.ConvertHtmlString(value);

			// save pdf document
			var res = doc.Save();

			// close pdf document
			return res;
		}
	}
}
