using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020011EC RID: 4588
	[Serializable]
	public class ForcefieldMaterial : IPart
	{
		// Token: 0x0600B6B7 RID: 46775 RVA: 0x0042A1CF File Offset: 0x004283CF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Object.ModIntProperty("Electromagnetic", 1, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6B8 RID: 46776 RVA: 0x0042A1E7 File Offset: 0x004283E7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<RealityStabilizeEvent>.ID;
		}

		// Token: 0x0600B6B9 RID: 46777 RVA: 0x0042A205 File Offset: 0x00428405
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600B6BA RID: 46778 RVA: 0x0042A210 File Offset: 0x00428410
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(true))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				Cell cell = ((currentCell != null) ? currentCell.GetRandomLocalAdjacentCell() : null);
				if (cell != null)
				{
					this.ParentObject.Discharge(cell, Stat.Random(1, 4), 0, "1d8", null, E.Effect.Owner, this.ParentObject, null, null, null, null, null, 0, false, false, false);
				}
				this.ParentObject.TileParticleBlip("items/sw_quills.bmp", "&B", "K", 10, false, false, false, 0L);
				base.DidX("collapse", "under the pressure of normality", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.Destroy(null, false, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}
	}
}
