using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009E5 RID: 2533
	public struct EventSourceOptions
	{
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x00134337 File Offset: 0x00132537
		// (set) Token: 0x06005A98 RID: 23192 RVA: 0x0013433F File Offset: 0x0013253F
		public EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005A99 RID: 23193 RVA: 0x00134358 File Offset: 0x00132558
		// (set) Token: 0x06005A9A RID: 23194 RVA: 0x00134360 File Offset: 0x00132560
		public EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x00134379 File Offset: 0x00132579
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005A9C RID: 23196 RVA: 0x00134386 File Offset: 0x00132586
		// (set) Token: 0x06005A9D RID: 23197 RVA: 0x0013438E File Offset: 0x0013258E
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005A9E RID: 23198 RVA: 0x001343A6 File Offset: 0x001325A6
		// (set) Token: 0x06005A9F RID: 23199 RVA: 0x001343AE File Offset: 0x001325AE
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x001343C6 File Offset: 0x001325C6
		// (set) Token: 0x06005AA1 RID: 23201 RVA: 0x001343CE File Offset: 0x001325CE
		public EventActivityOptions ActivityOptions
		{
			get
			{
				return this.activityOptions;
			}
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x040037DB RID: 14299
		internal EventKeywords keywords;

		// Token: 0x040037DC RID: 14300
		internal EventTags tags;

		// Token: 0x040037DD RID: 14301
		internal EventActivityOptions activityOptions;

		// Token: 0x040037DE RID: 14302
		internal byte level;

		// Token: 0x040037DF RID: 14303
		internal byte opcode;

		// Token: 0x040037E0 RID: 14304
		internal byte valuesSet;

		// Token: 0x040037E1 RID: 14305
		internal const byte keywordsSet = 1;

		// Token: 0x040037E2 RID: 14306
		internal const byte tagsSet = 2;

		// Token: 0x040037E3 RID: 14307
		internal const byte levelSet = 4;

		// Token: 0x040037E4 RID: 14308
		internal const byte opcodeSet = 8;

		// Token: 0x040037E5 RID: 14309
		internal const byte activityOptionsSet = 16;
	}
}
