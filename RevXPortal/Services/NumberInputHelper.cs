using System.Text.RegularExpressions;

namespace RevXPortal.Services
{
	public static class NumberInputHelper
	{
		public static string Validate(int max, string oldVal, string newVal)
		{
			return privateValidate(max, 0, oldVal, newVal);
		}

		public static string Validate(int max, int min, string oldVal, string newVal)
		{
			return privateValidate(max, min, oldVal, newVal);

		}

		private static string privateValidate(int max, int min, string oldVal, string newVal)
		{
			string maxStr = max.ToString();
			string validNums = Regex.Match(oldVal, ".*[^-]").Value;
			int validNumCount = validNums.Length;
			int finalLength = max.ToString().Length;
			int position = finalLength - validNumCount;
			if (int.Parse(newVal) > int.Parse(maxStr[ position ].ToString()))
			{
				return $"It will be greater, giving you back {validNums + "0" + newVal}";

			}
			return $"the valid numbers length is: {validNumCount}, and the final length should be {finalLength}, so the position you are trying to enter is: {position}. i think i have to add {position - 1} zeros.";
		}
	}
}
