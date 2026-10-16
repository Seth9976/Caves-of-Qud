using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.UI;
using XRL.World;

namespace Qud.API
{
	// Token: 0x020003FB RID: 1019
	[GenerateSerializationPartial]
	[Serializable]
	public class JournalObservation : IBaseJournalEntry
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000C5161 File Offset: 0x000C3361
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000C5164 File Offset: 0x000C3364
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Time);
			Writer.WriteOptimized(this.Category);
			Writer.WriteOptimized(this.RevealText);
			Writer.Write(this.Rumor);
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.History);
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.LearnedFrom);
			Writer.WriteOptimized(this.Weight);
			Writer.Write(this.Revealed);
			Writer.Write(this.Tradable);
			Writer.Write(this.Attributes);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000C5204 File Offset: 0x000C3404
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Time = Reader.ReadOptimizedInt64();
			this.Category = Reader.ReadOptimizedString();
			this.RevealText = Reader.ReadOptimizedString();
			this.Rumor = Reader.ReadBoolean();
			this.ID = Reader.ReadOptimizedString();
			this.History = Reader.ReadOptimizedString();
			this.Text = Reader.ReadOptimizedString();
			this.LearnedFrom = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.Revealed = Reader.ReadBoolean();
			this.Tradable = Reader.ReadBoolean();
			this.Attributes = Reader.ReadList<string>();
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000C52A4 File Offset: 0x000C34A4
		public override void Reveal(string LearnedFrom = null, bool Silent = false)
		{
			if (!this.Revealed)
			{
				base.Reveal(LearnedFrom, Silent);
				this.Updated();
				IBaseJournalEntry.SB.Clear().Append("{{W|").Append(this.RevealText.IsNullOrEmpty() ? this.Text : this.RevealText)
					.Append("}}\n\nYou note this piece of information in the {{W|")
					.Append(JournalScreen.STR_OBSERVATIONS)
					.Append("}} section of your journal.");
				IBaseJournalEntry.DisplayMessage(IBaseJournalEntry.SB.ToString(), null);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000C532A File Offset: 0x000C352A
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x000C5332 File Offset: 0x000C3532
		[Obsolete]
		public string category
		{
			get
			{
				return this.Category;
			}
			set
			{
				this.Category = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000C533B File Offset: 0x000C353B
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x000C5343 File Offset: 0x000C3543
		[Obsolete]
		public long time
		{
			get
			{
				return this.Time;
			}
			set
			{
				this.Time = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000C534C File Offset: 0x000C354C
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x000C5354 File Offset: 0x000C3554
		[Obsolete]
		public string id
		{
			get
			{
				return this.ID;
			}
			set
			{
				this.ID = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000C535D File Offset: 0x000C355D
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x000C5365 File Offset: 0x000C3565
		[Obsolete]
		public bool initCapAsFragment
		{
			get
			{
				return this.Rumor;
			}
			set
			{
				this.Rumor = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000C536E File Offset: 0x000C356E
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x000C5376 File Offset: 0x000C3576
		[Obsolete]
		public string additionalRevealText
		{
			get
			{
				return this.RevealText;
			}
			set
			{
				this.RevealText = value;
			}
		}

		// Token: 0x04001312 RID: 4882
		public long Time;

		// Token: 0x04001313 RID: 4883
		public string Category;

		// Token: 0x04001314 RID: 4884
		public string RevealText;

		// Token: 0x04001315 RID: 4885
		public bool Rumor;
	}
}
