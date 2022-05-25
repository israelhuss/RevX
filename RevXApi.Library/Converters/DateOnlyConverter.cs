﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace RevXApi.Library.Converters
{
	public class DateOnlyConverter : JsonConverter<DateOnly>
	{
		public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			try
			{
				if (reader.TryGetDateTime(out var dt))
				{
					return DateOnly.FromDateTime(dt);
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			string value = null;
			try
            {
				value = reader.GetString();
            }
			catch (Exception)
            {
				reader.Read();
            }
			if (value == null)
			{
				return default;
			}
			var match = new Regex("^(\\d\\d\\d\\d)-(\\d\\d)-(\\d\\d)(T|\\s|\\z)").Match(value);
			return match.Success
				? new DateOnly(int.Parse(match.Groups[ 1 ].Value), int.Parse(match.Groups[ 2 ].Value), int.Parse(match.Groups[ 3 ].Value))
				: default;
		}

		public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
			=> writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
	}

	public class DateOnlyNullableConverter : JsonConverter<DateOnly?>
	{
		public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TryGetDateTime(out var dt))
			{
				return DateOnly.FromDateTime(dt);
			};
			var value = reader.GetString();
			if (value == null)
			{
				return default;
			}
			var match = new Regex("^(\\d\\d\\d\\d)-(\\d\\d)-(\\d\\d)(T|\\s|\\z)").Match(value);
			return match.Success
				? new DateOnly(int.Parse(match.Groups[ 1 ].Value), int.Parse(match.Groups[ 2 ].Value), int.Parse(match.Groups[ 3 ].Value))
				: default;
		}

		public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
			=> writer.WriteStringValue(value?.ToString("yyyy-MM-dd"));
	}

	public static class DateOnlyConverterExtensions
	{
		public static void AddDateOnlyConverters(this JsonSerializerOptions options)
		{
			options.Converters.Add(new DateOnlyConverter());
			options.Converters.Add(new DateOnlyNullableConverter());
		}
	}
}
