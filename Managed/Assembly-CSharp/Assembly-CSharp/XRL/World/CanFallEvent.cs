using System;

namespace XRL.World
{
	// Token: 0x020006C3 RID: 1731
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanFallEvent : PooledEvent<CanFallEvent>
	{
		// Token: 0x06004955 RID: 18773 RVA: 0x001715DE File Offset: 0x0016F7DE
		public override int GetCascadeLevel()
		{
			return CanFallEvent.CascadeLevel;
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x001715E5 File Offset: 0x0016F7E5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x001715EE File Offset: 0x0016F7EE
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.CanFall = false;
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x00171604 File Offset: 0x0016F804
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (GameObject.Validate(ref Object))
			{
				if (Object.HasPropertyOrTag("SuspendedPlatform"))
				{
					flag = false;
				}
				else if (Object.IsScenery)
				{
					flag = false;
				}
				else if (Object.IsFlying)
				{
					flag = false;
				}
				else if (Object.GetWeight() < 0.0)
				{
					flag = false;
				}
				else if (!Object.IsSubjectToGravity)
				{
					flag = false;
				}
			}
			bool flag2 = true;
			if (flag2 && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanFall"))
			{
				Event @event = Event.New("CanFall", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetFlag("CanFall", flag);
				flag2 = Object.FireEvent(@event);
				flag = @event.HasFlag("CanFall");
			}
			if (flag2 && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanFallEvent>.ID, CanFallEvent.CascadeLevel))
			{
				CanFallEvent canFallEvent = PooledEvent<CanFallEvent>.FromPool();
				canFallEvent.Object = Object;
				canFallEvent.CanFall = flag;
				flag2 = Object.HandleEvent(canFallEvent);
				flag = canFallEvent.CanFall;
			}
			return flag;
		}

		// Token: 0x0400233A RID: 9018
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400233B RID: 9019
		public GameObject Object;

		// Token: 0x0400233C RID: 9020
		public bool CanFall;
	}
}
