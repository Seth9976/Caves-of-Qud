using System;

namespace XRL.World
{
	// Token: 0x0200077E RID: 1918
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetRandomBuyMutationCountEvent : PooledEvent<GetRandomBuyMutationCountEvent>
	{
		// Token: 0x06004EB3 RID: 20147 RVA: 0x0018138D File Offset: 0x0017F58D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00181396 File Offset: 0x0017F596
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.BaseAmount = 0;
			this.Amount = 0;
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x001813B3 File Offset: 0x0017F5B3
		public static GetRandomBuyMutationCountEvent FromPool(GameObject Actor, int BaseAmount, int Amount)
		{
			GetRandomBuyMutationCountEvent getRandomBuyMutationCountEvent = PooledEvent<GetRandomBuyMutationCountEvent>.FromPool();
			getRandomBuyMutationCountEvent.Actor = Actor;
			getRandomBuyMutationCountEvent.BaseAmount = BaseAmount;
			getRandomBuyMutationCountEvent.Amount = Amount;
			return getRandomBuyMutationCountEvent;
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x001813D0 File Offset: 0x0017F5D0
		public static int GetFor(GameObject Actor, int BaseAmount)
		{
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetRandomBuyMutationCountEvent>.ID, MinEvent.CascadeLevel))
			{
				GetRandomBuyMutationCountEvent getRandomBuyMutationCountEvent = GetRandomBuyMutationCountEvent.FromPool(Actor, BaseAmount, BaseAmount);
				Actor.HandleEvent(getRandomBuyMutationCountEvent);
				return getRandomBuyMutationCountEvent.Amount;
			}
			return BaseAmount;
		}

		// Token: 0x04002676 RID: 9846
		public GameObject Actor;

		// Token: 0x04002677 RID: 9847
		public int BaseAmount;

		// Token: 0x04002678 RID: 9848
		public int Amount;
	}
}
