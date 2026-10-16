using System;

namespace XRL.World
{
	// Token: 0x0200066B RID: 1643
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AfterReadBookEvent : PooledEvent<AfterReadBookEvent>
	{
		// Token: 0x060046CA RID: 18122 RVA: 0x00167BBC File Offset: 0x00165DBC
		public override int GetCascadeLevel()
		{
			return AfterReadBookEvent.CascadeLevel;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x00167BC3 File Offset: 0x00165DC3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x00167BCC File Offset: 0x00165DCC
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Source = null;
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00167BEC File Offset: 0x00165DEC
		public static void Send(GameObject Actor, GameObject Object, object Source, IEvent ParentEvent = null)
		{
			AfterReadBookEvent afterReadBookEvent = PooledEvent<AfterReadBookEvent>.FromPool();
			afterReadBookEvent.Actor = Actor;
			afterReadBookEvent.Object = Object;
			afterReadBookEvent.Source = Source;
			if ((Actor == null || Actor.HandleEvent(afterReadBookEvent)) && Object != null && Object != Actor)
			{
				Object.HandleEvent(afterReadBookEvent);
			}
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(afterReadBookEvent);
			}
			PooledEvent<AfterReadBookEvent>.ResetTo(ref afterReadBookEvent);
		}

		// Token: 0x040021AF RID: 8623
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040021B0 RID: 8624
		public GameObject Actor;

		// Token: 0x040021B1 RID: 8625
		public GameObject Object;

		// Token: 0x040021B2 RID: 8626
		public object Source;
	}
}
