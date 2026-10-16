using System;

namespace XRL.World
{
	// Token: 0x020007C5 RID: 1989
	[GameEvent(Base = true)]
	public abstract class IDeathEvent : MinEvent
	{
		// Token: 0x06005070 RID: 20592 RVA: 0x00186A71 File Offset: 0x00184C71
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x00186A7C File Offset: 0x00184C7C
		public override void Reset()
		{
			base.Reset();
			this.Dying = null;
			this.Killer = null;
			this.Weapon = null;
			this.Projectile = null;
			this.KillerText = null;
			this.Reason = null;
			this.ThirdPersonReason = null;
			this.Accidental = false;
			this.AlwaysUsePopups = false;
		}

		// Token: 0x040027ED RID: 10221
		public GameObject Dying;

		// Token: 0x040027EE RID: 10222
		public GameObject Killer;

		// Token: 0x040027EF RID: 10223
		public GameObject Weapon;

		// Token: 0x040027F0 RID: 10224
		public GameObject Projectile;

		// Token: 0x040027F1 RID: 10225
		public string KillerText;

		// Token: 0x040027F2 RID: 10226
		public string Reason;

		// Token: 0x040027F3 RID: 10227
		public string ThirdPersonReason;

		// Token: 0x040027F4 RID: 10228
		public bool Accidental;

		// Token: 0x040027F5 RID: 10229
		public bool AlwaysUsePopups;
	}
}
