using System;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D2D RID: 3373
	public class Dominating : Effect
	{
		// Token: 0x060084F7 RID: 34039 RVA: 0x003006A6 File Offset: 0x002FE8A6
		public Dominating()
		{
			this.DisplayName = "{{B|projecting consciousness}}";
			this.Duration = 1;
		}

		// Token: 0x060084F8 RID: 34040 RVA: 0x003006C0 File Offset: 0x002FE8C0
		public Dominating(GameObject Target)
			: this()
		{
			this.Target = Target;
		}

		// Token: 0x060084F9 RID: 34041 RVA: 0x003006CF File Offset: 0x002FE8CF
		public override string GetDetails()
		{
			return "This creature is unresponsive.\nDV set to 0.";
		}

		// Token: 0x060084FA RID: 34042 RVA: 0x003006D6 File Offset: 0x002FE8D6
		public override int GetEffectType()
		{
			return 33587202;
		}

		// Token: 0x060084FB RID: 34043 RVA: 0x003006E0 File Offset: 0x002FE8E0
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect(typeof(Dominating)) || !ApplyEffectEvent.Check(Object, "Dominating", this, null))
			{
				return false;
			}
			base.StatShifter.SetStatShift("DV", -Stats.GetCombatDV(Object), false);
			return base.Apply(Object);
		}

		// Token: 0x060084FC RID: 34044 RVA: 0x00300730 File Offset: 0x002FE930
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			base.Remove(Object);
		}

		// Token: 0x060084FD RID: 34045 RVA: 0x00300744 File Offset: 0x002FE944
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == BeforeDeathRemovalEvent.ID || ID == GetZoneFreezabilityEvent.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x060084FE RID: 34046 RVA: 0x0030077A File Offset: 0x002FE97A
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (E.Effect != this && E.Effect.IsOfType(33554432))
			{
				this.InterruptDomination();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060084FF RID: 34047 RVA: 0x003007A4 File Offset: 0x002FE9A4
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (base.Object.IsPlayer())
			{
				this.InterruptDomination();
			}
			if (!GameObject.Validate(ref this.Target) || !this.Target.HasEffect(typeof(Dominated)))
			{
				base.Object.RemoveEffect(this, true);
				return true;
			}
			if (!(base.Object.Brain.Goals.Peek() is Dormant))
			{
				base.Object.Brain.Goals.Clear();
				base.Object.Brain.PushGoal(new Dormant(-1));
			}
			return false;
		}

		// Token: 0x06008500 RID: 34048 RVA: 0x00300841 File Offset: 0x002FEA41
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			this.PerformMetempsychosis();
			return base.HandleEvent(E);
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x00300851 File Offset: 0x002FEA51
		public override bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			E.Freezability = Freezability.FormerPlayerObject;
			return false;
		}

		// Token: 0x06008502 RID: 34050 RVA: 0x0030085C File Offset: 0x002FEA5C
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object && GameObject.Validate(ref this.Target))
			{
				if (E.Mental && !E.Physical)
				{
					E.Message = base.Object.Poss("mind", true) + " seems to be elsewhere.";
				}
				else
				{
					E.Message = base.Object.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " utterly unresponsive.";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008503 RID: 34051 RVA: 0x00300904 File Offset: 0x002FEB04
		public void InterruptDomination()
		{
			if (GameObject.Validate(ref this.Target))
			{
				this.Target.FireEvent("InterruptDomination");
			}
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x00300924 File Offset: 0x002FEB24
		private bool IsOurDominationEffect(Dominated FX)
		{
			return FX.Dominator == base.Object;
		}

		// Token: 0x06008505 RID: 34053 RVA: 0x00300934 File Offset: 0x002FEB34
		public Dominated GetDominationEffect(GameObject obj = null)
		{
			GameObject gameObject = obj ?? this.Target;
			if (gameObject == null)
			{
				return null;
			}
			return gameObject.GetEffect<Dominated>(new Predicate<Dominated>(this.IsOurDominationEffect));
		}

		// Token: 0x06008506 RID: 34054 RVA: 0x00300958 File Offset: 0x002FEB58
		public bool PerformMetempsychosis()
		{
			if (!GameObject.Validate(ref this.Target))
			{
				return false;
			}
			Dominated dominationEffect = this.GetDominationEffect(null);
			if (dominationEffect != null && !dominationEffect.BeingRemovedBySource)
			{
				dominationEffect.BeingRemovedBySource = true;
				dominationEffect.Metempsychosis = true;
				this.Target.RemoveEffect(dominationEffect, true);
				Domination.Metempsychosis(this.Target, dominationEffect.FromOriginalPlayerBody);
			}
			this.Target = null;
			return true;
		}

		// Token: 0x040039AD RID: 14765
		public GameObject Target;
	}
}
