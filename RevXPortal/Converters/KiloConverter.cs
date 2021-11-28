namespace RevXPortal.Converters
{
	public static class KiloConverter
	{
		public static string KiloFormat(this int num)
		{
			if (num >= 100000000)
				return "$" + (num / 1000000).ToString("#,0M");

			if (num >= 10000000)
				return "$" + (num / 1000000).ToString("0.#") + "M";

			if (num >= 100000)
				return "$" + (num / 1000).ToString("#,0K");

			if (num >= 10000)
				return "$" + (num / 1000).ToString("0.#") + "K";

			if (num >= 1000)
				return "$" + (num / 1000).ToString("0.#") + "K";

			return "$" + num.ToString("#,0");
		}

		public static string KiloFormat(this double num)
		{
			if (num >= 100000000)
				return "$" + (num / 1000000).ToString("#,0M");

			if (num >= 10000000)
				return "$" + (num / 1000000).ToString("0.#") + "M";

			if (num >= 100000)
				return "$" + (num / 1000).ToString("#,0K");

			if (num >= 10000)
				return "$" + (num / 1000).ToString("0.#") + "K";

			if (num >= 1000)
				return "$" + (num / 1000).ToString("0.#") + "K";

			return "$" + num.ToString("#,0");
		}
	}
}
