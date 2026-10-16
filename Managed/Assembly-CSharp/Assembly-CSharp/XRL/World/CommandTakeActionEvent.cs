using System;

namespace XRL.World
{
	// Token: 0x020006ED RID: 1773
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class CommandTakeActionEvent : SingletonEvent<CommandTakeActionEvent>
	{
		// Token: 0x06004A62 RID: 19042 RVA: 0x00174317 File Offset: 0x00172517
		public override int GetCascadeLevel()
		{
			return CommandTakeActionEvent.CascadeLevel;
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x0017431E File Offset: 0x0017251E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x00174327 File Offset: 0x00172527
		public override void Reset()
		{
			base.Reset();
			this.PreventAction = false;
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00174338 File Offset: 0x00172538
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (Object.HasRegisteredEvent(CommandTakeActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(CommandTakeActionEvent.registeredInstance);
			}
			if (flag)
			{
				SingletonEvent<CommandTakeActionEvent>.Instance.PreventAction = false;
				flag = Object.HandleEvent(SingletonEvent<CommandTakeActionEvent>.Instance) && !SingletonEvent<CommandTakeActionEvent>.Instance.PreventAction;
			}
			return flag;
		}

		// Token: 0x040023D9 RID: 9177
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040023DA RID: 9178
		public bool PreventAction;

		// Token: 0x040023DB RID: 9179
		public static ImmutableEvent registeredInstance = new ImmutableEvent("CommandTakeAction");
	}
}
