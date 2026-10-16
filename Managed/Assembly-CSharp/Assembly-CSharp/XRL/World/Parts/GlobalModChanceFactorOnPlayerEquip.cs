using System;

namespace XRL.World.Parts
{
	// Token: 0x02001040 RID: 4160
	[Serializable]
	public class GlobalModChanceFactorOnPlayerEquip : IPart
	{
		// Token: 0x0600A5AB RID: 42411 RVA: 0x003CA53E File Offset: 0x003C873E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetModRarityWeightEvent>.ID;
		}

		// Token: 0x0600A5AC RID: 42412 RVA: 0x003CA554 File Offset: 0x003C8754
		public override bool HandleEvent(GetModRarityWeightEvent E)
		{
			if ((string.IsNullOrEmpty(this.Mod) || E.Mod.Part == this.Mod) && (this.ParentObject.Equipped == The.Player || this.ParentObject.Implantee == The.Player))
			{
				E.FactorAdjustment *= (double)this.Factor;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5AD RID: 42413 RVA: 0x003CA5C5 File Offset: 0x003C87C5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400446F RID: 17519
		public string Mod;

		// Token: 0x04004470 RID: 17520
		public float Factor;
	}
}
