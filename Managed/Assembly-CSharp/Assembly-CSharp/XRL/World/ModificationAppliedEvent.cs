using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000806 RID: 2054
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ModificationAppliedEvent : PooledEvent<ModificationAppliedEvent>
	{
		// Token: 0x0600521C RID: 21020 RVA: 0x0018BD69 File Offset: 0x00189F69
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0018BD72 File Offset: 0x00189F72
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Modification = null;
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0018BD88 File Offset: 0x00189F88
		public static ModificationAppliedEvent FromPool(GameObject Object, IModification Modification)
		{
			ModificationAppliedEvent modificationAppliedEvent = PooledEvent<ModificationAppliedEvent>.FromPool();
			modificationAppliedEvent.Object = Object;
			modificationAppliedEvent.Modification = Modification;
			return modificationAppliedEvent;
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0018BDA0 File Offset: 0x00189FA0
		public static void Send(GameObject Object, IModification Modification)
		{
			if (Object.WantEvent(PooledEvent<ModificationAppliedEvent>.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(ModificationAppliedEvent.FromPool(Object, Modification));
			}
			if (Object.HasRegisteredEvent("ModificationApplied"))
			{
				Object.FireEvent(Event.New("ModificationApplied", "Object", Object, "Modification", Modification));
			}
		}

		// Token: 0x04002946 RID: 10566
		public GameObject Object;

		// Token: 0x04002947 RID: 10567
		public IModification Modification;
	}
}
