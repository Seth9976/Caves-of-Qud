using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001216 RID: 4630
	[Serializable]
	public class RevealVillageHistoryOnLook : IPart
	{
		// Token: 0x0600B800 RID: 47104 RVA: 0x0042EC66 File Offset: 0x0042CE66
		public RevealVillageHistoryOnLook()
		{
		}

		// Token: 0x0600B801 RID: 47105 RVA: 0x0042EC6E File Offset: 0x0042CE6E
		public RevealVillageHistoryOnLook(string HistoryId)
			: this()
		{
			this.HistoryId = HistoryId;
		}

		// Token: 0x0600B802 RID: 47106 RVA: 0x0042EC7D File Offset: 0x0042CE7D
		public override bool SameAs(IPart Part)
		{
			return !((Part as RevealVillageHistoryOnLook).HistoryId != this.HistoryId) && base.SameAs(Part);
		}

		// Token: 0x0600B803 RID: 47107 RVA: 0x0042ECA0 File Offset: 0x0042CEA0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID;
		}

		// Token: 0x0600B804 RID: 47108 RVA: 0x0042ECB6 File Offset: 0x0042CEB6
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && !this.LookedAt && this.HasUnrevealedSecret())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B805 RID: 47109 RVA: 0x0042ECF4 File Offset: 0x0042CEF4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B806 RID: 47110 RVA: 0x0042ED09 File Offset: 0x0042CF09
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt)
			{
				this.LookedAt = true;
				if (this.HistoryId != null)
				{
					JournalAPI.RevealVillageNote(this.HistoryId, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B807 RID: 47111 RVA: 0x0042ED48 File Offset: 0x0042CF48
		public bool HasUnrevealedSecret()
		{
			return this.HistoryId != null && JournalAPI.HasUnrevealedVillageNote(this.HistoryId);
		}

		// Token: 0x04004A09 RID: 18953
		public string HistoryId;

		// Token: 0x04004A0A RID: 18954
		public bool LookedAt;
	}
}
