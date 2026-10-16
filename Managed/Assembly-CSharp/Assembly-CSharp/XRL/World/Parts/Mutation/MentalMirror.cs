using System;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F3 RID: 5107
	[Serializable]
	public class MentalMirror : BaseMutation
	{
		// Token: 0x0600C5CA RID: 50634 RVA: 0x004800C4 File Offset: 0x0047E2C4
		public MentalMirror()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C5CB RID: 50635 RVA: 0x004800D8 File Offset: 0x0047E2D8
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("BonusMA", this.GetMABonus(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C5CC RID: 50636 RVA: 0x00480126 File Offset: 0x0047E326
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || ID == BeforeMentalDefendEvent.ID;
		}

		// Token: 0x0600C5CD RID: 50637 RVA: 0x00480144 File Offset: 0x0047E344
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("glass", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5CE RID: 50638 RVA: 0x0048016C File Offset: 0x0047E36C
		public override bool HandleEvent(BeforeMentalDefendEvent E)
		{
			if (!this.CheckActive() || E.Attacker == this.ParentObject)
			{
				return true;
			}
			if (E.Reflectable && E.Penetrations <= 0)
			{
				E.Penetrations = this.RollReflection(E);
				if (E.Penetrations > 0)
				{
					this.ReflectMessage(E.Attacker);
					E.Reflected = true;
					E.Defender = E.Attacker;
				}
			}
			else if (E.Penetrations > 0)
			{
				this.ShatterMessage(E.Attacker);
			}
			this.Activate();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5CF RID: 50639 RVA: 0x004801FC File Offset: 0x0047E3FC
		public int RollReflection(IMentalAttackEvent E)
		{
			int combatMA = Stats.GetCombatMA(E.Attacker);
			int num = Math.Max(this.ParentObject.StatMod("Willpower", 0), base.Level);
			return Stat.RollPenetratingSuccesses("1d8", combatMA, num + E.Modifier, 1, null);
		}

		// Token: 0x0600C5D0 RID: 50640 RVA: 0x00480247 File Offset: 0x0047E447
		public override string GetDescription()
		{
			return "You reflect mental attacks back at your attackers.";
		}

		// Token: 0x0600C5D1 RID: 50641 RVA: 0x0048024E File Offset: 0x0047E44E
		public int GetMABonus(int Level)
		{
			return 3 + Level;
		}

		// Token: 0x0600C5D2 RID: 50642 RVA: 0x00480253 File Offset: 0x0047E453
		public int GetCooldown(int Level)
		{
			return 50;
		}

		// Token: 0x0600C5D3 RID: 50643 RVA: 0x00480257 File Offset: 0x0047E457
		public bool UpdateStatShifts()
		{
			if (this.Active)
			{
				base.StatShifter.SetStatShift("MA", this.GetMABonus(base.Level), false);
			}
			else
			{
				base.StatShifter.RemoveStatShifts();
			}
			return true;
		}

		// Token: 0x0600C5D4 RID: 50644 RVA: 0x00480290 File Offset: 0x0047E490
		public override string GetLevelText(int Level)
		{
			return "When you suffer a mental attack while Mental Mirror is off cooldown, you gain +{{rules|" + this.GetMABonus(Level).ToString() + "}} mental armor (MA).\n" + "If the attack then fails to penetrate your MA, it's reflected back at your attacker.\n" + "Cooldown: " + this.GetCooldown(Level).ToString();
		}

		// Token: 0x0600C5D5 RID: 50645 RVA: 0x004802DE File Offset: 0x0047E4DE
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C5D6 RID: 50646 RVA: 0x004802E1 File Offset: 0x0047E4E1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DefenderAfterAttack");
			Registrar.Register("ReflectProjectile");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C5D7 RID: 50647 RVA: 0x00480304 File Offset: 0x0047E504
		public bool CheckActive()
		{
			bool flag = base.GetMyActivatedAbilityCooldown(this.ActivatedAbilityID, null) <= 0;
			if (flag != this.Active)
			{
				this.Active = flag;
				this.UpdateStatShifts();
			}
			return flag;
		}

		// Token: 0x0600C5D8 RID: 50648 RVA: 0x0048033D File Offset: 0x0047E53D
		public void Activate()
		{
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
		}

		// Token: 0x0600C5D9 RID: 50649 RVA: 0x0048035C File Offset: 0x0047E55C
		public void ReflectMessage(GameObject Actor)
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_mentalMirror_reflect", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (this.ParentObject.IsPlayer() || Actor.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Poss("mental mirror reflects the attack!", true), null, true);
			}
		}

		// Token: 0x0600C5DA RID: 50650 RVA: 0x004803CB File Offset: 0x0047E5CB
		public void ShatterMessage(GameObject Actor)
		{
			if (this.ParentObject.IsPlayer() || Actor.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Poss("mental mirror shatters!", true), null, true);
			}
		}

		// Token: 0x0600C5DB RID: 50651 RVA: 0x004803FC File Offset: 0x0047E5FC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DefenderAfterAttack" && this.CheckActive())
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.HasAttribute("Mental"))
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					if (gameObjectParameter == null)
					{
						return true;
					}
					if (E.GetIntParameter("Penetrations", 0) <= 0)
					{
						this.ReflectMessage(gameObjectParameter);
						GameObject gameObjectParameter2 = E.GetGameObjectParameter("Reflector");
						GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
						Combat.MeleeAttackWithWeapon(gameObjectParameter, gameObjectParameter2 ?? gameObjectParameter, gameObjectParameter3, gameObjectParameter3.DefaultOrEquippedPart(), E.GetStringParameter("Properties", null), 0, 0, 0, this.MyPowerLoadBonus(int.MinValue, 100, 150), 0, false, true);
					}
					else
					{
						this.ShatterMessage(gameObjectParameter);
					}
					this.Activate();
				}
			}
			else if (E.ID == "ReflectProjectile" && this.CheckActive())
			{
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				Projectile projectile;
				if (gameObjectParameter4 != null && gameObjectParameter4.TryGetPart<Projectile>(out projectile) && projectile.Attributes.Contains("Mental"))
				{
					this.Activate();
					if (80.in100())
					{
						float num = (float)E.GetParameter("Angle");
						E.SetParameter("Direction", (int)num + 180);
						E.SetParameter("By", this.ParentObject);
						E.SetParameter("Verb", "reflect");
						return false;
					}
					this.ShatterMessage(E.GetGameObjectParameter("Attacker"));
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C5DC RID: 50652 RVA: 0x004805A0 File Offset: 0x0047E7A0
		public override bool ChangeLevel(int NewLevel)
		{
			this.UpdateStatShifts();
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C5DD RID: 50653 RVA: 0x004805B0 File Offset: 0x0047E7B0
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Mental Mirror", "CommandMentalMirror", "Mental Mutations", null, "m", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C5DE RID: 50654 RVA: 0x004805F9 File Offset: 0x0047E7F9
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.StatShifter.RemoveStatShifts();
			return base.Unmutate(GO);
		}

		// Token: 0x0600C5DF RID: 50655 RVA: 0x0048061B File Offset: 0x0047E81B
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C5E0 RID: 50656 RVA: 0x0048061E File Offset: 0x0047E81E
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!this.Active)
			{
				this.CheckActive();
			}
		}

		// Token: 0x04004E10 RID: 19984
		[NonSerialized]
		public bool Active;
	}
}
