using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x020007E5 RID: 2021
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class ImplantRemovedEvent : IActOnItemEvent
	{
		// Token: 0x06005139 RID: 20793 RVA: 0x00188D66 File Offset: 0x00186F66
		public ImplantRemovedEvent()
		{
			this.ID = ImplantRemovedEvent.ID;
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x00188D79 File Offset: 0x00186F79
		public override int GetCascadeLevel()
		{
			return ImplantRemovedEvent.CascadeLevel;
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x00188D80 File Offset: 0x00186F80
		public static int CountPool()
		{
			if (ImplantRemovedEvent.Pool != null)
			{
				return ImplantRemovedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x00188D95 File Offset: 0x00186F95
		public static void ResetPool()
		{
			while (ImplantRemovedEvent.PoolCounter > 0)
			{
				ImplantRemovedEvent.Pool[--ImplantRemovedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x00188DBD File Offset: 0x00186FBD
		public static void ResetTo(ref ImplantRemovedEvent E)
		{
			MinEvent.ResetTo<ImplantRemovedEvent>(E, ImplantRemovedEvent.Pool, ref ImplantRemovedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x00188DD3 File Offset: 0x00186FD3
		public static ImplantRemovedEvent FromPool()
		{
			return MinEvent.FromPool<ImplantRemovedEvent>(ref ImplantRemovedEvent.Pool, ref ImplantRemovedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x00188DE9 File Offset: 0x00186FE9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x00188DFD File Offset: 0x00186FFD
		public override void Reset()
		{
			base.Reset();
			this.Implantee = null;
			this.Part = null;
			this.Silent = false;
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x00188E1C File Offset: 0x0018701C
		public static void Send(GameObject Implantee, GameObject Implant, BodyPart Part, GameObject Actor = null, bool Silent = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Implantee) && Implantee.HasRegisteredEvent("ImplantRemoved"))
			{
				Event @event = new Event("ImplantRemoved");
				@event.SetParameter("Actor", Actor ?? Implantee);
				@event.SetParameter("Implantee", Implantee);
				@event.SetParameter("Object", Implantee);
				@event.SetParameter("Implant", Implant);
				@event.SetParameter("BodyPart", Part);
				@event.SetFlag("Silent", Silent);
				flag = Implantee.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Implantee) && Implantee.WantEvent(ImplantRemovedEvent.ID, ImplantRemovedEvent.CascadeLevel))
			{
				ImplantRemovedEvent implantRemovedEvent = ImplantRemovedEvent.FromPool();
				implantRemovedEvent.Actor = Actor ?? Implantee;
				implantRemovedEvent.Implantee = Implantee;
				implantRemovedEvent.Item = Implant;
				implantRemovedEvent.Part = Part;
				implantRemovedEvent.Silent = Silent;
				flag = Implantee.HandleEvent(implantRemovedEvent);
			}
		}

		// Token: 0x040028AA RID: 10410
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ImplantRemovedEvent), null, new MinEvent.EventPoolCount(ImplantRemovedEvent.CountPool), new MinEvent.EventPoolReset(ImplantRemovedEvent.ResetPool));

		// Token: 0x040028AB RID: 10411
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028AC RID: 10412
		private static List<ImplantRemovedEvent> Pool;

		// Token: 0x040028AD RID: 10413
		private static int PoolCounter;

		// Token: 0x040028AE RID: 10414
		public GameObject Implantee;

		// Token: 0x040028AF RID: 10415
		public BodyPart Part;

		// Token: 0x040028B0 RID: 10416
		public bool Silent;
	}
}
