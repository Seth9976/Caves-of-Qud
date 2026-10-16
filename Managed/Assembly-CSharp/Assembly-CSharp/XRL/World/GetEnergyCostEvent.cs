using System;

namespace XRL.World
{
	// Token: 0x02000747 RID: 1863
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Singleton)]
	public class GetEnergyCostEvent : SingletonEvent<GetEnergyCostEvent>
	{
		// Token: 0x06004D21 RID: 19745 RVA: 0x0017C2FD File Offset: 0x0017A4FD
		public override int GetCascadeLevel()
		{
			return GetEnergyCostEvent.CascadeLevel;
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0017C304 File Offset: 0x0017A504
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0017C30D File Offset: 0x0017A50D
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.BaseAmount = 0;
			this.Amount = 0;
			this.Type = null;
			this.PercentageReduction = 0;
			this.LinearReduction = 0;
			this.Passive = false;
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x0017C346 File Offset: 0x0017A546
		public bool TypeMatches(string lookFor)
		{
			return !string.IsNullOrEmpty(this.Type) && this.Type.Contains(lookFor);
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x0017C364 File Offset: 0x0017A564
		public static int GetFor(GameObject Actor, int BaseAmount, string Type, int PercentageReduction = 0, int LinearReduction = 0, int MinAmount = 0, bool Passive = false)
		{
			int num = BaseAmount;
			if (Actor.HasRegisteredEvent("UsingEnergy"))
			{
				Event @event = Event.New("UsingEnergy", "Amount", num, "Type", Type);
				Actor.FireEvent(@event);
				int intParameter = @event.GetIntParameter("Amount", 0);
				if (intParameter != num)
				{
					LinearReduction += num - intParameter;
				}
			}
			if (Actor.WantEvent(SingletonEvent<GetEnergyCostEvent>.ID, GetEnergyCostEvent.CascadeLevel))
			{
				SingletonEvent<GetEnergyCostEvent>.Instance.Actor = Actor;
				SingletonEvent<GetEnergyCostEvent>.Instance.BaseAmount = BaseAmount;
				SingletonEvent<GetEnergyCostEvent>.Instance.Amount = num;
				SingletonEvent<GetEnergyCostEvent>.Instance.Type = Type;
				SingletonEvent<GetEnergyCostEvent>.Instance.PercentageReduction = PercentageReduction;
				SingletonEvent<GetEnergyCostEvent>.Instance.LinearReduction = LinearReduction;
				SingletonEvent<GetEnergyCostEvent>.Instance.Passive = Passive;
				Actor.HandleEvent(SingletonEvent<GetEnergyCostEvent>.Instance);
				num = SingletonEvent<GetEnergyCostEvent>.Instance.Amount;
				PercentageReduction = SingletonEvent<GetEnergyCostEvent>.Instance.PercentageReduction;
				LinearReduction = SingletonEvent<GetEnergyCostEvent>.Instance.LinearReduction;
			}
			if (PercentageReduction != 0)
			{
				num = num * (100 - PercentageReduction) / 100;
			}
			if (LinearReduction != 0)
			{
				num -= LinearReduction;
			}
			if (num < MinAmount)
			{
				num = MinAmount;
			}
			return num;
		}

		// Token: 0x04002562 RID: 9570
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002563 RID: 9571
		public GameObject Actor;

		// Token: 0x04002564 RID: 9572
		public int BaseAmount;

		// Token: 0x04002565 RID: 9573
		public int Amount;

		// Token: 0x04002566 RID: 9574
		public string Type;

		// Token: 0x04002567 RID: 9575
		public int PercentageReduction;

		// Token: 0x04002568 RID: 9576
		public int LinearReduction;

		// Token: 0x04002569 RID: 9577
		public bool Passive;
	}
}
