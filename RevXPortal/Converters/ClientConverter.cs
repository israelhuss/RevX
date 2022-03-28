using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RevXPortal.Converters
{

	// NOT IN USE!!!

	public class ClientConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override ClientModel ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var result = new ClientModel();
			var split = value.ToString().Split(',');
			if (split is not null && split.Length == 3)
			{
				result.Id = int.Parse(split[ 0 ]);
				result.FirstName = split[ 1 ];
				result.LastName = split[ 2 ];

				return result;
			}
			else
			{
				return null;
			}
		}
	}

}
