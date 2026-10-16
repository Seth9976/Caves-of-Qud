using System;
using Newtonsoft.Json;

namespace XRL.Serialization
{
	// Token: 0x0200056C RID: 1388
	public class CommaDelimitedArrayConverter : JsonConverter<string[]>
	{
		// Token: 0x06003C47 RID: 15431 RVA: 0x00126D54 File Offset: 0x00124F54
		public override void WriteJson(JsonWriter writer, string[] value, JsonSerializer serializer)
		{
			int num = ((value != null) ? value.Length : 0);
			if (num == 0)
			{
				writer.WriteValue(null);
				return;
			}
			if (num == 1)
			{
				writer.WriteValue(value[0]);
				return;
			}
			writer.WriteValue(string.Join(',', value));
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x00126D94 File Offset: 0x00124F94
		public override string[] ReadJson(JsonReader reader, Type objectType, string[] existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				return serializer.Deserialize<string[]>(reader);
			}
			string text = (string)reader.Value;
			if (text != null)
			{
				return text.Split(',', StringSplitOptions.None);
			}
			return null;
		}
	}
}
