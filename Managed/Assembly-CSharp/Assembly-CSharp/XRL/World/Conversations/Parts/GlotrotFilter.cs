using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BFC RID: 3068
	public class GlotrotFilter : IConversationPart
	{
		// Token: 0x06007B4F RID: 31567 RVA: 0x002E553F File Offset: 0x002E373F
		public GlotrotFilter()
		{
			this.Priority = -2000;
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x002E5552 File Offset: 0x002E3752
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID || ID == GetTargetElementEvent.ID || ID == IsElementVisibleEvent.ID;
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x002E5578 File Offset: 0x002E3778
		public override bool HandleEvent(PrepareTextEvent E)
		{
			E.Text.Clear();
			E.Text.Append((Stat.RandomCosmetic(1, 100) <= 70) ? "N" : "G");
			E.Text.Append('n', Stat.RandomCosmetic(3, 11));
			E.Text.Append('.');
			return base.HandleEvent(E);
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x002E55E0 File Offset: 0x002E37E0
		public override bool HandleEvent(GetTargetElementEvent E)
		{
			E.Target = "End";
			return base.HandleEvent(E);
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x002E55F4 File Offset: 0x002E37F4
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			Choice choice = E.Element as Choice;
			if (choice != null)
			{
				if (choice.HasPart<Trade>())
				{
					return base.HandleEvent(E);
				}
				if (choice.Target != "End")
				{
					return false;
				}
				List<IConversationPart> parts = choice.Parts;
				if (parts != null)
				{
					parts.ForEach(delegate(IConversationPart x)
					{
						x.Propagation = 0;
					});
				}
				Dictionary<string, string> actions = choice.Actions;
				if (actions != null)
				{
					actions.Clear();
				}
			}
			return base.HandleEvent(E);
		}
	}
}
