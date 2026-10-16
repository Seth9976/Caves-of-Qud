using System;

namespace XRL.World
{
	// Token: 0x02000858 RID: 2136
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ShouldAttackToReachTargetEvent : PooledEvent<ShouldAttackToReachTargetEvent>
	{
		// Token: 0x06005545 RID: 21829 RVA: 0x00198132 File Offset: 0x00196332
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x0019813B File Offset: 0x0019633B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Target = null;
			this.ShouldAttack = false;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x00198160 File Offset: 0x00196360
		public static bool Check(GameObject Actor, GameObject Object, GameObject Target = null, bool ShouldAttack = false)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("ShouldAttackToReachTarget");
				bool flag3 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("ShouldAttackToReachTarget");
				if (flag2 || flag3)
				{
					Event @event = Event.New("ShouldAttackToReachTarget", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Target", Target);
					@event.SetFlag("ShouldAttack", ShouldAttack);
					flag = flag && (!flag2 || Actor.FireEvent(@event));
					flag = flag && (!flag3 || Object.FireEvent(@event));
					ShouldAttack = @event.HasFlag("ShouldAttack");
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<ShouldAttackToReachTargetEvent>.ID, MinEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<ShouldAttackToReachTargetEvent>.ID, MinEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					ShouldAttackToReachTargetEvent shouldAttackToReachTargetEvent = PooledEvent<ShouldAttackToReachTargetEvent>.FromPool();
					shouldAttackToReachTargetEvent.Actor = Actor;
					shouldAttackToReachTargetEvent.Object = Object;
					shouldAttackToReachTargetEvent.Target = Target;
					shouldAttackToReachTargetEvent.ShouldAttack = ShouldAttack;
					flag = flag && (!flag4 || Actor.HandleEvent(shouldAttackToReachTargetEvent));
					bool flag6 = flag && (!flag5 || Object.HandleEvent(shouldAttackToReachTargetEvent));
					ShouldAttack = shouldAttackToReachTargetEvent.ShouldAttack;
				}
			}
			return ShouldAttack;
		}

		// Token: 0x04002AE7 RID: 10983
		public GameObject Actor;

		// Token: 0x04002AE8 RID: 10984
		public GameObject Object;

		// Token: 0x04002AE9 RID: 10985
		public GameObject Target;

		// Token: 0x04002AEA RID: 10986
		public bool ShouldAttack;
	}
}
