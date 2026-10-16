using System;

namespace XRL.World.Parts
{
	// Token: 0x0200100D RID: 4109
	[Serializable]
	public class Crysteel : IPart
	{
		// Token: 0x0600A3AD RID: 41901 RVA: 0x003BCF0E File Offset: 0x003BB10E
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeImperviousToHeat();
		}

		// Token: 0x0600A3AE RID: 41902 RVA: 0x003BCF22 File Offset: 0x003BB122
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<TransparentToEMPEvent>.ID;
		}

		// Token: 0x0600A3AF RID: 41903 RVA: 0x003BCF60 File Offset: 0x003BB160
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 2 && E.Object == this.ParentObject)
			{
				E.MinValue(80);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3B0 RID: 41904 RVA: 0x003BCF88 File Offset: 0x003BB188
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction += 35;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3B1 RID: 41905 RVA: 0x003BCFA0 File Offset: 0x003BB1A0
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && !this.ParentObject.HasTag("Creature"))
			{
				if (E.Damage.IsHeatDamage())
				{
					NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
					E.Damage.Amount = 0;
				}
				else if (E.Damage.IsAcidDamage())
				{
					E.Damage.Amount /= 4;
				}
				else if (E.Damage.IsColdDamage())
				{
					E.Damage.Amount = E.Damage.Amount * 5 / 4;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3B2 RID: 41906 RVA: 0x003BD05C File Offset: 0x003BB25C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("glass", 5);
				E.Add("jewels", 1);
				E.Add("stars", 1);
				E.Add("ice", 1);
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3B3 RID: 41907 RVA: 0x003BD0BA File Offset: 0x003BB2BA
		public override bool HandleEvent(TransparentToEMPEvent E)
		{
			return false;
		}

		// Token: 0x0600A3B4 RID: 41908 RVA: 0x003BD0C0 File Offset: 0x003BB2C0
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.MakeImperviousToHeat();
			if (this.ParentObject.HasTagOrStringProperty("Flawless"))
			{
				this.ParentObject.SetStringProperty("EquipmentFrameColors", "KGKG", false);
			}
			else
			{
				this.ParentObject.SetStringProperty("EquipmentFrameColors", "KgKg", false);
			}
			return base.HandleEvent(E);
		}
	}
}
