using System;

namespace XRL.World
{
	// Token: 0x020007AB RID: 1963
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetZoneEvent : PooledEvent<GetZoneEvent>
	{
		// Token: 0x06004FE5 RID: 20453 RVA: 0x00185814 File Offset: 0x00183A14
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0018581D File Offset: 0x00183A1D
		public override void Reset()
		{
			base.Reset();
			this.ZoneID = null;
			this.Result = null;
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x00185834 File Offset: 0x00183A34
		public static bool TryGetFor(XRLGame Game, string ZoneID, out Zone Zone)
		{
			GetZoneEvent getZoneEvent = PooledEvent<GetZoneEvent>.FromPool();
			bool flag;
			try
			{
				getZoneEvent.ZoneID = ZoneID;
				Game.HandleEvent(getZoneEvent);
				Zone = getZoneEvent.Result;
				flag = Zone != null;
			}
			finally
			{
				PooledEvent<GetZoneEvent>.ResetTo(ref getZoneEvent);
			}
			return flag;
		}

		// Token: 0x0400278C RID: 10124
		public string ZoneID;

		// Token: 0x0400278D RID: 10125
		public Zone Result;
	}
}
