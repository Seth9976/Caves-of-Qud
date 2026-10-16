using System;
using XRL.World.Quests;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C14 RID: 3092
	public class SlynthCandidatesReady : IConversationPart
	{
		// Token: 0x06007BE8 RID: 31720 RVA: 0x002E9164 File Offset: 0x002E7364
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID || ID == IsElementVisibleEvent.ID;
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x002E9182 File Offset: 0x002E7382
		public override bool HandleEvent(EnteredElementEvent E)
		{
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			if (system != null)
			{
				system.RollResult();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x002E91A0 File Offset: 0x002E73A0
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			return system != null && system.candidateFactionsCount() >= LandingPadsSystem.REQUIRED_CANDIDATES;
		}
	}
}
