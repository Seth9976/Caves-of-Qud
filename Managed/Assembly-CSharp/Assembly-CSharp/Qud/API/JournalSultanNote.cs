using System;
using System.CodeDom.Compiler;
using HistoryKit;
using Occult.Engine.CodeGeneration;
using XRL;
using XRL.UI;
using XRL.World;

namespace Qud.API
{
	// Token: 0x020003FD RID: 1021
	[GenerateSerializationPartial]
	[Serializable]
	public class JournalSultanNote : IBaseJournalEntry
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x000C54FA File Offset: 0x000C36FA
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000C5500 File Offset: 0x000C3700
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.SultanID);
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

		// Token: 0x06002646 RID: 9798 RVA: 0x000C5588 File Offset: 0x000C3788
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.SultanID = Reader.ReadOptimizedString();
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

		// Token: 0x06002647 RID: 9799 RVA: 0x000C5610 File Offset: 0x000C3810
		public override bool Forgettable()
		{
			HistoricEvent @event = HistoryAPI.GetEvent(this.EventID);
			return @event == null || (!@event.HasEventProperty("revealsRegion") && !@event.HasEventProperty("revealsItem") && !@event.HasEventProperty("revealsItemLocation"));
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000C565C File Offset: 0x000C385C
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
						JournalScreen.GetSultansDisplayName(),
						" > ",
						HistoryAPI.GetEntityName(this.SultanID),
						"}} section of your journal."
					}), null);
				}
				bool flag = true;
				foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
				{
					if (journalSultanNote.SultanID == this.SultanID && !journalSultanNote.Revealed)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					Achievement.LEARN_ONE_SULTAN_HISTORY.Unlock();
				}
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000C5730 File Offset: 0x000C3930
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x000C5738 File Offset: 0x000C3938
		[Obsolete]
		public string sultan
		{
			get
			{
				return this.SultanID;
			}
			set
			{
				this.SultanID = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000C5741 File Offset: 0x000C3941
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x000C5749 File Offset: 0x000C3949
		[Obsolete]
		public long linkId
		{
			get
			{
				return this.EventID;
			}
			set
			{
				this.EventID = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x000C5752 File Offset: 0x000C3952
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x000C575A File Offset: 0x000C395A
		[Obsolete]
		public long eventId
		{
			get
			{
				return this.EventID;
			}
			set
			{
				this.EventID = value;
			}
		}

		// Token: 0x04001317 RID: 4887
		public string SultanID;

		// Token: 0x04001318 RID: 4888
		public long EventID;
	}
}
