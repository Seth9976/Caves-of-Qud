using System;
using XRL.Rules;
using XRL.World;

namespace HistoryKit
{
	// Token: 0x0200036E RID: 878
	[Serializable]
	public class HistoricPerspective
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x000A1368 File Offset: 0x0009F568
		public static HistoricPerspective Load(SerializationReader reader)
		{
			return new HistoricPerspective
			{
				entityId = reader.ReadString(),
				eventId = reader.ReadInt64(),
				feeling = reader.ReadInt32(),
				mainColor = reader.ReadString(),
				supportColor = reader.ReadString()
			};
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000A13B6 File Offset: 0x0009F5B6
		public virtual void Save(SerializationWriter writer)
		{
			writer.Write(this.entityId);
			writer.Write(this.eventId);
			writer.Write(this.feeling);
			writer.Write(this.mainColor);
			writer.Write(this.supportColor);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000A13F4 File Offset: 0x0009F5F4
		public void randomizeFeeling()
		{
			this.feeling = Stat.Random(-1000, 1000);
		}

		// Token: 0x04001095 RID: 4245
		public string entityId;

		// Token: 0x04001096 RID: 4246
		public long eventId;

		// Token: 0x04001097 RID: 4247
		public int feeling;

		// Token: 0x04001098 RID: 4248
		public string mainColor;

		// Token: 0x04001099 RID: 4249
		public string supportColor;
	}
}
