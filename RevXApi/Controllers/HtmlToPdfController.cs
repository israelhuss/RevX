using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SelectPdf;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HtmlToPdfController : ControllerBase
	{
		public record HtmlRequestContent
		{
			public string Key { get; set; }
			public string Value { get; set; }
		}
		[HttpPost]
		public IActionResult GetPdfBytes([FromBody] HtmlRequestContent content)
		{
			// instantiate the html to pdf converter
			HtmlToPdf converter = new();
			converter.Options.MarginBottom = 30;
			converter.Options.MarginTop = 30;
			converter.Options.MarginLeft = 30;
			converter.Options.MarginRight = 30;
			converter.Options.EmbedFonts = true;
			converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
			// convert the url to pdf
			PdfDocument doc = converter.ConvertHtmlString(content.Value);
			// save pdf document
			var res = doc.Save();

			return File(res, "application/pdf", "1.pdf");
		}
		[HttpPost]
		[Route("form")]
		public IActionResult GetPdfBytesForm([FromForm] string html)
		{
			// instantiate the html to pdf converter
			HtmlToPdf converter = new();
			converter.Options.MarginBottom = 30;
			converter.Options.MarginTop = 30;
			converter.Options.MarginLeft = 30;
			converter.Options.MarginRight = 30;
			converter.Options.EmbedFonts = true;
			converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
			// convert the url to pdf
			PdfDocument doc = converter.ConvertHtmlString(html);
			// save pdf document
			var res = doc.Save();

			return File(res, "application/pdf", "1.pdf");
		}
	}
}
