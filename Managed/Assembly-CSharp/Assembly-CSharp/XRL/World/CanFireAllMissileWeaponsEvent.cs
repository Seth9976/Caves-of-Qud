using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006C4 RID: 1732
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanFireAllMissileWeaponsEvent : PooledEvent<CanFireAllMissileWeaponsEvent>
	{
		// Token: 0x0600495B RID: 18779 RVA: 0x0017170E File Offset: 0x0016F90E
		public override int GetCascadeLevel()
		{
			return CanFireAllMissileWeaponsEvent.CascadeLevel;
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x00171715 File Offset: 0x0016F915
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0017171E File Offset: 0x0016F91E
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.MissileWeapons = null;
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x00171734 File Offset: 0x0016F934
		public static CanFireAllMissileWeaponsEvent FromPool(GameObject Actor)
		{
			CanFireAllMissileWeaponsEvent canFireAllMissileWeaponsEvent = PooledEvent<CanFireAllMissileWeaponsEvent>.FromPool();
			canFireAllMissileWeaponsEvent.Actor = Actor;
			return canFireAllMissileWeaponsEvent;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x00171744 File Offset: 0x0016F944
		public static bool Check(GameObject Actor, List<GameObject> MissileWeapons = null)
		{
			if (MissileWeapons == null && Actor != null)
			{
				MissileWeapons = Actor.GetMissileWeapons(null, null);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("CanFireAllMissileWeapons"))
			{
				Event @event = Event.New("CanFireAllMissileWeapons", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CanFireAllMissileWeaponsEvent>.ID, CanFireAllMissileWeaponsEvent.CascadeLevel))
			{
				flag = Actor.HandleEvent(CanFireAllMissileWeaponsEvent.FromPool(Actor));
			}
			return !flag;
		}

		// Token: 0x0400233D RID: 9021
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400233E RID: 9022
		public GameObject Actor;

		// Token: 0x0400233F RID: 9023
		public List<GameObject> MissileWeapons;
	}
}
