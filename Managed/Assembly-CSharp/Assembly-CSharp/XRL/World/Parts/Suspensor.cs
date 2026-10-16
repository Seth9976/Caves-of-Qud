using System;

namespace XRL.World.Parts
{
	// Token: 0x020010A6 RID: 4262
	[Serializable]
	public class Suspensor : IPoweredPart
	{
		// Token: 0x0600A99F RID: 43423 RVA: 0x003E0267 File Offset: 0x003DE467
		public Suspensor()
		{
			this.IsRealityDistortionBased = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A9A0 RID: 43424 RVA: 0x003E0280 File Offset: 0x003DE480
		public override bool SameAs(IPart p)
		{
			Suspensor suspensor = p as Suspensor;
			return suspensor.Force == this.Force && suspensor.PercentageForce == this.PercentageForce && base.SameAs(p);
		}

		// Token: 0x0600A9A1 RID: 43425 RVA: 0x003E02BC File Offset: 0x003DE4BC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustTotalWeightEvent.ID || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == PowerSwitchFlippedEvent.ID;
		}

		// Token: 0x0600A9A2 RID: 43426 RVA: 0x003E0310 File Offset: 0x003DE510
		public override bool HandleEvent(AdjustTotalWeightEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.PercentageForce != 0)
				{
					E.Weight -= E.Weight * (double)this.PercentageForce / 100.0;
				}
				if (this.Force != 0)
				{
					E.Weight -= (double)this.Force;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A3 RID: 43427 RVA: 0x003E0395 File Offset: 0x003DE595
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			base.FlushWeightCaches();
			this.ParentObject.Gravitate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A4 RID: 43428 RVA: 0x003E03AF File Offset: 0x003DE5AF
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			base.FlushWeightCaches();
			this.ParentObject.Gravitate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A5 RID: 43429 RVA: 0x003E03C9 File Offset: 0x003DE5C9
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			base.FlushWeightCaches();
			this.ParentObject.Gravitate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A6 RID: 43430 RVA: 0x003E03E3 File Offset: 0x003DE5E3
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			base.FlushWeightCaches();
			this.ParentObject.Gravitate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A7 RID: 43431 RVA: 0x003E03FD File Offset: 0x003DE5FD
		public override bool HandleEvent(CellChangedEvent E)
		{
			base.FlushWeightCaches();
			this.ParentObject.Gravitate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A8 RID: 43432 RVA: 0x003E0417 File Offset: 0x003DE617
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			base.FlushWeightCaches();
			this.ParentObject.Gravitate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9A9 RID: 43433 RVA: 0x003E0431 File Offset: 0x003DE631
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A9AA RID: 43434 RVA: 0x003E0434 File Offset: 0x003DE634
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
		}

		// Token: 0x040045F3 RID: 17907
		public int Force;

		// Token: 0x040045F4 RID: 17908
		public int PercentageForce;
	}
}
