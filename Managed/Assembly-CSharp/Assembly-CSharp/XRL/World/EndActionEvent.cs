using System;

namespace XRL.World
{
	// Token: 0x02000705 RID: 1797
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class EndActionEvent : SingletonEvent<EndActionEvent>
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x00175E99 File Offset: 0x00174099
		public override int GetCascadeLevel()
		{
			return EndActionEvent.CascadeLevel;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00175EA0 File Offset: 0x001740A0
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00175EAC File Offset: 0x001740AC
		public static void Send(GameObject Object)
		{
			bool flag = true;
			if (flag && Object.HasRegisteredEvent(EndActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(EndActionEvent.registeredInstance);
			}
			if (flag)
			{
				flag = Object.HandleEvent(SingletonEvent<EndActionEvent>.Instance);
			}
		}

		// Token: 0x0400243E RID: 9278
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400243F RID: 9279
		public static ImmutableEvent registeredInstance = new ImmutableEvent("EndAction");
	}
}
