namespace RevXPortal.Models
{
	public class StudentModel : ISelectionFriendly, IEnable
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Enabled { get; set; }
		public bool isEditMode { get; set; } = false;
		public string DisplayName => $"{FirstName} {LastName}";
	}
}
