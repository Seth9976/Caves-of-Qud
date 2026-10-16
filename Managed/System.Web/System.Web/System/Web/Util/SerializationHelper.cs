using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace System.Web.Util
{
	// Token: 0x02000144 RID: 324
	internal class SerializationHelper
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00027B6F File Offset: 0x00025D6F
		internal string SerializeToBase64(object value)
		{
			return Convert.ToBase64String(this.SerializeToBinary(value));
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00027B7D File Offset: 0x00025D7D
		internal object DeserializeFromBase64(string value)
		{
			return this.DeserializeFromBinary(Convert.FromBase64String(value));
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00027B8C File Offset: 0x00025D8C
		internal string SerializeToXml(object value)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new XmlSerializer(typeof(object), "http://www.nauck-it.de/PostgreSQLProvider").Serialize(memoryStream, value);
				text = Convert.ToBase64String(memoryStream.ToArray());
			}
			return text;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00027BE4 File Offset: 0x00025DE4
		internal object DeserializeFromXml(string value)
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(value)))
			{
				obj = new XmlSerializer(typeof(object), "http://www.nauck-it.de/PostgreSQLProvider").Deserialize(memoryStream);
			}
			return obj;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00027C38 File Offset: 0x00025E38
		internal byte[] SerializeToBinary(object value)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, value);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00027C7C File Offset: 0x00025E7C
		internal object DeserializeFromBinary(byte[] value)
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(value))
			{
				obj = new BinaryFormatter().Deserialize(memoryStream);
			}
			return obj;
		}
	}
}
