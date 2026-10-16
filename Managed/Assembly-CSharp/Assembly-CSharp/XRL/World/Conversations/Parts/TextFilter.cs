using System;
using XRL.Language;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C1A RID: 3098
	public class TextFilter : IConversationPart
	{
		// Token: 0x06007C07 RID: 31751 RVA: 0x002E9A65 File Offset: 0x002E7C65
		public TextFilter()
		{
			this.Priority = -1000;
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x002E9A7F File Offset: 0x002E7C7F
		public TextFilter(string ID, string Extras = null)
			: this()
		{
			this.FilterID = ID;
			this.Extras = Extras;
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x002E9A95 File Offset: 0x002E7C95
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextLateEvent.ID;
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x002E9AAB File Offset: 0x002E7CAB
		public override bool HandleEvent(PrepareTextLateEvent E)
		{
			if (!this.FilterID.IsNullOrEmpty())
			{
				E.Text = TextFilters.Filter(E.Text, this.FilterID, this.Extras, this.FormattingProtect);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003865 RID: 14437
		public string FilterID;

		// Token: 0x04003866 RID: 14438
		public string Extras;

		// Token: 0x04003867 RID: 14439
		public bool FormattingProtect = true;
	}
}
