using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x020007E6 RID: 2022
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ImplantedEvent : IActOnItemEvent
	{
		// Token: 0x06005143 RID: 20803 RVA: 0x00188F36 File Offset: 0x00187136
		public ImplantedEvent()
		{
			this.ID = ImplantedEvent.ID;
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x00188F49 File Offset: 0x00187149
		public static int CountPool()
		{
			if (ImplantedEvent.Pool != null)
			{
				return ImplantedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x00188F5E File Offset: 0x0018715E
		public static void ResetPool()
		{
			while (ImplantedEvent.PoolCounter > 0)
			{
				ImplantedEvent.Pool[--ImplantedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x00188F86 File Offset: 0x00187186
		public static void ResetTo(ref ImplantedEvent E)
		{
			MinEvent.ResetTo<ImplantedEvent>(E, ImplantedEvent.Pool, ref ImplantedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00188F9C File Offset: 0x0018719C
		public static ImplantedEvent FromPool()
		{
			return MinEvent.FromPool<ImplantedEvent>(ref ImplantedEvent.Pool, ref ImplantedEvent.PoolCounter, 8192);
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00188FB2 File Offset: 0x001871B2
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x00188FC6 File Offset: 0x001871C6
		public override void Reset()
		{
			base.Reset();
			this.Implantee = null;
			this.Part = null;
			this.ForDeepCopy = false;
			this.Silent = false;
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x00188FEC File Offset: 0x001871EC
		public static void Send(GameObject Implantee, GameObject Implant, BodyPart Part, GameObject Actor = null, bool ForDeepCopy = false, bool Silent = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Implant) && Implant.HasRegisteredEvent("Implanted"))
			{
				Event @event = new Event("Implanted");
				@event.SetParameter("Implantee", Implantee);
				@event.SetParameter("Actor", Actor ?? Implantee);
				@event.SetParameter("Object", Implantee);
				@event.SetParameter("Implant", Implant);
				@event.SetParameter("BodyPart", Part);
				@event.SetFlag("ForDeepCopy", ForDeepCopy);
				@event.SetFlag("Silent", Silent);
				flag = Implant.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Implant) && Implant.WantEvent(ImplantedEvent.ID, MinEvent.CascadeLevel))
			{
				ImplantedEvent implantedEvent = ImplantedEvent.FromPool();
				implantedEvent.Actor = Actor ?? Implantee;
				implantedEvent.Implantee = Implantee;
				implantedEvent.Item = Implant;
				implantedEvent.Part = Part;
				implantedEvent.ForDeepCopy = ForDeepCopy;
				implantedEvent.Silent = Silent;
				flag = Implant.HandleEvent(implantedEvent);
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x001890EC File Offset: 0x001872EC
		public static void Send(GameObject Implantee, GameObject Implant, IPart SpecificPart, BodyPart Part, GameObject Actor = null, bool ForDeepCopy = false, bool Silent = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Implant) && Implant.HasRegisteredEventFrom("Implanted", SpecificPart))
			{
				Event @event = new Event("Implanted");
				@event.SetParameter("Implantee", Implantee);
				@event.SetParameter("Actor", Actor ?? Implantee);
				@event.SetParameter("Object", Implantee);
				@event.SetParameter("Implant", Implant);
				@event.SetParameter("BodyPart", Part);
				@event.SetFlag("ForDeepCopy", ForDeepCopy);
				@event.SetFlag("Silent", Silent);
				flag = SpecificPart.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Implant) && SpecificPart.WantEvent(ImplantedEvent.ID, MinEvent.CascadeLevel))
			{
				ImplantedEvent implantedEvent = ImplantedEvent.FromPool();
				implantedEvent.Actor = Actor ?? Implantee;
				implantedEvent.Implantee = Implantee;
				implantedEvent.Item = Implant;
				implantedEvent.Part = Part;
				implantedEvent.ForDeepCopy = ForDeepCopy;
				implantedEvent.Silent = Silent;
				flag = SpecificPart.HandleEvent(implantedEvent);
			}
		}

		// Token: 0x040028B1 RID: 10417
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ImplantedEvent), null, new MinEvent.EventPoolCount(ImplantedEvent.CountPool), new MinEvent.EventPoolReset(ImplantedEvent.ResetPool));

		// Token: 0x040028B2 RID: 10418
		private static List<ImplantedEvent> Pool;

		// Token: 0x040028B3 RID: 10419
		private static int PoolCounter;

		// Token: 0x040028B4 RID: 10420
		public GameObject Implantee;

		// Token: 0x040028B5 RID: 10421
		public BodyPart Part;

		// Token: 0x040028B6 RID: 10422
		public bool ForDeepCopy;

		// Token: 0x040028B7 RID: 10423
		public bool Silent;
	}
}
