using System;

namespace XRL.World
{
	// Token: 0x020007C4 RID: 1988
	[GameEvent(Base = true)]
	public abstract class IDamageEvent : MinEvent
	{
		// Token: 0x0600506D RID: 20589 RVA: 0x00186A06 File Offset: 0x00184C06
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x00186A10 File Offset: 0x00184C10
		public override void Reset()
		{
			base.Reset();
			this.Damage = null;
			this.Object = null;
			this.Actor = null;
			this.Source = null;
			this.Weapon = null;
			this.Projectile = null;
			this.Indirect = false;
			this.WillUseOutcomeMessageFragment = false;
			this.DidSpecialEffect = false;
			this.OutcomeMessageFragment = null;
		}

		// Token: 0x040027E3 RID: 10211
		public Damage Damage;

		// Token: 0x040027E4 RID: 10212
		public GameObject Object;

		// Token: 0x040027E5 RID: 10213
		public GameObject Actor;

		// Token: 0x040027E6 RID: 10214
		public GameObject Source;

		// Token: 0x040027E7 RID: 10215
		public GameObject Weapon;

		// Token: 0x040027E8 RID: 10216
		public GameObject Projectile;

		// Token: 0x040027E9 RID: 10217
		public bool Indirect;

		// Token: 0x040027EA RID: 10218
		public bool WillUseOutcomeMessageFragment;

		// Token: 0x040027EB RID: 10219
		public bool DidSpecialEffect;

		// Token: 0x040027EC RID: 10220
		public string OutcomeMessageFragment;
	}
}
