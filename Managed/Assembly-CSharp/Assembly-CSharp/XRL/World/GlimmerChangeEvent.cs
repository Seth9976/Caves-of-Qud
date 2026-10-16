using System;

namespace XRL.World
{
	// Token: 0x020007AF RID: 1967
	[GameEvent(Cascade = 271, Cache = MinEvent.Cache.Pool)]
	public class GlimmerChangeEvent : PooledEvent<GlimmerChangeEvent>
	{
		// Token: 0x06004FFF RID: 20479 RVA: 0x00185BCF File Offset: 0x00183DCF
		public override int GetCascadeLevel()
		{
			return GlimmerChangeEvent.CascadeLevel;
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x00185BD6 File Offset: 0x00183DD6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x00185BDF File Offset: 0x00183DDF
		public static void Send(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Object.HandleEvent(PooledEvent<GlimmerChangeEvent>.FromPool());
			}
		}

		// Token: 0x0400279C RID: 10140
		public new static readonly int CascadeLevel = 271;
	}
}
