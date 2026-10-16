using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C17 RID: 3095
	public class Tag : IConversationPart
	{
		// Token: 0x06007BF3 RID: 31731 RVA: 0x002E93B8 File Offset: 0x002E75B8
		public Tag()
		{
			this.Priority = -500;
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x002E93CB File Offset: 0x002E75CB
		public override void LoadText(string Text)
		{
			this.Text = Text;
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x002E93D4 File Offset: 0x002E75D4
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID;
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x002E93EA File Offset: 0x002E75EA
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = this.Text;
			return false;
		}

		// Token: 0x04003855 RID: 14421
		public string Text;
	}
}
