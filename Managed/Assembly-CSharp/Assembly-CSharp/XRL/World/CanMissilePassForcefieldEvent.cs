using System;

namespace XRL.World
{
	// Token: 0x020006C8 RID: 1736
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanMissilePassForcefieldEvent : PooledEvent<CanMissilePassForcefieldEvent>
	{
		// Token: 0x0600497A RID: 18810 RVA: 0x00171D84 File Offset: 0x0016FF84
		public override int GetCascadeLevel()
		{
			return CanMissilePassForcefieldEvent.CascadeLevel;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00171D8B File Offset: 0x0016FF8B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00171D94 File Offset: 0x0016FF94
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Projectile = null;
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00171DAC File Offset: 0x0016FFAC
		public static bool Check(GameObject Actor, GameObject Projectile)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("CanMissilePassForcefield"))
			{
				Event @event = Event.New("CanMissilePassForcefield", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Projectile", Projectile);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CanMissilePassForcefieldEvent>.ID, CanMissilePassForcefieldEvent.CascadeLevel))
			{
				CanMissilePassForcefieldEvent canMissilePassForcefieldEvent = PooledEvent<CanMissilePassForcefieldEvent>.FromPool();
				canMissilePassForcefieldEvent.Actor = Actor;
				canMissilePassForcefieldEvent.Projectile = Projectile;
				flag = Actor.HandleEvent(canMissilePassForcefieldEvent);
			}
			return !flag;
		}

		// Token: 0x04002350 RID: 9040
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002351 RID: 9041
		public GameObject Actor;

		// Token: 0x04002352 RID: 9042
		public GameObject Projectile;
	}
}
