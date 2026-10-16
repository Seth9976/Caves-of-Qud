using System;

namespace XRL.World
{
	// Token: 0x020006F1 RID: 1777
	[GameEvent(Cascade = 271, Cache = MinEvent.Cache.Pool)]
	public class ContainsEvent : PooledEvent<ContainsEvent>
	{
		// Token: 0x06004A7C RID: 19068 RVA: 0x0017458A File Offset: 0x0017278A
		public override int GetCascadeLevel()
		{
			return ContainsEvent.CascadeLevel;
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00174591 File Offset: 0x00172791
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x0017459A File Offset: 0x0017279A
		public override void Reset()
		{
			base.Reset();
			this.Container = null;
			this.Object = null;
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x001745B0 File Offset: 0x001727B0
		public static ContainsEvent FromPool(GameObject Container, GameObject Object)
		{
			ContainsEvent containsEvent = PooledEvent<ContainsEvent>.FromPool();
			containsEvent.Container = Container;
			containsEvent.Object = Object;
			return containsEvent;
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x001745C5 File Offset: 0x001727C5
		public static bool Check(GameObject Container, GameObject Object)
		{
			return !Container.HandleEvent(ContainsEvent.FromPool(Container, Object));
		}

		// Token: 0x040023E9 RID: 9193
		public new static readonly int CascadeLevel = 271;

		// Token: 0x040023EA RID: 9194
		public GameObject Container;

		// Token: 0x040023EB RID: 9195
		public GameObject Object;
	}
}
