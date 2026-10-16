using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001426 RID: 5158
	[Serializable]
	public class StickyTongue : BaseMutation
	{
		// Token: 0x0600C8F9 RID: 51449 RVA: 0x00493704 File Offset: 0x00491904
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", StickyTongue.GetRange(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), StickyTongue.GetCooldown(Level), 0);
		}

		// Token: 0x0600C8FA RID: 51450 RVA: 0x00493750 File Offset: 0x00491950
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C8FB RID: 51451 RVA: 0x00493780 File Offset: 0x00491980
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.TongueCharging == 0 && E.Distance > 1 && E.Distance <= this.GetRange() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !this.ParentObject.HasEffect<Glotrot>())
			{
				E.Add("CommandStickyTongue", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8FC RID: 51452 RVA: 0x004937E0 File Offset: 0x004919E0
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.TongueCharging == 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !this.ParentObject.HasEffect<Glotrot>())
			{
				CommandEvent.Send(E.Actor, "CommandStickyTongue", null, null, 0, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8FD RID: 51453 RVA: 0x00493830 File Offset: 0x00491A30
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.TongueCharging > 0)
			{
				this.TongueCharging++;
				this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
				if (this.TongueCharging >= 2)
				{
					if (StickyTongue.HarpoonNearest(this.ParentObject, this.GetRange(), "&M", 1, this.PullSameCreatureType, this.PullHostileOnly) > 0)
					{
						base.CooldownMyActivatedAbility(this.ActivatedAbilityID, StickyTongue.GetCooldown(base.Level), null, null, false);
					}
					else
					{
						base.TakeMyActivatedAbilityOffCooldown(this.ActivatedAbilityID, null);
					}
					this.TongueCharging = 0;
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8FE RID: 51454 RVA: 0x004938E4 File Offset: 0x00491AE4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandStickyTongue")
			{
				if (this.ParentObject.HasEffect<Glotrot>())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.Show("You don't have a tongue!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					return true;
				}
				this.TongueCharging = 1;
				this.ParentObject.UseEnergy(1000, "Physical Mutation Sticky Tongue", null, null, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, StickyTongue.GetCooldown(base.Level), null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8FF RID: 51455 RVA: 0x0049397E File Offset: 0x00491B7E
		public override string GetDescription()
		{
			return "You capture prey with your sticky tongue.";
		}

		// Token: 0x0600C900 RID: 51456 RVA: 0x00493988 File Offset: 0x00491B88
		public override string GetLevelText(int Level)
		{
			return "You pull the nearest creature toward you.\n" + "Range: " + StickyTongue.GetRange(Level).ToString() + "\n" + "Cooldown: " + StickyTongue.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C901 RID: 51457 RVA: 0x004939D4 File Offset: 0x00491BD4
		public override bool Render(RenderEvent E)
		{
			if (this.TongueCharging == 1)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 35 && num < 45)
				{
					E.Tile = null;
					E.RenderString = "*";
					E.ColorString = "&M";
				}
			}
			return base.Render(E);
		}

		// Token: 0x0600C902 RID: 51458 RVA: 0x00493A21 File Offset: 0x00491C21
		public static int GetRange(int Level)
		{
			return 10 + Level * 2;
		}

		// Token: 0x0600C903 RID: 51459 RVA: 0x00493A29 File Offset: 0x00491C29
		public int GetRange()
		{
			return StickyTongue.GetRange(base.Level);
		}

		// Token: 0x0600C904 RID: 51460 RVA: 0x00493A36 File Offset: 0x00491C36
		public static int GetCooldown(int Level)
		{
			return 22 - Level;
		}

		// Token: 0x0600C905 RID: 51461 RVA: 0x00493A3C File Offset: 0x00491C3C
		public int GetCooldown()
		{
			return StickyTongue.GetCooldown(base.Level);
		}

		// Token: 0x0600C906 RID: 51462 RVA: 0x00493A4C File Offset: 0x00491C4C
		public static int HarpoonNearest(GameObject Actor, int Range, string harpoonColor = "&y", int Count = 1, bool PullSameCreatureType = true, bool PullHostileOnly = false)
		{
			int num = 0;
			Cell currentCell = Actor.CurrentCell;
			if (currentCell == null)
			{
				return num;
			}
			List<GameObject> objects = currentCell.ParentZone.GetObjects(delegate(GameObject o)
			{
				if (!o.IsCombatObject())
				{
					return false;
				}
				if (o == Actor)
				{
					return false;
				}
				int num4 = Actor.DistanceTo(o);
				return num4 > 1 && num4 <= Range && (PullSameCreatureType || !(o.Blueprint == Actor.Blueprint)) && (!PullHostileOnly || Actor.IsHostileTowards(o)) && Actor.PhaseMatches(o) && Actor.HasLOSTo(o, true, false, false, true, null);
			});
			if (objects.Count < 1)
			{
				if (Actor.IsPlayer())
				{
					Popup.ShowFail("There are no creatures in range.", true, true, true);
				}
				return num;
			}
			GameObject actor = Actor;
			if (actor != null)
			{
				actor.PlayWorldSound("Sounds/Abilities/sfx_ability_tonguePull", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (objects.Count > 1)
			{
				objects.Sort((GameObject a, GameObject b) => a.DistanceTo(Actor).CompareTo(b.DistanceTo(Actor)));
			}
			int num2 = 0;
			while (num2 < objects.Count && num2 < Count)
			{
				GameObject defender = objects[num2];
				Cell currentCell2 = defender.CurrentCell;
				List<Cell> localEmptyAdjacentCells = currentCell.GetLocalEmptyAdjacentCells();
				if (localEmptyAdjacentCells.Count <= 0)
				{
					break;
				}
				localEmptyAdjacentCells.Sort((Cell a, Cell b) => a.DistanceTo(defender).CompareTo(b.DistanceTo(defender)));
				List<Tuple<Cell, char>> lineTo = Actor.GetLineTo(defender, true, false);
				if (lineTo[0].Item1 != currentCell)
				{
					lineTo.Reverse();
				}
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				string text;
				if (currentCell.X == currentCell2.X)
				{
					text = harpoonColor + "|";
				}
				else if (currentCell.Y == currentCell2.Y)
				{
					text = harpoonColor + "-";
				}
				else if (currentCell.Y < currentCell2.Y)
				{
					if (currentCell.X > currentCell2.X)
					{
						text = harpoonColor + "/";
					}
					else
					{
						text = harpoonColor + "\\";
					}
				}
				else if (currentCell.X > currentCell2.X)
				{
					text = harpoonColor + "\\";
				}
				else
				{
					text = harpoonColor + "/";
				}
				int num3 = 0;
				bool flag = false;
				Cell cell = defender.CurrentCell;
				for (int i = lineTo.Count - 2; i >= 1; i--)
				{
					if (!flag)
					{
						if (defender.CurrentCell != cell)
						{
							break;
						}
						Cell cell2 = currentCell.ParentZone.GetCell(lineTo[i].Item1.X, lineTo[i].Item1.Y);
						if (cell2 == null || defender.CurrentCell != cell || !cell2.IsAdjacentTo(cell, true) || !defender.Move(cell.GetDirectionFromCell(cell2, false), true, false, true, false, true, true, Actor, null, false, null, null, null, false, false, null, null, 0))
						{
							flag = true;
						}
						cell = cell2;
						num3++;
					}
					bool flag2 = false;
					scrapBuffer.RenderBase();
					for (int j = 1; j < i - 1; j++)
					{
						if (lineTo[j].Item1.IsVisible())
						{
							scrapBuffer.Goto(lineTo[j].Item1.X, lineTo[j].Item1.Y);
							scrapBuffer.Write(text, true, false, false, null, -1);
							flag2 = true;
						}
					}
					if (flag2)
					{
						scrapBuffer.Draw();
						Thread.Sleep(50);
					}
				}
				if (Actor != null && defender != null)
				{
					if (num3 == 0)
					{
						IComponent<GameObject>.XDidYToZ(Actor, "try", "to pull", defender, "toward " + Actor.them + ", but cannot", "!", null, null, null, Actor, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
					else if (Actor.DistanceTo(defender) <= 1)
					{
						IComponent<GameObject>.XDidYToZ(Actor, "pull", defender, "to " + Actor.them, "!", null, null, null, defender, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
					else
					{
						IComponent<GameObject>.XDidYToZ(Actor, "pull", defender, "toward " + Actor.them, "!", null, null, null, defender, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
				}
				defender.Gravitate();
				num++;
				num2++;
			}
			return num;
		}

		// Token: 0x0600C907 RID: 51463 RVA: 0x00493F0C File Offset: 0x0049210C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C908 RID: 51464 RVA: 0x00493F18 File Offset: 0x00492118
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Tongue", "CommandStickyTongue", "Physical Mutations", this.GetDescription(), "\u00ad", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C909 RID: 51465 RVA: 0x00493F66 File Offset: 0x00492166
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EA9 RID: 20137
		public int TongueCharging;

		// Token: 0x04004EAA RID: 20138
		public bool PullSameCreatureType = true;

		// Token: 0x04004EAB RID: 20139
		public bool PullHostileOnly;
	}
}
