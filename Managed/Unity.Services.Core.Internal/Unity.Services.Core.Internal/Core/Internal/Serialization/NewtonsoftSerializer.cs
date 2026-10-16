using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Unity.Services.Core.Internal.Serialization
{
	// Token: 0x0200005A RID: 90
	internal class NewtonsoftSerializer : IJsonSerializer
	{
		// Token: 0x0600019C RID: 412 RVA: 0x000041E9 File Offset: 0x000023E9
		public NewtonsoftSerializer(JsonSerializerSettings settings = null)
			: this(JsonSerializer.Create(settings))
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000041F7 File Offset: 0x000023F7
		internal NewtonsoftSerializer(JsonSerializer serializer)
		{
			this.m_Serializer = serializer;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004208 File Offset: 0x00002408
		public string SerializeObject<T>(T value)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(new StringBuilder(256), CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.Formatting = this.m_Serializer.Formatting;
					this.m_Serializer.Serialize(jsonTextWriter, value, typeof(T));
					text = stringWriter.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004298 File Offset: 0x00002498
		public T DeserializeObject<T>(string value)
		{
			T t;
			using (JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(value)))
			{
				t = (T)((object)this.m_Serializer.Deserialize(jsonTextReader, typeof(T)));
			}
			return t;
		}

		// Token: 0x0400007A RID: 122
		private readonly JsonSerializer m_Serializer;
	}
}
