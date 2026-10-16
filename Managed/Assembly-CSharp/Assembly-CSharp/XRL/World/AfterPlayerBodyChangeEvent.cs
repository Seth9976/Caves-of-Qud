using System;

namespace XRL.World
{
	// Token: 0x02000669 RID: 1641
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AfterPlayerBodyChangeEvent : PooledEvent<AfterPlayerBodyChangeEvent>
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x001679E2 File Offset: 0x00165BE2
		public override int GetCascadeLevel()
		{
			return AfterPlayerBodyChangeEvent.CascadeLevel;
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x001679E9 File Offset: 0x00165BE9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x001679F2 File Offset: 0x00165BF2
		public override void Reset()
		{
			base.Reset();
			this.NewBody = null;
			this.OldBody = null;
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00167A08 File Offset: 0x00165C08
		public static void Send(GameObject NewBody, GameObject OldBody)
		{
			if (NewBody == OldBody)
			{
				return;
			}
			bool flag = The.Game.WantEvent(PooledEvent<AfterPlayerBodyChangeEvent>.ID, AfterPlayerBodyChangeEvent.CascadeLevel);
			bool flag2 = NewBody != null && NewBody.WantEvent(PooledEvent<AfterPlayerBodyChangeEvent>.ID, AfterPlayerBodyChangeEvent.CascadeLevel);
			bool flag3 = OldBody != null && OldBody.WantEvent(PooledEvent<AfterPlayerBodyChangeEvent>.ID, AfterPlayerBodyChangeEvent.CascadeLevel);
			if (flag || flag2 || flag3)
			{
				AfterPlayerBodyChangeEvent afterPlayerBodyChangeEvent = PooledEvent<AfterPlayerBodyChangeEvent>.FromPool();
				afterPlayerBodyChangeEvent.NewBody = NewBody;
				afterPlayerBodyChangeEvent.OldBody = OldBody;
				if (flag)
				{
					The.Game.HandleEvent(afterPlayerBodyChangeEvent);
				}
				if (flag2)
				{
					NewBody.HandleEvent(afterPlayerBodyChangeEvent);
				}
				if (flag3)
				{
					OldBody.HandleEvent(afterPlayerBodyChangeEvent);
				}
			}
		}

		// Token: 0x040021A7 RID: 8615
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040021A8 RID: 8616
		public GameObject NewBody;

		// Token: 0x040021A9 RID: 8617
		public GameObject OldBody;
	}
}
