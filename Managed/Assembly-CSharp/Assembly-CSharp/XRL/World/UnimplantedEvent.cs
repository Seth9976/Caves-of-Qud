using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000871 RID: 2161
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class UnimplantedEvent : IActOnItemEvent
	{
		// Token: 0x060055DC RID: 21980 RVA: 0x00199A57 File Offset: 0x00197C57
		public UnimplantedEvent()
		{
			this.ID = UnimplantedEvent.ID;
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x00199A6A File Offset: 0x00197C6A
		public static int CountPool()
		{
			if (UnimplantedEvent.Pool != null)
			{
				return UnimplantedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x00199A7F File Offset: 0x00197C7F
		public static void ResetPool()
		{
			while (UnimplantedEvent.PoolCounter > 0)
			{
				UnimplantedEvent.Pool[--UnimplantedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x00199AA7 File Offset: 0x00197CA7
		public static void ResetTo(ref UnimplantedEvent E)
		{
			MinEvent.ResetTo<UnimplantedEvent>(E, UnimplantedEvent.Pool, ref UnimplantedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x00199ABD File Offset: 0x00197CBD
		public static UnimplantedEvent FromPool()
		{
			return MinEvent.FromPool<UnimplantedEvent>(ref UnimplantedEvent.Pool, ref UnimplantedEvent.PoolCounter, 8192);
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x00199AD3 File Offset: 0x00197CD3
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x00199AE7 File Offset: 0x00197CE7
		public override void Reset()
		{
			base.Reset();
			this.Implantee = null;
			this.Part = null;
			this.Silent = false;
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x00199B04 File Offset: 0x00197D04
		public static void Send(GameObject Implantee, GameObject Implant, BodyPart Part, GameObject Actor = null, bool Silent = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Implant) && Implant.HasRegisteredEvent("Unimplanted"))
			{
				Event @event = new Event("Unimplanted");
				@event.SetParameter("Actor", Actor ?? Implantee);
				@event.SetParameter("Implantee", Implantee);
				@event.SetParameter("Object", Implantee);
				@event.SetParameter("Implant", Implant);
				@event.SetParameter("BodyPart", Part);
				@event.SetFlag("Silent", Silent);
				flag = Implant.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Implant) && Implant.WantEvent(UnimplantedEvent.ID, MinEvent.CascadeLevel))
			{
				UnimplantedEvent unimplantedEvent = UnimplantedEvent.FromPool();
				unimplantedEvent.Actor = Actor ?? Implantee;
				unimplantedEvent.Implantee = Implantee;
				unimplantedEvent.Item = Implant;
				unimplantedEvent.Part = Part;
				unimplantedEvent.Silent = Silent;
				flag = Implant.HandleEvent(unimplantedEvent);
			}
		}

		// Token: 0x04002B3F RID: 11071
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(UnimplantedEvent), null, new MinEvent.EventPoolCount(UnimplantedEvent.CountPool), new MinEvent.EventPoolReset(UnimplantedEvent.ResetPool));

		// Token: 0x04002B40 RID: 11072
		private static List<UnimplantedEvent> Pool;

		// Token: 0x04002B41 RID: 11073
		private static int PoolCounter;

		// Token: 0x04002B42 RID: 11074
		public GameObject Implantee;

		// Token: 0x04002B43 RID: 11075
		public BodyPart Part;

		// Token: 0x04002B44 RID: 11076
		public bool Silent;
	}
}
