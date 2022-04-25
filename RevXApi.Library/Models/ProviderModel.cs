namespace RevXApi.Library.Models
{
	public class ProviderModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public bool IsDefault { get; set; }
	}
}
