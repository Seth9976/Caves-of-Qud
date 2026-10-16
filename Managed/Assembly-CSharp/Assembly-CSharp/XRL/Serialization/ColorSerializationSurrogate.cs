using System;
using UnityEngine;
using XRL.World;

namespace XRL.Serialization
{
	// Token: 0x0200056B RID: 1387
	internal sealed class ColorSerializationSurrogate : IFastSerializationTypeSurrogate
	{
		// Token: 0x06003C43 RID: 15427 RVA: 0x00126CA4 File Offset: 0x00124EA4
		public object Deserialize(SerializationReader reader, Type type)
		{
			return new Color
			{
				r = reader.ReadSingle(),
				g = reader.ReadSingle(),
				b = reader.ReadSingle(),
				a = reader.ReadSingle()
			};
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x00126CF4 File Offset: 0x00124EF4
		public void Serialize(SerializationWriter writer, object value)
		{
			Color color = (Color)value;
			writer.Write(color.r);
			writer.Write(color.g);
			writer.Write(color.b);
			writer.Write(color.a);
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00126D38 File Offset: 0x00124F38
		public bool SupportsType(Type type)
		{
			return type == typeof(Color);
		}
	}
}
