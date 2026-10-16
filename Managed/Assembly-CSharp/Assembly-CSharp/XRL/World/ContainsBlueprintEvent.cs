using System;

namespace XRL.World
{
	// Token: 0x020006F0 RID: 1776
	[GameEvent(Cascade = 271, Cache = MinEvent.Cache.Pool)]
	public class ContainsBlueprintEvent : PooledEvent<ContainsBlueprintEvent>
	{
		// Token: 0x06004A74 RID: 19060 RVA: 0x001744F2 File Offset: 0x001726F2
		public override int GetCascadeLevel()
		{
			return ContainsBlueprintEvent.CascadeLevel;
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x001744F9 File Offset: 0x001726F9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00174502 File Offset: 0x00172702
		public override void Reset()
		{
			base.Reset();
			this.Container = null;
			this.Blueprint = null;
			this.Object = null;
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x0017451F File Offset: 0x0017271F
		public static ContainsBlueprintEvent FromPool(GameObject Container, string Blueprint)
		{
			ContainsBlueprintEvent containsBlueprintEvent = PooledEvent<ContainsBlueprintEvent>.FromPool();
			containsBlueprintEvent.Container = Container;
			containsBlueprintEvent.Blueprint = Blueprint;
			containsBlueprintEvent.Object = null;
			return containsBlueprintEvent;
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0017453B File Offset: 0x0017273B
		public static bool Check(GameObject Container, string Blueprint)
		{
			return !Container.HandleEvent(ContainsBlueprintEvent.FromPool(Container, Blueprint));
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00174550 File Offset: 0x00172750
		public static GameObject Find(GameObject Container, string Blueprint)
		{
			ContainsBlueprintEvent containsBlueprintEvent = ContainsBlueprintEvent.FromPool(Container, Blueprint);
			if (!Container.HandleEvent(containsBlueprintEvent))
			{
				return containsBlueprintEvent.Object;
			}
			return null;
		}

		// Token: 0x040023E5 RID: 9189
		public new static readonly int CascadeLevel = 271;

		// Token: 0x040023E6 RID: 9190
		public GameObject Container;

		// Token: 0x040023E7 RID: 9191
		public string Blueprint;

		// Token: 0x040023E8 RID: 9192
		public GameObject Object;
	}
}
