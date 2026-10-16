using System;
using XRL.Language;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C0D RID: 3085
	public class MoundContext : IConversationPart
	{
		// Token: 0x06007BC1 RID: 31681 RVA: 0x002E7FC7 File Offset: 0x002E61C7
		public MoundContext()
		{
			this.Priority = -1000;
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x002E7FE1 File Offset: 0x002E61E1
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007BC3 RID: 31683 RVA: 0x002E7FF8 File Offset: 0x002E61F8
		public override bool HandleEvent(PrepareTextEvent E)
		{
			if (E.Text.Contains("=mound"))
			{
				string text = "soon";
				Zone currentZone = The.Speaker.CurrentZone;
				GolemQuestMound golemQuestMound;
				if (currentZone == null)
				{
					golemQuestMound = null;
				}
				else
				{
					GameObject firstObjectWithPart = currentZone.GetFirstObjectWithPart("GolemQuestMound");
					golemQuestMound = ((firstObjectWithPart != null) ? firstObjectWithPart.GetPart<GolemQuestMound>() : null);
				}
				GolemQuestMound golemQuestMound2 = golemQuestMound;
				if (golemQuestMound2 != null)
				{
					int completeDays = golemQuestMound2.CompleteDays;
					if (completeDays >= 1)
					{
						text = (this.Preposition ? "in " : "") + Grammar.Cardinal(completeDays) + ((completeDays == 1) ? " day" : " days");
					}
				}
				E.Text.Replace("=mound.complete.days=", text);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400383A RID: 14394
		public bool Preposition = true;
	}
}
