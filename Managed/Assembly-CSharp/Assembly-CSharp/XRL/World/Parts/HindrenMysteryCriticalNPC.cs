using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FDE RID: 4062
	[Serializable]
	public class HindrenMysteryCriticalNPC : IPart
	{
		// Token: 0x0600A14D RID: 41293 RVA: 0x003AB250 File Offset: 0x003A9450
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A14E RID: 41294 RVA: 0x003AB253 File Offset: 0x003A9453
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x0600A14F RID: 41295 RVA: 0x003AB274 File Offset: 0x003A9474
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			The.Game.SetStringGameState("HindrenMysteryCriticalNPCKilled", "1");
			if (The.Game.HasQuest("Kith and Kin") && !The.Game.FinishedQuest("Kith and Kin"))
			{
				Popup.Show("The death of " + this.ParentObject.BaseDisplayName + " means that the investigation can go no further.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Game.FailQuest("Kith and Kin");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A150 RID: 41296 RVA: 0x003AB2F8 File Offset: 0x003A94F8
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A151 RID: 41297 RVA: 0x003AB316 File Offset: 0x003A9516
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
