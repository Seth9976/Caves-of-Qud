using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006EB RID: 1771
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CommandSmartUseEarlyEvent : IActOnItemEvent
	{
		// Token: 0x06004A51 RID: 19025 RVA: 0x00174159 File Offset: 0x00172359
		public CommandSmartUseEarlyEvent()
		{
			this.ID = CommandSmartUseEarlyEvent.ID;
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x0017416C File Offset: 0x0017236C
		public static int CountPool()
		{
			if (CommandSmartUseEarlyEvent.Pool != null)
			{
				return CommandSmartUseEarlyEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00174181 File Offset: 0x00172381
		public static void ResetPool()
		{
			while (CommandSmartUseEarlyEvent.PoolCounter > 0)
			{
				CommandSmartUseEarlyEvent.Pool[--CommandSmartUseEarlyEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x001741A9 File Offset: 0x001723A9
		public static void ResetTo(ref CommandSmartUseEarlyEvent E)
		{
			MinEvent.ResetTo<CommandSmartUseEarlyEvent>(E, CommandSmartUseEarlyEvent.Pool, ref CommandSmartUseEarlyEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x001741BF File Offset: 0x001723BF
		public static CommandSmartUseEarlyEvent FromPool()
		{
			return MinEvent.FromPool<CommandSmartUseEarlyEvent>(ref CommandSmartUseEarlyEvent.Pool, ref CommandSmartUseEarlyEvent.PoolCounter, 8192);
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x001741D5 File Offset: 0x001723D5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x001741E9 File Offset: 0x001723E9
		public static CommandSmartUseEarlyEvent FromPool(GameObject Actor, GameObject Item)
		{
			CommandSmartUseEarlyEvent commandSmartUseEarlyEvent = CommandSmartUseEarlyEvent.FromPool();
			commandSmartUseEarlyEvent.Actor = Actor;
			commandSmartUseEarlyEvent.Item = Item;
			return commandSmartUseEarlyEvent;
		}

		// Token: 0x040023D2 RID: 9170
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CommandSmartUseEarlyEvent), null, new MinEvent.EventPoolCount(CommandSmartUseEarlyEvent.CountPool), new MinEvent.EventPoolReset(CommandSmartUseEarlyEvent.ResetPool));

		// Token: 0x040023D3 RID: 9171
		private static List<CommandSmartUseEarlyEvent> Pool;

		// Token: 0x040023D4 RID: 9172
		private static int PoolCounter;
	}
}
