﻿namespace RevXPortal.Models
{
	public interface ISelectionFriendly
	{
		int Id { get; set; }
		string DisplayName { get; }
		bool IsDefault { get; set; }
	}
}