using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ClientController : ControllerBase
	{
		private readonly IClientData _clientData;

		public ClientController(IClientData clientData)
		{
			_clientData = clientData;
		}

		[HttpGet]
		public List<ClientModel> GetAll()
		{
			return _clientData.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpGet("enabled")]
		public List<ClientModel> GetEnabled()
		{
			return _clientData.GetEnabled(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpPost("add")]
		public void Add(ClientModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_clientData.AddClient(model);
		}

		[HttpPost("edit")]
		public void Edit(ClientModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_clientData.EditClient(model);
		}

		[HttpGet("{id}")]
		public ClientModel GetById(int id)
		{
			return _clientData.GetById(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}
