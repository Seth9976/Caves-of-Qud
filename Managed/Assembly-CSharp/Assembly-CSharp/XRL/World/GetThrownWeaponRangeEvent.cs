using System;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000798 RID: 1944
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetThrownWeaponRangeEvent : PooledEvent<GetThrownWeaponRangeEvent>
	{
		// Token: 0x06004F5C RID: 20316 RVA: 0x001837F7 File Offset: 0x001819F7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00183800 File Offset: 0x00181A00
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.MaxRange = 0;
			this.MidRange = 0;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00183824 File Offset: 0x00181A24
		public static void GetFor(out int MaxRange, out int MidRange, GameObject Object, GameObject Actor, int DefaultMaxRange = 9999, int DefaultMidRange = -1)
		{
			MaxRange = DefaultMaxRange;
			MidRange = DefaultMidRange;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetThrownWeaponRange"))
			{
				Event @event = Event.New("GetThrownWeaponRange", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("MaxRange", MaxRange);
				@event.SetParameter("MidRange", MidRange);
				flag = Object.FireEvent(@event);
				MaxRange = @event.GetIntParameter("MaxRange", 0);
				MidRange = @event.GetIntParameter("MidRange", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetThrownWeaponRangeEvent>.ID, MinEvent.CascadeLevel))
			{
				GetThrownWeaponRangeEvent getThrownWeaponRangeEvent = PooledEvent<GetThrownWeaponRangeEvent>.FromPool();
				getThrownWeaponRangeEvent.Object = Object;
				getThrownWeaponRangeEvent.Actor = Actor;
				getThrownWeaponRangeEvent.MaxRange = MaxRange;
				getThrownWeaponRangeEvent.MidRange = MidRange;
				flag = Object.HandleEvent(getThrownWeaponRangeEvent);
				MaxRange = getThrownWeaponRangeEvent.MaxRange;
				MidRange = getThrownWeaponRangeEvent.MidRange;
			}
			if (flag && MidRange == DefaultMidRange)
			{
				int num;
				int num2;
				bool flag2;
				GetThrowProfileEvent.Process(out MidRange, out num, out num2, out flag2, Actor, Object, null, null, 0);
				if (MaxRange == DefaultMaxRange)
				{
					MaxRange = MidRange + RuleSettings.THROW_RANGE_RANDOM_VARIANCE_MAX;
				}
				MidRange += RuleSettings.THROW_RANGE_RANDOM_VARIANCE_MIN;
			}
		}

		// Token: 0x04002711 RID: 10001
		public GameObject Object;

		// Token: 0x04002712 RID: 10002
		public GameObject Actor;

		// Token: 0x04002713 RID: 10003
		public int MaxRange;

		// Token: 0x04002714 RID: 10004
		public int MidRange;
	}
}
