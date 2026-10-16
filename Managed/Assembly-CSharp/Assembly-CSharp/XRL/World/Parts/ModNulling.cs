using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, reality stabilization strength levels for
	/// weapon applications are increased by ((power load - 100) / 30,
	/// i.e. 10 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001155 RID: 4437
	[Serializable]
	public class ModNulling : IModification
	{
		// Token: 0x0600B03F RID: 45119 RVA: 0x0040E6ED File Offset: 0x0040C8ED
		public ModNulling()
		{
		}

		// Token: 0x0600B040 RID: 45120 RVA: 0x0040E6F5 File Offset: 0x0040C8F5
		public ModNulling(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B041 RID: 45121 RVA: 0x0040E6FE File Offset: 0x0040C8FE
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600B042 RID: 45122 RVA: 0x0040E70E File Offset: 0x0040C90E
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<Armor>() || Object.HasPart<Shield>() || Object.HasPart<MeleeWeapon>() || Object.HasPart<MissileWeapon>() || Object.HasPart<ThrownWeapon>();
		}

		// Token: 0x0600B043 RID: 45123 RVA: 0x0040E73C File Offset: 0x0040C93C
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			RealityStabilization realityStabilization = Object.RequirePart<RealityStabilization>(false);
			if (Object.HasPart<Armor>() || Object.HasPart<Shield>())
			{
				if (realityStabilization.ChargeUse < 3)
				{
					realityStabilization.ChargeUse = 3;
				}
				if (realityStabilization.Visibility < 1)
				{
					realityStabilization.Visibility = 1;
				}
				if (realityStabilization.SelfVisibility < 1)
				{
					realityStabilization.SelfVisibility = 1;
				}
				if (realityStabilization.CellVisibilityOffset < 1)
				{
					realityStabilization.CellVisibilityOffset = 1;
				}
				realityStabilization.Projective = true;
				realityStabilization.WorksOn(false, false, true, false, false, true, false, false, true, true);
				realityStabilization.HitpointsAffectPerformance = true;
				realityStabilization.GlimmerInterference = true;
				realityStabilization.IsPowerLoadSensitive = true;
				int num = Math.Max((this.Tier >= 5) ? (10 - this.Tier) : (this.Tier + 2), 2);
				BootSequence bootSequence = Object.GetPart<BootSequence>();
				if (bootSequence == null)
				{
					bootSequence = new BootSequence();
					bootSequence.BootTime = num;
					bootSequence.ChargeUse = 750;
					Object.AddPart<BootSequence>(bootSequence, true, false);
				}
				else
				{
					if (bootSequence.BootTime < num)
					{
						bootSequence.BootTime = num;
					}
					if (bootSequence.ChargeUse < 750)
					{
						bootSequence.ChargeUse = 750;
					}
				}
				bootSequence.ReadoutInName = true;
				bootSequence.ReadoutInDescription = true;
				bootSequence.SyncWorksOn(realityStabilization);
				if (!Object.HasPart<PowerSwitch>())
				{
					Object.AddPart<PowerSwitch>(new PowerSwitch
					{
						EnergyCost = 100,
						FlippableWithoutUnderstanding = true,
						ActivateSuccessMessage = "",
						ActivateFailureMessage = "",
						DeactivateSuccessMessage = "",
						DeactivateFailureMessage = ""
					}, true, false);
				}
			}
			else
			{
				if (realityStabilization.ChargeUse < 150)
				{
					realityStabilization.ChargeUse = 150;
				}
				realityStabilization.ResetWorksOn();
				realityStabilization.IsPowerLoadSensitive = true;
				realityStabilization.HitpointsAffectPerformance = true;
				realityStabilization.GlimmerInterference = true;
			}
			base.IncreaseDifficultyAndComplexity(1, 2, null);
		}

		// Token: 0x0600B044 RID: 45124 RVA: 0x0040E8FA File Offset: 0x0040CAFA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckUsesChargeWhileEquippedEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B045 RID: 45125 RVA: 0x0040E920 File Offset: 0x0040CB20
		public override bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			return (this.ParentObject.HasPart<Armor>() || this.ParentObject.HasPart<Shield>()) && base.HandleEvent(E);
		}

		// Token: 0x0600B046 RID: 45126 RVA: 0x0040E945 File Offset: 0x0040CB45
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{K|nulling}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B047 RID: 45127 RVA: 0x0040E96F File Offset: 0x0040CB6F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B048 RID: 45128 RVA: 0x0040E9A3 File Offset: 0x0040CBA3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B049 RID: 45129 RVA: 0x0040E9A6 File Offset: 0x0040CBA6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LauncherProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponPseudoThrowHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B04A RID: 45130 RVA: 0x0040E9DC File Offset: 0x0040CBDC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "LauncherProjectileHit" || E.ID == "WeaponThrowHit" || E.ID == "WeaponPseudoThrowHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				RealityStabilization part = this.ParentObject.GetPart<RealityStabilization>();
				if (part != null && GameObject.Validate(ref gameObjectParameter2))
				{
					int num = this.MyPowerLoadLevel();
					IActivePart activePart = part;
					bool flag = true;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = true;
					bool flag10 = false;
					int num2 = 1;
					int? num3 = new int?(num);
					if (activePart.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
					{
						this.RealityStabilizeTransient(gameObjectParameter, gameObjectParameter2, part, num);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B04B RID: 45131 RVA: 0x0040EAA3 File Offset: 0x0040CCA3
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B04C RID: 45132 RVA: 0x0040EAAA File Offset: 0x0040CCAA
		public static string GetDescription(int Tier)
		{
			return "Nulling: When powered on a weapon, this item astrally burdens its target on hit. When powered on a piece of armor or shield, this item astrally burdens its wearer. Compute power on the local lattice increases the effectiveness of this effect.";
		}

		// Token: 0x0600B04D RID: 45133 RVA: 0x0040EAB1 File Offset: 0x0040CCB1
		public string GetInstanceDescription()
		{
			if (this.ParentObject.HasPart<Armor>())
			{
				return "Nulling: When powered, this armor astrally burdens its wearer. Compute power on the local lattice increases the effectiveness of this effect.";
			}
			if (this.ParentObject.HasPart<Shield>())
			{
				return "Nulling: When powered, this shield astrally burdens its wielder. Compute power on the local lattice increases the effectiveness of this effect.";
			}
			return "Nulling: When powered, this weapon astrally burdens its target on hit. Compute power on the local lattice increases the effectiveness of this effect.";
		}

		// Token: 0x0600B04E RID: 45134 RVA: 0x0040EAE0 File Offset: 0x0040CCE0
		public void RealityStabilizeTransient(GameObject who, GameObject obj, RealityStabilization rs = null, int PowerLoad = 100)
		{
			if (rs == null)
			{
				rs = this.ParentObject.GetPart<RealityStabilization>();
				if (rs == null)
				{
					return;
				}
			}
			rs.FlushEffectiveStrengthCache();
			rs.LastStatus = new ActivePartStatus?(ActivePartStatus.Operational);
			rs.Strength = Stat.Random(1, 100) + this.Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 30) + GetAvailableComputePowerEvent.GetFor(who);
			int effectiveStrength = rs.EffectiveStrength;
			if (effectiveStrength <= 0)
			{
				return;
			}
			int num = Stat.Random(2, 3);
			RealityStabilized realityStabilized = obj.GetEffect<RealityStabilized>();
			if (realityStabilized == null)
			{
				realityStabilized = new RealityStabilized();
				if (!obj.ForceApplyEffect(realityStabilized, null))
				{
					return;
				}
			}
			if (realityStabilized.IndependentStrength < effectiveStrength)
			{
				realityStabilized.IndependentStrength = effectiveStrength;
			}
			if (realityStabilized.Duration < num)
			{
				realityStabilized.Duration = num;
			}
			if (!GameObject.Validate(ref realityStabilized.Owner))
			{
				realityStabilized.Owner = who;
			}
			realityStabilized.Stabilize(false);
		}
	}
}
