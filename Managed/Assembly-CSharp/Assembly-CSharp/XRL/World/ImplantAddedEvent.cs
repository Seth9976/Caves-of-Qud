using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x020007E4 RID: 2020
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class ImplantAddedEvent : IActOnItemEvent
	{
		// Token: 0x0600512F RID: 20783 RVA: 0x00188B72 File Offset: 0x00186D72
		public ImplantAddedEvent()
		{
			this.ID = ImplantAddedEvent.ID;
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00188B85 File Offset: 0x00186D85
		public override int GetCascadeLevel()
		{
			return ImplantAddedEvent.CascadeLevel;
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00188B8C File Offset: 0x00186D8C
		public static int CountPool()
		{
			if (ImplantAddedEvent.Pool != null)
			{
				return ImplantAddedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00188BA1 File Offset: 0x00186DA1
		public static void ResetPool()
		{
			while (ImplantAddedEvent.PoolCounter > 0)
			{
				ImplantAddedEvent.Pool[--ImplantAddedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x00188BC9 File Offset: 0x00186DC9
		public static void ResetTo(ref ImplantAddedEvent E)
		{
			MinEvent.ResetTo<ImplantAddedEvent>(E, ImplantAddedEvent.Pool, ref ImplantAddedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x00188BDF File Offset: 0x00186DDF
		public static ImplantAddedEvent FromPool()
		{
			return MinEvent.FromPool<ImplantAddedEvent>(ref ImplantAddedEvent.Pool, ref ImplantAddedEvent.PoolCounter, 8192);
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x00188BF5 File Offset: 0x00186DF5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00188C09 File Offset: 0x00186E09
		public override void Reset()
		{
			base.Reset();
			this.Implantee = null;
			this.Part = null;
			this.ForDeepCopy = false;
			this.Silent = false;
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x00188C30 File Offset: 0x00186E30
		public static void Send(GameObject Implantee, GameObject Implant, BodyPart Part, GameObject Actor = null, bool ForDeepCopy = false, bool Silent = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Implantee) && Implantee.HasRegisteredEvent("ImplantAdded"))
			{
				Event @event = new Event("ImplantAdded");
				@event.SetParameter("Implantee", Implantee);
				@event.SetParameter("Actor", Actor ?? Implantee);
				@event.SetParameter("Object", Implantee);
				@event.SetParameter("Implant", Implant);
				@event.SetParameter("BodyPart", Part);
				@event.SetFlag("ForDeepCopy", ForDeepCopy);
				@event.SetFlag("Silent", Silent);
				flag = Implantee.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Implantee) && Implantee.WantEvent(ImplantAddedEvent.ID, ImplantAddedEvent.CascadeLevel))
			{
				ImplantAddedEvent implantAddedEvent = ImplantAddedEvent.FromPool();
				implantAddedEvent.Actor = Actor ?? Implantee;
				implantAddedEvent.Implantee = Implantee;
				implantAddedEvent.Item = Implant;
				implantAddedEvent.Part = Part;
				implantAddedEvent.ForDeepCopy = ForDeepCopy;
				implantAddedEvent.Silent = Silent;
				flag = Implantee.HandleEvent(implantAddedEvent);
			}
		}

		// Token: 0x040028A2 RID: 10402
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ImplantAddedEvent), null, new MinEvent.EventPoolCount(ImplantAddedEvent.CountPool), new MinEvent.EventPoolReset(ImplantAddedEvent.ResetPool));

		// Token: 0x040028A3 RID: 10403
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028A4 RID: 10404
		private static List<ImplantAddedEvent> Pool;

		// Token: 0x040028A5 RID: 10405
		private static int PoolCounter;

		// Token: 0x040028A6 RID: 10406
		public GameObject Implantee;

		// Token: 0x040028A7 RID: 10407
		public BodyPart Part;

		// Token: 0x040028A8 RID: 10408
		public bool ForDeepCopy;

		// Token: 0x040028A9 RID: 10409
		public bool Silent;
	}
}
