using System;

namespace XRL.World
{
	// Token: 0x020006D7 RID: 1751
	[GameEvent(Cascade = 7, Cache = MinEvent.Cache.Pool)]
	public class CheckAnythingToCleanWithEvent : PooledEvent<CheckAnythingToCleanWithEvent>
	{
		// Token: 0x060049E1 RID: 18913 RVA: 0x00172CFC File Offset: 0x00170EFC
		public override int GetCascadeLevel()
		{
			return CheckAnythingToCleanWithEvent.CascadeLevel;
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x00172D03 File Offset: 0x00170F03
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x00172D0C File Offset: 0x00170F0C
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Item = null;
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x00172D22 File Offset: 0x00170F22
		public static CheckAnythingToCleanWithEvent FromPool(GameObject Actor, GameObject Item)
		{
			CheckAnythingToCleanWithEvent checkAnythingToCleanWithEvent = PooledEvent<CheckAnythingToCleanWithEvent>.FromPool();
			checkAnythingToCleanWithEvent.Actor = Actor;
			checkAnythingToCleanWithEvent.Item = Item;
			return checkAnythingToCleanWithEvent;
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x00172D37 File Offset: 0x00170F37
		public static bool Check(GameObject Actor, GameObject Item)
		{
			return (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CheckAnythingToCleanWithEvent>.ID, CheckAnythingToCleanWithEvent.CascadeLevel) && !Actor.HandleEvent(CheckAnythingToCleanWithEvent.FromPool(Actor, Item))) || CheckAnythingToCleanWithNearbyEvent.Check(Actor, Item);
		}

		// Token: 0x0400237E RID: 9086
		public new static readonly int CascadeLevel = 7;

		// Token: 0x0400237F RID: 9087
		public GameObject Actor;

		// Token: 0x04002380 RID: 9088
		public GameObject Item;
	}
}
