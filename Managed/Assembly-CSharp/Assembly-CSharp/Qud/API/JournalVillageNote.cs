using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.UI;
using XRL.World;

namespace Qud.API
{
	// Token: 0x020003FE RID: 1022
	[GenerateSerializationPartial]
	[Serializable]
	public class JournalVillageNote : IBaseJournalEntry
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000C576B File Offset: 0x000C396B
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000C5770 File Offset: 0x000C3970
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.VillageID);
			Writer.WriteOptimized(this.EventID);
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.History);
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.LearnedFrom);
			Writer.WriteOptimized(this.Weight);
			Writer.Write(this.Revealed);
			Writer.Write(this.Tradable);
			Writer.Write(this.Attributes);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000C57F8 File Offset: 0x000C39F8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.VillageID = Reader.ReadOptimizedString();
			this.EventID = Reader.ReadOptimizedInt64();
			this.ID = Reader.ReadOptimizedString();
			this.History = Reader.ReadOptimizedString();
			this.Text = Reader.ReadOptimizedString();
			this.LearnedFrom = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.Revealed = Reader.ReadBoolean();
			this.Tradable = Reader.ReadBoolean();
			this.Attributes = Reader.ReadList<string>();
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000C5880 File Offset: 0x000C3A80
		public override void Reveal(string LearnedFrom = null, bool Silent = false)
		{
			if (!this.Revealed)
			{
				base.Reveal(LearnedFrom, Silent);
				this.Updated();
				if (!Silent)
				{
					IBaseJournalEntry.DisplayMessage(string.Concat(new string[]
					{
						"You note this piece of information in the {{W|",
						JournalScreen.STR_VILLAGES,
						" > ",
						HistoryAPI.GetEntityName(this.VillageID),
						"}} section of your journal."
					}), null);
				}
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000C58E8 File Offset: 0x000C3AE8
		public override string GetShortText()
		{
			int num = this.Text.LastIndexOf('|');
			if (num == -1)
			{
				return this.Text;
			}
			return this.Text.Substring(0, num);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000C591C File Offset: 0x000C3B1C
		public override string GetDisplayText()
		{
			string shortText = this.GetShortText();
			if (this.History.Length > 0)
			{
				return shortText + "\n" + this.History;
			}
			return shortText;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000C5951 File Offset: 0x000C3B51
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x000C5959 File Offset: 0x000C3B59
		[Obsolete]
		public string villageID
		{
			get
			{
				return this.VillageID;
			}
			set
			{
				this.VillageID = value;
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000C5962 File Offset: 0x000C3B62
		[Obsolete]
		public long getEventId()
		{
			return this.EventID;
		}

		// Token: 0x04001319 RID: 4889
		public string VillageID;

		// Token: 0x0400131A RID: 4890
		public long EventID;
	}
}
