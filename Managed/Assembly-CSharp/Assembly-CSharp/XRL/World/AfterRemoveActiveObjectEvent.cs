using System;

namespace XRL.World
{
	// Token: 0x0200066D RID: 1645
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class AfterRemoveActiveObjectEvent : PooledEvent<AfterRemoveActiveObjectEvent>
	{
		// Token: 0x060046D4 RID: 18132 RVA: 0x00167D7A File Offset: 0x00165F7A
		public override int GetCascadeLevel()
		{
			return AfterRemoveActiveObjectEvent.CascadeLevel;
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x00167D81 File Offset: 0x00165F81
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00167D8A File Offset: 0x00165F8A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x00167D9C File Offset: 0x00165F9C
		public static void Send(GameObject Object)
		{
			if (!GameObject.Validate(Object))
			{
				return;
			}
			AfterRemoveActiveObjectEvent afterRemoveActiveObjectEvent = PooledEvent<AfterRemoveActiveObjectEvent>.FromPool();
			afterRemoveActiveObjectEvent.Object = Object;
			The.Game.HandleEvent(afterRemoveActiveObjectEvent);
			Object.HandleEvent(afterRemoveActiveObjectEvent);
			PooledEvent<AfterRemoveActiveObjectEvent>.ResetTo(ref afterRemoveActiveObjectEvent);
		}

		// Token: 0x040021B9 RID: 8633
		public new static readonly int CascadeLevel;

		// Token: 0x040021BA RID: 8634
		public GameObject Object;
	}
}
