using System;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C07 RID: 3079
	public class KithAndKinExclusion : IKithAndKinPart
	{
		// Token: 0x06007B9E RID: 31646 RVA: 0x002E697F File Offset: 0x002E4B7F
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID || ID == LeftElementEvent.ID || ID == GetTextElementEvent.ID || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x002E69AD File Offset: 0x002E4BAD
		public override bool HandleEvent(LeftElementEvent E)
		{
			if (!base.Eliminated.Contains(base.Thief))
			{
				base.Eliminated.Add(base.Thief);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x002E69DC File Offset: 0x002E4BDC
		public override bool HandleEvent(GetTextElementEvent E)
		{
			if (!base.Eliminated.Contains(base.Thief))
			{
				E.Selected = E.Texts.Find(delegate(ConversationText x)
				{
					string id = x.ID;
					JournalObservation circumstance = base.Circumstance;
					return id == ((circumstance != null) ? circumstance.ID : null);
				}) ?? E.Selected;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x002E6A2A File Offset: 0x002E4C2A
		public override bool HandleEvent(PrepareTextEvent E)
		{
			E.Text.Replace("=thief.name=", base.ThiefName);
			return base.HandleEvent(E);
		}
	}
}
