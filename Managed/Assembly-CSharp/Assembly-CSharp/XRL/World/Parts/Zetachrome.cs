using System;

namespace XRL.World.Parts
{
	// Token: 0x020010C2 RID: 4290
	[Serializable]
	public class Zetachrome : IPart
	{
		// Token: 0x0600AAC9 RID: 43721 RVA: 0x003E7542 File Offset: 0x003E5742
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeNonflammable();
		}

		// Token: 0x0600AACA RID: 43722 RVA: 0x003E7556 File Offset: 0x003E5756
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<TransparentToEMPEvent>.ID;
		}

		// Token: 0x0600AACB RID: 43723 RVA: 0x003E7594 File Offset: 0x003E5794
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 2 && E.Object == this.ParentObject)
			{
				E.MinValue(60);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AACC RID: 43724 RVA: 0x003E75BC File Offset: 0x003E57BC
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction += 30;
			return base.HandleEvent(E);
		}

		// Token: 0x0600AACD RID: 43725 RVA: 0x003E75D4 File Offset: 0x003E57D4
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && !this.ParentObject.HasTag("Creature"))
			{
				if (E.Damage.IsHeatDamage() || E.Damage.IsColdDamage())
				{
					NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
					E.Damage.Amount = 0;
				}
				else if (E.Damage.IsAcidDamage())
				{
					E.Damage.Amount /= 4;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AACE RID: 43726 RVA: 0x003E766E File Offset: 0x003E586E
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("jewels", 1);
				E.Add("stars", 1);
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AACF RID: 43727 RVA: 0x003E76A9 File Offset: 0x003E58A9
		public override bool HandleEvent(TransparentToEMPEvent E)
		{
			return false;
		}

		// Token: 0x0600AAD0 RID: 43728 RVA: 0x003E76AC File Offset: 0x003E58AC
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.MakeImperviousToHeat();
			this.ParentObject.SetStringProperty("EquipmentFrameColors", "mCmC", false);
			return base.HandleEvent(E);
		}
	}
}
