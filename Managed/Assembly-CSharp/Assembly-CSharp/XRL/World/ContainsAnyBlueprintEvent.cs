using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006EF RID: 1775
	[GameEvent(Cascade = 271, Cache = MinEvent.Cache.Pool)]
	public class ContainsAnyBlueprintEvent : PooledEvent<ContainsAnyBlueprintEvent>
	{
		// Token: 0x06004A6C RID: 19052 RVA: 0x0017445A File Offset: 0x0017265A
		public override int GetCascadeLevel()
		{
			return ContainsAnyBlueprintEvent.CascadeLevel;
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00174461 File Offset: 0x00172661
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x0017446A File Offset: 0x0017266A
		public override void Reset()
		{
			base.Reset();
			this.Container = null;
			this.Blueprints = null;
			this.Object = null;
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00174487 File Offset: 0x00172687
		public static ContainsAnyBlueprintEvent FromPool(GameObject Container, List<string> Blueprints)
		{
			ContainsAnyBlueprintEvent containsAnyBlueprintEvent = PooledEvent<ContainsAnyBlueprintEvent>.FromPool();
			containsAnyBlueprintEvent.Container = Container;
			containsAnyBlueprintEvent.Blueprints = Blueprints;
			containsAnyBlueprintEvent.Object = null;
			return containsAnyBlueprintEvent;
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x001744A3 File Offset: 0x001726A3
		public static bool Check(GameObject Container, List<string> Blueprints)
		{
			return !Container.HandleEvent(ContainsAnyBlueprintEvent.FromPool(Container, Blueprints));
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x001744B8 File Offset: 0x001726B8
		public static GameObject Find(GameObject Container, List<string> Blueprints)
		{
			ContainsAnyBlueprintEvent containsAnyBlueprintEvent = ContainsAnyBlueprintEvent.FromPool(Container, Blueprints);
			if (!Container.HandleEvent(containsAnyBlueprintEvent))
			{
				return containsAnyBlueprintEvent.Object;
			}
			return null;
		}

		// Token: 0x040023E1 RID: 9185
		public new static readonly int CascadeLevel = 271;

		// Token: 0x040023E2 RID: 9186
		public GameObject Container;

		// Token: 0x040023E3 RID: 9187
		public List<string> Blueprints;

		// Token: 0x040023E4 RID: 9188
		public GameObject Object;
	}
}
