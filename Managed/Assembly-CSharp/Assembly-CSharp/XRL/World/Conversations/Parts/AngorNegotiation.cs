using System;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF2 RID: 3058
	public class AngorNegotiation : IConversationPart
	{
		// Token: 0x06007AF6 RID: 31478 RVA: 0x002E2F3F File Offset: 0x002E113F
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x002E2F55 File Offset: 0x002E1155
		public override bool HandleEvent(EnteredElementEvent E)
		{
			The.Speaker.RequirePart<SpindleNegotiation>(false);
			The.Speaker.FireEvent("BeginSpindleNegotiation");
			return base.HandleEvent(E);
		}
	}
}
