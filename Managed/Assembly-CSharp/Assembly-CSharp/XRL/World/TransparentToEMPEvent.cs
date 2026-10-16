using System;

namespace XRL.World
{
	// Token: 0x0200086C RID: 2156
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TransparentToEMPEvent : PooledEvent<TransparentToEMPEvent>
	{
		// Token: 0x060055BC RID: 21948 RVA: 0x00199526 File Offset: 0x00197726
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x0019952F File Offset: 0x0019772F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x00199540 File Offset: 0x00197740
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(Object) && Object.WantEvent(PooledEvent<TransparentToEMPEvent>.ID, MinEvent.CascadeLevel))
			{
				TransparentToEMPEvent transparentToEMPEvent = PooledEvent<TransparentToEMPEvent>.FromPool();
				transparentToEMPEvent.Object = Object;
				flag = Object.HandleEvent(transparentToEMPEvent);
			}
			return flag;
		}

		// Token: 0x04002B31 RID: 11057
		public GameObject Object;
	}
}
