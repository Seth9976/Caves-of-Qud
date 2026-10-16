using System;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF7 RID: 3063
	public class ChavvahAttune : IConversationPart
	{
		// Token: 0x06007B0C RID: 31500 RVA: 0x002E32ED File Offset: 0x002E14ED
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetTargetElementEvent.ID;
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x002E3304 File Offset: 0x002E1504
		public override bool HandleEvent(GetTargetElementEvent E)
		{
			if (!The.Game.GetSystem<ChavvahSystem>().Reveal(true))
			{
				E.Target = this.FailTarget;
			}
			else
			{
				JournalAPI.AddAccomplishment("You touched the chiming rock and attuned to Chavvah, the Tree of Life.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003817 RID: 14359
		public string FailTarget;
	}
}
