using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CBF RID: 3263
	public class CookingDomainHP_OnLowHealth : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x0600823A RID: 33338 RVA: 0x002F7639 File Offset: 0x002F5839
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.currentlyBelow = false;
		}

		// Token: 0x0600823B RID: 33339 RVA: 0x002F7649 File Offset: 0x002F5849
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature drop@s below 20% HP,";
		}

		// Token: 0x0600823C RID: 33340 RVA: 0x002F7650 File Offset: 0x002F5850
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<StatChangeEvent>.ID;
		}

		// Token: 0x0600823D RID: 33341 RVA: 0x002F7668 File Offset: 0x002F5868
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (E.Name == "Hitpoints")
			{
				if (this.currentlyBelow)
				{
					if (!this.IsLow())
					{
						this.currentlyBelow = false;
					}
				}
				else if (this.IsLow())
				{
					this.currentlyBelow = true;
					base.Trigger();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x002F76BC File Offset: 0x002F58BC
		public bool IsLow()
		{
			return base.Object != null && base.Object.HasStat("Hitpoints") && base.Object.isDamaged((double)this.LowAmount, true);
		}

		// Token: 0x04003932 RID: 14642
		public bool currentlyBelow;

		// Token: 0x04003933 RID: 14643
		protected float LowAmount = 0.2f;
	}
}
