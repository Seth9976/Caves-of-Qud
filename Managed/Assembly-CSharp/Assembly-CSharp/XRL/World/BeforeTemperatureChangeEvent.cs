using System;

namespace XRL.World
{
	// Token: 0x020006A3 RID: 1699
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeTemperatureChangeEvent : PooledEvent<BeforeTemperatureChangeEvent>
	{
		// Token: 0x06004868 RID: 18536 RVA: 0x0016C311 File Offset: 0x0016A511
		public override int GetCascadeLevel()
		{
			return BeforeTemperatureChangeEvent.CascadeLevel;
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x0016C318 File Offset: 0x0016A518
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x0016C324 File Offset: 0x0016A524
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

		// Token: 0x0600486B RID: 18539 RVA: 0x0016C380 File Offset: 0x0016A580
		public static int GetFor(GameObject Object, int Amount, GameObject Actor = null, bool Radiant = false, bool MinAmbient = false, bool MaxAmbient = false, bool IgnoreResistance = false, int Phase = 0, int? Min = null, int? Max = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforeTemperatureChange"))
			{
				Event @event = Event.New("BeforeTemperatureChange", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Amount", Amount);
				@event.SetParameter("Actor", Actor);
				@event.SetFlag("Radiant", Radiant);
				@event.SetFlag("MinAmbient", MinAmbient);
				@event.SetFlag("MaxAmbient", MaxAmbient);
				@event.SetFlag("IgnoreResistance", IgnoreResistance);
				@event.SetParameter("Min", Min);
				@event.SetParameter("Max", Max);
				flag = Object.FireEvent(@event);
				Amount = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BeforeTemperatureChangeEvent>.ID, BeforeTemperatureChangeEvent.CascadeLevel))
			{
				BeforeTemperatureChangeEvent beforeTemperatureChangeEvent = PooledEvent<BeforeTemperatureChangeEvent>.FromPool();
				beforeTemperatureChangeEvent.Object = Object;
				beforeTemperatureChangeEvent.Amount = Amount;
				beforeTemperatureChangeEvent.Actor = Actor;
				beforeTemperatureChangeEvent.Radiant = Radiant;
				beforeTemperatureChangeEvent.MinAmbient = MinAmbient;
				beforeTemperatureChangeEvent.MaxAmbient = MaxAmbient;
				beforeTemperatureChangeEvent.IgnoreResistance = IgnoreResistance;
				beforeTemperatureChangeEvent.Min = Min;
				beforeTemperatureChangeEvent.Max = Max;
				flag = Object.HandleEvent(beforeTemperatureChangeEvent);
				Amount = beforeTemperatureChangeEvent.Amount;
				PooledEvent<BeforeTemperatureChangeEvent>.ResetTo(ref beforeTemperatureChangeEvent);
			}
			if (!flag)
			{
				Amount = 0;
			}
			return Amount;
		}

		// Token: 0x040022A4 RID: 8868
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040022A5 RID: 8869
		public GameObject Object;

		// Token: 0x040022A6 RID: 8870
		public int Amount;

		// Token: 0x040022A7 RID: 8871
		public GameObject Actor;

		// Token: 0x040022A8 RID: 8872
		public bool Radiant;

		// Token: 0x040022A9 RID: 8873
		public bool MinAmbient;

		// Token: 0x040022AA RID: 8874
		public bool MaxAmbient;

		// Token: 0x040022AB RID: 8875
		public bool IgnoreResistance;

		// Token: 0x040022AC RID: 8876
		public int? Min;

		// Token: 0x040022AD RID: 8877
		public int? Max;
	}
}
