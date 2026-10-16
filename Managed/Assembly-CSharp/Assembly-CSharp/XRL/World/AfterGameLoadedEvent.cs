using System;

namespace XRL.World
{
	// Token: 0x02000660 RID: 1632
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class AfterGameLoadedEvent : SingletonEvent<AfterGameLoadedEvent>
	{
		// Token: 0x0600467B RID: 18043 RVA: 0x00166CFF File Offset: 0x00164EFF
		public override int GetCascadeLevel()
		{
			return AfterGameLoadedEvent.CascadeLevel;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00166D06 File Offset: 0x00164F06
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00166D0F File Offset: 0x00164F0F
		public static void Send(Zone Z)
		{
			if (Z == null)
			{
				return;
			}
			Z.HandleEvent(SingletonEvent<AfterGameLoadedEvent>.Instance);
			SingletonEvent<AfterGameLoadedEvent>.Instance.Reset();
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x00166D2B File Offset: 0x00164F2B
		public static void Send(XRLGame Game)
		{
			Game.HandleEvent(SingletonEvent<AfterGameLoadedEvent>.Instance);
			Game.ZoneManager.HandleEvent<AfterGameLoadedEvent>(SingletonEvent<AfterGameLoadedEvent>.Instance);
			SingletonEvent<AfterGameLoadedEvent>.Instance.Reset();
		}

		// Token: 0x0400217C RID: 8572
		public new static readonly int CascadeLevel = 15;
	}
}
