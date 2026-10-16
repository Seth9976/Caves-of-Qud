using System;
using XRL.World;

namespace XRL
{
	// Token: 0x0200052D RID: 1325
	public abstract class IPartEntry
	{
		/// <summary>Entry is hidden until acquired.</summary>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x00116723 File Offset: 0x00114923
		// (set) Token: 0x060038B0 RID: 14512 RVA: 0x00116731 File Offset: 0x00114931
		public bool Hidden
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		/// <summary>Entry is excluded from random acquisition by mutation/skill pools.</summary>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x00116740 File Offset: 0x00114940
		// (set) Token: 0x060038B2 RID: 14514 RVA: 0x0011674E File Offset: 0x0011494E
		public bool ExcludeFromPool
		{
			get
			{
				return this.Flags.HasBit(8);
			}
			set
			{
				(ref this.Flags).SetBit(8, value);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060038B3 RID: 14515
		public abstract IPart Instance { get; }

		// Token: 0x060038B4 RID: 14516 RVA: 0x00116760 File Offset: 0x00114960
		public virtual void HandleXMLNode(XmlDataHelper Reader)
		{
			this.Class = Reader.ParseAttribute<string>("Class", this.Class, this.Class == null, null);
			this.Attribute = Reader.ParseAttribute<string>("Attribute", this.Attribute, false, null);
			this.Snippet = Reader.ParseAttribute<string>("Snippet", this.Snippet, false, null);
			this.Cost = Reader.ParseAttribute<int>("Cost", this.Cost, false, null);
			this.Hidden = !Reader.ParseAttribute<bool>("Visible", !this.Hidden, false, null);
			this.Hidden = Reader.ParseAttribute<bool>("Hidden", this.Hidden, false, null);
			this.ExcludeFromPool = Reader.ParseAttribute<bool>("ExcludeFromPool", this.ExcludeFromPool, false, null);
		}

		// Token: 0x04001C8F RID: 7311
		public const int FLAG_HIDDEN = 1;

		// Token: 0x04001C90 RID: 7312
		public const int FLAG_OBFUSCATED = 2;

		// Token: 0x04001C91 RID: 7313
		public const int FLAG_INITIATORY = 4;

		// Token: 0x04001C92 RID: 7314
		public const int FLAG_EX_POOL = 8;

		// Token: 0x04001C93 RID: 7315
		public string Name;

		// Token: 0x04001C94 RID: 7316
		public string Class;

		// Token: 0x04001C95 RID: 7317
		public string Attribute;

		// Token: 0x04001C96 RID: 7318
		public string Snippet = "";

		// Token: 0x04001C97 RID: 7319
		public int Cost = -999;

		// Token: 0x04001C98 RID: 7320
		public int Flags;
	}
}
