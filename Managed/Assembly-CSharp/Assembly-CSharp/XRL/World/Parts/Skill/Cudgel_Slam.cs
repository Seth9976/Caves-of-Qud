using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConsoleLib.Console;
using Genkit;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001335 RID: 4917
	[Serializable]
	public class Cudgel_Slam : BaseSkill
	{
		// Token: 0x0600C013 RID: 49171 RVA: 0x00465740 File Offset: 0x00463940
		public Cudgel_Slam()
		{
		}

		// Token: 0x0600C014 RID: 49172 RVA: 0x00465753 File Offset: 0x00463953
		public Cudgel_Slam(GameObject ParentObject)
			: this()
		{
			this.ParentObject = ParentObject;
		}

		// Token: 0x0600C015 RID: 49173 RVA: 0x00465762 File Offset: 0x00463962
		public int GetSlamDistance()
		{
			return 3 + this.ParentObject.GetIntProperty("SlamDistanceBonus", 0);
		}

		// Token: 0x0600C016 RID: 49174 RVA: 0x00465777 File Offset: 0x00463977
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Distance", this.GetSlamDistance(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Cudgel_Slam.COOLDOWN, 0);
		}

		// Token: 0x0600C017 RID: 49175 RVA: 0x004657A5 File Offset: 0x004639A5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C018 RID: 49176 RVA: 0x004657C3 File Offset: 0x004639C3
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C019 RID: 49177 RVA: 0x004657E8 File Offset: 0x004639E8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (GameObject.Validate(E.Target) && E.Target != E.Actor && E.Distance == 1 && (E.Actor.PhaseAndFlightMatches(E.Target) || (E.Actor.Stat("Intelligence", 0) < 30 && E.Actor.FlightMatches(E.Target) && !E.Actor.HasIntProperty("HasTriedToSlamOutOfPhase" + E.Target.ID))) && this.IsCudgelEquipped() && E.Actor.CanMoveExtremities(null, false, false, false) && !E.Actor.HasEffect<Enclosed>() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandCudgelSlam", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C01A RID: 49178 RVA: 0x004658CC File Offset: 0x00463ACC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandCudgelSlam");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C01B RID: 49179 RVA: 0x004658E1 File Offset: 0x00463AE1
		public GameObject GetEquippedCudgel()
		{
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetWeaponOfType("Cudgel", false, true);
		}

		// Token: 0x0600C01C RID: 49180 RVA: 0x00465900 File Offset: 0x00463B00
		public bool IsCudgelEquipped()
		{
			return this.GetEquippedCudgel() != null;
		}

		// Token: 0x0600C01D RID: 49181 RVA: 0x0046590B File Offset: 0x00463B0B
		public static bool IsSlammable(GameObject Object)
		{
			return Object.IsWall() || Object.IsDoor() || Object.IsCombatObject();
		}

		// Token: 0x0600C01E RID: 49182 RVA: 0x0046592C File Offset: 0x00463B2C
		public static bool IsSlammableAndNotOpenDoor(GameObject Object)
		{
			Door door;
			return Object.IsWall() || (Object.IsDoor() && Object.TryGetPart<Door>(out door) && !door.Open) || Object.IsCombatObject();
		}

		// Token: 0x0600C01F RID: 49183 RVA: 0x0046596C File Offset: 0x00463B6C
		public static bool Cast(GameObject attacker, Cudgel_Slam skill = null, string slamDir = null, GameObject target = null, bool requireWeapon = true, int presetSlamPower = -2147483648, string impactDamageIncrement = null)
		{
			Cell currentCell = attacker.GetCurrentCell();
			if (currentCell == null)
			{
				return false;
			}
			if (skill == null)
			{
				skill = new Cudgel_Slam(attacker);
			}
			Cell cell;
			string text;
			if (target != null)
			{
				cell = target.GetCurrentCell();
				text = slamDir ?? currentCell.GetDirectionFromCell(cell, true) ?? Directions.GetRandomDirection();
			}
			else
			{
				text = slamDir ?? skill.PickDirectionS(true, null, "Slam what?");
				cell = currentCell.GetCellFromDirection(text, true);
				if (cell == null)
				{
					return false;
				}
				Engulfed effect = attacker.GetEffect<Engulfed>();
				if (effect != null && effect.EngulfedBy != null)
				{
					target = effect.EngulfedBy;
					cell = target.CurrentCell;
				}
				else if (cell == attacker.CurrentCell)
				{
					target = attacker;
				}
				else
				{
					target = cell.GetCombatTarget(attacker, false, false, true, 0, null, null, null, null, null, true, false, new Predicate<GameObject>(Cudgel_Slam.IsSlammable));
				}
				if (target == null)
				{
					if (attacker.IsPlayer())
					{
						if ((target = cell.GetCombatTarget(attacker, false, false, false, 0, null, null, null, null, null, true, false, null)) != null)
						{
							attacker.ShowFailure("You cannot slam " + target.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
						}
						else if ((target = cell.GetCombatTarget(attacker, true, false, false, 0, null, null, null, null, null, true, false, new Predicate<GameObject>(Cudgel_Slam.IsSlammable))) != null)
						{
							attacker.ShowFailure(string.Concat(new string[]
							{
								"You cannot reach ",
								target.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" to slam ",
								target.them,
								"."
							}));
						}
						else
						{
							attacker.ShowFailure("There's nothing there to slam.");
						}
					}
					return false;
				}
			}
			if (!attacker.IsPlayer() && target == attacker)
			{
				return false;
			}
			GameObject gameObject = skill.GetEquippedCudgel() ?? attacker.GetPrimaryWeapon();
			if (gameObject == null && requireWeapon)
			{
				attacker.ShowFailure("You have no weapon!");
				return false;
			}
			if (!attacker.PhaseMatches(target))
			{
				IComponent<GameObject>.XDidYToZ(attacker, "try", "to slam", target, string.Concat(new string[]
				{
					"but ",
					attacker.poss("attack", true),
					" passes through ",
					target.them,
					" harmlessly"
				}), null, null, null, null, attacker, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				attacker.UseEnergy(1000, "Skill Cudgel Slam", null, null, false);
				return false;
			}
			int num = 1;
			Event @event = new Event("GetslamMultiplier", "Multiplier", num, "Weapon", gameObject);
			attacker.FireEvent(@event);
			if (gameObject != null)
			{
				gameObject.FireEvent(@event);
			}
			num = @event.GetIntParameter("Multiplier", 0);
			int num2 = attacker.StatMod("Strength", 0) * 5 * num;
			if (presetSlamPower != -2147483648)
			{
				num2 = presetSlamPower;
			}
			int slamWeightLimit = 2000 * num;
			if (target == attacker)
			{
				if (attacker.IsPlayer())
				{
					if (Popup.ShowYesNo("Are you sure you want to slam " + attacker.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
				}
				else
				{
					MetricsManager.LogError(attacker.DebugName + " attempted to use Slam on self " + ((target == null) ? "via cell targeting" : "via explicit specification"));
				}
			}
			int num3 = num;
			if (!BeforeSlamEvent.Check(attacker, target, cell, ref num, ref num2))
			{
				return false;
			}
			if (num != num3 && presetSlamPower != -2147483648)
			{
				num2 = num2 / num3 * num;
			}
			if (target.IsWall() && !target.IsCombatObject())
			{
				if (Stats.GetCombatAV(target) >= num2)
				{
					attacker.ShowFailure("You aren't strong enough to slam through " + target.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					return false;
				}
				attacker.PlayWorldSound("Sounds/Abilities/sfx_ability_cudgel_slam", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				target.DustPuff("&y", 15);
				for (int i = 0; i < 5; i++)
				{
					target.ParticleText(target.Render.TileColor + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
				}
				IComponent<GameObject> component = skill;
				string text2 = "slam";
				string text3 = "through";
				GameObject gameObject2 = target;
				string text4 = null;
				string text5 = null;
				string text6 = null;
				string text7 = null;
				GameObject gameObject3 = target;
				component.DidXToY(text2, text3, gameObject2, text4, text5, text6, text7, attacker, gameObject3, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				IComponent<GameObject>.XDidY(target, "are", "destroyed", null, null, null, null, target, false, false, null, null, false, false, false, false, false, null);
				target.Destroy(null, false, false, null);
				attacker.UseEnergy(1000, "Skill Cudgel Slam", null, null, false);
				if (!attacker.HasEffect<Cudgel_SmashingUp>())
				{
					skill.CooldownMyActivatedAbility(skill.ActivatedAbilityID, Cudgel_Slam.COOLDOWN, null, null, false);
				}
			}
			else if (target.IsDoor() && !target.IsCombatObject())
			{
				Door door;
				if (target.TryGetPart<Door>(out door) && door.Open)
				{
					attacker.ShowFailure(target.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " open.");
					return false;
				}
				if (Stats.GetCombatAV(target) >= num2)
				{
					attacker.ShowFailure("You aren't strong enough to slam through " + target.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					return false;
				}
				attacker.PlayWorldSound("Sounds/Abilities/sfx_ability_cudgel_slam", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				target.DustPuff("&y", 15);
				for (int j = 0; j < 5; j++)
				{
					target.ParticleText(target.Render.TileColor + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
				}
				IComponent<GameObject> component2 = skill;
				string text8 = "slam";
				string text9 = "through";
				GameObject gameObject4 = target;
				string text10 = null;
				string text11 = null;
				string text12 = null;
				string text13 = null;
				GameObject gameObject3 = target;
				component2.DidXToY(text8, text9, gameObject4, text10, text11, text12, text13, attacker, gameObject3, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				IComponent<GameObject>.XDidY(target, "are", "destroyed", null, null, null, null, target, false, false, null, null, false, false, false, false, false, null);
				target.Destroy(null, false, false, null);
				attacker.UseEnergy(1000, "Skill Cudgel Slam", null, null, false);
				if (!attacker.HasEffect<Cudgel_SmashingUp>())
				{
					skill.CooldownMyActivatedAbility(skill.ActivatedAbilityID, Cudgel_Slam.COOLDOWN, null, null, false);
				}
			}
			else
			{
				attacker.PlayWorldSound("Sounds/Abilities/sfx_ability_cudgel_slam", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				IComponent<GameObject> component3 = skill;
				string text14 = "attempt";
				string text15 = "to slam into";
				GameObject gameObject5 = target;
				string text16 = null;
				string text17 = "!";
				string text18 = null;
				string text19 = null;
				GameObject gameObject3 = target;
				component3.DidXToY(text14, text15, gameObject5, text16, text17, text18, text19, attacker, gameObject3, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				Event event2 = Event.New("BeginAttack", 0, 0, 0);
				event2.SetParameter("target", target);
				event2.SetParameter("TargetCell", cell);
				if (attacker.FireEvent(event2) && cell != null && attacker.Physics != null)
				{
					GameObject attacker2 = attacker;
					GameObject gameObject6 = target;
					GameObject gameObject7 = gameObject;
					Body body = attacker.Body;
					ref MeleeAttackResult ptr = Combat.MeleeAttackWithWeapon(attacker2, gameObject6, gameObject7, (body != null) ? body.FindDefaultOrEquippedItem(gameObject) : null, "Slamming", 0, 1, 1, 0, 0, gameObject != null && gameObject.IsEquippedOrDefaultOfPrimary(attacker), true);
					attacker.UseEnergy(1000, "Skill Cudgel Slam", null, null, false);
					if (!attacker.HasEffect<Cudgel_SmashingUp>())
					{
						skill.CooldownMyActivatedAbility(skill.ActivatedAbilityID, Cudgel_Slam.COOLDOWN, null, null, false);
					}
					if (ptr.Hits > 0 || !requireWeapon)
					{
						bool flag = false;
						if (!GameObject.Validate(target))
						{
							skill.LogInEditor("Displacing corpse mode!");
							flag = true;
							if (cell.Objects.Count == 0)
							{
								cell.AddObject("Garbage", null, null, null, null);
							}
							target = cell.GetObjectWithTag("Corpse");
						}
						int num4 = num;
						if (!SlamEvent.Check(attacker, target, cell, ref num, ref num2))
						{
							return true;
						}
						if (num != num4 && presetSlamPower != -2147483648)
						{
							num2 = num2 / num4 * num;
						}
						Cudgel_Slam.<>c__DisplayClass15_1 CS$<>8__locals2;
						CS$<>8__locals2.showJuice = false;
						CS$<>8__locals2.juiceTile = null;
						CS$<>8__locals2.juiceDetail = null;
						CS$<>8__locals2.juiceColor = null;
						CS$<>8__locals2.juiceStart = Location2D.Invalid;
						CS$<>8__locals2.juiceEnd = Location2D.Invalid;
						if (target != null)
						{
							CS$<>8__locals2.juiceTile = target.Render.Tile;
							CS$<>8__locals2.juiceDetail = target.Render.getDetailColor().ToString();
							CS$<>8__locals2.juiceColor = target.Render.GetTileForegroundColor();
							CS$<>8__locals2.juiceStart = target.CurrentCell.Location;
							CS$<>8__locals2.juiceEnd = target.CurrentCell.Location;
							if (target.IsVisible())
							{
								CS$<>8__locals2.showJuice = true;
								CombatJuice.cameraShake(0.1f, false);
							}
						}
						Dictionary<GameObject, string> dictionary = new Dictionary<GameObject, string>(8);
						int num5 = (3 + attacker.GetIntProperty("SlamDistanceBonus", 0)) * num;
						Cell cell2 = cell;
						int num6 = 0;
						while (num6 < num5 && skill.Slam(target, text, num5 - num6, num2, slamWeightLimit, dictionary))
						{
							if (GameObject.Validate(target))
							{
								cell2 = target.CurrentCell;
							}
							num6++;
						}
						CS$<>8__locals2.juiceEnd = cell2.Location;
						Cudgel_Slam.<Cast>g__ExecuteJuice|15_0(ref CS$<>8__locals2);
						string text20 = impactDamageIncrement ?? gameObject.GetPart<MeleeWeapon>().BaseDamage;
						if (dictionary.Count == 0 && target != null)
						{
							dictionary.Add(target, "s");
						}
						if (!Options.UseParticleVFX)
						{
							XRLCore.Core.RenderBaseToBuffer(ScreenBuffer.GetScrapBuffer2(false));
						}
						foreach (KeyValuePair<GameObject, string> keyValuePair in dictionary)
						{
							GameObject key = keyValuePair.Key;
							string value = keyValuePair.Value;
							if (key.IsPlayer())
							{
								CombatJuice.cameraShake(0.5f, false);
							}
							if (!flag || key != target)
							{
								int length = value.Length;
								int num7 = 0;
								int num8 = 0;
								for (int k = 0; k < value.Length; k++)
								{
									if (value[k] == 'w')
									{
										num7++;
									}
									else if (value[k] == 's')
									{
										num8++;
									}
								}
								int num9 = 0;
								for (int l = 0; l < num7; l++)
								{
									num9 += text20.RollCached();
								}
								int num10 = Math.Min(4, (num8 == 1) ? 1 : (num8 + 1));
								if (num9 > 0)
								{
									string text21 = null;
									string text22 = null;
									string text23 = null;
									string text24 = attacker.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
									string text25 = ((gameObject != null) ? (" with " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false)) : "");
									if (num7 == 0)
									{
										text21 = "from %o slam!";
										text22 = "You were slammed by " + text24 + text25 + ".";
										text23 = string.Concat(new string[] { key.It, " was @@slammed by ", text24, text25, "." });
									}
									else if (num7 == 1)
									{
										text21 = "from slamming into a wall!";
										text22 = "You were slammed into a wall by " + text24 + text25 + ".";
										text23 = string.Concat(new string[] { key.It, " was @@slammed into a wall by ", text24, text25, "." });
									}
									else if (num7 == 2)
									{
										text21 = "from slamming into {{W|two}} walls!";
										text22 = "You were slammed into two walls by " + text24 + text25 + ".";
										text23 = string.Concat(new string[] { key.It, " was @@slammed into two walls by ", text24, text25, "." });
									}
									else if (num7 >= 3)
									{
										string text26 = Grammar.Cardinal(num7);
										text21 = "from slamming into {{r|" + text26 + "}} walls!";
										text22 = string.Concat(new string[] { "You were slammed into ", text26, " walls by ", text24, text25, "." });
										text23 = string.Concat(new string[] { key.It, " was @@slammed into ", text26, " walls by ", text24, text25, "." });
									}
									GameObject gameObject8 = key;
									int num11 = num9;
									bool flag2 = key != target;
									gameObject3 = attacker;
									gameObject8.TakeDamage(num11, text21, "Crushing", text22, text23, null, gameObject3, null, null, null, flag2, false, false, true, false, false, false, false, false, 5, null);
								}
								key.ApplyEffect(new Stun(num10, -1, false), null);
							}
						}
						if (cell2 != null && cell2 != cell)
						{
							foreach (GameObject gameObject9 in cell.GetRealNonSceneryObjects((GameObject obj) => obj.PhaseAndFlightMatches(attacker) && obj.Weight < slamWeightLimit && obj.CanBeInvoluntarilyMoved()))
							{
								gameObject9.DirectMoveTo(cell2, 0, false, false, false, null);
							}
						}
						if (target != null)
						{
							target.Gravitate();
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600C020 RID: 49184 RVA: 0x004667DC File Offset: 0x004649DC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandCudgelSlam")
			{
				if (!this.IsCudgelEquipped())
				{
					this.ParentObject.ShowFailure("You must have a cudgel equipped in order to use slam.");
					return false;
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				Enclosed effect = this.ParentObject.GetEffect<Enclosed>();
				if (effect != null && effect.EnclosedBy != null)
				{
					return false;
				}
				if (!Cudgel_Slam.Cast(this.ParentObject, this, null, null, true, -2147483648, null))
				{
					return false;
				}
			}
			else
			{
				E.ID == "AttackerHit";
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C021 RID: 49185 RVA: 0x00466874 File Offset: 0x00464A74
		public bool Slam(GameObject Target, string Direction, int MaxDistance, int SlamPower, int SlamWeightLimit, Dictionary<GameObject, string> Effects)
		{
			if (Target == null)
			{
				return false;
			}
			if (MaxDistance < 0)
			{
				return false;
			}
			if (!GameObject.Validate(ref Target))
			{
				return false;
			}
			Cell cell = Target.CurrentCell;
			if (cell == null)
			{
				return false;
			}
			int num = 0;
			GameObject gameObject = null;
			Predicate<GameObject> <>9__0;
			for (;;)
			{
				Cell cell2 = cell;
				GameObject parentObject = this.ParentObject;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				int num2 = 0;
				GameObject gameObject2 = null;
				GameObject gameObject3 = null;
				GameObject gameObject4 = null;
				GameObject gameObject5 = null;
				List<GameObject> list = null;
				bool flag4 = true;
				bool flag5 = false;
				Predicate<GameObject> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (GameObject o) => o != Target && Cudgel_Slam.IsSlammableAndNotOpenDoor(o));
				}
				GameObject combatTarget = cell2.GetCombatTarget(parentObject, flag, flag2, flag3, num2, gameObject2, gameObject3, gameObject4, gameObject5, list, flag4, flag5, predicate);
				if (combatTarget == null || combatTarget == gameObject)
				{
					goto IL_018F;
				}
				if ((combatTarget.IsWall() || combatTarget.IsDoor()) && !combatTarget.IsCombatObject())
				{
					if (gameObject == null)
					{
						if (!Effects.ContainsKey(Target))
						{
							Effects.Add(Target, "");
						}
						GameObject gameObject6 = Target;
						Effects[gameObject6] += "w";
					}
					if (Stats.GetCombatAV(combatTarget) >= SlamPower)
					{
						break;
					}
					combatTarget.DustPuff("&y", 15);
					for (int i = 0; i < 5; i++)
					{
						combatTarget.ParticleText(combatTarget.Render.TileColor + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
					}
					IComponent<GameObject>.XDidY(combatTarget, "are", "destroyed", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					combatTarget.Destroy(null, false, false, null);
				}
				gameObject = combatTarget;
			}
			return false;
			IL_018F:
			if (cell != null)
			{
				cell.Smoke();
			}
			num++;
			cell = Target.CurrentCell.GetLocalCellFromDirection(Direction, true);
			if (cell == null)
			{
				return false;
			}
			gameObject = null;
			for (;;)
			{
				GameObject combatTarget2 = cell.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, new Predicate<GameObject>(Cudgel_Slam.IsSlammableAndNotOpenDoor));
				if (combatTarget2 == null || combatTarget2 == gameObject)
				{
					goto IL_02EF;
				}
				if ((combatTarget2.IsWall() || combatTarget2.IsDoor()) && !combatTarget2.IsCombatObject())
				{
					if (gameObject == null)
					{
						if (!Effects.ContainsKey(Target))
						{
							Effects.Add(Target, "");
						}
						GameObject gameObject6 = Target;
						Effects[gameObject6] += "w";
					}
					if (Stats.GetCombatAV(combatTarget2) >= SlamPower)
					{
						break;
					}
					combatTarget2.DustPuff("&y", 15);
					for (int j = 0; j < 5; j++)
					{
						combatTarget2.ParticleText(combatTarget2.Render.TileColor + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
					}
					IComponent<GameObject>.XDidY(combatTarget2, "are", "destroyed", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					combatTarget2.Destroy(null, false, false, null);
				}
				gameObject = combatTarget2;
			}
			return false;
			IL_02EF:
			if (cell.IsEmptyOfSolidFor(Target, true) && Target.Weight < SlamWeightLimit && Target.CanBeInvoluntarilyMoved())
			{
				if (Target.Move(Direction, true, false, true, false, true, true, null, null, false, null, null, null, false, false, null, null, 0))
				{
					if (!Effects.ContainsKey(Target))
					{
						Effects.Add(Target, "");
					}
					GameObject gameObject6 = Target;
					Effects[gameObject6] += "s";
					if (Target.CurrentCell != null)
					{
						foreach (Cell cell3 in Target.CurrentCell.GetAdjacentCells(true))
						{
							cell3.FireEvent(Event.New("ObjectEnteredAdjacentCell", "Object", Target));
						}
					}
					return true;
				}
			}
			else
			{
				gameObject = null;
				for (;;)
				{
					GameObject combatTarget3 = cell.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, new Predicate<GameObject>(Cudgel_Slam.IsSlammableAndNotOpenDoor));
					if (combatTarget3 == null || combatTarget3 == gameObject)
					{
						break;
					}
					gameObject = combatTarget3;
					this.Slam(combatTarget3, Direction, MaxDistance - 1, SlamPower, SlamWeightLimit, Effects);
					if (GameObject.Validate(ref combatTarget3))
					{
						combatTarget3.Gravitate();
					}
				}
			}
			if (cell.IsEmptyOfSolidFor(Target, true) && Target.Weight < SlamWeightLimit && Target.CanBeInvoluntarilyMoved() && Target.Move(Direction, true, false, true, false, true, true, null, null, false, null, null, null, false, false, null, null, 0))
			{
				if (!Effects.ContainsKey(Target))
				{
					Effects.Add(Target, "");
				}
				GameObject gameObject6 = Target;
				Effects[gameObject6] += "s";
				if (Target.CurrentCell != null)
				{
					foreach (Cell cell4 in Target.CurrentCell.GetAdjacentCells(true))
					{
						cell4.FireEvent(Event.New("ObjectEnteredAdjacentCell", "Object", Target));
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600C022 RID: 49186 RVA: 0x00466E10 File Offset: 0x00465010
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Slam", "CommandCudgelSlam", "Skills", null, "-", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C023 RID: 49187 RVA: 0x00466E58 File Offset: 0x00465058
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C025 RID: 49189 RVA: 0x00466E78 File Offset: 0x00465078
		[CompilerGenerated]
		internal static void <Cast>g__ExecuteJuice|15_0(ref Cudgel_Slam.<>c__DisplayClass15_1 A_0)
		{
			if ((Options.UseParticleVFX & A_0.showJuice) && A_0.juiceEnd != Location2D.Invalid && !string.IsNullOrEmpty(A_0.juiceTile) && !string.IsNullOrEmpty(A_0.juiceColor) && !string.IsNullOrEmpty(A_0.juiceDetail))
			{
				CombatJuice.playPrefabAnimation(A_0.juiceEnd, "Abilities/AbilityVFXSlam", null, string.Concat(new string[]
				{
					A_0.juiceTile,
					";",
					A_0.juiceColor,
					";",
					A_0.juiceDetail,
					";",
					A_0.juiceStart.ToString(),
					";",
					A_0.juiceEnd.ToString()
				}), null, true);
			}
		}

		// Token: 0x04004D41 RID: 19777
		public const int MAX_STUN_DURATION = 4;

		// Token: 0x04004D42 RID: 19778
		public static readonly int COOLDOWN = 50;

		// Token: 0x04004D43 RID: 19779
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
