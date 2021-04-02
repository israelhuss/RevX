using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using RevXPortal.Models;

namespace RevXPortal.Shared
{
	public class CustomInputSelect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TValue> : InputSelect<TValue>
	{
		protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string validationErrorMessage)
		{
			if (typeof(TValue) == typeof(StudentModel))
			{
				var split = value.Split(',');
				if (split is not null && split.Length == 3)
				{
					var model = new StudentModel()
					{
						Id = int.Parse(split[0]),
						FirstName = split[1],
						LastName = split[2]
					};
					validationErrorMessage = null;
					result = (TValue)(object)model;
					return true;
				}
				else
				{
					validationErrorMessage = "Please select a valid Student";
					result = default;
					return false;
				}
			}
			else if (typeof(TValue) == typeof(ProviderModel))
			{
				var split = value.Split(',');
				if (split is not null && split.Length == 2)
				{
					var model = new ProviderModel()
					{
						Id = int.Parse(split[0]),
						Name = split[1]
					};
					validationErrorMessage = null;
					result = (TValue)(object)model;
					return true;
				}
				else
				{
					validationErrorMessage = "Please select a valid Provider";
					result = default;
					return false;
				}
			}
			else if (typeof(TValue) == typeof(BillingStatusModel))
			{
				var split = value.Split(',');
				if (split is not null && split.Length == 2)
				{
					var model = new BillingStatusModel()
					{
						Id = int.Parse(split[0]),
						BillingStatus = split[1]
					};
					validationErrorMessage = null;
					result = (TValue)(object)model;
					return true;
				}
				else
				{
					validationErrorMessage = "Please select a valid Billing Status";
					result = default;
					return false;
				}
			}
			else
			{
				return base.TryParseValueFromString(value, out result, out validationErrorMessage);
			}
		}
	}
}
