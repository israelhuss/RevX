namespace RevXPortal.Models
{
	public class ProviderModel : ISelectionFriendly, IEnable
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }

		public string DisplayName => Name;
		public bool isEditMode { get; set; }
	}
}
