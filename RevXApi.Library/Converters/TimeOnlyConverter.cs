using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RevXApi.Library.Converters
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out var dt))
            {
                return TimeOnly.FromDateTime(dt);
            };
            var value = reader.GetString();
            if (value == null)
            {
                return default;
            }
            var match = new Regex("^(?<Hour>2[0-3]|1[1-9]|0?[1-9]):(?<Minutes>[0-5][0-9]):?(?<Seconds>[0-5][0-9])? ?(?<AmPm>AM|PM)?$", RegexOptions.IgnoreCase).Match(value);
			string AmPm = match.Groups[ "AmPm" ].Value;
            int hours;
            if (AmPm == null)
			{
                hours = int.Parse(match.Groups[ "Hour" ].Value);
			}
			else
			{
                var rawHour = int.Parse(match.Groups[ "Hour" ].Value);
                hours = AmPm.ToLower() == "pm" ? rawHour - 12 : rawHour;
            }
            int minutes = int.Parse(match.Groups[ "Minutes" ].Value);
            int seconds = int.Parse(match.Groups[ "Seconds" ].Value);
            return match.Success
                ? new TimeOnly(hours, minutes, seconds)
                : default;
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("HH:mm:ss"));
    }

    public class TimeOnlyNullableConverter : JsonConverter<TimeOnly?>
    {
        public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out var dt))
            {
                return TimeOnly.FromDateTime(dt);
            };
            var value = reader.GetString();
            if (value == null)
            {
                return default;
            }
            var match = new Regex("^(?<Hour>2[0-3]|1[1-9]|0?[1-9]):(?<Minutes>[0-5][0-9]):?(?<Seconds>[0-5][0-9])? ?(?<AmPm>AM|PM)?$", RegexOptions.IgnoreCase).Match(value);
            string AmPm = match.Groups[ "AmPm" ].Value;
            int hours;
            if (AmPm == null)
            {
                hours = int.Parse(match.Groups[ "Hour" ].Value);
            }
            else
            {
                var rawHour = int.Parse(match.Groups[ "Hour" ].Value);
                hours = AmPm.ToLower() == "pm" ? rawHour - 12 : rawHour;
            }
            int minutes = int.Parse(match.Groups[ "Minutes" ].Value);
            int seconds = int.Parse(match.Groups[ "Seconds" ].Value);
            return match.Success
                ? new TimeOnly(hours, minutes, seconds)
                : default;
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.HasValue ? value.Value.ToString("HH:mm:ss") : "null");
    }

    public static class TimeOnlyConverterExtensions
    {
        public static void AddTimeOnlyConverters(this JsonSerializerOptions options)
        {
            options.Converters.Add(new TimeOnlyConverter());
            options.Converters.Add(new TimeOnlyNullableConverter());
        }
    }
}
