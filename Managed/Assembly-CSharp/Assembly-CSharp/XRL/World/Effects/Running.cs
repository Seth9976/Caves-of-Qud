using System;
using System.Text;
using XRL.Rules;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D7C RID: 3452
	[Serializable]
	public class Running : Effect, ITierInitialized
	{
		// Token: 0x06008901 RID: 35073 RVA: 0x00310DB3 File Offset: 0x0030EFB3
		public Running()
		{
			this.DisplayName = "sprinting";
		}

		// Token: 0x06008902 RID: 35074 RVA: 0x00310DC6 File Offset: 0x0030EFC6
		public Running(int Duration = 0, string DisplayName = "sprinting", string MessageName = "sprinting", bool SpringingEffective = true)
			: this()
		{
			this.Duration = Duration;
			this.DisplayName = DisplayName;
			this.MessageName = MessageName;
			this.SpringingEffective = SpringingEffective;
		}

		// Token: 0x06008903 RID: 35075 RVA: 0x00310DEB File Offset: 0x0030EFEB
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 50);
			this.DisplayName = "sprinting";
			this.MessageName = "sprinting";
		}

		// Token: 0x06008904 RID: 35076 RVA: 0x00310E12 File Offset: 0x0030F012
		public override int GetEffectType()
		{
			return 218103936;
		}

		// Token: 0x06008905 RID: 35077 RVA: 0x00310E19 File Offset: 0x0030F019
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008906 RID: 35078 RVA: 0x00310E1C File Offset: 0x0030F01C
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (!base.Object.HasSkill("Tactics_Hurdle"))
			{
				stringBuilder.Compound("-5 DV", '\n');
			}
			stringBuilder.Compound(string.Format("Moves at {0}X the normal speed. (+{1} move speed)", this.GetMovespeedMultiplier(), this.MovespeedBonus), '\n');
			if (!this.IsEnhanced())
			{
				if (base.Object.HasSkill("Pistol_SlingAndRun"))
				{
					stringBuilder.Compound("Reduced accuracy with missile weapons, except pistols.", '\n');
				}
				else
				{
					stringBuilder.Compound("Reduced accuracy with missile weapons.", '\n');
				}
				stringBuilder.Compound("-10 to hit in melee combat.", '\n').Compound("Is ended by attacking in melee, by effects that interfere with movement, and by most other actions that have action costs, other than using physical mutations.", '\n');
			}
			if (this.Duration == 9999)
			{
				stringBuilder.Compound("Indefinite duration.", '\n');
			}
			else
			{
				stringBuilder.Compound(this.Duration.ToString() + " " + ((this.Duration == 1) ? "round" : "rounds") + " left.", '\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008907 RID: 35079 RVA: 0x00310F28 File Offset: 0x0030F128
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Running>())
			{
				return false;
			}
			if (!Object.CanChangeMovementMode(this.MessageName, false, false, false, false))
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyRunning", "Effect", this)))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_movementBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			base.StatShifter.DefaultDisplayName = this.DisplayName;
			Object.MovementModeChanged(this.MessageName, false);
			base.DidX("begin", this.MessageName, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			if (!Object.HasSkill("Tactics_Hurdle") && Object.HasStat("DV"))
			{
				base.StatShifter.SetStatShift("DV", -5, false);
				this.PenaltyApplied = true;
			}
			this.RecalcMovespeedBonus();
			return true;
		}

		// Token: 0x06008908 RID: 35080 RVA: 0x00311018 File Offset: 0x0030F218
		public override void Remove(GameObject Object)
		{
			base.DidX("stop", this.MessageName, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			base.StatShifter.RemoveStatShifts(Object);
			base.Remove(Object);
		}

		// Token: 0x06008909 RID: 35081 RVA: 0x00311058 File Offset: 0x0030F258
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<EndActionEvent>.ID || ID == SingletonEvent<UseEnergyEvent>.ID || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x0600890A RID: 35082 RVA: 0x00311088 File Offset: 0x0030F288
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == base.Object && E.Checking == "Actor" && E.Melee && !this.IsEnhanced())
			{
				E.Modifier -= 10;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600890B RID: 35083 RVA: 0x003110DC File Offset: 0x0030F2DC
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (base.Object != null && base.Object.OnWorldMap())
				{
					this.Duration = 0;
				}
				else
				{
					this.RecalcMovespeedBonus();
					if (this.Duration != 9999 && base.Object.GetIntProperty("SkatesEquipped", 0) <= 0)
					{
						this.Duration--;
					}
				}
				this.RecalcMovespeedBonus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600890C RID: 35084 RVA: 0x00311152 File Offset: 0x0030F352
		public override bool HandleEvent(EndActionEvent E)
		{
			this.RecalcMovespeedBonus();
			return base.HandleEvent(E);
		}

		// Token: 0x0600890D RID: 35085 RVA: 0x00311164 File Offset: 0x0030F364
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (E.Type != null && !E.Type.Contains("Movement") && E.Type.Contains("Physical") && !this.IsEnhanced() && E.Amount >= 500)
			{
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600890E RID: 35086 RVA: 0x003111C0 File Offset: 0x0030F3C0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginAttack");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600890F RID: 35087 RVA: 0x003111EB File Offset: 0x0030F3EB
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("\u001a", "Tiles2/status_sprinting.bmp", "&W", "W", 35, 10);
			}
			return true;
		}

		// Token: 0x06008910 RID: 35088 RVA: 0x00311218 File Offset: 0x0030F418
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginAttack")
			{
				if (!this.IsEnhanced())
				{
					this.Duration = 0;
				}
			}
			else if (E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				string stringParameter = E.GetStringParameter("To", null);
				if (stringParameter == "Frozen")
				{
					base.Object.RemoveEffect(this, true);
				}
				else if (!this.IsEnhanced() && (!(stringParameter == "Jumping") || !base.Object.HasPart<Tactics_Hurdle>()))
				{
					base.Object.RemoveEffect(this, true);
				}
				else
				{
					this.RecalcMovespeedBonus();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008911 RID: 35089 RVA: 0x003112DB File Offset: 0x0030F4DB
		public static bool IsEnhanced(GameObject Object)
		{
			return Object != null && Object.GetIntProperty("EnhancedSprint", 0) > 0;
		}

		// Token: 0x06008912 RID: 35090 RVA: 0x003112F1 File Offset: 0x0030F4F1
		public bool IsEnhanced()
		{
			return Running.IsEnhanced(base.Object);
		}

		// Token: 0x06008913 RID: 35091 RVA: 0x00311300 File Offset: 0x0030F500
		public void RecalcMovespeedBonus()
		{
			if (this.Duration > 0)
			{
				base.StatShifter.RemoveStatShift(base.Object, "MoveSpeed", false);
				int num = 100 - base.Object.Stat("MoveSpeed", 0) + 100;
				this.MovespeedBonus = (int)((float)num * (this.GetMovespeedMultiplier() - 1f));
				base.StatShifter.SetStatShift("MoveSpeed", -this.MovespeedBonus, false);
			}
		}

		// Token: 0x06008914 RID: 35092 RVA: 0x00311374 File Offset: 0x0030F574
		public float GetMovespeedMultiplier()
		{
			return Running.GetMovespeedMultiplier(base.Object, this.SpringingEffective, null);
		}

		// Token: 0x06008915 RID: 35093 RVA: 0x00311388 File Offset: 0x0030F588
		public static float GetMovespeedMultiplier(GameObject Object, bool SpringingEffective = true, Templates.StatCollector stats = null)
		{
			float num;
			if (SpringingEffective && Object.HasEffect<Springing>())
			{
				if (stats != null)
				{
					stats.AddPercentageBonusModifier("Multiplier", 100, "springing effect");
				}
				num = 3f;
			}
			else
			{
				num = 2f;
			}
			Wings part = Object.GetPart<Wings>();
			if (part != null)
			{
				float num2 = part.SprintingMoveSpeedBonus(part.Level);
				if (stats != null)
				{
					stats.AddPercentageBonusModifier("Multiplier", (int)(num2 * 100f), part.GetDisplayName(true) + " " + part.GetMutationTerm(null));
				}
				num *= 1f + part.SprintingMoveSpeedBonus(part.Level);
			}
			if (stats != null)
			{
				stats.CollectBonusModifiers("Multiplier", 100, "Move speed multiplier", true);
			}
			if (stats != null)
			{
				stats.Set("Multipler", (int)((num - 1f) * 100f), num != 2f, (num > 2f) ? 1 : (-1));
			}
			return num;
		}

		// Token: 0x04003A69 RID: 14953
		public bool PenaltyApplied;

		// Token: 0x04003A6A RID: 14954
		public string MessageName;

		// Token: 0x04003A6B RID: 14955
		public bool SpringingEffective;

		// Token: 0x04003A6C RID: 14956
		public int MovespeedBonus;
	}
}
