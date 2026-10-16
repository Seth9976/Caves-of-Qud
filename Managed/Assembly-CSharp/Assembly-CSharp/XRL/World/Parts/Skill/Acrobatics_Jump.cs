using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001318 RID: 4888
	[Serializable]
	public class Acrobatics_Jump : BaseSkill
	{
		// Token: 0x0600BF17 RID: 48919 RVA: 0x0045F97C File Offset: 0x0045DB7C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterAddSkillEvent.ID || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetRetreatAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetMovementCapabilitiesEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BF18 RID: 48920 RVA: 0x0045F9D8 File Offset: 0x0045DBD8
		public void CollectStats(Templates.StatCollector stats)
		{
			int num;
			int num2;
			string text;
			string text2;
			string text3;
			bool flag;
			GetJumpingBehaviorEvent.Retrieve(this.ParentObject, out num, out num2, out text, out text2, out text3, out flag, stats);
			stats.Set("CanJumpOverCreatures", flag ? "true" : "false", false, 0);
			stats.Set("MinimumRange", num2, num2 != 2, num2 - 2);
			int num3 = stats.CollectBonusModifiers("Range", 2, "Range", true);
			stats.Set("IsRangeIncreased", (num3 > 2) ? "true" : "false", false, 0);
			stats.Set("Range", num3, num3 != 2, num3 - 2);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetBaseCooldown(), 0);
		}

		// Token: 0x0600BF19 RID: 48921 RVA: 0x0045FA95 File Offset: 0x0045DC95
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF1A RID: 48922 RVA: 0x0045FAB8 File Offset: 0x0045DCB8
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Jump to Square", Acrobatics_Jump.COMMAND_NAME, 7000, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF1B RID: 48923 RVA: 0x0045FAE4 File Offset: 0x0045DCE4
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (GameObject.Validate(E.Actor) && E.TargetCell != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.HasBodyPart("Feet"))
			{
				int minimumRange;
				bool canJumpOverCreatures;
				int num;
				string text;
				string text2;
				string text3;
				GetJumpingBehaviorEvent.Retrieve(E.Actor, out num, out minimumRange, out text, out text2, out text3, out canJumpOverCreatures, null);
				int range = this.GetBaseRange() + num;
				if (E.StandoffDistance > 0)
				{
					Cell cell = null;
					int num2 = 1000;
					int num3 = 1000;
					foreach (Cell cell2 in E.TargetCell.GetLocalAdjacentCellsAtRadius(E.StandoffDistance, true))
					{
						if (this.ValidJump(cell2, E.Actor, new int?(range), new int?(minimumRange)) && Acrobatics_Jump.CheckPath(E.Actor, cell2, canJumpOverCreatures, false, true, text2))
						{
							int navigationWeightFor = cell2.GetNavigationWeightFor(E.Actor, false, false, false, false, false);
							if (navigationWeightFor < 10)
							{
								int num4 = E.Actor.DistanceTo(cell2);
								if (num4 + navigationWeightFor < num2 + num3)
								{
									cell = cell2;
									num2 = num4;
									num3 = navigationWeightFor;
								}
							}
						}
					}
					if (cell != null && Stat.Random(1, 20) < num2)
					{
						E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, cell);
					}
				}
				else if (E.Distance < minimumRange || !Acrobatics_Jump.CheckPath(E.Actor, E.TargetCell, canJumpOverCreatures, false, true, "jump") || E.TargetCell.GetNavigationWeightFor(E.Actor, false, false, false, false, false) >= 10)
				{
					Cell randomLocalAdjacentCell = E.TargetCell.GetRandomLocalAdjacentCell((Cell c) => this.ValidJump(c, E.Actor, new int?(range), new int?(minimumRange)) && Acrobatics_Jump.CheckPath(E.Actor, c, canJumpOverCreatures, false, true, "jump") && c.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCell != null && Stat.Random(1, 20) < E.Actor.DistanceTo(randomLocalAdjacentCell))
					{
						E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, randomLocalAdjacentCell);
					}
				}
				else if (this.ValidJump(E.Distance, E.Actor, new int?(range), new int?(minimumRange)) && Stat.Random(1, 20) < E.Actor.DistanceTo(E.TargetCell))
				{
					E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF1C RID: 48924 RVA: 0x0045FE08 File Offset: 0x0045E008
		public override bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			if (GameObject.Validate(E.Actor) && E.Actor.HasBodyPart("Feet") && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				int minimumRange;
				bool canJumpOverCreatures;
				int num;
				string text;
				string text2;
				string text3;
				GetJumpingBehaviorEvent.Retrieve(E.Actor, out num, out minimumRange, out text, out text2, out text3, out canJumpOverCreatures, null);
				int range = this.GetBaseRange() + num;
				if (E.TargetCell == null)
				{
					Cell cell;
					if ((cell = E.AvoidCell) == null)
					{
						GameObject target = E.Target;
						cell = ((target != null) ? target.CurrentCell : null);
					}
					Cell cell2 = cell;
					if (cell2 != null)
					{
						Cell cell3 = null;
						int num2 = 0;
						int num3 = 0;
						foreach (Cell cell4 in E.Actor.CurrentCell.GetLocalAdjacentCells(range, false))
						{
							if (this.ValidJump(cell4, E.Actor, new int?(range), new int?(minimumRange)) && Acrobatics_Jump.CheckPath(E.Actor, cell4, canJumpOverCreatures, false, true, "jump"))
							{
								int navigationWeightFor = cell4.GetNavigationWeightFor(E.Actor, false, false, false, false, false);
								if (navigationWeightFor < 10)
								{
									int num4 = cell2.DistanceTo(cell4);
									if (num4 - navigationWeightFor > num2 - num3)
									{
										cell3 = cell4;
										num2 = num4;
										num3 = navigationWeightFor;
									}
								}
							}
						}
						if (cell3 != null)
						{
							E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, cell3);
						}
					}
					else
					{
						Zone currentZone = E.Actor.CurrentZone;
						Cell cell5 = ((currentZone != null) ? currentZone.GetEmptyReachableCells().GetRandomElement<Cell>(null) : null);
						if (cell5 != null && this.ValidJump(cell5, E.Actor, new int?(range), new int?(minimumRange)) && Acrobatics_Jump.CheckPath(E.Actor, cell5, canJumpOverCreatures, false, true, "jump") && cell5.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 10)
						{
							E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, cell5);
						}
					}
				}
				else if (E.StandoffDistance > 0)
				{
					Cell randomLocalAdjacentCellAtRadius = E.TargetCell.GetRandomLocalAdjacentCellAtRadius(E.StandoffDistance, (Cell c) => this.ValidJump(c, E.Actor, new int?(range), new int?(minimumRange)) && Acrobatics_Jump.CheckPath(E.Actor, c, canJumpOverCreatures, false, true, "jump") && c.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCellAtRadius != null)
					{
						E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, randomLocalAdjacentCellAtRadius);
					}
				}
				else if (E.Distance < minimumRange || !Acrobatics_Jump.CheckPath(E.Actor, E.TargetCell, canJumpOverCreatures, false, true, "jump") || E.TargetCell.GetNavigationWeightFor(E.Actor, false, false, false, false, false) > 10)
				{
					Cell randomLocalAdjacentCell = E.TargetCell.GetRandomLocalAdjacentCell((Cell c) => this.ValidJump(c, E.Actor, new int?(range), new int?(minimumRange)) && Acrobatics_Jump.CheckPath(E.Actor, c, canJumpOverCreatures, false, true, "jump") && c.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCell != null)
					{
						E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, randomLocalAdjacentCell);
					}
				}
				else if (this.ValidJump(E.Distance, E.Actor, new int?(range), new int?(minimumRange)))
				{
					E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF1D RID: 48925 RVA: 0x004601F8 File Offset: 0x0045E3F8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.HasBodyPart("Feet") && Stat.Random(1, 10) < E.Distance)
			{
				int num;
				int num2;
				string text;
				string text2;
				string text3;
				bool flag;
				GetJumpingBehaviorEvent.Retrieve(E.Actor, out num, out num2, out text, out text2, out text3, out flag, null);
				if (E.Distance > num2 + 1)
				{
					int num3 = this.GetBaseRange() + num;
					if (E.Distance <= num3 + 1)
					{
						Cell cell = Acrobatics_Jump.FindCellToApproachTarget(E.Actor, E.Target, num2, num3, false, false, "jump");
						if (cell != null)
						{
							E.Add(Acrobatics_Jump.COMMAND_NAME, 1, null, false, false, null, cell);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF1E RID: 48926 RVA: 0x004602AC File Offset: 0x0045E4AC
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Acrobatics_Jump.COMMAND_NAME && Acrobatics_Jump.Jump(this.ParentObject, 0, E.TargetCell, null))
			{
				this.ParentObject.UseEnergy(1000, "Movement Jump", null, null, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetBaseCooldown(), null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF1F RID: 48927 RVA: 0x0046031E File Offset: 0x0045E51E
		public override bool BeforeAddSkill(BeforeAddSkillEvent E)
		{
			if (E.Actor.IsPlayer() && E.Source != null && E.Source.GetClass() == "frog")
			{
				Achievement.LEARN_JUMP.Unlock();
			}
			return base.BeforeAddSkill(E);
		}

		// Token: 0x0600BF20 RID: 48928 RVA: 0x0046035D File Offset: 0x0045E55D
		public override bool HandleEvent(AfterAddSkillEvent E)
		{
			if (E.Skill == this)
			{
				this.SyncAbility(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF21 RID: 48929 RVA: 0x00460376 File Offset: 0x0045E576
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF22 RID: 48930 RVA: 0x00460399 File Offset: 0x0045E599
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600BF23 RID: 48931 RVA: 0x004603B0 File Offset: 0x0045E5B0
		public static bool CheckPath(GameObject Actor, Cell TargetCell, out GameObject Over, out List<Point> Path, bool Silent = false, bool CanJumpOverCreatures = false, bool CanLandOnCreature = false, string Verb = "jump")
		{
			Over = null;
			Path = null;
			Cell currentCell = Actor.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			Zone parentZone = currentCell.ParentZone;
			Path = Zone.Line(currentCell.X, currentCell.Y, TargetCell.X, TargetCell.Y, false);
			int num = 0;
			foreach (Point point in Path)
			{
				Cell cell = parentZone.GetCell(point.X, point.Y);
				if (cell != currentCell)
				{
					int i = 0;
					int count = cell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = cell.Objects[i];
						if (num == Path.Count - 1)
						{
							if (gameObject.ConsiderSolidFor(Actor, false) || (!CanLandOnCreature && gameObject.IsCombatObject()))
							{
								if (!Silent)
								{
									Actor.Fail("You can only " + Verb + " into empty spaces.");
								}
								return false;
							}
						}
						else if (((gameObject.ConsiderSolidFor(Actor, false) && !gameObject.HasPropertyOrTag("Flyover")) || (!CanJumpOverCreatures && gameObject.IsCombatObject())) && gameObject.PhaseAndFlightMatches(Actor))
						{
							if (!Silent)
							{
								Actor.Fail(string.Concat(new string[]
								{
									"You can't ",
									Verb,
									" over ",
									gameObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
									"."
								}));
							}
							return false;
						}
						if (gameObject.IsReal && (Over == null || (Over.IsScenery && !gameObject.IsScenery) || (CanJumpOverCreatures && !Over.IsCreature && gameObject.IsCreature)))
						{
							Over = gameObject;
						}
						i++;
					}
				}
				num++;
			}
			return true;
		}

		// Token: 0x0600BF24 RID: 48932 RVA: 0x004605BC File Offset: 0x0045E7BC
		public static bool CheckPath(GameObject Actor, Cell TargetCell, out GameObject Over, bool Silent = false, bool CanJumpOverCreatures = false, bool CanLandOnCreature = false, string Verb = "jump")
		{
			List<Point> list;
			return Acrobatics_Jump.CheckPath(Actor, TargetCell, out Over, out list, Silent, CanJumpOverCreatures, CanLandOnCreature, Verb);
		}

		// Token: 0x0600BF25 RID: 48933 RVA: 0x004605DC File Offset: 0x0045E7DC
		public static bool CheckPath(GameObject Actor, Cell TargetCell, out List<Point> Path, bool Silent = false, bool CanJumpOverCreatures = false, bool CanLandOnCreature = false, string Verb = "jump")
		{
			GameObject gameObject;
			return Acrobatics_Jump.CheckPath(Actor, TargetCell, out gameObject, out Path, Silent, CanJumpOverCreatures, CanLandOnCreature, Verb);
		}

		// Token: 0x0600BF26 RID: 48934 RVA: 0x004605FC File Offset: 0x0045E7FC
		public static bool CheckPath(GameObject Actor, Cell TargetCell, bool CanJumpOverCreatures = false, bool CanLandOnCreature = false, bool Silent = false, string Verb = "jump")
		{
			GameObject gameObject;
			List<Point> list;
			return Acrobatics_Jump.CheckPath(Actor, TargetCell, out gameObject, out list, Silent, CanJumpOverCreatures, CanLandOnCreature, Verb);
		}

		// Token: 0x0600BF27 RID: 48935 RVA: 0x0046061C File Offset: 0x0045E81C
		public static Cell FindCellToApproachTarget(GameObject Actor, GameObject Target, int MinimumRange, int MaximumRange, bool CanJumpOverCreatures = false, bool CanLandOnCreature = false, string Verb = "jump")
		{
			if (Target == null || Target.IsInvalid())
			{
				return null;
			}
			Cell cell = null;
			int num = 1000;
			int num2 = 1000;
			foreach (Cell cell2 in Target.CurrentCell.GetLocalAdjacentCells())
			{
				int navigationWeightFor = cell2.GetNavigationWeightFor(Actor, false, false, false, false, false);
				if (navigationWeightFor <= 20)
				{
					int num3 = Actor.DistanceTo(cell2);
					if (num3 >= MinimumRange && num3 <= MaximumRange && num3 + navigationWeightFor < num + num2 && cell2.IsEmptyOfSolidFor(Actor, true) && !cell2.HasCombatObject(true) && Acrobatics_Jump.CheckPath(Actor, cell2, CanJumpOverCreatures, CanLandOnCreature, true, Verb))
					{
						cell = cell2;
						num = num3;
						num2 = navigationWeightFor;
					}
				}
			}
			return cell;
		}

		// Token: 0x0600BF28 RID: 48936 RVA: 0x004606EC File Offset: 0x0045E8EC
		public static int GetBaseRange(GameObject Actor)
		{
			Acrobatics_Jump part = Actor.GetPart<Acrobatics_Jump>();
			if (part != null)
			{
				return part.GetBaseRange();
			}
			return 2 + Actor.GetIntProperty("JumpRangeModifier", 0);
		}

		// Token: 0x0600BF29 RID: 48937 RVA: 0x00460718 File Offset: 0x0045E918
		public int GetBaseRange()
		{
			return this.MaxDistance + this.ParentObject.GetIntProperty("JumpRangeModifier", 0);
		}

		// Token: 0x0600BF2A RID: 48938 RVA: 0x00460734 File Offset: 0x0045E934
		public static int GetActiveRange(GameObject Actor, out int MinimumRange, int RangeModifier = 0)
		{
			int num;
			string text;
			string text2;
			string text3;
			bool flag;
			GetJumpingBehaviorEvent.Retrieve(Actor, out num, out MinimumRange, out text, out text2, out text3, out flag, null);
			return Acrobatics_Jump.GetBaseRange(Actor) + RangeModifier + num;
		}

		// Token: 0x0600BF2B RID: 48939 RVA: 0x00460760 File Offset: 0x0045E960
		public static int GetActiveRange(GameObject Actor, int RangeModifier = 0)
		{
			int num;
			int num2;
			string text;
			string text2;
			string text3;
			bool flag;
			GetJumpingBehaviorEvent.Retrieve(Actor, out num, out num2, out text, out text2, out text3, out flag, null);
			return Acrobatics_Jump.GetBaseRange(Actor) + RangeModifier + num;
		}

		// Token: 0x0600BF2C RID: 48940 RVA: 0x0046078C File Offset: 0x0045E98C
		public int GetActiveRange(int RangeModifier = 0)
		{
			int num;
			int num2;
			string text;
			string text2;
			string text3;
			bool flag;
			GetJumpingBehaviorEvent.Retrieve(this.ParentObject, out num, out num2, out text, out text2, out text3, out flag, null);
			return this.GetBaseRange() + RangeModifier + num;
		}

		// Token: 0x0600BF2D RID: 48941 RVA: 0x004607BC File Offset: 0x0045E9BC
		public static bool Jump(GameObject Actor, int RangeModifier = 0, Cell TargetCell = null, string SourceKey = null)
		{
			bool flag = TargetCell != null;
			int num;
			int num2;
			string text;
			string text2;
			string text3;
			bool flag2;
			GetJumpingBehaviorEvent.Retrieve(Actor, out num, out num2, out text, out text2, out text3, out flag2, null);
			int num3 = Acrobatics_Jump.GetBaseRange(Actor) + RangeModifier + num;
			if (Actor.OnWorldMap())
			{
				Actor.Fail("You cannot " + text2 + " on the world map.");
				return false;
			}
			if (Actor.IsFlying)
			{
				Actor.Fail("You cannot " + text2 + " while flying.");
				return false;
			}
			if (!Actor.CanChangeMovementMode("Jumping", true, false, false, false))
			{
				return false;
			}
			if (!Actor.CanChangeBodyPosition("Jumping", true, false, false, false))
			{
				return false;
			}
			if (!Actor.HasBodyPart("Feet"))
			{
				Actor.Fail("You cannot " + text2 + " without feet.");
				return false;
			}
			SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_jump");
			Cell currentCell = Actor.CurrentCell;
			GameObject gameObject;
			for (;;)
			{
				if (TargetCell == null)
				{
					if (Actor.IsPlayer())
					{
						TargetCell = PickTarget.ShowPicker(PickTarget.PickStyle.Line, num3, num3, currentCell.X, currentCell.Y, false, AllowVis.OnlyVisible, null, null, Actor, null, "Jump where?", false, false);
					}
					else
					{
						TargetCell = Acrobatics_Jump.FindCellToApproachTarget(Actor, Actor.Target, num2, num3, flag2, false, text2);
					}
					if (TargetCell == null)
					{
						break;
					}
				}
				int num4 = Actor.DistanceTo(TargetCell);
				if (num4 > num3)
				{
					if (flag)
					{
						return false;
					}
					Actor.Fail(string.Concat(new string[]
					{
						"You may not ",
						text2,
						" more than ",
						Grammar.Cardinal(num3),
						" ",
						(num3 == 1) ? "square" : "squares",
						"!"
					}));
					if (!Actor.IsPlayer())
					{
						return false;
					}
					TargetCell = null;
				}
				else if (num4 < num2)
				{
					if (flag)
					{
						return false;
					}
					Actor.Fail(string.Concat(new string[]
					{
						"You must ",
						text2,
						" at least ",
						Grammar.Cardinal(num2),
						" ",
						(num2 == 1) ? "square" : "squares",
						"!"
					}));
					if (!Actor.IsPlayer())
					{
						return false;
					}
					TargetCell = null;
				}
				else
				{
					Cell cell = TargetCell;
					bool flag3 = flag2;
					string text4 = text2;
					List<Point> list;
					if (Acrobatics_Jump.CheckPath(Actor, cell, out gameObject, out list, flag, flag3, false, text4))
					{
						goto IL_0235;
					}
					if (flag || !Actor.IsPlayer())
					{
						return false;
					}
					TargetCell = null;
				}
			}
			return false;
			IL_0235:
			if (Actor != null)
			{
				Actor.PlayWorldSound("Sounds/Abilities/sfx_ability_jump", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Actor.MovementModeChanged("Jumping", false);
			Actor.BodyPositionChanged("Jumping", false);
			Acrobatics_Jump.PlayAnimation(Actor, TargetCell);
			if (gameObject != null)
			{
				IComponent<GameObject>.XDidYToZ(Actor, text2, "over", gameObject, null, ".", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else
			{
				IComponent<GameObject>.XDidY(Actor, text2, null, ".", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			if (Actor.DirectMoveTo(TargetCell, 0, false, true, true, null))
			{
				List<Point> list;
				JumpedEvent.Send(Actor, currentCell, TargetCell, list, num3, text, text3, SourceKey);
			}
			Actor.Gravitate();
			Acrobatics_Jump.Land(currentCell, TargetCell, 4, 8);
			return true;
		}

		// Token: 0x0600BF2E RID: 48942 RVA: 0x00460AC4 File Offset: 0x0045ECC4
		public static void PlayAnimation(GameObject Actor, Cell TargetCell)
		{
			if (!Options.UseOverlayCombatEffects)
			{
				return;
			}
			if (!TargetCell.IsVisible())
			{
				return;
			}
			CombatJuice.BlockUntilFinished(CombatJuice.Jump(Actor, Actor.CurrentCell.Location, TargetCell.Location, Stat.Random(0.3f, 0.4f), Stat.Random(0.4f, 0.5f), 0.75f, Actor.IsPlayer(), true), new GameObject[] { Actor }, 10000, true);
		}

		// Token: 0x0600BF2F RID: 48943 RVA: 0x00460B38 File Offset: 0x0045ED38
		public static void Leap(Cell From, Cell To, int Count = 3, int Life = 12)
		{
			if (!From.IsVisible())
			{
				return;
			}
			float num = (float)Math.Atan2((double)(From.X - To.X), (double)(From.Y - To.Y));
			Acrobatics_Jump.Leap(From.X, From.Y, num, Count, Life);
		}

		// Token: 0x0600BF30 RID: 48944 RVA: 0x00460B88 File Offset: 0x0045ED88
		public static void Leap(int X, int Y, float Angle, int Count = 3, int Life = 12)
		{
			for (int i = 0; i < Count; i++)
			{
				float num = (float)Stat.RandomCosmetic(-90, 90) * 0.017453292f + Angle;
				float num2 = Mathf.Sin(num) / (float)Life;
				float num3 = Mathf.Cos(num) / (float)Life;
				XRLCore.ParticleManager.Add("&y±", (float)X, (float)Y, num2, num3, Life, 0f, 0f, 0L);
			}
		}

		// Token: 0x0600BF31 RID: 48945 RVA: 0x00460BEC File Offset: 0x0045EDEC
		public static void Land(Cell From, Cell To, int Count = 4, int Life = 8)
		{
			if (!To.IsVisible())
			{
				return;
			}
			float num = (float)Math.Atan2((double)(To.X - From.X), (double)(To.Y - From.Y));
			Acrobatics_Jump.Land(To.X, To.Y, num, Count, Life);
		}

		// Token: 0x0600BF32 RID: 48946 RVA: 0x00460C3C File Offset: 0x0045EE3C
		public static void Land(int X, int Y, float Angle, int Count = 4, int Life = 8)
		{
			for (int i = 0; i < Count; i++)
			{
				float num = (float)Stat.RandomCosmetic(-75, 75) * 0.017453292f + Angle;
				float num2 = Mathf.Sin(num) / ((float)Life / 2f);
				float num3 = Mathf.Cos(num) / ((float)Life / 2f);
				string text = ((Stat.RandomCosmetic(1, 4) <= 3) ? "&y." : "&y±");
				XRLCore.ParticleManager.Add(text, (float)X, (float)Y, num2, num3, Life, 0f, 0f, 0L);
			}
		}

		// Token: 0x0600BF33 RID: 48947 RVA: 0x00460CBE File Offset: 0x0045EEBE
		private bool ValidJump(Cell Cell, GameObject Actor = null, int? Range = null, int? MinimumRange = null)
		{
			return this.ValidJump((Actor ?? this.ParentObject).DistanceTo(Cell), Actor, Range, MinimumRange);
		}

		// Token: 0x0600BF34 RID: 48948 RVA: 0x00460CDC File Offset: 0x0045EEDC
		private bool ValidJump(int Distance, GameObject Actor = null, int? Range = null, int? MinimumRange = null)
		{
			return Distance >= (MinimumRange ?? 2) && Distance <= (Range ?? this.GetBaseRange());
		}

		// Token: 0x0600BF35 RID: 48949 RVA: 0x00460D24 File Offset: 0x0045EF24
		public void SyncAbility(bool Silent = false)
		{
			int num;
			int num2;
			string text;
			string text2;
			string text3;
			bool flag;
			GetJumpingBehaviorEvent.Retrieve(this.ParentObject, out num, out num2, out text, out text2, out text3, out flag, null);
			int num3 = this.GetBaseRange() + num;
			if (this.ActivatedAbilityID == Guid.Empty || text != this.ActiveAbilityName || num3 != this.ActiveRange)
			{
				bool flag2 = this.ActiveAbilityName == text;
				base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
				this.ActiveAbilityName = text;
				this.ActiveRange = num3;
				if (!text.IsNullOrEmpty())
				{
					this.ActivatedAbilityID = base.AddMyActivatedAbility(text, Acrobatics_Jump.COMMAND_NAME, "Skills", null, "\u0017", null, false, false, false, false, false, false, Silent || flag2, false, true, true, false, -1, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x0600BF36 RID: 48950 RVA: 0x00460DE3 File Offset: 0x0045EFE3
		public static void SyncAbility(GameObject Actor, bool Silent = false)
		{
			Acrobatics_Jump part = Actor.GetPart<Acrobatics_Jump>();
			if (part == null)
			{
				return;
			}
			part.SyncAbility(Silent);
		}

		// Token: 0x0600BF37 RID: 48951 RVA: 0x00460DF6 File Offset: 0x0045EFF6
		public int GetBaseCooldown()
		{
			return 100;
		}

		// Token: 0x04004D0B RID: 19723
		public const int DEFAULT_MAX_DISTANCE = 2;

		// Token: 0x04004D0C RID: 19724
		public static readonly string COMMAND_NAME = "CommandAcrobaticsJump";

		// Token: 0x04004D0D RID: 19725
		public int MaxDistance = 2;

		// Token: 0x04004D0E RID: 19726
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D0F RID: 19727
		public string ActiveAbilityName;

		// Token: 0x04004D10 RID: 19728
		public string ActiveAbilityDescription;

		// Token: 0x04004D11 RID: 19729
		public int ActiveRange;
	}
}
