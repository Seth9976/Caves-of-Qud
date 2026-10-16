using System;

namespace XRL.World
{
	// Token: 0x0200067C RID: 1660
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AttackerBeforeTemperatureChangeEvent : PooledEvent<AttackerBeforeTemperatureChangeEvent>
	{
		// Token: 0x06004744 RID: 18244 RVA: 0x00168E5F File Offset: 0x0016705F
		public override int GetCascadeLevel()
		{
			return AttackerBeforeTemperatureChangeEvent.CascadeLevel;
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00168E66 File Offset: 0x00167066
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00168E70 File Offset: 0x00167070
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Amount = 0;
			this.Actor = null;
			this.Radiant = false;
			this.MinAmbient = false;
			this.MaxAmbient = false;
			this.IgnoreResistance = false;
			this.Min = null;
			this.Max = null;
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00168ECC File Offset: 0x001670CC
		public static int GetFor(GameObject Object, int Amount, GameObject Actor = null, bool Radiant = false, bool MinAmbient = false, bool MaxAmbient = false, bool IgnoreResistance = false, int Phase = 0, int? Min = null, int? Max = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AttackerBeforeTemperatureChange"))
			{
				Event @event = Event.New("AttackerBeforeTemperatureChange", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Amount", Amount);
				@event.SetParameter("Actor", Actor);
				@event.SetFlag("Radiant", Radiant);
				@event.SetFlag("MinAmbient", MinAmbient);
				@event.SetFlag("MaxAmbient", MaxAmbient);
				@event.SetFlag("IgnoreResistance", IgnoreResistance);
				@event.SetParameter("Min", Min);
				@event.SetParameter("Max", Max);
				flag = Actor.FireEvent(@event);
				Amount = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<AttackerBeforeTemperatureChangeEvent>.ID, AttackerBeforeTemperatureChangeEvent.CascadeLevel))
			{
				AttackerBeforeTemperatureChangeEvent attackerBeforeTemperatureChangeEvent = PooledEvent<AttackerBeforeTemperatureChangeEvent>.FromPool();
				attackerBeforeTemperatureChangeEvent.Object = Object;
				attackerBeforeTemperatureChangeEvent.Amount = Amount;
				attackerBeforeTemperatureChangeEvent.Actor = Actor;
				attackerBeforeTemperatureChangeEvent.Radiant = Radiant;
				attackerBeforeTemperatureChangeEvent.MinAmbient = MinAmbient;
				attackerBeforeTemperatureChangeEvent.MaxAmbient = MaxAmbient;
				attackerBeforeTemperatureChangeEvent.IgnoreResistance = IgnoreResistance;
				attackerBeforeTemperatureChangeEvent.Min = Min;
				attackerBeforeTemperatureChangeEvent.Max = Max;
				flag = Actor.HandleEvent(attackerBeforeTemperatureChangeEvent);
				Amount = attackerBeforeTemperatureChangeEvent.Amount;
			}
			if (!flag)
			{
				Amount = 0;
			}
			return Amount;
		}

		// Token: 0x040021F5 RID: 8693
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040021F6 RID: 8694
		public GameObject Object;

		// Token: 0x040021F7 RID: 8695
		public int Amount;

		// Token: 0x040021F8 RID: 8696
		public GameObject Actor;

		// Token: 0x040021F9 RID: 8697
		public bool Radiant;

		// Token: 0x040021FA RID: 8698
		public bool MinAmbient;

		// Token: 0x040021FB RID: 8699
		public bool MaxAmbient;

		// Token: 0x040021FC RID: 8700
		public bool IgnoreResistance;

		// Token: 0x040021FD RID: 8701
		public int? Min;

		// Token: 0x040021FE RID: 8702
		public int? Max;
	}
}
