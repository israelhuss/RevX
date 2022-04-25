namespace RevXPortal.Models
{
	public class BillingStatusModel : ISelectionFriendly, IEnable
	{
		public int Id { get; set; }
		public string BillingStatus { get; set; }
		public bool Enabled { get; set; }


		public string DisplayName => BillingStatus;
		public bool isEditMode { get; set; }
		public bool IsDefault { get; set; }
	}
}
