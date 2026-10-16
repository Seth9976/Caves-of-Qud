using System;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x0200115D RID: 4445
	[Serializable]
	public class ModPsionic : IModification
	{
		// Token: 0x0600B0B1 RID: 45233 RVA: 0x0040FB86 File Offset: 0x0040DD86
		public ModPsionic()
		{
		}

		// Token: 0x0600B0B2 RID: 45234 RVA: 0x0040FBBE File Offset: 0x0040DDBE
		public ModPsionic(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0B3 RID: 45235 RVA: 0x0040FBF7 File Offset: 0x0040DDF7
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B0B4 RID: 45236 RVA: 0x0040FC00 File Offset: 0x0040DE00
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600B0B5 RID: 45237 RVA: 0x0040FC08 File Offset: 0x0040DE08
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<MeleeWeapon>(false).Stat = "Ego";
			Object.RemovePart<TinkerItem>();
			base.IncreaseComplexityIfComplex(1, null);
		}

		// Token: 0x0600B0B6 RID: 45238 RVA: 0x0040FC2A File Offset: 0x0040DE2A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B0B7 RID: 45239 RVA: 0x0040FC30 File Offset: 0x0040DE30
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanBeModdedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<ModificationAppliedEvent>.ID || ID == PooledEvent<NotifyTargetImmuneEvent>.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B0B8 RID: 45240 RVA: 0x0040FC84 File Offset: 0x0040DE84
		public override bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			if (E.Weapon == this.ParentObject && GameObject.Validate(E.Actor) && !E.Actor.IsPlayer() && E.Actor.Brain != null && !E.Actor.Brain.HasGoal("ChangeEquipment"))
			{
				GoalHandler goalHandler = E.Actor.Brain.FindGoal("Kill");
				if (goalHandler != null)
				{
					goalHandler.PushChildGoal(new ChangeEquipment(E.Weapon));
					if (!E.Actor.Brain.HasGoal("Reequip"))
					{
						goalHandler.InsertGoalAsParent(new Reequip());
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0B9 RID: 45241 RVA: 0x0040FD33 File Offset: 0x0040DF33
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600B0BA RID: 45242 RVA: 0x0040FD3E File Offset: 0x0040DF3E
		public override bool HandleEvent(CanBeModdedEvent E)
		{
			return false;
		}

		// Token: 0x0600B0BB RID: 45243 RVA: 0x0040FD44 File Offset: 0x0040DF44
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (((E != null) ? E.Object : null) != null)
			{
				if (!E.Understood() || !E.Object.HasProperName)
				{
					E.AddAdjective("{{psionic|psionic}}", 0);
				}
				E.AddTag("(" + this.GetPsionicModSwingsRemainingDescription() + ")", -40);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0BC RID: 45244 RVA: 0x0040FDA4 File Offset: 0x0040DFA4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModPsionic.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0BD RID: 45245 RVA: 0x0040FDC4 File Offset: 0x0040DFC4
		public override bool HandleEvent(ModificationAppliedEvent E)
		{
			if (E.Modification == this)
			{
				this.SwingsRemaining = this.StartingSwings.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0BE RID: 45246 RVA: 0x0040FDE7 File Offset: 0x0040DFE7
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.SwingsRemaining = this.StartingSwings.RollCached();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0BF RID: 45247 RVA: 0x0040FE01 File Offset: 0x0040E001
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B0C0 RID: 45248 RVA: 0x0040FE04 File Offset: 0x0040E004
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponAfterAttack");
			Registrar.Register("WeaponAfterAttackMissed");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B0C1 RID: 45249 RVA: 0x0040FE24 File Offset: 0x0040E024
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponAfterAttack" || E.ID == "WeaponAfterAttackMissed")
			{
				this.SwingsRemaining--;
				if (this.SwingsRemaining <= 0)
				{
					GameObject equipped = this.ParentObject.Equipped;
					base.DidX("disappear", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					this.ParentObject.ForceUnequipRemoveAndRemoveContents(true, true);
					this.ParentObject.Destroy(null, false, false, null);
					if (equipped != null && equipped.IsValid() && !equipped.IsPlayer() && equipped.Brain != null)
					{
						equipped.Brain.PerformReequip(false, true, false, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B0C2 RID: 45250 RVA: 0x0040FEE4 File Offset: 0x0040E0E4
		public static string GetDescription(int Tier)
		{
			return "Psionic: This weapon uses the wielder's Ego modifier for penetration bonus instead of Strength mod and attacks MA instead of AV. It will dissipate from the corporeal realm after some use.";
		}

		// Token: 0x0600B0C3 RID: 45251 RVA: 0x0040FEEC File Offset: 0x0040E0EC
		public string GetPsionicModSwingsRemainingDescription()
		{
			if (this.SwingsRemaining > this.SwingThreshold3)
			{
				return "substantial";
			}
			if (this.SwingsRemaining > this.SwingThreshold2)
			{
				return "lambent";
			}
			if (this.SwingsRemaining > this.SwingThreshold1)
			{
				return "tenuous";
			}
			return "insubstantial";
		}

		// Token: 0x0400481E RID: 18462
		public int SwingsRemaining = -1;

		// Token: 0x0400481F RID: 18463
		public string StartingSwings = "126-500";

		// Token: 0x04004820 RID: 18464
		public int SwingThreshold1 = 125;

		// Token: 0x04004821 RID: 18465
		public int SwingThreshold2 = 250;

		// Token: 0x04004822 RID: 18466
		public int SwingThreshold3 = 375;
	}
}
