using System;

namespace XRL.World.Parts
{
	// Token: 0x02001038 RID: 4152
	[Serializable]
	public class Fur : IPart
	{
		// Token: 0x0600A558 RID: 42328 RVA: 0x003C76B6 File Offset: 0x003C58B6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetSpringinessEvent>.ID;
		}

		// Token: 0x0600A559 RID: 42329 RVA: 0x003C76DC File Offset: 0x003C58DC
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageIncrease += 100;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A55A RID: 42330 RVA: 0x003C76F4 File Offset: 0x003C58F4
		public override bool HandleEvent(GetSpringinessEvent E)
		{
			E.LinearIncrease += this.ParentObject.GetKineticResistance() * 2 / 3;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A55B RID: 42331 RVA: 0x003C771C File Offset: 0x003C591C
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				if (E.Damage.IsColdDamage())
				{
					E.Damage.Amount /= 4;
				}
				if (E.Damage.IsHeatDamage())
				{
					E.Damage.Amount = E.Damage.Amount * 3 / 2;
				}
			}
			return base.HandleEvent(E);
		}
	}
}
