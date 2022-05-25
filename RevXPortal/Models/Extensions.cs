using System.Reflection;

namespace RevXPortal.Models
{
	static class Extensions
	{
		public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
		{
			return listToClone.Select(item => (T)item.Clone()).ToList();
		}
		public static string GetDescription(this Enum GenericEnum)
		{
			Type genericEnumType = GenericEnum.GetType();
			MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
			if (( memberInfo != null && memberInfo.Length > 0 ))
			{
				var _Attribs = memberInfo[ 0 ].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
				if (_Attribs != null && _Attribs.Count() > 0)
				{
					return ( (System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0) ).Description;
				}
			}
			return GenericEnum.ToString();
		}

		public static TAttribute GetAttribute<TAttribute>(this Enum value)
		where TAttribute : Attribute
		{
			var enumType = value.GetType();
			var name = Enum.GetName(enumType, value);
			return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
		}

		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}

		public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			int diff = ( 7 + ( dt.DayOfWeek - startOfWeek ) ) % 7;
			return dt.AddDays(-1 * diff).Date;
		}
		public static DateOnly StartOfWeek(this DateOnly dt, DayOfWeek startOfWeek)
		{
			int diff = ( 7 + ( dt.DayOfWeek - startOfWeek ) ) % 7;
			return dt.AddDays(-1 * diff);
		}

		public static T GetAtIndex<T>(this List<T> list, int index, T element = default)
		{
			string typeName = typeof(T).Name;
			if (index >= list.Count)
			{
				if (element is null)
					element = default(T);
				int amountToAdd = list.Count - ( index - 1 );
				list.AddRange(Enumerable.Repeat(element, amountToAdd));
			}
			T toReturn = list[ index ];
			return toReturn;
		}
	}
}
