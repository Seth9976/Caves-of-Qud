using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006EC RID: 1772
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CommandSmartUseEvent : IActOnItemEvent
	{
		// Token: 0x06004A59 RID: 19033 RVA: 0x0017422D File Offset: 0x0017242D
		public CommandSmartUseEvent()
		{
			this.ID = CommandSmartUseEvent.ID;
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00174240 File Offset: 0x00172440
		public static int CountPool()
		{
			if (CommandSmartUseEvent.Pool != null)
			{
				return CommandSmartUseEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x00174255 File Offset: 0x00172455
		public static void ResetPool()
		{
			while (CommandSmartUseEvent.PoolCounter > 0)
			{
				CommandSmartUseEvent.Pool[--CommandSmartUseEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0017427D File Offset: 0x0017247D
		public static void ResetTo(ref CommandSmartUseEvent E)
		{
			MinEvent.ResetTo<CommandSmartUseEvent>(E, CommandSmartUseEvent.Pool, ref CommandSmartUseEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00174293 File Offset: 0x00172493
		public static CommandSmartUseEvent FromPool()
		{
			return MinEvent.FromPool<CommandSmartUseEvent>(ref CommandSmartUseEvent.Pool, ref CommandSmartUseEvent.PoolCounter, 8192);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x001742A9 File Offset: 0x001724A9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x001742BD File Offset: 0x001724BD
		public override void Reset()
		{
			base.Reset();
			this.MinPriority = 0;
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x001742CC File Offset: 0x001724CC
		public static CommandSmartUseEvent FromPool(GameObject Actor, GameObject Item, int MinPriority)
		{
			CommandSmartUseEvent commandSmartUseEvent = CommandSmartUseEvent.FromPool();
			commandSmartUseEvent.Actor = Actor;
			commandSmartUseEvent.Item = Item;
			commandSmartUseEvent.MinPriority = MinPriority;
			return commandSmartUseEvent;
		}

		// Token: 0x040023D5 RID: 9173
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CommandSmartUseEvent), null, new MinEvent.EventPoolCount(CommandSmartUseEvent.CountPool), new MinEvent.EventPoolReset(CommandSmartUseEvent.ResetPool));

		// Token: 0x040023D6 RID: 9174
		private static List<CommandSmartUseEvent> Pool;

		// Token: 0x040023D7 RID: 9175
		private static int PoolCounter;

		// Token: 0x040023D8 RID: 9176
		public int MinPriority;
	}
}
