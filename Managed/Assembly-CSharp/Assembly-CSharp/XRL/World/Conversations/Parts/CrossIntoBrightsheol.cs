using System;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF8 RID: 3064
	public class CrossIntoBrightsheol : IConversationPart
	{
		// Token: 0x06007B0F RID: 31503 RVA: 0x002E3359 File Offset: 0x002E1559
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID;
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x002E3377 File Offset: 0x002E1577
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{M|[lesser victory]}}";
			return false;
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x002E3385 File Offset: 0x002E1585
		public override bool HandleEvent(EnterElementEvent E)
		{
			return ThinWorld.CrossIntoBrightsheol();
		}
	}
}
