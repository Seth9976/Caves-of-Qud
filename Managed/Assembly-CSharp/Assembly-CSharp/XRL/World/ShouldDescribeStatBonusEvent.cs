using System;

namespace XRL.World
{
	// Token: 0x02000859 RID: 2137
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ShouldDescribeStatBonusEvent : PooledEvent<ShouldDescribeStatBonusEvent>
	{
		// Token: 0x06005549 RID: 21833 RVA: 0x001982CF File Offset: 0x001964CF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x001982D8 File Offset: 0x001964D8
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Component = null;
			this.Stat = null;
			this.Amount = 0;
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x001982FC File Offset: 0x001964FC
		public static bool Check(GameObject Object, IComponent<GameObject> Component, string Stat, int Amount)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("ShouldDescribeStatBonus"))
			{
				Event @event = Event.New("ShouldDescribeStatBonus", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Component", Component);
				@event.SetParameter("Stat", Stat);
				@event.SetParameter("Amount", Amount);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<ShouldDescribeStatBonusEvent>.ID, MinEvent.CascadeLevel))
			{
				ShouldDescribeStatBonusEvent shouldDescribeStatBonusEvent = PooledEvent<ShouldDescribeStatBonusEvent>.FromPool();
				shouldDescribeStatBonusEvent.Object = Object;
				shouldDescribeStatBonusEvent.Component = Component;
				shouldDescribeStatBonusEvent.Stat = Stat;
				shouldDescribeStatBonusEvent.Amount = Amount;
				flag = Object.HandleEvent(shouldDescribeStatBonusEvent);
			}
			return flag;
		}

		// Token: 0x04002AEB RID: 10987
		public GameObject Object;

		// Token: 0x04002AEC RID: 10988
		public IComponent<GameObject> Component;

		// Token: 0x04002AED RID: 10989
		public string Stat;

		// Token: 0x04002AEE RID: 10990
		public int Amount;
	}
}
