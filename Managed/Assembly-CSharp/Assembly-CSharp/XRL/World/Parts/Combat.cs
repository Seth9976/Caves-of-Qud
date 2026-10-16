using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000E58 RID: 3672
	[Serializable]
	public class Combat : IPart
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600948D RID: 38029 RVA: 0x00358342 File Offset: 0x00356542
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x0600948E RID: 38030 RVA: 0x00358349 File Offset: 0x00356549
		public override void Attach()
		{
			if (this.ParentObject.Brain != null)
			{
				(ref this.ParentObject.Flags).SetBit(2, true);
			}
		}

		// Token: 0x0600948F RID: 38031 RVA: 0x0035836A File Offset: 0x0035656A
		public override void Remove()
		{
			(ref this.ParentObject.Flags).SetBit(2, false);
		}

		// Token: 0x06009490 RID: 38032 RVA: 0x0035837E File Offset: 0x0035657E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009491 RID: 38033 RVA: 0x00358381 File Offset: 0x00356581
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetDefenderHitDiceEvent.ID || ID == GetNavigationWeightEvent.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x06009492 RID: 38034 RVA: 0x003583B7 File Offset: 0x003565B7
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.ParentObject.ClearShieldBlocks();
			return base.HandleEvent(E);
		}

		// Token: 0x06009493 RID: 38035 RVA: 0x003583CC File Offset: 0x003565CC
		public override bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			GameObject shield = this.ParentObject.GetShield(new Predicate<GameObject>(this.CanBlockWithShield), E.Attacker);
			if (shield == null)
			{
				return true;
			}
			if (!this.ParentObject.CanMoveExtremities(null, false, false, false))
			{
				return true;
			}
			Shield part = shield.GetPart<Shield>();
			if (part == null)
			{
				return true;
			}
			this.BlockedWithShield(shield, part);
			int num;
			if (this.ParentObject.HasEffect<ShieldWall>() && part.WornOn == "Hand")
			{
				num = 100;
			}
			else
			{
				num = 25 * (1 + this.ParentObject.GetIntProperty("ImprovedBlock", 0));
				if (this.ParentObject.HasSkill("Shield_Block"))
				{
					num += 25;
				}
				if (this.ParentObject.HasSkill("Shield_DeftBlocking"))
				{
					num += 25;
				}
			}
			if (BeforeShieldBlockEvent.Check(E.Attacker, E.Defender, E.Weapon, shield, part, ref num) && num.in100())
			{
				E.ShieldBlocked = true;
				if (Combat.TrackShieldBlock > 0)
				{
					Combat.TrackShieldBlock++;
				}
				if (this.ParentObject.IsPlayer())
				{
					string text = (shield.HasProperName ? shield.ShortDisplayName : this.ParentObject.poss(shield, true, null));
					IComponent<GameObject>.AddPlayerMessage(string.Format("You block with {0}! (+{1} AV)", text, part.AV), 'g', true);
				}
				this.ParentObject.ParticleText("*block (" + part.AV.Signed(false) + " AV)", IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null), false, 1.5f, -8f);
				this.ParentObject.PlayWorldSound(shield.GetPropertyOrTag("BlockSound", null).Coalesce("Sounds/Melee/multiUseBlock/sfx_melee_naturalWeapon_fist_blocked"), 0.5f, 0f, false, shield.HasIntProperty("BlockSoundDelay") ? ((float)shield.GetIntProperty("BlockSoundDelay", 0) / 1000f) : 0f, 1f, 1f, int.MaxValue);
				E.AV += part.AV;
				if (this.ParentObject.HasSkill("Shield_StaggeringBlock") && GameObject.Validate(E.Attacker))
				{
					int num2 = 20;
					if (this.ParentObject.HasStat("Strength"))
					{
						num2 = this.ParentObject.Stat("Strength", 0) * 2 - 35;
					}
					if (num2.in100())
					{
						if (this.ParentObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You stagger " + E.Attacker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " with your shield block!", 'g', true);
						}
						else if (E.Attacker.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You are staggered by " + this.ParentObject.poss("block", true) + "!", 'r', true);
						}
						E.Attacker.ApplyEffect(new Stun(Stat.Random(1, 2), 12, false), null);
					}
				}
				AfterShieldBlockEvent.Send(E.Attacker, E.Defender, E.Weapon, shield, part);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009494 RID: 38036 RVA: 0x003586FC File Offset: 0x003568FC
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.Actor != this.ParentObject)
			{
				if (E.IgnoreCreatures)
				{
					E.MinWeight(2);
				}
				else if (!this.ParentObject.CanBePositionSwapped(null) && (!E.Flying || this.ParentObject.IsFlying))
				{
					E.MinWeight(100);
				}
				else if (E.Actor != null)
				{
					E.Uncacheable = true;
					if (this.ParentObject.IsHostileTowards(E.Actor))
					{
						if (this.ParentObject != E.Actor.Target)
						{
							if (E.Flying && !this.ParentObject.IsFlying)
							{
								E.MinWeight(5);
							}
							else
							{
								E.MinWeight(E.Autoexploring ? 30 : 95);
							}
						}
					}
					else
					{
						GameObject target = this.ParentObject.Target;
						if (target != null)
						{
							if (this.ParentObject.isAdjacentTo(target) || E.Actor.Target == target)
							{
								E.MinWeight(95);
							}
							else
							{
								E.MinWeight(3);
							}
						}
						else
						{
							Brain brain = this.ParentObject.Brain;
							int? num;
							if (brain == null)
							{
								num = null;
							}
							else
							{
								CleanStack<GoalHandler> goals = brain.Goals;
								if (goals == null)
								{
									num = null;
								}
								else
								{
									List<GoalHandler> items = goals.Items;
									num = ((items != null) ? new int?(items.Count<GoalHandler>()) : null);
								}
							}
							E.MinWeight(num ?? 2);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009495 RID: 38037 RVA: 0x0035887E File Offset: 0x00356A7E
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (E.Actor != this.ParentObject)
			{
				E.MinWeight(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009496 RID: 38038 RVA: 0x0035889C File Offset: 0x00356A9C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Flight.SWOOP_ATTACK_COMMAND_NAME)
			{
				Combat.SwoopAttack(this.ParentObject, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009497 RID: 38039 RVA: 0x003588C4 File Offset: 0x00356AC4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009498 RID: 38040 RVA: 0x003588C8 File Offset: 0x00356AC8
		public static bool ThrowWeapon(GameObject Attacker, GameObject Defender = null, Cell TargetCell = null, MissilePath Path = null, IList<GameObject> Weapons = null)
		{
			if (!Attacker.CanMoveExtremities(null, true, false, true))
			{
				return false;
			}
			if (Attacker.OnWorldMap())
			{
				return Attacker.Fail("You cannot throw things on the world map.");
			}
			if (Weapons == null)
			{
				Weapons = Attacker.GetThrownWeapons(null, null);
			}
			if (Weapons.IsNullOrEmpty<GameObject>())
			{
				return Attacker.Fail("You do not have a thrown weapon equipped.");
			}
			int count = Weapons.Count;
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			GameObject gameObject3 = null;
			if (TargetCell == null && Attacker.IsPlayer())
			{
				Physics physics;
				if (((Defender != null) ? Defender.CurrentCell : null) != null && !Attacker.IsConfused)
				{
					physics = Defender.Physics;
				}
				else
				{
					GameObject currentTarget = Sidebar.CurrentTarget;
					if (((currentTarget != null) ? currentTarget.CurrentCell : null) != null && !Attacker.IsConfused)
					{
						physics = Sidebar.CurrentTarget.Physics;
					}
					else
					{
						physics = Attacker.Physics;
					}
				}
				FireType fireType = FireType.Normal;
				int num = 9999;
				int num2 = 9999;
				foreach (GameObject gameObject4 in Weapons)
				{
					int num3;
					int num4;
					GetThrownWeaponRangeEvent.GetFor(out num3, out num4, gameObject4, Attacker, 9999, -1);
					if (num3 < num)
					{
						num = num3;
					}
					if (num4 < num2)
					{
						num2 = num4;
					}
				}
				switch (Weapons.Count)
				{
				case 1:
					goto IL_0149;
				case 2:
					break;
				case 3:
					gameObject3 = Weapons[2];
					break;
				default:
					Weapons = new List<GameObject>(Weapons);
					goto IL_0160;
				}
				gameObject2 = Weapons[1];
				IL_0149:
				gameObject = Weapons[0];
				Weapons = null;
				IL_0160:
				int x = physics.CurrentCell.X;
				int y = physics.CurrentCell.Y;
				bool flag = true;
				AllowVis allowVis = AllowVis.Any;
				int num5 = num;
				bool flag2 = false;
				int num6 = num2;
				Path = MissileWeapon.ShowPicker(x, y, flag, allowVis, num5, flag2, gameObject ?? Weapons[0], ref fireType, num6);
				if (Path == null)
				{
					return false;
				}
				TargetCell = physics.CurrentZone.GetCell((int)(Path.X1 / 3f), (int)(Path.Y1 / 3f));
			}
			if (TargetCell == null)
			{
				return false;
			}
			int num7 = (int)(1000.0 / (double)count);
			bool flag3 = false;
			if (gameObject != null)
			{
				GameObject gameObject5 = gameObject;
				Cell cell = TargetCell;
				MissilePath missilePath = Path;
				int num8 = 0;
				int? num9 = new int?(num7);
				if (Attacker.PerformThrow(gameObject5, cell, Defender, missilePath, num8, null, null, num9))
				{
					flag3 = true;
				}
			}
			if (gameObject2 != null)
			{
				GameObject gameObject6 = gameObject2;
				Cell cell2 = TargetCell;
				MissilePath missilePath2 = Path;
				int num10 = 0;
				int? num9 = new int?(num7);
				if (Attacker.PerformThrow(gameObject6, cell2, Defender, missilePath2, num10, null, null, num9))
				{
					flag3 = true;
				}
			}
			if (gameObject3 != null)
			{
				GameObject gameObject7 = gameObject3;
				Cell cell3 = TargetCell;
				MissilePath missilePath3 = Path;
				int num11 = 0;
				int? num9 = new int?(num7);
				if (Attacker.PerformThrow(gameObject7, cell3, Defender, missilePath3, num11, null, null, num9))
				{
					flag3 = true;
				}
			}
			if (Weapons != null)
			{
				foreach (GameObject gameObject8 in Weapons)
				{
					GameObject gameObject9 = gameObject8;
					Cell cell4 = TargetCell;
					MissilePath missilePath4 = Path;
					int num12 = 0;
					int? num9 = new int?(num7);
					if (Attacker.PerformThrow(gameObject9, cell4, Defender, missilePath4, num12, null, null, num9))
					{
						flag3 = true;
					}
				}
			}
			return flag3;
		}

		// Token: 0x06009499 RID: 38041 RVA: 0x00358BCC File Offset: 0x00356DCC
		public static bool FireMissileWeapon(GameObject Attacker, GameObject AimedAt = null, Cell TargetCell = null, FireType FireType = FireType.Normal, string Skill = null, int Rapid = 0, int SweepShots = 0, int SweepWidth = 90, float? EnergyMultiplier = null, bool SkipPastMaxRange = false)
		{
			bool flag = !Attacker.IsPlayer();
			if (!Attacker.CanMoveExtremities(null, true, false, true))
			{
				return false;
			}
			if (Attacker.OnWorldMap())
			{
				return Attacker.Fail("You cannot fire missile weapons on the world map.");
			}
			if (!Attacker.FireEvent("CanFireMissileWeapon"))
			{
				return false;
			}
			Combat combat;
			if (!Attacker.TryGetPart<Combat>(out combat))
			{
				return false;
			}
			List<GameObject> list;
			if (Skill.IsNullOrEmpty())
			{
				list = Attacker.GetMissileWeapons(null, null);
			}
			else
			{
				list = Attacker.GetMissileWeapons(null, (MissileWeapon mw) => mw.Skill == Skill);
			}
			if (list == null || list.Count == 0)
			{
				return Attacker.Fail("You do not have " + (Skill.IsNullOrEmpty() ? "a" : "an appropriate") + " missile weapon equipped!");
			}
			List<MissileWeapon> list2;
			if (Combat.MissileWeaponPartListInUse)
			{
				list2 = new List<MissileWeapon>(list.Count);
			}
			else
			{
				Combat.MissileWeaponPartList.Clear();
				list2 = Combat.MissileWeaponPartList;
				Combat.MissileWeaponPartListInUse = true;
			}
			try
			{
				GameObject gameObject = Attacker.Target;
				foreach (GameObject gameObject2 in list)
				{
					MissileWeapon missileWeapon;
					if (gameObject2.TryGetPart<MissileWeapon>(out missileWeapon) && !list2.Contains(missileWeapon) && (!flag || AIWantUseWeaponEvent.Check(gameObject2, Attacker, gameObject)))
					{
						list2.Add(missileWeapon);
					}
				}
				if (SkipPastMaxRange && gameObject != null)
				{
					int num = Attacker.DistanceTo(gameObject);
					int i = 0;
					int num2 = list2.Count;
					while (i < num2)
					{
						if (num > list2[i].MaxRange)
						{
							list2.Remove(list2[i]);
							i = -1;
							num2--;
							if (num2 <= 0)
							{
								return false;
							}
						}
						i++;
					}
				}
				if (list2.Count > 1 && !CanFireAllMissileWeaponsEvent.Check(Attacker, list))
				{
					if (combat.LastFired != null)
					{
						list2.Remove(combat.LastFired);
					}
					MissileWeapon randomElement = list2.GetRandomElement<MissileWeapon>(null);
					list2.Clear();
					list2.Add(randomElement);
				}
				if (TargetCell == null && gameObject != null && !Attacker.IsPlayer())
				{
					TargetCell = gameObject.CurrentCell;
				}
				MissilePath missilePath = null;
				if (TargetCell == null)
				{
					if (Attacker.IsPlayer())
					{
						if (gameObject != null && !Attacker.IsConfused)
						{
							TargetCell = gameObject.CurrentCell;
						}
						else
						{
							TargetCell = Attacker.CurrentCell;
						}
						if (TargetCell == null)
						{
							return true;
						}
						bool flag2 = false;
						int num3 = 0;
						foreach (MissileWeapon missileWeapon2 in list2)
						{
							if (!flag2 && (missileWeapon2.Skill == "Rifle" || missileWeapon2.Skill == "Bow"))
							{
								flag2 = true;
							}
							if (missileWeapon2.MaxRange > num3)
							{
								num3 = missileWeapon2.MaxRange;
							}
						}
						GameObject gameObject3 = null;
						GameObject gameObject4 = null;
						string text = null;
						GetMissileWeaponProjectileEvent.GetFor(list[0], ref gameObject3, ref text);
						if (gameObject3 == null && !text.IsNullOrEmpty())
						{
							gameObject4 = GameObject.Create(text);
							if (gameObject4 != null)
							{
								MissileWeapon.SetupProjectile(gameObject4, Attacker, null, null);
								gameObject3 = gameObject4;
							}
						}
						try
						{
							missilePath = MissileWeapon.ShowPicker(TargetCell.X, TargetCell.Y, true, AllowVis.Any, num3, flag2, gameObject3 ?? Attacker, ref FireType, -1);
							if (missilePath == null)
							{
								return false;
							}
							TargetCell = missilePath.Path.Last<Cell>();
							if (FireType == FireType.Mark)
							{
								Rifle_DrawABead part2 = Attacker.GetPart<Rifle_DrawABead>();
								GameObject combatTarget = TargetCell.GetCombatTarget(Attacker, true, false, false, 5, gameObject3, null, null, null, null, true, false, null);
								part2.SetMark(combatTarget);
								Attacker.UseEnergy(1000, "Physical Skill", null, null, false);
								FireType = FireType.Normal;
								AutoAct.Setting = "ReopenMissileUI";
								return true;
							}
							goto IL_0457;
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("CommandFireMissileWeapon", ex, "game_exception");
							goto IL_0457;
						}
						finally
						{
							if (gameObject4 != null)
							{
								gameObject4.Obliterate(null, false, null);
							}
						}
					}
					return false;
				}
				if (Attacker.CurrentZone != TargetCell.ParentZone)
				{
					return false;
				}
				IL_0457:
				int num4 = 0;
				float num5 = 1f;
				if (EnergyMultiplier != null)
				{
					num5 = EnergyMultiplier.Value;
				}
				else if (list2.Count > 1)
				{
					num5 /= (float)list2.Count;
				}
				float num6 = 1f;
				if (list2.Count > 1)
				{
					num6 /= (float)list2.Count;
				}
				if (AimedAt == null)
				{
					AimedAt = TargetCell.GetCombatTarget(Attacker, true, false, false, 5, null, null, null, null, null, true, true, null);
				}
				if (AimedAt != null)
				{
					if (gameObject == null)
					{
						gameObject = AimedAt;
					}
					Event @event = Event.New("TargetedForMissileWeapon", 0, 0, 0);
					@event.SetParameter("Attacker", Attacker);
					@event.SetParameter("Defender", AimedAt);
					@event.SetParameter("MissileWeapons", list2);
					if (!AimedAt.FireEvent(@event))
					{
						return false;
					}
					if (AimedAt.HasEffect<RifleMark>((RifleMark fx) => fx.Marker == Attacker))
					{
						num4 += 2;
					}
				}
				if (!BeforeFireMissileWeaponsEvent.Check(Attacker, AimedAt, TargetCell, missilePath, list2))
				{
					return false;
				}
				bool flag3 = Sidebar.CurrentTarget == null;
				if (AimedAt != null && AimedAt != Attacker && Attacker.IsPlayer())
				{
					if (!flag3 && !AimedAt.IsCreature)
					{
						GameObject currentTarget = Sidebar.CurrentTarget;
						if (currentTarget != null && currentTarget.IsCreature)
						{
							goto IL_05D5;
						}
					}
					if (AimedAt.IsVisible())
					{
						if (!list2.Any<MissileWeapon>((MissileWeapon part) => part.ParentObject.HasTagOrProperty("NoMissileSetTarget")))
						{
							Sidebar.CurrentTarget = AimedAt;
							flag3 = false;
						}
					}
				}
				IL_05D5:
				Attacker.FireEvent("FiringMissile");
				foreach (MissileWeapon missileWeapon3 in list2)
				{
					Event event2 = Event.New("CommandFireMissile", 0, 0, 0);
					event2.SetParameter("EnergyMultiplier", num5);
					if (num6 != 1f)
					{
						event2.SetParameter("AnimationDelayMultiplier", num6);
					}
					event2.SetParameter("AimLevel", num4);
					event2.SetParameter("Owner", Attacker);
					event2.SetParameter("TargetCell", TargetCell);
					event2.SetParameter("AimedAt", AimedAt);
					event2.SetParameter("Path", missilePath);
					event2.SetParameter("FireType", FireType);
					event2.SetFlag("TargetUnset", flag3);
					if (SweepShots > 0)
					{
						event2.SetParameter("EnergyMultiplier", 0f);
						if (Rapid > 0)
						{
							event2.SetParameter("AnimationDelayMultiplier", num6 / (float)(SweepShots * Rapid));
						}
						else
						{
							event2.SetParameter("AnimationDelayMultiplier", num6 / (float)SweepShots);
						}
						int num7 = -SweepWidth / 2;
						int num8 = SweepWidth / SweepShots;
						if (50.in100())
						{
							num7 = -num7;
							num8 = -num8;
						}
						event2.SetParameter("SweepShots", SweepShots);
						event2.SetParameter("SweepWidth", SweepWidth);
						int j = 0;
						while (j < SweepShots)
						{
							if (Attacker != null)
							{
								CommandReloadEvent.Execute(Attacker, true, false, 0);
							}
							event2.SetParameter("FlatVariance", num7);
							event2.SetParameter("SweepShot", j + 1);
							if (Rapid > 0)
							{
								event2.SetParameter("RapidShots", Rapid);
								for (int k = 0; k < Rapid; k++)
								{
									event2.SetParameter("RapidShot", k + 1);
									missileWeapon3.ParentObject.FireEvent(event2);
								}
							}
							else
							{
								missileWeapon3.ParentObject.FireEvent(event2);
							}
							j++;
							num7 += num8;
						}
					}
					else if (Rapid > 0)
					{
						event2.SetParameter("EnergyMultiplier", 0f);
						event2.SetParameter("AnimationDelayMultiplier", num6 / (float)Rapid);
						event2.SetParameter("RapidShots", Rapid);
						for (int l = 0; l < Rapid; l++)
						{
							event2.SetParameter("RapidShot", l + 1);
							missileWeapon3.ParentObject.FireEvent(event2);
						}
					}
					else
					{
						event2.SetParameter("FlatVariance", 0);
						missileWeapon3.ParentObject.FireEvent(event2);
					}
					combat.LastFired = missileWeapon3;
					if (flag)
					{
						AIAfterMissileEvent.Send(missileWeapon3.ParentObject, Attacker, gameObject);
					}
					Attacker.FireEvent("FiredMissileWeapon");
				}
			}
			finally
			{
				if (list2 == Combat.MissileWeaponPartList)
				{
					Combat.MissileWeaponPartListInUse = false;
				}
			}
			return true;
		}

		// Token: 0x0600949A RID: 38042 RVA: 0x00359554 File Offset: 0x00357754
		public static int GetShieldBlocksPerTurn(GameObject Actor)
		{
			if (!Actor.HasSkill("Shield_SwiftBlocking"))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600949B RID: 38043 RVA: 0x00359566 File Offset: 0x00357766
		public int GetShieldBlocksPerTurn()
		{
			return Combat.GetShieldBlocksPerTurn(this.ParentObject);
		}

		// Token: 0x0600949C RID: 38044 RVA: 0x00359573 File Offset: 0x00357773
		public bool CanBlockWithShield(GameObject Shield)
		{
			return Combat.CanBlockWithShield(this.ParentObject, Shield);
		}

		// Token: 0x0600949D RID: 38045 RVA: 0x00359584 File Offset: 0x00357784
		public static bool CanBlockWithShield(GameObject Actor, GameObject Shield)
		{
			Shield shield;
			return Actor.HasEffect<ShieldWall>() || (Shield.TryGetPart<Shield>(out shield) && shield.Blocks < Combat.GetShieldBlocksPerTurn(Actor));
		}

		// Token: 0x0600949E RID: 38046 RVA: 0x003595B5 File Offset: 0x003577B5
		public void BlockedWithShield(GameObject ShieldObject, Shield Shield = null)
		{
			Combat.BlockedWithShield(this.ParentObject, ShieldObject, Shield);
		}

		// Token: 0x0600949F RID: 38047 RVA: 0x003595C4 File Offset: 0x003577C4
		public static void BlockedWithShield(GameObject Actor, GameObject ShieldObject, Shield Shield = null)
		{
			if (Shield == null)
			{
				Shield = ShieldObject.GetPart<Shield>();
			}
			if (Shield != null)
			{
				Shield.Blocks++;
			}
		}

		// Token: 0x060094A0 RID: 38048 RVA: 0x003595E4 File Offset: 0x003577E4
		public static bool PerformMeleeAttack(GameObject Attacker, GameObject Defender, int EnergyCost = 1000, int HitModifier = 0, int PenModifier = 0, int PenCapModifier = 0, string Properties = null, bool IgnoreFlight = false)
		{
			if (!IgnoreFlight && !Attacker.FlightMatches(Defender))
			{
				IComponent<GameObject>.EmitMessage(Attacker, Attacker.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " cannot reach " + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, false, false, null, null);
				return false;
			}
			if (!Attacker.PhaseMatches(Defender))
			{
				IComponent<GameObject>.EmitMessage(Attacker, Attacker.Poss("attack", true) + " passes through " + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", ' ', false, false, false, null, null);
				return false;
			}
			if (!Attacker.CanMoveExtremities("Attack", true, false, false))
			{
				return true;
			}
			if (Attacker.Body == null)
			{
				return false;
			}
			List<BodyPart> list = new List<BodyPart>();
			List<GameObject> list2 = Event.NewGameObjectList();
			int num;
			BodyPart bodyPart;
			GameObject mainWeapon = Attacker.Body.GetMainWeapon(out num, out bodyPart, Defender, true, true, list);
			GetMeleeAttacksEvent getMeleeAttacksEvent = GetMeleeAttacksEvent.HandleFrom(Attacker, null, 0, 1.0);
			GetMeleeAttackChanceEvent getMeleeAttackChanceEvent = PooledEvent<GetMeleeAttackChanceEvent>.FromPool();
			bool flag = GameObject.Validate(Defender);
			list.ShuffleInPlace<BodyPart>(null);
			int num2 = list.IndexOf(bodyPart);
			if (num2 > 0)
			{
				BodyPart bodyPart2 = list[0];
				list[0] = list[num2];
				list[num2] = bodyPart2;
			}
			int i = 0;
			int num3 = list.Count;
			while (i < num3)
			{
				GameObject firstValidWeapon = list[i].GetFirstValidWeapon(Defender);
				if (firstValidWeapon == null || list2.Contains(firstValidWeapon) || !firstValidWeapon.FireEvent("CanMeleeAttack"))
				{
					num3--;
					list.RemoveAt(i);
				}
				else
				{
					list2.Add(firstValidWeapon);
					i++;
				}
			}
			int num4 = 0;
			int count = list.Count;
			int num5 = getMeleeAttacksEvent.Attacks.Count;
			while (flag && num4 < count)
			{
				BodyPart bodyPart3 = list[num4];
				GameObject gameObject = list2[num4];
				bool flag2 = num4 == 0;
				getMeleeAttackChanceEvent.HandleFor(Attacker, gameObject, -1, 0, 1.0, Properties, bodyPart3, bodyPart, flag2, true, false);
				bool flag3 = false;
				int num6 = getMeleeAttackChanceEvent.GetFinalizedChance();
				while (num6 > 0 && num6.in100())
				{
					int num7 = 0;
					while (num7 < getMeleeAttackChanceEvent.Attempts && flag)
					{
						Combat.MeleeAttackWithWeapon(Attacker, Defender, gameObject, bodyPart3, Properties, HitModifier, PenModifier, PenCapModifier, 0, 0, flag2, true);
						flag = GameObject.Validate(Defender);
						num7++;
					}
					flag3 = true;
					num6 -= 100;
				}
				for (int j = 0; j < num5; j++)
				{
					MeleeAttack meleeAttack = getMeleeAttacksEvent.Attacks[j];
					if ((meleeAttack.Primary == null || meleeAttack.Primary.Value == flag2) && meleeAttack.IsValidFor(bodyPart3))
					{
						string text = Properties;
						if (!meleeAttack.Properties.IsNullOrEmpty())
						{
							text = (text.IsNullOrEmpty() ? meleeAttack.Properties : (text + "," + meleeAttack.Properties));
						}
						GetMeleeAttackChanceEvent getMeleeAttackChanceEvent2 = getMeleeAttackChanceEvent;
						GameObject gameObject2 = gameObject;
						int chance = meleeAttack.Chance;
						int num8 = 0;
						double num9 = 1.0;
						string text2 = text;
						BodyPart bodyPart4 = bodyPart3;
						bool flag4 = flag2;
						getMeleeAttackChanceEvent2.HandleFor(Attacker, gameObject2, chance, num8, num9, text2, bodyPart4, bodyPart, flag4, false, flag3);
						num6 = getMeleeAttackChanceEvent.GetFinalizedChance();
						while (num6 > 0 && num6.in100())
						{
							int num10 = 0;
							while (num10 < getMeleeAttackChanceEvent.Attempts && flag)
							{
								Combat.MeleeAttackWithWeapon(Attacker, Defender, gameObject, bodyPart3, text, meleeAttack.HitModifier + HitModifier, meleeAttack.PenModifier + PenModifier, meleeAttack.PenModifier + PenCapModifier, 0, 0, flag2, false);
								flag = GameObject.Validate(Defender);
								num10++;
							}
							flag3 = true;
							num6 -= 100;
						}
						getMeleeAttacksEvent.RemoveAttackAt(j--);
						num5--;
					}
				}
				num4++;
			}
			PooledEvent<GetMeleeAttacksEvent>.ResetTo(ref getMeleeAttacksEvent);
			PooledEvent<GetMeleeAttackChanceEvent>.ResetTo(ref getMeleeAttackChanceEvent);
			if (mainWeapon != null && mainWeapon.GetWeaponSkill() == "ShortBlades" && Attacker.HasSkill("ShortBlades_Expertise"))
			{
				EnergyCost = EnergyCost * 3 / 4;
			}
			Attacker.UseEnergy(EnergyCost, "Combat Melee", null, null, false);
			Event @event = Event.New("AIMessage", 0, 0, 0);
			@event.SetParameter("Message", "Attacked");
			@event.SetParameter("By", Attacker);
			Defender.FireEvent(@event);
			return true;
		}

		// Token: 0x060094A1 RID: 38049 RVA: 0x00359A4C File Offset: 0x00357C4C
		public static bool BeginAttack(GameObject Attacker, GameObject Defender, Cell AttackerCell, Cell DefenderCell)
		{
			if (!Defender.HasStat("Hitpoints"))
			{
				return false;
			}
			if (Defender.IsPlayerLed())
			{
				AttackerCell.ParentZone.MarkActive(DefenderCell.ParentZone);
			}
			if (Attacker.HasRegisteredEvent("BeginAttack"))
			{
				Event @event = Event.New("BeginAttack", 0, 0, 0);
				@event.SetParameter("TargetObject", Defender);
				@event.SetParameter("TargetCell", DefenderCell);
				if (!Attacker.FireEvent(@event))
				{
					return false;
				}
			}
			if (DefenderCell.HasObjectWithRegisteredEvent("ObjectAttacking"))
			{
				Event event2 = Event.New("ObjectAttacking", 0, 0, 0);
				event2.SetParameter("Object", Attacker);
				event2.SetParameter("TargetObject", Defender);
				event2.SetParameter("TargetCell", DefenderCell);
				if (!DefenderCell.FireEvent(event2))
				{
					return false;
				}
			}
			if (Attacker.IsPlayer() && IComponent<GameObject>.Visible(Defender))
			{
				Sidebar.CurrentTarget = Defender;
			}
			return true;
		}

		// Token: 0x060094A2 RID: 38050 RVA: 0x00359B28 File Offset: 0x00357D28
		public static bool AttackDirection(GameObject Attacker, string Direction)
		{
			if (Attacker.Physics == null)
			{
				return false;
			}
			if (!Attacker.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			Cell cellFromDirection = Attacker.CurrentCell.GetCellFromDirection(Direction, false);
			return cellFromDirection != null && Combat.AttackCell(Attacker, cellFromDirection, null, 0, 0, 0, false);
		}

		// Token: 0x060094A3 RID: 38051 RVA: 0x00359B6C File Offset: 0x00357D6C
		public static bool AttackObject(GameObject Attacker, GameObject Defender, string Properties = null, int HitModifier = 0, int PenModifier = 0, int PenCapModifier = 0, bool IgnoreFlight = false)
		{
			Cell currentCell = Attacker.CurrentCell;
			Cell cell = ((Defender != null) ? Defender.CurrentCell : null);
			return currentCell != null && cell != null && Combat.BeginAttack(Attacker, Defender, currentCell, cell) && Combat.PerformMeleeAttack(Attacker, Defender, 1000, HitModifier, PenModifier, PenCapModifier, Properties, IgnoreFlight);
		}

		// Token: 0x060094A4 RID: 38052 RVA: 0x00359BB8 File Offset: 0x00357DB8
		public static bool AttackCell(GameObject Attacker, Cell Cell, string Properties = null, int HitModifier = 0, int PenModifier = 0, int PenCapModifier = 0, bool IgnoreFlight = false)
		{
			Cell currentCell = Attacker.CurrentCell;
			GameObject combatTarget = Cell.GetCombatTarget(Attacker, IgnoreFlight, false, false, 5, null, null, null, null, null, true, false, null);
			return currentCell != null && combatTarget != null && Combat.BeginAttack(Attacker, combatTarget, currentCell, Cell) && Combat.PerformMeleeAttack(Attacker, combatTarget, 1000, HitModifier, PenModifier, PenCapModifier, Properties, IgnoreFlight);
		}

		// Token: 0x060094A5 RID: 38053 RVA: 0x00359C0C File Offset: 0x00357E0C
		private static MeleeAttackResult MeleeAttackWithWeaponInternal(GameObject Attacker, GameObject Defender, GameObject Weapon, BodyPart BodyPart, string Properties = null, int HitModifier = 0, int PenModifier = 0, int PenCapModifier = 0, int AdjustDamageResult = 0, int AdjustDamageDieSize = 0, bool Primary = false, bool Intrinsic = true)
		{
			MeleeAttackResult meleeAttackResult = default(MeleeAttackResult);
			int num = 0;
			int num2 = 0;
			string text = "1d1";
			string text2 = "Strength";
			Damage damage = new Damage(0);
			damage.AddAttribute("Melee");
			MeleeWeapon meleeWeapon;
			if (Weapon == null || !Weapon.TryGetPart<MeleeWeapon>(out meleeWeapon))
			{
				return meleeAttackResult;
			}
			text = meleeWeapon.BaseDamage;
			num2 = meleeWeapon.MaxStrengthBonus;
			text2 = meleeWeapon.Stat;
			if (text2.Contains(","))
			{
				int num3 = int.MinValue;
				foreach (string text3 in text2.CachedCommaExpansion())
				{
					int num4 = Attacker.Stat(text3, 0);
					if (num4 > num3)
					{
						text2 = text3;
						num3 = num4;
					}
				}
			}
			if (meleeWeapon.HasTag("WeaponUnarmed"))
			{
				damage.AddAttribute("Unarmed");
			}
			if (!meleeWeapon.Attributes.IsNullOrEmpty())
			{
				damage.AddAttributes(meleeWeapon.Attributes);
			}
			if (AdjustDamageResult != 0)
			{
				text = DieRoll.AdjustResult(text, AdjustDamageResult);
			}
			if (AdjustDamageDieSize != 0)
			{
				text = DieRoll.AdjustDieSize(text, AdjustDamageDieSize);
			}
			bool flag = false;
			damage.AddAttribute(text2);
			if (Statistic.IsMental(text2))
			{
				damage.AddAttribute("Mental");
				flag = true;
			}
			string text4 = ((meleeWeapon != null) ? meleeWeapon.Skill : null) ?? "Unarmed";
			Combat.PlaySwingSound(Attacker, Weapon, text4);
			BeforeMeleeAttackEvent.Send(Attacker, Defender, Weapon, text4, text2);
			int num5 = Stat.Random(1, 20);
			string text5 = text4;
			string text6 = text2;
			int @for = GetToHitModifierEvent.GetFor(Attacker, Defender, Weapon, HitModifier, null, null, text5, text6, false, false, false);
			int num6 = num5 + @for;
			Event @event = Event.New("GetDefenderDV", 0, 0, 0);
			@event.SetParameter("Weapon", Weapon);
			@event.SetParameter("Damage", damage);
			@event.SetParameter("Attacker", Attacker);
			@event.SetParameter("Defender", Defender);
			@event.SetParameter("NaturalHitResult", num5);
			@event.SetParameter("Result", num6);
			@event.SetParameter("Skill", text4);
			@event.SetParameter("Stat", text2);
			@event.SetParameter("DV", Stats.GetCombatDV(Defender));
			Defender.FireEvent(@event);
			@event.ID = "WeaponGetDefenderDV";
			if (Weapon != null)
			{
				Weapon.FireEvent(@event);
			}
			@event.ID = "AttackerGetDefenderDV";
			Attacker.FireEvent(@event);
			num6 = @event.GetIntParameter("Result", 0);
			num5 = @event.GetIntParameter("NaturalHitResult", 0);
			bool flag2 = Properties.Contains("Critical");
			if (!flag2)
			{
				int num7 = GetCriticalThresholdEvent.GetFor(Attacker, Defender, Weapon, null, text4, 20);
				int for2 = GetSpecialEffectChanceEvent.GetFor(Attacker, Weapon, "Melee Critical", 5, Defender, null, true, false);
				if (for2 != 5)
				{
					num7 -= (for2 - 5) / 5;
				}
				if (num5 >= num7)
				{
					flag2 = true;
				}
			}
			bool flag3 = true;
			bool flag4 = flag2 || num6 > @event.GetIntParameter("DV", 0) || Properties.Contains("Autohit");
			if (flag4)
			{
				@event.ID = "DefenderBeforeHit";
				if (!Defender.FireEvent(@event))
				{
					flag4 = false;
				}
			}
			if (@event.HasIntParameter("NoMissMessage"))
			{
				flag3 = false;
			}
			if (!flag4)
			{
				if (CombatJuice.enabled)
				{
					CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceMissAnimationPrefab", null, null, null, false);
					CombatJuice.playWorldSound(Attacker, ((Weapon != null) ? Weapon.GetTagOrStringProperty("MissSound", null) : null) ?? "sfx_melee_generic_miss", 0.5f, 0f, 0f, 0f);
					CombatJuice.punch(Attacker, Defender, 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f, 1f, "PunchSound", null, false, false);
				}
				else
				{
					Defender.ParticleBlip("&K\t", 10, 0L, false);
				}
				if (Attacker.IsPlayer())
				{
					if (flag3)
					{
						if (IComponent<GameObject>.TerseMessages)
						{
							IComponent<GameObject>.AddPlayerMessage("You miss!", 'r', true);
						}
						else if (Weapon != null)
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"{{r|You miss with ",
								Attacker.its_(Weapon, false),
								"!}} [",
								num6.ToString(),
								" vs ",
								@event.GetIntParameter("DV", 0).ToString(),
								"]"
							}), null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"{{r|You miss!}} [",
								num6.ToString(),
								" vs ",
								@event.GetIntParameter("DV", 0).ToString(),
								"]"
							}), null, true);
						}
					}
				}
				else if (Defender.IsPlayer())
				{
					if (AutoAct.IsInterruptable() && Defender.IsRelevantHostile(Attacker))
					{
						AutoAct.Interrupt("you are under attack by " + Attacker.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false), null, Attacker, true);
					}
					if (flag3)
					{
						if (IComponent<GameObject>.TerseMessages)
						{
							IComponent<GameObject>.AddPlayerMessage(Attacker.Does("miss", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you!", null, true);
						}
						else if (Weapon != null)
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								Attacker.Does("miss", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" you with ",
								Attacker.its_(Weapon, false),
								"! [",
								num6.ToString(),
								" vs ",
								@event.GetIntParameter("DV", 0).ToString(),
								"]"
							}), null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								Attacker.Does("miss", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" you! [",
								num6.ToString(),
								" vs ",
								@event.GetIntParameter("DV", 0).ToString(),
								"]"
							}), null, true);
						}
					}
				}
				else if (AutoAct.IsInterruptable())
				{
					if (Attacker.IsPlayerLedAndPerceptible() && !Attacker.IsTrifling && (!IComponent<GameObject>.Visible(Defender) || The.Player.IsRelevantHostile(Defender)))
					{
						AutoAct.Interrupt(string.Concat(new string[]
						{
							"you ",
							Attacker.GetPerceptionVerb(),
							" ",
							Attacker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
							" fighting",
							Attacker.IsVisible() ? "" : (" " + The.Player.DescribeDirectionToward(Attacker, false, false))
						}), null, Attacker, true);
					}
					else if (Defender.IsPlayerLedAndPerceptible() && !Defender.IsTrifling && (!IComponent<GameObject>.Visible(Attacker) || The.Player.IsRelevantHostile(Attacker)))
					{
						AutoAct.Interrupt(string.Concat(new string[]
						{
							"you ",
							Defender.GetPerceptionVerb(),
							" ",
							Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
							" fighting",
							Defender.IsVisible() ? "" : (" " + The.Player.DescribeDirectionToward(Defender, false, false))
						}), null, Defender, true);
					}
				}
				Event event2 = Event.New("AttackerMeleeMiss", 0, 0, 0);
				event2.SetParameter("Weapon", Weapon);
				event2.SetParameter("Attacker", Attacker);
				event2.SetParameter("Defender", Defender);
				Attacker.FireEvent(event2);
				Event event3 = Event.New("DefenderAfterAttackMissed", 0, 0, 0);
				event3.SetParameter("Attacker", Attacker);
				event3.SetParameter("Defender", Defender);
				event3.SetParameter("Weapon", Weapon);
				Defender.FireEvent(event3);
				if (Weapon != null)
				{
					event3.ID = "WeaponAfterAttackMissed";
					Weapon.FireEvent(event3);
				}
			}
			else
			{
				Defender.FireEvent("DefenderAttackHit");
				if (!Defender.HasStat("AV"))
				{
					return meleeAttackResult;
				}
				DefendMeleeHitEvent.Send(Attacker, Defender, Weapon, damage, num6);
				int num8 = (flag ? Stats.GetCombatMA(Defender) : Stats.GetCombatAV(Defender));
				int num9 = 0;
				bool flag5 = false;
				GetAttackerHitDiceEvent.Process(ref num9, ref num8, ref flag5, Attacker, Defender, Weapon);
				GetWeaponHitDiceEvent.Process(ref num9, ref num8, ref flag5, Attacker, Defender, Weapon);
				GetDefenderHitDiceEvent.Process(ref num9, ref num8, ref flag5, Attacker, Defender, Weapon);
				int num10 = ((meleeWeapon != null && meleeWeapon.HasTag("WeaponIgnoreStrength")) ? 0 : Attacker.StatMod(text2, 0));
				int num11 = PenModifier;
				int num12 = PenCapModifier;
				if (meleeWeapon != null)
				{
					num11 += meleeWeapon.PenBonus;
					num12 += meleeWeapon.PenBonus;
				}
				string text7 = (Primary ? "Primary" : "Secondary");
				GetWeaponMeleePenetrationEvent.Process(ref num, ref num10, ref num2, ref num11, ref num12, num8, flag2, Properties, text7, Attacker, Defender, Weapon);
				GetAttackerMeleePenetrationEvent.Process(ref num, ref num10, ref num2, ref num11, ref num12, num8, flag2, Properties, text7, Attacker, Defender, Weapon);
				GetDefenderMeleePenetrationEvent.Process(ref num, ref num10, ref num2, ref num11, ref num12, num8, flag2, Properties, text7, Attacker, Defender, Weapon);
				BaseSkill baseSkill = null;
				bool flag6 = false;
				int num13 = 0;
				int num14 = 0;
				if (flag2)
				{
					Attacker.PlayWorldSound("Sounds/Damage/sfx_damage_critical", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
					num13 = 1;
					num14 = 1;
					flag6 = true;
					if (baseSkill == null)
					{
						baseSkill = Skills.GetGenericSkill(text4, Attacker);
					}
					if (baseSkill != null)
					{
						int weaponCriticalModifier = baseSkill.GetWeaponCriticalModifier(Attacker, Defender, Weapon);
						if (weaponCriticalModifier != 0)
						{
							num13 += weaponCriticalModifier;
							num14 += weaponCriticalModifier;
						}
					}
					Event event4 = Event.New("WeaponCriticalModifier", 0, 0, 0);
					event4.SetParameter("Attacker", Attacker);
					event4.SetParameter("Defender", Defender);
					event4.SetParameter("Weapon", Weapon);
					event4.SetParameter("Skill", text4);
					event4.SetParameter("Stat", text2);
					event4.SetParameter("PenBonus", num13);
					event4.SetParameter("CapBonus", num14);
					event4.SetFlag("AutoPen", flag6);
					if (Weapon != null)
					{
						Weapon.FireEvent(event4);
					}
					event4.ID = "AttackerCriticalModifier";
					Attacker.FireEvent(event4);
					num13 = event4.GetIntParameter("PenBonus", 0);
					num14 = event4.GetIntParameter("CapBonus", 0);
					flag6 = event4.HasFlag("AutoPen");
				}
				num += Stat.RollDamagePenetrations(num8, num10 + num11 + num9 + num13, num2 + num12 + num9 + num14);
				meleeAttackResult.Penetrations = num;
				bool flag7 = false;
				Event event5 = Event.New("AttackerHit", 5, 0, 2);
				event5.SetParameter("Penetrations", num);
				event5.SetParameter("Damage", damage);
				event5.SetParameter("Attacker", Attacker);
				event5.SetParameter("Defender", Defender);
				event5.SetParameter("Weapon", Weapon);
				event5.SetParameter("Properties", Properties);
				event5.SetFlag("Critical", flag2);
				if (!Attacker.FireEvent(event5))
				{
					return meleeAttackResult;
				}
				if (event5.HasFlag("DidSpecialEffect"))
				{
					flag7 = true;
				}
				Properties = event5.GetStringParameter("Properties", "") ?? Properties;
				Event event6 = Event.New("DefenderHit", 4, 0, 2);
				event6.SetParameter("Penetrations", event5.GetIntParameter("Penetrations", 0));
				event6.SetParameter("Damage", damage);
				event6.SetParameter("Attacker", Attacker);
				event6.SetParameter("Defender", Defender);
				event6.SetParameter("Weapon", Weapon);
				event6.SetFlag("Critical", flag2);
				if (!Defender.FireEvent(event6))
				{
					return meleeAttackResult;
				}
				if (event6.HasFlag("DidSpecialEffect"))
				{
					flag7 = true;
				}
				Event event7 = Event.New("WeaponHit", 4, 0, 2);
				event7.SetParameter("Penetrations", event6.GetIntParameter("Penetrations", 0));
				event7.SetParameter("Damage", damage);
				event7.SetParameter("Attacker", Attacker);
				event7.SetParameter("Defender", Defender);
				event7.SetParameter("Weapon", Weapon);
				event7.SetFlag("Critical", flag2);
				event7.SetParameter("Properties", Properties);
				if (Weapon != null && !Weapon.FireEvent(event7))
				{
					return meleeAttackResult;
				}
				if (event7.HasFlag("DidSpecialEffect"))
				{
					flag7 = true;
				}
				Properties = event7.GetStringParameter("Properties", "") ?? Properties;
				bool flag8 = Defender.HasTag("Creature");
				string blueprint = Defender.Blueprint;
				WeaponUsageTracking.TrackMeleeWeaponHit(Attacker, Weapon, flag8, blueprint);
				if (Attacker.HasRegisteredEvent("WieldedWeaponHit"))
				{
					Event event8 = Event.New("WieldedWeaponHit", 4, 0, 2);
					event8.SetParameter("Penetrations", event7.GetIntParameter("Penetrations", 0));
					event8.SetParameter("Damage", damage);
					event8.SetParameter("Attacker", Attacker);
					event8.SetParameter("Defender", Defender);
					event8.SetParameter("Weapon", Weapon);
					event8.SetFlag("Critical", flag2);
					if (!Attacker.FireEvent(event8))
					{
						return meleeAttackResult;
					}
					if (event8.HasFlag("DidSpecialEffect"))
					{
						flag7 = true;
					}
				}
				if (!event7.HasParameter("Penetrations"))
				{
					return meleeAttackResult;
				}
				num = event7.GetIntParameter("Penetrations", 0);
				if (num <= 0 && Properties.Contains("Autopen"))
				{
					num = 1;
				}
				else if (num > 1 && Properties.Contains("MaxPens1"))
				{
					num = 1;
				}
				else if (num <= 0 && flag6 && Attacker != null && Attacker.IsPlayer())
				{
					num = 1;
				}
				meleeAttackResult.Penetrations = num;
				Cell currentCell = Defender.CurrentCell;
				if (num > 0)
				{
					if (num < 2)
					{
						if (Attacker != null)
						{
							Attacker.PlayWorldSound("Sounds/Damage/sfx_damage_penetrate_low", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (num < 4)
					{
						if (Attacker != null)
						{
							Attacker.PlayWorldSound("Sounds/Damage/sfx_damage_penetrate_med", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (Attacker != null)
					{
						Attacker.PlayWorldSound("Sounds/Damage/sfx_damage_penetrate_high", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					damage.AddAttribute(text4);
					if (Weapon != null && flag2)
					{
						damage.AddAttribute("Critical");
						Event event9 = Event.New("CriticalHit", 0, 0, 0);
						event9.SetParameter("Attacker", Attacker);
						event9.SetParameter("Defender", Defender);
						event9.SetParameter("BaseDamage", text);
						event9.SetParameter("Weapon", Weapon);
						event9.SetParameter("Skill", text4);
						event9.SetParameter("Stat", text2);
						event9.ID = "AttackerCriticalHit";
						Attacker.FireEvent(event9);
						event9.ID = "WeaponCriticalHit";
						if (Weapon != null)
						{
							Weapon.FireEvent(event9);
						}
						else
						{
							Attacker.FireEvent(event9);
						}
						event9.ID = "DefenderCriticalHit";
						Defender.FireEvent(event9);
						text = event9.GetStringParameter("BaseDamage", null);
						if (event9.HasFlag("DidSpecialEffect"))
						{
							flag7 = true;
						}
					}
					int num15 = 0;
					if (damage.HasAttribute("Mental") && Defender.Brain == null)
					{
						if (Attacker.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("Your mental attack does not affect " + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
						}
						flag7 = true;
					}
					else
					{
						DieRoll cachedDieRoll = text.GetCachedDieRoll();
						for (int i = 0; i < num; i++)
						{
							num15 += cachedDieRoll.Resolve();
						}
					}
					if (num15 > 0 || flag7)
					{
						if (flag2)
						{
							Attacker.ParticleText("*critical hit*", IComponent<GameObject>.ConsequentialColorChar(Attacker, null), false, 1.5f, -8f);
						}
						string resultColor = Stat.GetResultColor(num);
						if (!Options.ShowMonsterHPHearts)
						{
							Defender.ParticleBlip(resultColor + "\u0003", 10, 0L, false);
						}
						damage.Amount += num15;
						Event event10 = Event.New("DealDamage", 0, 0, 0);
						event10.SetParameter("Penetrations", num);
						event10.SetParameter("Damage", damage);
						event10.SetParameter("Attacker", Attacker);
						event10.SetParameter("Defender", Defender);
						event10.SetParameter("Weapon", Weapon);
						event10.SetParameter("Properties", Properties);
						event10.SetParameter("Cell", currentCell);
						event10.SetFlag("Critical", flag2);
						if (event10.HasFlag("DidSpecialEffect"))
						{
							flag7 = true;
						}
						Event event11 = Event.New("WeaponDealDamage", 0, 0, 0);
						event11.SetParameter("Penetrations", num);
						event11.SetParameter("Damage", damage);
						event11.SetParameter("Attacker", Attacker);
						event11.SetParameter("Defender", Defender);
						event11.SetParameter("Weapon", Weapon);
						event11.SetParameter("Properties", Properties);
						event11.SetParameter("Cell", currentCell);
						event11.SetFlag("Critical", flag2);
						if (event11.HasFlag("DidSpecialEffect"))
						{
							flag7 = true;
						}
						Event event12 = Event.New("TakeDamage", 0, 0, 0);
						event12.SetParameter("Penetrations", num);
						event12.SetParameter("Damage", damage);
						event12.SetParameter("Owner", Attacker);
						event12.SetParameter("Attacker", Attacker);
						event12.SetParameter("Defender", Defender);
						event12.SetParameter("Weapon", Weapon);
						event12.SetParameter("Properties", Properties);
						event12.SetParameter("Message", "");
						event12.SetParameter("Cell", currentCell);
						event12.SetFlag("NoDamageMessage", true);
						event12.SetFlag("Critical", flag2);
						if (CombatJuice.enabled)
						{
							if (damage.IsBludgeoningDamage())
							{
								if (num <= 1)
								{
									CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceBashAnimationPrefabLow", null, null, null, false);
								}
								else if (num <= 3)
								{
									CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceBashAnimationPrefabMedium", null, null, null, false);
								}
								else
								{
									CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceBashAnimationPrefabHigh", null, null, null, false);
								}
							}
							else if (num <= 1)
							{
								CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceSlashAnimationPrefabLow", null, null, null, false);
							}
							else if (num <= 3)
							{
								CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceSlashAnimationPrefabMedium", null, null, null, false);
							}
							else
							{
								CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceSlashAnimationPrefabHigh", null, null, null, false);
							}
							if (Weapon.HasTagOrStringProperty("HitSound"))
							{
								CombatJuice.playWorldSound(Attacker, Weapon.GetTagOrStringProperty("HitSound", null), 0.5f, 0f, 0f, Weapon.HasIntProperty("HitSoundDelay") ? ((float)Weapon.GetIntProperty("HitSoundDelay", 0) / 1000f) : 0.135f);
							}
							CombatJuice.punch(Attacker, Defender, 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f, 1f, "PunchSound", null, false, false);
						}
						bool flag9 = false;
						if (Attacker.FireEvent(event10) || flag7)
						{
							WeaponUsageTracking.TrackMeleeWeaponDamage(Attacker, Weapon, flag8, blueprint, damage);
							if (Weapon == null || Weapon.FireEvent(event11))
							{
								StringBuilder stringBuilder = Event.NewStringBuilder(null);
								if (Attacker.IsPlayer())
								{
									stringBuilder.Append("{{g|You");
									if (flag2)
									{
										stringBuilder.Append(" critically");
									}
									stringBuilder.Append(" hit");
									if (!IComponent<GameObject>.TerseMessages)
									{
										stringBuilder.Append(" {{").Append(resultColor).Append("|(x")
											.Append(num)
											.Append(")}}");
									}
									if (damage.Amount > 0)
									{
										stringBuilder.Append(" for ").Append(damage.Amount).Append(" damage");
									}
									if (!IComponent<GameObject>.TerseMessages)
									{
										stringBuilder.Append(" with ");
										if (Weapon == null)
										{
											stringBuilder.Append("your bare hands");
										}
										else
										{
											Attacker.its_(Weapon, stringBuilder, false);
										}
										stringBuilder.Append("! [").Append(num6).Append(']');
									}
									stringBuilder.Append("}}");
								}
								else if (Defender.IsPlayer())
								{
									stringBuilder.Append("%T");
									if (flag2)
									{
										stringBuilder.Append(" critically");
									}
									stringBuilder.Append(Attacker.GetVerb("hit", true, false, false, true, false));
									if (!IComponent<GameObject>.TerseMessages)
									{
										stringBuilder.Append(" {{").Append(resultColor).Append("|(x")
											.Append(num)
											.Append(")}}");
									}
									if (damage.Amount > 0)
									{
										stringBuilder.Append(" for ").Append(damage.Amount).Append(" damage");
									}
									if (!IComponent<GameObject>.TerseMessages)
									{
										if (Weapon == null)
										{
											stringBuilder.Append(" barehanded");
										}
										else
										{
											stringBuilder.Append(" with ");
											Attacker.its_(Weapon, stringBuilder, false);
										}
										stringBuilder.Append('.');
										stringBuilder.Append(" [").Append(num6).Append(']');
									}
								}
								event12.SetParameter("Message", stringBuilder.ToString());
								if (flag2 && Defender.GetIntProperty("Bleeds", 0) > 0)
								{
									Defender.Bloodsplatter();
								}
								if (Defender.FireEvent(event12))
								{
									flag9 = true;
									if (event12.HasFlag("DidSpecialEffect"))
									{
										flag7 = true;
									}
									if (Defender.IsValid())
									{
										if (flag2)
										{
											if (baseSkill == null)
											{
												baseSkill = Skills.GetGenericSkill(text4, Attacker);
											}
											if (baseSkill != null)
											{
												baseSkill.WeaponMadeCriticalHit(Attacker, Defender, Weapon, Properties);
											}
											Event event13 = Event.New("AfterCriticalHit", 0, 0, 0);
											event13.SetParameter("Attacker", Attacker);
											event13.SetParameter("Defender", Attacker);
											event13.SetParameter("Weapon", Weapon);
											event13.SetParameter("Skill", text4);
											event13.SetParameter("Stat", text2);
											event13.SetParameter("Properties", Properties);
											event13.SetParameter("Cell", currentCell);
											event13.ID = "AttackerAfterCriticalHit";
											Attacker.FireEvent(event13);
											event13.ID = "WeaponAfterCriticalHit";
											if (Weapon != null)
											{
												Weapon.FireEvent(event13);
											}
											else
											{
												Attacker.FireEvent(event13);
											}
											event13.ID = "DefenderAfterCriticalHit";
											Defender.FireEvent(event13);
											if (event13.HasFlag("DidSpecialEffect"))
											{
												flag7 = true;
											}
										}
										Event event14 = Event.New("AttackerAfterDamage", 0, 0, 0);
										event14.SetParameter("Penetrations", num);
										event14.SetParameter("Damage", damage);
										event14.SetParameter("Attacker", Attacker);
										event14.SetParameter("Defender", Defender);
										event14.SetParameter("Weapon", Weapon);
										event14.SetParameter("Message", "");
										event14.SetParameter("Properties", Properties);
										event14.SetParameter("Cell", currentCell);
										event14.SetFlag("Critical", flag2);
										Attacker.FireEvent(event14);
										event14.ID = "WeaponAfterDamage";
										if (Weapon != null)
										{
											Weapon.FireEvent(event14);
										}
										if (event14.HasFlag("DidSpecialEffect"))
										{
											flag7 = true;
										}
									}
								}
								if (event12.HasFlag("DidSpecialEffect"))
								{
									flag7 = true;
								}
							}
						}
						if (!flag9 && !flag7)
						{
							if (!damage.SuppressionMessageDone && Attacker.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage("You fail to deal damage with your attack! [" + num6.ToString() + "]", 'r', true);
							}
							if (Defender.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
								{
									Attacker.Does("fail", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
									" to deal damage with ",
									Attacker.its,
									" attack! [",
									num6.ToString(),
									"]"
								}), null, true);
							}
						}
						if (Options.ShowMonsterHPHearts)
						{
							Defender.ParticleBlip(Defender.GetHPColor() + "\u0003", 10, 0L, false);
						}
					}
				}
				else
				{
					if (CombatJuice.enabled)
					{
						CombatJuice.playPrefabAnimation(Defender, "CombatJuice/CombatJuiceBlockAnimationPrefab", null, null, null, false);
						CombatJuice.punch(Attacker, Defender, 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f, 1f, "PunchSound", null, false, false);
					}
					if (Attacker.IsPlayer())
					{
						if (IComponent<GameObject>.TerseMessages)
						{
							IComponent<GameObject>.AddPlayerMessage("You don't penetrate " + Defender.poss("armor", true) + ".", 'r', true);
						}
						else if (Weapon != null)
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"You don't penetrate ",
								Defender.poss("armor", true),
								" with ",
								Attacker.its_(Weapon, false),
								". {{y|[",
								num6.ToString(),
								"]}}"
							}), 'r', true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"You don't penetrate ",
								Defender.poss("armor", true),
								". {{y|[",
								num6.ToString(),
								"]}}"
							}), 'r', true);
						}
					}
					else if (Defender.IsPlayer())
					{
						if (IComponent<GameObject>.TerseMessages)
						{
							IComponent<GameObject>.AddPlayerMessage(Attacker.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " penetrate your armor.", null, true);
						}
						else if (Weapon != null)
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								Attacker.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" penetrate your armor with ",
								Attacker.its_(Weapon, false),
								"! [",
								num6.ToString(),
								"]"
							}), null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(Attacker.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " penetrate your armor! [" + num6.ToString() + "]", null, true);
						}
					}
					if (flag5)
					{
						if (!CombatJuice.enabled)
						{
							Defender.ParticleBlip("&G\a", 10, 0L, false);
						}
						Combat.PlayBlockedSound(Attacker, Weapon, text4);
					}
					else
					{
						if (!CombatJuice.enabled)
						{
							Defender.ParticleBlip("&K\a", 10, 0L, false);
						}
						Combat.PlayBlockedSound(Attacker, Weapon, text4);
					}
				}
				Event event15 = Event.New("AttackerAfterAttack", 0, 0, 0);
				event15.SetParameter("Penetrations", num);
				event15.SetParameter("Damage", damage);
				event15.SetParameter("Attacker", Attacker);
				event15.SetParameter("Defender", Defender);
				event15.SetParameter("Weapon", Weapon);
				event15.SetParameter("Skill", text4);
				event15.SetParameter("Stat", text2);
				event15.SetParameter("Properties", Properties);
				event15.SetParameter("Cell", currentCell);
				event15.SetFlag("Critical", flag2);
				Attacker.FireEvent(event15);
				event15.ID = "DefenderAfterAttack";
				Defender.FireEvent(event15);
				if (Weapon != null)
				{
					event15.ID = "WeaponAfterAttack";
					Weapon.FireEvent(event15);
				}
			}
			meleeAttackResult.Attacks++;
			if (flag4)
			{
				meleeAttackResult.Hits++;
			}
			if (flag2)
			{
				meleeAttackResult.Criticals++;
			}
			meleeAttackResult.Damage += damage.Amount;
			meleeAttackResult.Penetrations += num;
			return meleeAttackResult;
		}

		/// <summary>Perform a single weapon's attack.</summary>
		/// <returns>true if parameters valid and defender survived; otherwise false</returns>
		// Token: 0x060094A6 RID: 38054 RVA: 0x0035B870 File Offset: 0x00359A70
		public static MeleeAttackResult MeleeAttackWithWeapon(GameObject Attacker, GameObject Defender, GameObject Weapon, BodyPart BodyPart, string Properties = null, int HitModifier = 0, int PenModifier = 0, int PenCapModifier = 0, int AdjustDamageResult = 0, int AdjustDamageDieSize = 0, bool Primary = false, bool Intrinsic = true)
		{
			MeleeAttackResult meleeAttackResult = default(MeleeAttackResult);
			if (Weapon == null)
			{
				return meleeAttackResult;
			}
			GetWeaponMeleeAttacksEvent getWeaponMeleeAttacksEvent = GetWeaponMeleeAttacksEvent.HandleFrom(Attacker, Weapon, Primary, Intrinsic);
			int num = -1;
			while (GameObject.Validate(Defender))
			{
				if (!GameObject.Validate(Weapon) || (Weapon.Equipped != Attacker && !Attacker.IsADefaultBehavior(Weapon)))
				{
					return meleeAttackResult;
				}
				string text = Properties ?? "";
				if (num < 0)
				{
					goto IL_00A9;
				}
				MeleeAttack meleeAttack = getWeaponMeleeAttacksEvent.Attacks[num];
				if (meleeAttack.IsValidFor(BodyPart) && meleeAttack.Chance.in100())
				{
					if (!meleeAttack.Properties.IsNullOrEmpty())
					{
						text = (text.IsNullOrEmpty() ? meleeAttack.Properties : (text + "," + meleeAttack.Properties));
						goto IL_00A9;
					}
					goto IL_00A9;
				}
				IL_00C6:
				if (++num >= getWeaponMeleeAttacksEvent.Attacks.Count)
				{
					return meleeAttackResult;
				}
				continue;
				IL_00A9:
				meleeAttackResult += Combat.MeleeAttackWithWeaponInternal(Attacker, Defender, Weapon, BodyPart, text, HitModifier, PenModifier, PenCapModifier, AdjustDamageResult, AdjustDamageDieSize, false, true);
				goto IL_00C6;
			}
			return meleeAttackResult;
		}

		// Token: 0x060094A7 RID: 38055 RVA: 0x0035B95C File Offset: 0x00359B5C
		public static bool SwoopAttack(GameObject Attacker, string Direction = null)
		{
			if (Direction == null)
			{
				if (Attacker.IsPlayer())
				{
					Direction = Attacker.Physics.PickDirectionS("Swoop", null);
				}
				if (Direction == null)
				{
					return false;
				}
			}
			Attacker.PlayWorldSound("Sounds/Abilities/sfx_ability_swoop", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			Attacker.Physics.DidX("swoop", "down to attack", null, null, null, Attacker, null, false, false, null, null, false, false, false, false, false, null);
			Flight.SuspendFlight(Attacker);
			Combat.TrackShieldBlock = 1;
			bool flag = false;
			try
			{
				bool flag2 = Combat.AttackDirection(Attacker, Direction);
				bool flag3 = Combat.TrackShieldBlock > 1;
				Flight.DesuspendFlight(Attacker);
				Combat.TrackShieldBlock = 0;
				flag = true;
				if (!flag2)
				{
					return false;
				}
				int num = Flight.GetSwoopFallChance(Attacker, null);
				if (flag3)
				{
					num *= 2;
				}
				if (num.in100())
				{
					Flight.Fall(Attacker);
				}
				else
				{
					Attacker.UseEnergy(1000, "Swoop Return", null, null, false);
				}
			}
			finally
			{
				if (!flag)
				{
					Flight.DesuspendFlight(Attacker);
					Combat.TrackShieldBlock = 0;
				}
			}
			return true;
		}

		// Token: 0x060094A8 RID: 38056 RVA: 0x0035BA70 File Offset: 0x00359C70
		private static void PlaySwingSound(GameObject Attacker, GameObject Weapon, string Skill)
		{
			string text = ((Weapon != null) ? Weapon.GetTagOrStringProperty("SwingSound", null) : null);
			if (text.IsNullOrEmpty() && !Skill.IsNullOrEmpty())
			{
				bool flag = Weapon != null && Weapon.IsNatural();
				if (!(Skill == "LongBlades"))
				{
					if (!(Skill == "Axe"))
					{
						if (!(Skill == "ShortBlades"))
						{
							if (!(Skill == "Unarmed"))
							{
								text = (flag ? "sfx_melee_naturalWeapon_cudgel_swing" : "sfx_melee_cudgel_wood_swing");
							}
							else
							{
								text = "sfx_melee_naturalWeapon_fist_swing";
							}
						}
						else
						{
							text = (flag ? "sfx_melee_naturalWeapon_shortblade_swing" : "sfx_melee_shortSword_metal_swing");
						}
					}
					else
					{
						text = (flag ? "sfx_melee_naturalWeapon_axe_swing" : "sfx_melee_axe_oneHanded_metal_swing");
					}
				}
				else
				{
					text = (flag ? "sfx_melee_naturalWeapon_longblade_swing" : "sfx_melee_longBlade_metal_swing");
				}
			}
			Attacker.PlayWorldSound(text.Coalesce("sfx_melee_cudgel_wood_swing"), 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x060094A9 RID: 38057 RVA: 0x0035BB68 File Offset: 0x00359D68
		private static void PlayBlockedSound(GameObject Attacker, GameObject Weapon, string Skill)
		{
			string text = ((Weapon != null) ? Weapon.GetTagOrStringProperty("BlockedSound", null) : null);
			if (text.IsNullOrEmpty() && !Skill.IsNullOrEmpty())
			{
				bool flag = Weapon != null && Weapon.IsNatural();
				if (!(Skill == "LongBlades"))
				{
					if (!(Skill == "Axe"))
					{
						if (!(Skill == "ShortBlades"))
						{
							if (!(Skill == "Unarmed"))
							{
								text = (flag ? "sfx_melee_naturalWeapon_cudgel_blocked" : "sfx_melee_cudgel_wood_blocked");
							}
							else
							{
								text = "sfx_melee_naturalWeapon_fist_blocked";
							}
						}
						else
						{
							text = (flag ? "sfx_melee_naturalWeapon_shortblade_blocked" : "sfx_melee_metal_blocked");
						}
					}
					else
					{
						text = (flag ? "sfx_melee_naturalWeapon_axe_blocked" : "sfx_melee_metal_blocked");
					}
				}
				else
				{
					text = (flag ? "sfx_melee_naturalWeapon_longblade_blocked" : "sfx_melee_metal_blocked");
				}
			}
			Attacker.PlayWorldSound(text.Coalesce("sfx_melee_generic_miss"), 1f, 0.2f, true, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x04003D74 RID: 15732
		[NonSerialized]
		public MissileWeapon LastFired;

		// Token: 0x04003D75 RID: 15733
		[NonSerialized]
		public static int TrackShieldBlock = 0;

		// Token: 0x04003D76 RID: 15734
		[NonSerialized]
		private static List<MissileWeapon> MissileWeaponPartList = new List<MissileWeapon>();

		// Token: 0x04003D77 RID: 15735
		[NonSerialized]
		private static bool MissileWeaponPartListInUse;
	}
}
