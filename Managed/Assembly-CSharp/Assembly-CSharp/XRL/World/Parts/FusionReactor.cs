using System;

namespace XRL.World.Parts
{
	// Token: 0x020011C9 RID: 4553
	[Serializable]
	public class FusionReactor : IPoweredPart
	{
		// Token: 0x0600B506 RID: 46342 RVA: 0x0042118C File Offset: 0x0041F38C
		public FusionReactor()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B507 RID: 46343 RVA: 0x004211CC File Offset: 0x0041F3CC
		public override bool SameAs(IPart p)
		{
			FusionReactor fusionReactor = p as FusionReactor;
			return fusionReactor.ChargeRate == this.ChargeRate && fusionReactor.ExplodeChance == this.ExplodeChance && fusionReactor.ExplodeForce == this.ExplodeForce && !(fusionReactor.Description != this.Description) && fusionReactor.CatastrophicDisable == this.CatastrophicDisable && fusionReactor.PartWasDisabled == this.PartWasDisabled && base.SameAs(p);
		}

		// Token: 0x0600B508 RID: 46344 RVA: 0x0042124C File Offset: 0x0041F44C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B509 RID: 46345 RVA: 0x00421284 File Offset: 0x0041F484
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.ChargeRate > this.ChargeUse)
			{
				E.Amount += (this.ChargeRate - this.ChargeUse) * E.Multiple;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B50A RID: 46346 RVA: 0x004212F0 File Offset: 0x0041F4F0
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (!this.PartWasDisabled && base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.PartWasDisabled = true;
				if (this.CatastrophicDisable && this.ExplodeChance.in100())
				{
					this.ForceExplode = true;
					this.ParentObject.Die(null, "catastrophic " + this.Description + " failure", "Your " + this.Description + " failed catastrophically.", this.ParentObject.Its + " " + this.Description + " failed catastrophically.", true, null, null, false, false, null, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B50B RID: 46347 RVA: 0x004213BC File Offset: 0x0041F5BC
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B50C RID: 46348 RVA: 0x004213E0 File Offset: 0x0041F5E0
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.PartWasDisabled = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B50D RID: 46349 RVA: 0x00421424 File Offset: 0x0041F624
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.ForceExplode || (this.ExplodeChance.in100() && (this.CatastrophicDisable || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))))
			{
				this.Explode(this.ParentObject.IsCombatObject() ? this.ParentObject : E.Killer, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B50E RID: 46350 RVA: 0x004214A0 File Offset: 0x0041F6A0
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B50F RID: 46351 RVA: 0x004214A3 File Offset: 0x0041F6A3
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B510 RID: 46352 RVA: 0x004214AC File Offset: 0x0041F6AC
		public bool Explode(GameObject Actor = null, bool FromDeathRemoval = false)
		{
			base.DidX("explode", null, "!", null, null, null, Actor, false, false, null, null, false, false, false, false, false, null);
			this.ParentObject.Explode(this.ExplodeForce, Actor, null, 1f, false, FromDeathRemoval, false, 0, null);
			return true;
		}

		// Token: 0x0600B511 RID: 46353 RVA: 0x004214F8 File Offset: 0x0041F6F8
		public void ProduceCharge(int Turns = 1)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				this.ParentObject.ChargeAvailable(this.ChargeRate - this.ChargeUse, 0L, Turns, false, null);
			}
		}

		// Token: 0x040048FD RID: 18685
		public int ChargeRate = 1000;

		// Token: 0x040048FE RID: 18686
		public int ExplodeChance = 50;

		// Token: 0x040048FF RID: 18687
		public int ExplodeForce = 10000;

		// Token: 0x04004900 RID: 18688
		public string Description = "fusion reactor";

		// Token: 0x04004901 RID: 18689
		public bool CatastrophicDisable;

		// Token: 0x04004902 RID: 18690
		public bool PartWasDisabled;

		// Token: 0x04004903 RID: 18691
		[NonSerialized]
		private bool ForceExplode;
	}
}
