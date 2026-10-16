using System;

namespace XRL.World
{
	// Token: 0x020006DE RID: 1758
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckPaintabilityEvent : PooledEvent<CheckPaintabilityEvent>
	{
		// Token: 0x06004A08 RID: 18952 RVA: 0x001731BD File Offset: 0x001713BD
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x001731C6 File Offset: 0x001713C6
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x001731D5 File Offset: 0x001713D5
		public static CheckPaintabilityEvent FromPool(GameObject Object)
		{
			CheckPaintabilityEvent checkPaintabilityEvent = PooledEvent<CheckPaintabilityEvent>.FromPool();
			checkPaintabilityEvent.Object = Object;
			return checkPaintabilityEvent;
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x001731E3 File Offset: 0x001713E3
		public static bool Check(GameObject Object)
		{
			return !GameObject.Validate(ref Object) || !Object.WantEvent(PooledEvent<CheckPaintabilityEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CheckPaintabilityEvent.FromPool(Object));
		}

		// Token: 0x04002391 RID: 9105
		public GameObject Object;
	}
}
