using System;

namespace XRL.World.Parts
{
	// Token: 0x0200107A RID: 4218
	[Serializable]
	public class PassiveFactionDeed : IPart
	{
		// Token: 0x0600A7D8 RID: 42968 RVA: 0x003D5618 File Offset: 0x003D3818
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A7D9 RID: 42969 RVA: 0x003D561B File Offset: 0x003D381B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A7DA RID: 42970 RVA: 0x003D563C File Offset: 0x003D383C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivatePassiveFactionDeed", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7DB RID: 42971 RVA: 0x003D5674 File Offset: 0x003D3874
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivatePassiveFactionDeed" && string.IsNullOrEmpty(this.Faction))
			{
				The.Game.PlayerReputation.Modify(this.Faction, this.Amount.RollCached(), "PassiveFactionDeed", null, null, false, false);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004541 RID: 17729
		public string Faction = "";

		// Token: 0x04004542 RID: 17730
		public string Amount = "500";
	}
}
