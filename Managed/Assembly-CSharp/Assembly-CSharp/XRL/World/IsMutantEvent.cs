using System;

namespace XRL.World
{
	// Token: 0x020007F0 RID: 2032
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IsMutantEvent : PooledEvent<IsMutantEvent>
	{
		// Token: 0x0600518D RID: 20877 RVA: 0x00189FDE File Offset: 0x001881DE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00189FE7 File Offset: 0x001881E7
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.IsMutant = false;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x0018A000 File Offset: 0x00188200
		public static bool Check(GameObject Object)
		{
			bool? flag;
			if (Object == null)
			{
				flag = null;
			}
			else
			{
				GenotypeEntry genotypeEntry = Object.genotypeEntry;
				flag = ((genotypeEntry != null) ? new bool?(genotypeEntry.IsMutant) : null);
			}
			bool flag2 = flag ?? (Object.IsCreature && !Object.HasTagOrProperty("NonMutant"));
			bool flag3 = true;
			if (flag3 && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsMutant"))
			{
				Event @event = Event.New("IsMutant", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetFlag("IsMutant", flag2);
				flag3 = Object.FireEvent(@event);
				flag2 = @event.HasFlag("IsMutant");
			}
			if (flag3 && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsMutantEvent>.ID, MinEvent.CascadeLevel))
			{
				IsMutantEvent isMutantEvent = PooledEvent<IsMutantEvent>.FromPool();
				isMutantEvent.Object = Object;
				isMutantEvent.IsMutant = flag2;
				flag3 = Object.HandleEvent(isMutantEvent);
				flag2 = isMutantEvent.IsMutant;
			}
			return flag2;
		}

		// Token: 0x040028E5 RID: 10469
		public GameObject Object;

		// Token: 0x040028E6 RID: 10470
		public bool IsMutant;
	}
}
