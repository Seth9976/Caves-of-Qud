using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D56 RID: 3414
	[Serializable]
	public class Invulnerable : Effect
	{
		// Token: 0x060086EA RID: 34538 RVA: 0x003072F8 File Offset: 0x003054F8
		public Invulnerable()
		{
			this.DisplayName = "{{O|invulnerable}}";
		}

		// Token: 0x060086EB RID: 34539 RVA: 0x0030730B File Offset: 0x0030550B
		public Invulnerable(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060086EC RID: 34540 RVA: 0x0030731A File Offset: 0x0030551A
		public override string GetDescription()
		{
			if (!this.VisibleFlag)
			{
				return null;
			}
			return base.GetDescription();
		}

		// Token: 0x060086ED RID: 34541 RVA: 0x0030732C File Offset: 0x0030552C
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x00307330 File Offset: 0x00305530
		public override bool Apply(GameObject Object)
		{
			Invulnerable invulnerable;
			if (Object.TryGetEffect<Invulnerable>(out invulnerable))
			{
				invulnerable.Duration = Math.Max(invulnerable.Duration, this.Duration);
				invulnerable.VisibleFlag = this.VisibleFlag;
				return false;
			}
			return base.Apply(Object);
		}

		// Token: 0x060086EF RID: 34543 RVA: 0x00307373 File Offset: 0x00305573
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == BeforeDieEvent.ID || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID;
		}

		// Token: 0x060086F0 RID: 34544 RVA: 0x003073A1 File Offset: 0x003055A1
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return E.Object != base.Object && base.HandleEvent(E);
		}

		// Token: 0x060086F1 RID: 34545 RVA: 0x003073BA File Offset: 0x003055BA
		public override bool HandleEvent(BeforeDieEvent E)
		{
			return E.Dying != base.Object && base.HandleEvent(E);
		}

		// Token: 0x060086F2 RID: 34546 RVA: 0x003073D3 File Offset: 0x003055D3
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != base.Object && base.HandleEvent(E);
		}

		// Token: 0x060086F3 RID: 34547 RVA: 0x003073EC File Offset: 0x003055EC
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == base.Object)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040039FF RID: 14847
		public bool VisibleFlag;
	}
}
