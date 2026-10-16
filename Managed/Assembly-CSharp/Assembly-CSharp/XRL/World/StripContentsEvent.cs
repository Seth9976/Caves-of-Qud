using System;

namespace XRL.World
{
	// Token: 0x0200085F RID: 2143
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class StripContentsEvent : PooledEvent<StripContentsEvent>
	{
		// Token: 0x06005567 RID: 21863 RVA: 0x00198843 File Offset: 0x00196A43
		public override int GetCascadeLevel()
		{
			return StripContentsEvent.CascadeLevel;
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x0019884A File Offset: 0x00196A4A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x00198853 File Offset: 0x00196A53
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.KeepNatural = false;
			this.Silent = false;
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x00198870 File Offset: 0x00196A70
		public static StripContentsEvent FromPool(GameObject Object, bool KeepNatural = false, bool Silent = false)
		{
			StripContentsEvent stripContentsEvent = PooledEvent<StripContentsEvent>.FromPool();
			stripContentsEvent.Object = Object;
			stripContentsEvent.KeepNatural = KeepNatural;
			stripContentsEvent.Silent = Silent;
			return stripContentsEvent;
		}

		// Token: 0x04002B0C RID: 11020
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B0D RID: 11021
		public GameObject Object;

		// Token: 0x04002B0E RID: 11022
		public bool KeepNatural;

		// Token: 0x04002B0F RID: 11023
		public bool Silent;
	}
}
