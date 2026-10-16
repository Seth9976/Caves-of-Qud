using System;

namespace XRL.World
{
	// Token: 0x0200068A RID: 1674
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class BeforeBeginTakeActionEvent : SingletonEvent<BeforeBeginTakeActionEvent>
	{
		// Token: 0x060047B4 RID: 18356 RVA: 0x0016A3AE File Offset: 0x001685AE
		public override int GetCascadeLevel()
		{
			return BeforeBeginTakeActionEvent.CascadeLevel;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x0016A3B5 File Offset: 0x001685B5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x0016A3BE File Offset: 0x001685BE
		public override void Reset()
		{
			base.Reset();
			this.PreventAction = false;
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x0016A3D0 File Offset: 0x001685D0
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && Object.HasRegisteredEvent(BeforeBeginTakeActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(BeforeBeginTakeActionEvent.registeredInstance);
			}
			if (flag)
			{
				SingletonEvent<BeforeBeginTakeActionEvent>.Instance.PreventAction = false;
				flag = Object.HandleEvent(SingletonEvent<BeforeBeginTakeActionEvent>.Instance) && !SingletonEvent<BeforeBeginTakeActionEvent>.Instance.PreventAction;
			}
			return flag;
		}

		// Token: 0x0400222F RID: 8751
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002230 RID: 8752
		public bool PreventAction;

		// Token: 0x04002231 RID: 8753
		public static ImmutableEvent registeredInstance = new ImmutableEvent("BeforeBeginTakeAction");
	}
}
