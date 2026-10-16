using System;

namespace XRL.World
{
	// Token: 0x020006E4 RID: 1764
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckTileChangeEvent : PooledEvent<CheckTileChangeEvent>
	{
		// Token: 0x06004A21 RID: 18977 RVA: 0x0017392F File Offset: 0x00171B2F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00173938 File Offset: 0x00171B38
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00173947 File Offset: 0x00171B47
		public static CheckTileChangeEvent FromPool(GameObject Object)
		{
			CheckTileChangeEvent checkTileChangeEvent = PooledEvent<CheckTileChangeEvent>.FromPool();
			checkTileChangeEvent.Object = Object;
			return checkTileChangeEvent;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00173955 File Offset: 0x00171B55
		public static bool Check(GameObject Object)
		{
			return !GameObject.Validate(ref Object) || !Object.WantEvent(PooledEvent<CheckTileChangeEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CheckTileChangeEvent.FromPool(Object));
		}

		// Token: 0x040023AA RID: 9130
		public GameObject Object;
	}
}
