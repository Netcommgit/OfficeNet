using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeNet.Infrastructure.Mapping
{
    public class DdMmYyyyDateConverter : JsonConverter<DateTime?>
    {
        private const string Format = "dd-MM-yyyy";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (DateTime.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            throw new JsonException($"Invalid date format. Use {Format}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(Format));
        }
    }
}
