using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006CB RID: 1739
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanRefreshAbilityEvent : PooledEvent<CanRefreshAbilityEvent>
	{
		// Token: 0x0600498A RID: 18826 RVA: 0x00171F17 File Offset: 0x00170117
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x00171F20 File Offset: 0x00170120
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Ability = null;
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00171F38 File Offset: 0x00170138
		public static bool Check(GameObject Actor, ActivatedAbilityEntry Ability)
		{
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CanRefreshAbilityEvent>.ID, MinEvent.CascadeLevel))
			{
				CanRefreshAbilityEvent canRefreshAbilityEvent = PooledEvent<CanRefreshAbilityEvent>.FromPool();
				canRefreshAbilityEvent.Actor = Actor;
				canRefreshAbilityEvent.Ability = Ability;
				return Actor.HandleEvent(canRefreshAbilityEvent);
			}
			return true;
		}

		// Token: 0x04002357 RID: 9047
		public GameObject Actor;

		// Token: 0x04002358 RID: 9048
		public ActivatedAbilityEntry Ability;
	}
}
