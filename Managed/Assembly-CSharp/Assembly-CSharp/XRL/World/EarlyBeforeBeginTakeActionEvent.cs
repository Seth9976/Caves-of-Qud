using System;

namespace XRL.World
{
	// Token: 0x020006FD RID: 1789
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class EarlyBeforeBeginTakeActionEvent : SingletonEvent<EarlyBeforeBeginTakeActionEvent>
	{
		// Token: 0x06004AD3 RID: 19155 RVA: 0x0017542F File Offset: 0x0017362F
		public override int GetCascadeLevel()
		{
			return EarlyBeforeBeginTakeActionEvent.CascadeLevel;
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00175436 File Offset: 0x00173636
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x0017543F File Offset: 0x0017363F
		public override void Reset()
		{
			base.Reset();
			this.PreventAction = false;
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00175450 File Offset: 0x00173650
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && Object.HasRegisteredEvent(EarlyBeforeBeginTakeActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(EarlyBeforeBeginTakeActionEvent.registeredInstance);
			}
			if (flag)
			{
				SingletonEvent<EarlyBeforeBeginTakeActionEvent>.Instance.PreventAction = false;
				flag = Object.HandleEvent(SingletonEvent<EarlyBeforeBeginTakeActionEvent>.Instance) && !SingletonEvent<EarlyBeforeBeginTakeActionEvent>.Instance.PreventAction;
			}
			return flag;
		}

		// Token: 0x04002424 RID: 9252
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002425 RID: 9253
		public bool PreventAction;

		// Token: 0x04002426 RID: 9254
		public static ImmutableEvent registeredInstance = new ImmutableEvent("EarlyBeforeBeginTakeAction");
	}
}
