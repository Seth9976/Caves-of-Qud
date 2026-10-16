using System;

namespace XRL.World
{
	// Token: 0x020006A2 RID: 1698
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class BeforeTakeActionEvent : SingletonEvent<BeforeTakeActionEvent>
	{
		// Token: 0x06004862 RID: 18530 RVA: 0x0016C272 File Offset: 0x0016A472
		public override int GetCascadeLevel()
		{
			return BeforeTakeActionEvent.CascadeLevel;
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x0016C279 File Offset: 0x0016A479
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x0016C282 File Offset: 0x0016A482
		public override void Reset()
		{
			base.Reset();
			this.PreventAction = false;
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x0016C294 File Offset: 0x0016A494
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && Object.HasRegisteredEvent(BeforeTakeActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(BeforeTakeActionEvent.registeredInstance);
			}
			if (flag)
			{
				SingletonEvent<BeforeTakeActionEvent>.Instance.PreventAction = false;
				flag = Object.HandleEvent(SingletonEvent<BeforeTakeActionEvent>.Instance) && !SingletonEvent<BeforeTakeActionEvent>.Instance.PreventAction;
			}
			return flag;
		}

		// Token: 0x040022A1 RID: 8865
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040022A2 RID: 8866
		public bool PreventAction;

		// Token: 0x040022A3 RID: 8867
		public static ImmutableEvent registeredInstance = new ImmutableEvent("BeforeTakeAction");
	}
}
