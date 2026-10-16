using System;
using XRL.World.AI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C16 RID: 3094
	public class StartFight : IConversationPart
	{
		// Token: 0x06007BEF RID: 31727 RVA: 0x002E9317 File Offset: 0x002E7517
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007BF0 RID: 31728 RVA: 0x002E9335 File Offset: 0x002E7535
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{R|[Fight]}}";
			return false;
		}

		// Token: 0x06007BF1 RID: 31729 RVA: 0x002E9344 File Offset: 0x002E7544
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (The.Speaker.PartyLeader == The.Player)
			{
				The.Speaker.PartyLeader = null;
			}
			The.Speaker.AddOpinion<OpinionGoad>(The.Player, 1f);
			The.Speaker.Target = The.Player;
			AIHelpBroadcastEvent.Send(The.Speaker, The.Player, null, null, 20, 1f, HelpCause.General);
			return base.HandleEvent(E);
		}

		// Token: 0x04003854 RID: 14420
		public bool BroadcastForHelp;
	}
}
