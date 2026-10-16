using System;

namespace XRL.World
{
	// Token: 0x0200083F RID: 2111
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class RefreshTileEvent : SingletonEvent<RefreshTileEvent>
	{
		// Token: 0x060053E3 RID: 21475 RVA: 0x001909D0 File Offset: 0x0018EBD0
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x001909D9 File Offset: 0x0018EBD9
		public static void Send(GameObject obj)
		{
			obj.HandleEvent(SingletonEvent<RefreshTileEvent>.Instance);
		}
	}
}
