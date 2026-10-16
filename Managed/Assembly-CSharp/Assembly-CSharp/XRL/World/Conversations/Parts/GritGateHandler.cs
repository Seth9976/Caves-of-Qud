using System;
using XRL.World.Quests;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BFD RID: 3069
	public class GritGateHandler : IConversationPart
	{
		// Token: 0x06007B54 RID: 31572 RVA: 0x002E567C File Offset: 0x002E387C
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x002E5694 File Offset: 0x002E3894
		public override bool HandleEvent(EnteredElementEvent E)
		{
			string rank = this.Rank;
			string text = ((rank != null) ? rank.ToUpperInvariant() : null);
			if (!text.IsNullOrEmpty())
			{
				if (!(text == "APPRENTICE"))
				{
					if (!(text == "JOURNEYFRIEND"))
					{
						if (!(text == "DISCIPLE"))
						{
							if (text == "MEYVN")
							{
								GritGateScripts.PromoteToMeyvn();
							}
						}
						else
						{
							GritGateScripts.PromoteToDisciple();
						}
					}
					else
					{
						GritGateScripts.PromoteToJourneyfriend();
					}
				}
				else
				{
					GritGateScripts.PromoteToApprentice();
				}
			}
			if (this.Door >= 0)
			{
				GritGateScripts.OpenGritGateDoors(this.Door);
			}
			if (this.Invasion)
			{
				GritGateScripts.BeginInvasion();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400381E RID: 14366
		public string Rank;

		// Token: 0x0400381F RID: 14367
		public int Door = -1;

		// Token: 0x04003820 RID: 14368
		public bool Invasion;
	}
}
