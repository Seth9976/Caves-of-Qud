using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleLib.Console;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F1 RID: 5105
	[Serializable]
	public class LightManipulation : BaseMutation
	{
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600C5A1 RID: 50593 RVA: 0x0047ED0D File Offset: 0x0047CF0D
		private static GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref LightManipulation._Projectile))
				{
					LightManipulation._Projectile = GameObject.CreateUnmodified("ProjectileLightManipulation");
				}
				return LightManipulation._Projectile;
			}
		}

		// Token: 0x0600C5A2 RID: 50594 RVA: 0x0047ED2F File Offset: 0x0047CF2F
		public LightManipulation()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C5A3 RID: 50595 RVA: 0x0047ED58 File Offset: 0x0047CF58
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Radius", this.GetMaxLightRadius(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("RechargeRate", "1 per " + this.GetRadiusRegrowthTurns().ToString() + " rounds", !stats.mode.Contains("ability"), 0);
			stats.Set("LaserDamageInc", this.GetDamage(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("LaserPen", this.GetLasePenetrationBonus(Level) + RuleSettings.VISUAL_PENETRATION_BONUS, !stats.mode.Contains("ability"), 0);
		}

		// Token: 0x0600C5A4 RID: 50596 RVA: 0x0047EE19 File Offset: 0x0047D019
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == BeforeRenderEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C5A5 RID: 50597 RVA: 0x0047EE4F File Offset: 0x0047D04F
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.LaseActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			base.DescribeMyActivatedAbility(this.LightActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5A6 RID: 50598 RVA: 0x0047EE90 File Offset: 0x0047D090
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 999 && this.RadiusPenalty < this.MaxLightRadius && base.IsMyActivatedAbilityAIUsable(this.LaseActivatedAbilityID, null) && GameObject.Validate(E.Target) && !E.Target.IsInStasis() && this.ParentObject.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add("CommandLase", 2, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5A7 RID: 50599 RVA: 0x0047EF11 File Offset: 0x0047D111
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.RadiusPenalty < this.MaxLightRadius && base.IsMyActivatedAbilityToggledOn(this.LightActivatedAbilityID, null))
			{
				base.AddLight(this.MaxLightRadius - this.RadiusPenalty, LightLevel.Light, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5A8 RID: 50600 RVA: 0x0047EF50 File Offset: 0x0047D150
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandLase")
			{
				if (!base.IsMyActivatedAbilityUsable(this.LaseActivatedAbilityID, null))
				{
					return false;
				}
				if (this.ParentObject.OnWorldMap())
				{
					this.ParentObject.Fail("You cannot do that on the world map.");
					return false;
				}
				if (this.RadiusPenalty >= this.MaxLightRadius)
				{
					this.ParentObject.Fail("Your capacity is too weak.");
					return false;
				}
				List<Cell> list = base.PickLine(999, AllowVis.Any, (GameObject o) => o.IsCombat && o.PhaseMatches(this.ParentObject), false, false, true, false, this.ParentObject, LightManipulation.Projectile, "Lase", true, true);
				if (list == null || list.Count <= 0)
				{
					return false;
				}
				if (list.Count > 1000)
				{
					list.RemoveRange(1000, list.Count - 1000);
				}
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_lightManipulation_laser_fire", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				base.UseEnergy(1000, "Mental Mutation LightManipulation Lase");
				Cell cell = list[0];
				Cell cell2 = list.Last<Cell>();
				float num = (float)Math.Atan2((double)(cell2.X - cell.X), (double)(cell2.Y - cell.Y)).toDegrees();
				list.RemoveAt(0);
				for (int i = 0; i < list.Count; i++)
				{
					Cell cell3 = list[i];
					if (cell3.IsBlackedOut())
					{
						if (cell3.IsVisible() || (i > 0 && list[i - 1].IsVisible()))
						{
							IComponent<GameObject>.AddPlayerMessage("The darkness absorbs the laser beam.", null, true);
						}
						list.RemoveRange(i, list.Count - i);
					}
					else if (cell3.HasObjectWithRegisteredEvent("RefractLight") || cell3.HasObjectWithRegisteredEvent("ReflectProjectile"))
					{
						bool flag = true;
						GameObject gameObject = null;
						string text = null;
						string text2 = "refract";
						int num2 = -1;
						if (cell3.HasObjectWithRegisteredEvent("RefractLight"))
						{
							Event @event = Event.New("RefractLight", 0, 0, 0);
							@event.SetParameter("Projectile", null);
							@event.SetParameter("Attacker", this.ParentObject);
							@event.SetParameter("Cell", cell3);
							@event.SetParameter("Angle", num);
							@event.SetParameter("Direction", Stat.Random(0, 359));
							@event.SetParameter("Verb", null);
							@event.SetParameter("Sound", "sfx_light_refract");
							@event.SetParameter("By", null);
							flag = cell3.FireEvent(@event);
							if (!flag)
							{
								gameObject = @event.GetGameObjectParameter("By");
								text = @event.GetStringParameter("Sound", null);
								text2 = @event.GetStringParameter("Verb", null) ?? "refract";
								num2 = @event.GetIntParameter("Direction", 0).normalizeDegrees();
							}
						}
						if (flag && cell3.HasObjectWithRegisteredEvent("ReflectProjectile"))
						{
							Event event2 = Event.New("ReflectProjectile", 0, 0, 0);
							event2.SetParameter("Projectile", null);
							event2.SetParameter("Attacker", this.ParentObject);
							event2.SetParameter("Cell", cell3);
							event2.SetParameter("Angle", num);
							event2.SetParameter("Direction", Stat.Random(0, 359));
							event2.SetParameter("Verb", null);
							event2.SetParameter("Sound", "sfx_light_refract");
							event2.SetParameter("By", null);
							flag = cell3.FireEvent(event2);
							if (!flag)
							{
								gameObject = event2.GetGameObjectParameter("By");
								text = event2.GetStringParameter("Sound", null);
								text2 = event2.GetStringParameter("Verb", null) ?? "refract";
								num2 = event2.GetIntParameter("Direction", 0).normalizeDegrees();
							}
						}
						if (!flag && GameObject.Validate(ref gameObject))
						{
							base.PlayWorldSound(text, 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
							IComponent<GameObject>.XDidY(gameObject, text2, "the laser beam", "!", null, null, gameObject, null, false, false, null, null, false, false, false, false, false, null);
							float num3 = (float)cell3.X;
							float num4 = (float)cell3.Y;
							float num5 = (float)Math.Sin((double)((float)num2 * 0.017453292f));
							float num6 = (float)Math.Cos((double)((float)num2 * 0.017453292f));
							list.RemoveRange(i, list.Count - i);
							Cell cell4 = cell3;
							do
							{
								num3 += num5;
								num4 += num6;
								Cell cell5 = cell3.ParentZone.GetCell((int)num3, (int)num4);
								if (cell5 == null)
								{
									break;
								}
								if (cell5 != cell4)
								{
									list.Add(cell5);
									cell4 = cell5;
									if (cell5.GetCombatTarget(this.ParentObject, true, false, false, 0, LightManipulation.Projectile, null, null, null, null, true, true, null) != null || cell5.HasSolidObjectForMissile(this.ParentObject, null, LightManipulation.Projectile, false, false))
									{
										break;
									}
								}
							}
							while (num3 > 0f && num3 < 79f && num4 > 0f && num4 < 24f && list.Count < 400);
						}
					}
				}
				int num7 = 0;
				int count = list.Count;
				while (num7 < count && !this.Lase(list[num7], count))
				{
					num7++;
				}
				this.RadiusPenalty++;
				this.SyncAbilityName();
				if (this.RadiusPenalty > this.MaxLightRadius)
				{
				}
			}
			else if (E.Command == "CommandAmbientLight")
			{
				if (base.IsMyActivatedAbilityToggledOn(this.LightActivatedAbilityID, null))
				{
					base.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_lightManipulation_deactivate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					base.ToggleMyActivatedAbility(this.LightActivatedAbilityID, null, false, null);
				}
				else
				{
					if (base.IsMyActivatedAbilityCoolingDown(this.LaseActivatedAbilityID, null))
					{
						if (this.ParentObject.IsPlayer())
						{
							if (Options.AbilityCooldownWarningAsMessage)
							{
								MessageQueue.AddPlayerMessage("You must wait {{C|" + base.GetMyActivatedAbilityCooldownDescription(this.LaseActivatedAbilityID, null) + "}} before you can enable ambient light.", null, true);
							}
							else
							{
								Popup.ShowFail("You must wait {{C|" + base.GetMyActivatedAbilityCooldownDescription(this.LaseActivatedAbilityID, null) + "}} before you can enable ambient light.", true, true, true);
							}
						}
						return false;
					}
					base.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_lightManipulation_activate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					base.ToggleMyActivatedAbility(this.LightActivatedAbilityID, null, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5A9 RID: 50601 RVA: 0x0047F619 File Offset: 0x0047D819
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("stars", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5AA RID: 50602 RVA: 0x0047F641 File Offset: 0x0047D841
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C5AB RID: 50603 RVA: 0x0047F644 File Offset: 0x0047D844
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterMassMind");
			Registrar.Register("EndTurn");
			Registrar.Register("RefractLight");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C5AC RID: 50604 RVA: 0x0047F66F File Offset: 0x0047D86F
		public override string GetDescription()
		{
			return "You manipulate light to your advantage.";
		}

		// Token: 0x0600C5AD RID: 50605 RVA: 0x0047F678 File Offset: 0x0047D878
		public override string GetLevelText(int Level)
		{
			return "" + "You produce ambient light within a radius of {{rules|" + this.GetMaxLightRadius(Level).ToString() + "}}.\n" + "You may focus the light into a laser beam, temporarily reducing the radius of your ambient light by 1.\n" + "Laser damage increment: {{rules|" + this.GetDamage(Level) + "}}\n" + "Laser penetration: {{rules|" + (this.GetLasePenetrationBonus(Level) + RuleSettings.VISUAL_PENETRATION_BONUS).ToString() + "}}\n" + "Ambient light recharges at a rate of 1 unit every " + this.GetRadiusRegrowthTurns().ToString() + " rounds until it reaches its maximum value.\n" + "{{rules|" + this.GetReflectChance(Level).ToString() + "%}} chance to reflect light-based damage";
		}

		// Token: 0x0600C5AE RID: 50606 RVA: 0x0047F727 File Offset: 0x0047D927
		public int GetMaxLightRadius(int Level)
		{
			return (int)(4.0 + Math.Floor((double)((float)Level / 2f)));
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600C5AF RID: 50607 RVA: 0x0047F742 File Offset: 0x0047D942
		public int MaxLightRadius
		{
			get
			{
				return this.GetMaxLightRadius(base.Level);
			}
		}

		// Token: 0x0600C5B0 RID: 50608 RVA: 0x0047F750 File Offset: 0x0047D950
		public string GetDamage(int Level)
		{
			if (Level <= 1)
			{
				return "1d3";
			}
			if (Level <= 2)
			{
				return "1d4";
			}
			if (Level <= 3)
			{
				return "1d5";
			}
			if (Level <= 4)
			{
				return "1d4+1";
			}
			if (Level <= 5)
			{
				return "1d5+1";
			}
			if (Level <= 6)
			{
				return "1d4+2";
			}
			if (Level <= 7)
			{
				return "1d5+2";
			}
			if (Level <= 8)
			{
				return "1d4+3";
			}
			if (Level <= 9)
			{
				return "1d5+3";
			}
			if (Level > 9)
			{
				return "1d5+" + (Level - 6).ToString();
			}
			return "1d4+4";
		}

		// Token: 0x0600C5B1 RID: 50609 RVA: 0x0047F7D8 File Offset: 0x0047D9D8
		public int GetLasePenetrationBonus()
		{
			return this.GetLasePenetrationBonus(base.Level);
		}

		// Token: 0x0600C5B2 RID: 50610 RVA: 0x0047F7E6 File Offset: 0x0047D9E6
		public int GetLasePenetrationBonus(int Level)
		{
			return 4 + (Level - 1) / 2;
		}

		// Token: 0x0600C5B3 RID: 50611 RVA: 0x0047F7F0 File Offset: 0x0047D9F0
		public bool Lase(Cell C, int PathLength = 0)
		{
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer screenBuffer = TextConsole.ScrapBuffer.WithMap();
			bool flag = false;
			if (C != null)
			{
				GameObject combatTarget = C.GetCombatTarget(this.ParentObject, true, false, false, 0, LightManipulation.Projectile, null, null, null, null, true, true, null);
				if (combatTarget != null)
				{
					int lasePenetrationBonus = this.GetLasePenetrationBonus();
					int num = Stat.RollDamagePenetrations(combatTarget.Stat("AV", 0), lasePenetrationBonus, lasePenetrationBonus);
					if (num > 0)
					{
						string resultColor = Stat.GetResultColor(num);
						int num2 = 0;
						string damage = this.GetDamage(base.Level);
						for (int i = 0; i < num; i++)
						{
							num2 += damage.RollCached();
						}
						GameObject gameObject = combatTarget;
						int num3 = num2;
						GameObject parentObject = this.ParentObject;
						gameObject.TakeDamage(num3, string.Concat(new string[]
						{
							"from %t laser beam! {{",
							resultColor,
							"|(x",
							num.ToString(),
							")}}"
						}), "Light Laser", null, null, parentObject, null, null, null, null, false, false, false, false, false, true, false, false, false, 5, null);
					}
					else if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("Your laser beam doesn't penetrate " + combatTarget.poss("armor", true) + ".", 'r', true);
					}
					else if (combatTarget.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Poss("laser beam", true) + " doesn't penetrate your armor.", 'g', true);
					}
					flag = true;
				}
			}
			if (C.IsVisible() || this.ParentObject.IsPlayer())
			{
				int num4 = Stat.Random(1, 3);
				if (num4 == 1)
				{
					screenBuffer.WriteAt(C, "&C\u000f", true);
				}
				else if (num4 == 2)
				{
					screenBuffer.WriteAt(C, "&Y\u000f", true);
				}
				else
				{
					screenBuffer.WriteAt(C, "&B\u000f", true);
				}
				screenBuffer.Draw();
				int num5 = 10 - PathLength / 5;
				if (num5 > 0)
				{
					Thread.Sleep(num5);
				}
			}
			return flag;
		}

		// Token: 0x0600C5B4 RID: 50612 RVA: 0x0047F9C2 File Offset: 0x0047DBC2
		public int GetReflectChance()
		{
			return this.GetReflectChance(base.Level);
		}

		// Token: 0x0600C5B5 RID: 50613 RVA: 0x0047F9D0 File Offset: 0x0047DBD0
		public int GetReflectChance(int Level)
		{
			return 10 + 3 * Level;
		}

		// Token: 0x0600C5B6 RID: 50614 RVA: 0x0047F9D8 File Offset: 0x0047DBD8
		public void SyncAbilityName()
		{
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.LaseActivatedAbilityID, null);
			if (activatedAbilityEntry != null)
			{
				activatedAbilityEntry.DisplayName = "Lase (" + (this.MaxLightRadius - this.RadiusPenalty).ToString() + " charges)";
			}
		}

		// Token: 0x0600C5B7 RID: 50615 RVA: 0x0047FA20 File Offset: 0x0047DC20
		public static int GetRadiusRegrowthTurns(int Willpower)
		{
			int num = 15;
			if (!GlobalConfig.GetBoolSetting("LightManipulationWillpowerRecharge", false))
			{
				return num;
			}
			int num2 = num;
			int num3 = (Willpower - 16) * 5;
			if (num3 != 0)
			{
				num2 = num2 * (100 - num3) / 100;
			}
			return Math.Max(Math.Min(num2, num * 5), num / 5);
		}

		// Token: 0x0600C5B8 RID: 50616 RVA: 0x0047FA66 File Offset: 0x0047DC66
		public int GetRadiusRegrowthTurns()
		{
			GameObject parentObject = this.ParentObject;
			return LightManipulation.GetRadiusRegrowthTurns((parentObject != null) ? parentObject.Stat("Willpower", 0) : 16);
		}

		// Token: 0x0600C5B9 RID: 50617 RVA: 0x0047FA88 File Offset: 0x0047DC88
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterMassMind")
			{
				this.RadiusPenalty = 0;
				this.SyncAbilityName();
			}
			else if (E.ID == "EndTurn")
			{
				if (this.RadiusPenalty > 0)
				{
					this.RadiusRegrowthTimer++;
					if (this.RadiusRegrowthTimer >= this.GetRadiusRegrowthTurns())
					{
						this.RadiusRegrowthTimer = 0;
						this.RadiusPenalty--;
						this.SyncAbilityName();
					}
				}
			}
			else if (E.ID == "RefractLight" && this.GetReflectChance().in100())
			{
				E.SetParameter("By", this.ParentObject);
				E.SetParameter("Direction", (int)((float)E.GetParameter("Angle")) + 180);
				E.SetParameter("Verb", "reflect");
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C5BA RID: 50618 RVA: 0x0047FB81 File Offset: 0x0047DD81
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C5BB RID: 50619 RVA: 0x0047FB8C File Offset: 0x0047DD8C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.LaseActivatedAbilityID = base.AddMyActivatedAbility("Lase", "CommandLase", "Mental Mutations", null, "\u000f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.LightActivatedAbilityID = base.AddMyActivatedAbility("Ambient Light", "CommandAmbientLight", "Mental Mutations", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C5BC RID: 50620 RVA: 0x0047FC09 File Offset: 0x0047DE09
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.LightActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.LaseActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E03 RID: 19971
		public const int RANGE = 999;

		// Token: 0x04004E04 RID: 19972
		public const int COUNT = 1000;

		// Token: 0x04004E05 RID: 19973
		public const int BASE_RADIUS_REGROWTH_TURNS = 15;

		// Token: 0x04004E06 RID: 19974
		public const int WILLPOWER_BASELINE = 16;

		// Token: 0x04004E07 RID: 19975
		public const int WILLPOWER_FACTOR = 5;

		// Token: 0x04004E08 RID: 19976
		public const int WILLPOWER_CEILING_FACTOR = 5;

		// Token: 0x04004E09 RID: 19977
		public const int WILLPOWER_FLOOR_DIVISOR = 5;

		// Token: 0x04004E0A RID: 19978
		public Guid LaseActivatedAbilityID = Guid.Empty;

		// Token: 0x04004E0B RID: 19979
		public Guid LightActivatedAbilityID = Guid.Empty;

		// Token: 0x04004E0C RID: 19980
		public int RadiusPenalty;

		// Token: 0x04004E0D RID: 19981
		public int RadiusRegrowthTimer;

		// Token: 0x04004E0E RID: 19982
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
