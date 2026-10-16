using System;

namespace XRL.World.Parts
{
	// Token: 0x02001275 RID: 4725
	[Serializable]
	public class MissileCover : IPart
	{
		// Token: 0x0600BB21 RID: 47905 RVA: 0x00445B18 File Offset: 0x00443D18
		public override bool SameAs(IPart p)
		{
			MissileCover missileCover = p as MissileCover;
			return missileCover.Percentage == this.Percentage && missileCover.DisabledByPenetrateCreatures == this.DisabledByPenetrateCreatures && missileCover.DisabledByPenetrateWalls == this.DisabledByPenetrateWalls && base.SameAs(p);
		}

		// Token: 0x0600BB22 RID: 47906 RVA: 0x00445B63 File Offset: 0x00443D63
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMissileCoverPercentageEvent>.ID;
		}

		// Token: 0x0600BB23 RID: 47907 RVA: 0x00445B79 File Offset: 0x00443D79
		public override bool HandleEvent(GetMissileCoverPercentageEvent E)
		{
			if (!this.DisabledByPenetrateCreatures || !E.PenetrateCreatures || !this.DisabledByPenetrateWalls || !E.PenetrateWalls)
			{
				E.MinPercentage(this.Percentage);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004B22 RID: 19234
		public int Percentage;

		// Token: 0x04004B23 RID: 19235
		public bool DisabledByPenetrateCreatures = true;

		// Token: 0x04004B24 RID: 19236
		public bool DisabledByPenetrateWalls = true;
	}
}
