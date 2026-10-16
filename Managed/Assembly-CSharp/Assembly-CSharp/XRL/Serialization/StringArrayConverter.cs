using System;
using Newtonsoft.Json;

namespace XRL.Serialization
{
	// Token: 0x02000574 RID: 1396
	public class StringArrayConverter : JsonConverter<string[]>
	{
		// Token: 0x06003C56 RID: 15446 RVA: 0x00127054 File Offset: 0x00125254
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
			writer.WriteStartArray();
			for (int i = 0; i < num; i++)
			{
				writer.WriteValue(value[i]);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x001270A8 File Offset: 0x001252A8
		public override string[] ReadJson(JsonReader reader, Type objectType, string[] existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				return serializer.Deserialize<string[]>(reader);
			}
			string text = (string)reader.Value;
			if (text != null)
			{
				return new string[] { text };
			}
			return null;
		}
	}
}
