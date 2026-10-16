using System;

namespace XRL.World.Parts
{
	// Token: 0x02001207 RID: 4615
	[Serializable]
	public class DynamicQuestTarget : IPart
	{
		// Token: 0x0600B79B RID: 47003 RVA: 0x0042CFC8 File Offset: 0x0042B1C8
		public override void Initialize()
		{
			this.ParentObject.SetIntProperty("NoAIEquip", 1, false);
			this.ParentObject.SetIntProperty("QuestItem", 1, false);
			this.ParentObject.SetEpistemicStatus(2);
			Physics physics = this.ParentObject.Physics;
			if (!string.IsNullOrEmpty((physics != null) ? physics.Category : null))
			{
				this.ParentObject.SetStringProperty("OriginalCategory", this.ParentObject.Physics.Category, false);
			}
			this.ParentObject.Physics.Category = "Quest Items";
			base.Initialize();
		}

		// Token: 0x0600B79C RID: 47004 RVA: 0x0042D062 File Offset: 0x0042B262
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == PooledEvent<AllowTradeWithNoInventoryEvent>.ID;
		}

		// Token: 0x0600B79D RID: 47005 RVA: 0x0042D080 File Offset: 0x0042B280
		public override bool HandleEvent(AdjustValueEvent E)
		{
			E.AdjustValue(2.0);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B79E RID: 47006 RVA: 0x0042D098 File Offset: 0x0042B298
		public override bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return false;
		}
	}
}
