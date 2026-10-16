using System;
using System.Collections.Generic;
using Genkit;

namespace XRL.World
{
	// Token: 0x02000923 RID: 2339
	[Serializable]
	public class DynamicQuestDeliveryTarget : IComposite
	{
		// Token: 0x060059EE RID: 23022 RVA: 0x001B4832 File Offset: 0x001B2A32
		public void Write(SerializationWriter Writer)
		{
			Writer.Write(this.location);
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x001B4840 File Offset: 0x001B2A40
		public void Read(SerializationReader Reader)
		{
			this.location = Reader.ReadLocation2D();
		}

		// Token: 0x04002C8C RID: 11404
		public string type;

		// Token: 0x04002C8D RID: 11405
		public string zoneId;

		// Token: 0x04002C8E RID: 11406
		[NonSerialized]
		public Location2D location;

		// Token: 0x04002C8F RID: 11407
		public string displayName;

		// Token: 0x04002C90 RID: 11408
		public string secretId;

		// Token: 0x04002C91 RID: 11409
		public List<string> attributes;

		// Token: 0x04002C92 RID: 11410
		public List<string> factions;
	}
}
