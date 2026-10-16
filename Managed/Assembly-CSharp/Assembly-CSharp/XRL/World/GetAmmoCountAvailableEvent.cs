using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000726 RID: 1830
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetAmmoCountAvailableEvent : PooledEvent<GetAmmoCountAvailableEvent>
	{
		// Token: 0x06004BF9 RID: 19449 RVA: 0x0017868D File Offset: 0x0017688D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x00178696 File Offset: 0x00176896
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.MissileWeapon = null;
			this.Count = 0;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x001786B3 File Offset: 0x001768B3
		public void Register(int Amount)
		{
			if (Amount > 0 && (this.Count == 0 || this.Count > Amount))
			{
				this.Count = Amount;
			}
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x001786D4 File Offset: 0x001768D4
		public static int GetFor(GameObject Object, MissileWeapon MissileWeapon = null)
		{
			if (MissileWeapon == null)
			{
				MissileWeapon = ((Object != null) ? Object.GetPart<MissileWeapon>() : null);
			}
			bool flag = true;
			int num = 0;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetAmmoCountAvailable"))
			{
				Event @event = Event.New("GetAmmoCountAvailable", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("MissileWeapon", MissileWeapon);
				@event.SetParameter("Count", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Count", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetAmmoCountAvailableEvent>.ID, MinEvent.CascadeLevel))
			{
				GetAmmoCountAvailableEvent getAmmoCountAvailableEvent = PooledEvent<GetAmmoCountAvailableEvent>.FromPool();
				getAmmoCountAvailableEvent.Object = Object;
				getAmmoCountAvailableEvent.MissileWeapon = MissileWeapon;
				getAmmoCountAvailableEvent.Count = num;
				flag = Object.HandleEvent(getAmmoCountAvailableEvent);
				num = getAmmoCountAvailableEvent.Count;
			}
			return num;
		}

		// Token: 0x040024C0 RID: 9408
		public GameObject Object;

		// Token: 0x040024C1 RID: 9409
		public MissileWeapon MissileWeapon;

		// Token: 0x040024C2 RID: 9410
		public int Count;
	}
}
