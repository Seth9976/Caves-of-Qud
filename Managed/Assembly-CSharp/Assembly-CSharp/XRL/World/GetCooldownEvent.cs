using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200073B RID: 1851
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetCooldownEvent : PooledEvent<GetCooldownEvent>
	{
		// Token: 0x06004C9C RID: 19612 RVA: 0x0017A395 File Offset: 0x00178595
		public override int GetCascadeLevel()
		{
			return GetCooldownEvent.CascadeLevel;
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0017A39C File Offset: 0x0017859C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0017A3A5 File Offset: 0x001785A5
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Ability = null;
			this.Base = 0;
			this.PercentageReduction = 0;
			this.LinearReduction = 0;
			this.Calculations.Clear();
			this.StoreCalculations = false;
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x0017A3E2 File Offset: 0x001785E2
		public int ResultUncapped
		{
			get
			{
				return this.Base * (100 - this.PercentageReduction) / 100 - this.LinearReduction;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x0017A3FE File Offset: 0x001785FE
		public int Result
		{
			get
			{
				return Math.Max(this.ResultUncapped, ActivatedAbilities.MinimumValueForCooldown(this.Base));
			}
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0017A418 File Offset: 0x00178618
		public void AddCalculation(string Reason, int PercentageReduction, int LinearReduction)
		{
			if (this.StoreCalculations)
			{
				this.Calculations.Add(new GetCooldownEvent.CooldownCalculation
				{
					PercentageReduction = PercentageReduction,
					LinearReduction = LinearReduction,
					Reason = Reason
				});
			}
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0017A459 File Offset: 0x00178659
		public static GetCooldownEvent FromPool(GameObject Actor, ActivatedAbilityEntry Ability, int Base, int PercentageReduction = 0, int LinearReduction = 0, bool StoreCalculations = false)
		{
			GetCooldownEvent getCooldownEvent = PooledEvent<GetCooldownEvent>.FromPool();
			getCooldownEvent.Actor = Actor;
			getCooldownEvent.Ability = Ability;
			getCooldownEvent.Base = Base;
			getCooldownEvent.PercentageReduction = PercentageReduction;
			getCooldownEvent.LinearReduction = LinearReduction;
			getCooldownEvent.StoreCalculations = StoreCalculations;
			getCooldownEvent.Calculations.Clear();
			return getCooldownEvent;
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0017A497 File Offset: 0x00178697
		public static int GetFor(GameObject Actor, ActivatedAbilityEntry Ability, int Base)
		{
			GetCooldownEvent getCooldownEvent = GetCooldownEvent.TryCalculateFor(Actor, Ability, Base, 0, 0, false);
			if (getCooldownEvent == null)
			{
				return Base;
			}
			return getCooldownEvent.Result;
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0017A4B0 File Offset: 0x001786B0
		public static GetCooldownEvent TryCalculateFor(GameObject Actor, ActivatedAbilityEntry Ability, int Base, int PercentageReduction = 0, int LinearReduction = 0, bool StoreCalculations = true)
		{
			GetCooldownEvent getCooldownEvent = GetCooldownEvent.FromPool(Actor, Ability, Base, PercentageReduction, LinearReduction, StoreCalculations);
			if (Ability != null && Ability.ParentObject != null && Ability.AffectedByWillpower && Ability.ParentObject.HasStat("Willpower"))
			{
				int num = Math.Min(80, (Ability.ParentObject.Stat("Willpower", 0) - 16) * 5);
				getCooldownEvent.PercentageReduction += num;
				if (StoreCalculations)
				{
					string text = ((num > 0) ? "high" : "low");
					getCooldownEvent.AddCalculation(text + " Willpower", num, 0);
				}
			}
			if (Actor != null && Actor.WantEvent(PooledEvent<GetCooldownEvent>.ID, GetCooldownEvent.CascadeLevel))
			{
				Actor.HandleEvent(getCooldownEvent);
			}
			return getCooldownEvent;
		}

		// Token: 0x04002516 RID: 9494
		public new static readonly int CascadeLevel = 1;

		// Token: 0x04002517 RID: 9495
		public GameObject Actor;

		// Token: 0x04002518 RID: 9496
		public ActivatedAbilityEntry Ability;

		// Token: 0x04002519 RID: 9497
		public int Base;

		// Token: 0x0400251A RID: 9498
		public int PercentageReduction;

		// Token: 0x0400251B RID: 9499
		public int LinearReduction;

		// Token: 0x0400251C RID: 9500
		public List<GetCooldownEvent.CooldownCalculation> Calculations = new List<GetCooldownEvent.CooldownCalculation>();

		// Token: 0x0400251D RID: 9501
		public bool StoreCalculations;

		// Token: 0x02001A1D RID: 6685
		public struct CooldownCalculation
		{
			// Token: 0x0400681D RID: 26653
			public int PercentageReduction;

			// Token: 0x0400681E RID: 26654
			public int LinearReduction;

			// Token: 0x0400681F RID: 26655
			public string Reason;
		}
	}
}
