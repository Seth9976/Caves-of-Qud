using System;

namespace XRL.World
{
	// Token: 0x02000746 RID: 1862
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetElectricalConductivityEvent : PooledEvent<GetElectricalConductivityEvent>
	{
		// Token: 0x06004D19 RID: 19737 RVA: 0x0017C091 File Offset: 0x0017A291
		public override int GetCascadeLevel()
		{
			return GetElectricalConductivityEvent.CascadeLevel;
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x0017C098 File Offset: 0x0017A298
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x0017C0A1 File Offset: 0x0017A2A1
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Source = null;
			this.Base = 0;
			this.Value = 0;
			this.Phase = 0;
			this.Pass = 0;
			this.ReductionObject = null;
			this.ReductionReason = null;
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0017C0E4 File Offset: 0x0017A2E4
		public static int GetFor(GameObject Object, out GameObject ReductionObject, out string ReductionReason, int Base = -2147483648, GameObject Source = null, int Phase = 0)
		{
			ReductionObject = null;
			ReductionReason = null;
			if (Phase == 0)
			{
				Phase = (GameObject.Validate(ref Source) ? Source.GetPhase() : 5);
			}
			if (Base == -2147483648 && GameObject.Validate(ref Object))
			{
				Base = (Object.PhaseMatches(Phase) ? Object.BaseElectricalConductivity : 0);
			}
			int num = Base;
			bool flag = true;
			Event @event = null;
			GetElectricalConductivityEvent getElectricalConductivityEvent = null;
			int num2 = 1;
			while (flag && num2 <= 3)
			{
				bool flag2 = false;
				if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetElectricalConductivity"))
				{
					flag2 = true;
					if (@event == null)
					{
						@event = Event.New("GetElectricalConductivity", 0, 0, 0);
						@event.SetParameter("Object", Object);
						@event.SetParameter("Base", Base);
						@event.SetParameter("Source", Source);
						@event.SetParameter("Phase", Phase);
					}
					@event.SetParameter("Value", num);
					@event.SetParameter("Pass", num2);
					@event.SetParameter("ReductionObject", ReductionObject);
					@event.SetParameter("ReductionReason", ReductionReason);
					flag = Object.FireEvent(@event);
					num = @event.GetIntParameter("Value", 0);
					ReductionObject = @event.GetGameObjectParameter("ReductionObject");
					ReductionReason = @event.GetStringParameter("ReductionReason", null);
				}
				if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetElectricalConductivityEvent>.ID, GetElectricalConductivityEvent.CascadeLevel))
				{
					flag2 = true;
					if (getElectricalConductivityEvent == null)
					{
						getElectricalConductivityEvent = PooledEvent<GetElectricalConductivityEvent>.FromPool();
						getElectricalConductivityEvent.Object = Object;
						getElectricalConductivityEvent.Base = Base;
						getElectricalConductivityEvent.Source = Source;
						getElectricalConductivityEvent.Phase = Phase;
					}
					getElectricalConductivityEvent.Value = num;
					getElectricalConductivityEvent.Pass = num2;
					getElectricalConductivityEvent.ReductionObject = ReductionObject;
					getElectricalConductivityEvent.ReductionReason = ReductionReason;
					flag = Object.HandleEvent(getElectricalConductivityEvent);
					num = getElectricalConductivityEvent.Value;
					ReductionObject = getElectricalConductivityEvent.ReductionObject;
					ReductionReason = getElectricalConductivityEvent.ReductionReason;
				}
				if (flag && !flag2)
				{
					flag = false;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x0017C2C0 File Offset: 0x0017A4C0
		public static int GetFor(GameObject Object, int Base = -2147483648, GameObject Source = null, int Phase = 0)
		{
			GameObject gameObject;
			string text;
			return GetElectricalConductivityEvent.GetFor(Object, out gameObject, out text, Base, Source, Phase);
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x0017C2DA File Offset: 0x0017A4DA
		public void MinValue(int Amount)
		{
			if (this.Value < Amount)
			{
				this.Value = Amount;
			}
		}

		// Token: 0x04002558 RID: 9560
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002559 RID: 9561
		public const int PASSES = 3;

		// Token: 0x0400255A RID: 9562
		public GameObject Object;

		// Token: 0x0400255B RID: 9563
		public GameObject Source;

		// Token: 0x0400255C RID: 9564
		public int Base;

		// Token: 0x0400255D RID: 9565
		public int Value;

		// Token: 0x0400255E RID: 9566
		public int Phase;

		// Token: 0x0400255F RID: 9567
		public int Pass;

		// Token: 0x04002560 RID: 9568
		public GameObject ReductionObject;

		// Token: 0x04002561 RID: 9569
		public string ReductionReason;
	}
}
