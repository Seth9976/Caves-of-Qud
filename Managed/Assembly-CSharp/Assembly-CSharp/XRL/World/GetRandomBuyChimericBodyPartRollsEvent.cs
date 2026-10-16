using System;

namespace XRL.World
{
	// Token: 0x0200077D RID: 1917
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetRandomBuyChimericBodyPartRollsEvent : PooledEvent<GetRandomBuyChimericBodyPartRollsEvent>
	{
		// Token: 0x06004EAE RID: 20142 RVA: 0x00181300 File Offset: 0x0017F500
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00181309 File Offset: 0x0017F509
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.BaseAmount = 0;
			this.Amount = 0;
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x00181326 File Offset: 0x0017F526
		public static GetRandomBuyChimericBodyPartRollsEvent FromPool(GameObject Actor, int BaseAmount, int Amount)
		{
			GetRandomBuyChimericBodyPartRollsEvent getRandomBuyChimericBodyPartRollsEvent = PooledEvent<GetRandomBuyChimericBodyPartRollsEvent>.FromPool();
			getRandomBuyChimericBodyPartRollsEvent.Actor = Actor;
			getRandomBuyChimericBodyPartRollsEvent.BaseAmount = BaseAmount;
			getRandomBuyChimericBodyPartRollsEvent.Amount = Amount;
			return getRandomBuyChimericBodyPartRollsEvent;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00181344 File Offset: 0x0017F544
		public static int GetFor(GameObject Actor, int BaseAmount)
		{
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetRandomBuyChimericBodyPartRollsEvent>.ID, MinEvent.CascadeLevel))
			{
				GetRandomBuyChimericBodyPartRollsEvent getRandomBuyChimericBodyPartRollsEvent = GetRandomBuyChimericBodyPartRollsEvent.FromPool(Actor, BaseAmount, BaseAmount);
				Actor.HandleEvent(getRandomBuyChimericBodyPartRollsEvent);
				return getRandomBuyChimericBodyPartRollsEvent.Amount;
			}
			return BaseAmount;
		}

		// Token: 0x04002673 RID: 9843
		public GameObject Actor;

		// Token: 0x04002674 RID: 9844
		public int BaseAmount;

		// Token: 0x04002675 RID: 9845
		public int Amount;
	}
}
