using System;

namespace XRL.World
{
	// Token: 0x02000652 RID: 1618
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class AfterAddActiveObjectEvent : PooledEvent<AfterAddActiveObjectEvent>
	{
		// Token: 0x06004614 RID: 17940 RVA: 0x00165A0E File Offset: 0x00163C0E
		public override int GetCascadeLevel()
		{
			return AfterAddActiveObjectEvent.CascadeLevel;
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00165A15 File Offset: 0x00163C15
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x00165A1E File Offset: 0x00163C1E
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x00165A30 File Offset: 0x00163C30
		public static void Send(GameObject Object)
		{
			AfterAddActiveObjectEvent afterAddActiveObjectEvent = PooledEvent<AfterAddActiveObjectEvent>.FromPool();
			afterAddActiveObjectEvent.Object = Object;
			The.Game.HandleEvent(afterAddActiveObjectEvent);
			Object.HandleEvent(afterAddActiveObjectEvent);
			PooledEvent<AfterAddActiveObjectEvent>.ResetTo(ref afterAddActiveObjectEvent);
		}

		// Token: 0x04002140 RID: 8512
		public new static readonly int CascadeLevel;

		// Token: 0x04002141 RID: 8513
		public GameObject Object;
	}
}
