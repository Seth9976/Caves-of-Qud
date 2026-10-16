using System;

namespace XRL.World
{
	// Token: 0x02000693 RID: 1683
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeInteriorCollapseEvent : PooledEvent<BeforeInteriorCollapseEvent>
	{
		// Token: 0x060047F5 RID: 18421 RVA: 0x0016B0C6 File Offset: 0x001692C6
		public override int GetCascadeLevel()
		{
			return BeforeInteriorCollapseEvent.CascadeLevel;
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0016B0CD File Offset: 0x001692CD
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x0016B0D6 File Offset: 0x001692D6
		public override void Reset()
		{
			base.Reset();
			this.Zone = null;
			this.Object = null;
		}

		// Token: 0x04002253 RID: 8787
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002254 RID: 8788
		public InteriorZone Zone;

		// Token: 0x04002255 RID: 8789
		public GameObject Object;
	}
}
