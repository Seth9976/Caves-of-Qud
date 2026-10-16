using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E52 RID: 3666
	[Serializable]
	public class BurstOnDeath : IPart
	{
		// Token: 0x06009471 RID: 38001 RVA: 0x00356F86 File Offset: 0x00355186
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009472 RID: 38002 RVA: 0x00356F89 File Offset: 0x00355189
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06009473 RID: 38003 RVA: 0x00356FA0 File Offset: 0x003551A0
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (!this.Blueprint.IsNullOrEmpty() && !this.ParentObject.IsNowhere())
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.IterateAdjacent(this.Radius, true, false, true))
				{
					if ((!this.SkipOccluding || !cell.IsOccluding()) && (!this.SkipSolid || !cell.IsSolid()) && this.Chance.in100())
					{
						cell.AddObject(this.Blueprint.GetRandomSubstring(',', false, null), null, null, null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003D5F RID: 15711
		public string Blueprint;

		// Token: 0x04003D60 RID: 15712
		public int Radius = 1;

		// Token: 0x04003D61 RID: 15713
		public int Chance = 100;

		// Token: 0x04003D62 RID: 15714
		public bool SkipOccluding;

		// Token: 0x04003D63 RID: 15715
		public bool SkipSolid;
	}
}
