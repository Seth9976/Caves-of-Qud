using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200080F RID: 2063
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class NeutronFluxPourExplodesEvent : PooledEvent<NeutronFluxPourExplodesEvent>
	{
		// Token: 0x06005259 RID: 21081 RVA: 0x0018C67B File Offset: 0x0018A87B
		public override int GetCascadeLevel()
		{
			return NeutronFluxPourExplodesEvent.CascadeLevel;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0018C682 File Offset: 0x0018A882
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0018C68B File Offset: 0x0018A88B
		public override void Reset()
		{
			base.Reset();
			this.PouredFrom = null;
			this.PouredTo = null;
			this.PouredBy = null;
			this.PouredLiquid = null;
			this.Prospective = false;
			this.Interrupt = false;
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x0018C6C0 File Offset: 0x0018A8C0
		public static bool Check(out bool Interrupt, GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy = null, LiquidVolume PouredLiquid = null, bool Prospective = false)
		{
			Interrupt = false;
			bool flag = true;
			if (flag && GameObject.Validate(ref PouredFrom) && PouredFrom.HasRegisteredEvent("NeutronFluxPourExplodes"))
			{
				Event @event = Event.New("NeutronFluxPourExplodes", 0, 0, 0);
				@event.SetParameter("PouredFrom", PouredFrom);
				@event.SetParameter("PouredTo", PouredTo);
				@event.SetParameter("PouredBy", PouredBy);
				@event.SetParameter("PouredLiquid", PouredLiquid);
				@event.SetFlag("Interrupt", Interrupt);
				@event.SetFlag("Prospective", Prospective);
				flag = PouredFrom.FireEvent(@event);
				Interrupt = @event.HasFlag("Interrupt");
			}
			if (flag && GameObject.Validate(ref PouredTo) && PouredTo.HasRegisteredEvent("NeutronFluxPourExplodes"))
			{
				Event event2 = Event.New("NeutronFluxPourExplodes", 0, 0, 0);
				event2.SetParameter("PouredFrom", PouredFrom);
				event2.SetParameter("PouredTo", PouredTo);
				event2.SetParameter("PouredBy", PouredBy);
				event2.SetParameter("PouredLiquid", PouredLiquid);
				event2.SetFlag("Interrupt", Interrupt);
				event2.SetFlag("Prospective", Prospective);
				flag = PouredTo.FireEvent(event2);
				Interrupt = event2.HasFlag("Interrupt");
			}
			if (flag && GameObject.Validate(ref PouredBy) && PouredBy.HasRegisteredEvent("NeutronFluxPourExplodes"))
			{
				Event event3 = Event.New("NeutronFluxPourExplodes", 0, 0, 0);
				event3.SetParameter("PouredFrom", PouredFrom);
				event3.SetParameter("PouredTo", PouredTo);
				event3.SetParameter("PouredBy", PouredBy);
				event3.SetParameter("PouredLiquid", PouredLiquid);
				event3.SetFlag("Interrupt", Interrupt);
				event3.SetFlag("Prospective", Prospective);
				flag = PouredBy.FireEvent(event3);
				Interrupt = event3.HasFlag("Interrupt");
			}
			NeutronFluxPourExplodesEvent neutronFluxPourExplodesEvent = null;
			if (flag && GameObject.Validate(ref PouredFrom) && PouredFrom.WantEvent(PooledEvent<NeutronFluxPourExplodesEvent>.ID, NeutronFluxPourExplodesEvent.CascadeLevel))
			{
				if (neutronFluxPourExplodesEvent == null)
				{
					neutronFluxPourExplodesEvent = PooledEvent<NeutronFluxPourExplodesEvent>.FromPool();
				}
				neutronFluxPourExplodesEvent.PouredFrom = PouredFrom;
				neutronFluxPourExplodesEvent.PouredTo = PouredTo;
				neutronFluxPourExplodesEvent.PouredBy = PouredBy;
				neutronFluxPourExplodesEvent.PouredLiquid = PouredLiquid;
				neutronFluxPourExplodesEvent.Interrupt = Interrupt;
				neutronFluxPourExplodesEvent.Prospective = Prospective;
				flag = PouredFrom.HandleEvent(neutronFluxPourExplodesEvent);
				Interrupt = neutronFluxPourExplodesEvent.Interrupt;
			}
			if (flag && GameObject.Validate(ref PouredTo) && PouredTo.WantEvent(PooledEvent<NeutronFluxPourExplodesEvent>.ID, NeutronFluxPourExplodesEvent.CascadeLevel))
			{
				if (neutronFluxPourExplodesEvent == null)
				{
					neutronFluxPourExplodesEvent = PooledEvent<NeutronFluxPourExplodesEvent>.FromPool();
				}
				neutronFluxPourExplodesEvent.PouredFrom = PouredFrom;
				neutronFluxPourExplodesEvent.PouredTo = PouredTo;
				neutronFluxPourExplodesEvent.PouredBy = PouredBy;
				neutronFluxPourExplodesEvent.PouredLiquid = PouredLiquid;
				neutronFluxPourExplodesEvent.Interrupt = Interrupt;
				neutronFluxPourExplodesEvent.Prospective = Prospective;
				flag = PouredTo.HandleEvent(neutronFluxPourExplodesEvent);
				Interrupt = neutronFluxPourExplodesEvent.Interrupt;
			}
			if (flag && GameObject.Validate(ref PouredBy) && PouredBy.WantEvent(PooledEvent<NeutronFluxPourExplodesEvent>.ID, NeutronFluxPourExplodesEvent.CascadeLevel))
			{
				if (neutronFluxPourExplodesEvent == null)
				{
					neutronFluxPourExplodesEvent = PooledEvent<NeutronFluxPourExplodesEvent>.FromPool();
				}
				neutronFluxPourExplodesEvent.PouredFrom = PouredFrom;
				neutronFluxPourExplodesEvent.PouredTo = PouredTo;
				neutronFluxPourExplodesEvent.PouredBy = PouredBy;
				neutronFluxPourExplodesEvent.PouredLiquid = PouredLiquid;
				neutronFluxPourExplodesEvent.Interrupt = Interrupt;
				neutronFluxPourExplodesEvent.Prospective = Prospective;
				flag = PouredBy.HandleEvent(neutronFluxPourExplodesEvent);
				Interrupt = neutronFluxPourExplodesEvent.Interrupt;
			}
			return flag;
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0018C9DC File Offset: 0x0018ABDC
		public static bool Check(GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy = null, LiquidVolume PouredLiquid = null, bool Prospective = false)
		{
			bool flag;
			return NeutronFluxPourExplodesEvent.Check(out flag, PouredFrom, PouredTo, PouredBy, PouredLiquid, Prospective);
		}

		// Token: 0x04002961 RID: 10593
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002962 RID: 10594
		public GameObject PouredFrom;

		// Token: 0x04002963 RID: 10595
		public GameObject PouredTo;

		// Token: 0x04002964 RID: 10596
		public GameObject PouredBy;

		// Token: 0x04002965 RID: 10597
		public LiquidVolume PouredLiquid;

		// Token: 0x04002966 RID: 10598
		public bool Prospective;

		// Token: 0x04002967 RID: 10599
		public bool Interrupt;
	}
}
