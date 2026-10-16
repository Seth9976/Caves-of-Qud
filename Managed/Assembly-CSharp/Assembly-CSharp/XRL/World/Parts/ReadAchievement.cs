using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DFB RID: 3579
	[Serializable]
	public class ReadAchievement : IPart
	{
		// Token: 0x060090D6 RID: 37078 RVA: 0x003434EA File Offset: 0x003416EA
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090D7 RID: 37079 RVA: 0x003434ED File Offset: 0x003416ED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || (ID == InventoryActionEvent.ID && !this.Triggered);
		}

		// Token: 0x060090D8 RID: 37080 RVA: 0x00343516 File Offset: 0x00341716
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read")
			{
				Achievement.STAT_BOOKS_READ.Increment(1);
				this.Triggered = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060090D9 RID: 37081 RVA: 0x00343543 File Offset: 0x00341743
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C67 RID: 15463
		public bool Triggered;
	}
}
