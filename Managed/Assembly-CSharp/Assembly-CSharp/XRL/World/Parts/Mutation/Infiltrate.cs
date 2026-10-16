using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using XRL.UI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200141B RID: 5147
	[Serializable]
	public class Infiltrate : BaseMutation
	{
		// Token: 0x0600C82D RID: 51245 RVA: 0x0048DC7B File Offset: 0x0048BE7B
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
		}

		// Token: 0x0600C82E RID: 51246 RVA: 0x0048DC85 File Offset: 0x0048BE85
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
		}

		// Token: 0x0600C82F RID: 51247 RVA: 0x0048DC8F File Offset: 0x0048BE8F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C830 RID: 51248 RVA: 0x0048DCA8 File Offset: 0x0048BEA8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Infiltrating <= 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !E.Actor.OnWorldMap() && E.Distance <= this.GetTeleportDistance(base.Level))
			{
				E.Add("CommandInfiltrate", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C831 RID: 51249 RVA: 0x0048DD06 File Offset: 0x0048BF06
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandInfiltrate");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C832 RID: 51250 RVA: 0x0048DD26 File Offset: 0x0048BF26
		private bool ValidGazeTarget(GameObject obj)
		{
			return obj != null && obj.IsCombat;
		}

		// Token: 0x0600C833 RID: 51251 RVA: 0x0048DD34 File Offset: 0x0048BF34
		public void PickInfiltrateDestination()
		{
			if (this.Infiltrating <= 0)
			{
				if (!base.IsPlayer() && this.ParentObject.CurrentCell != null)
				{
					int num = int.MinValue;
					GameObject gameObject = null;
					foreach (GameObject gameObject2 in this.ParentObject.CurrentCell.ParentZone.LoopObjectsWithPart("Combat"))
					{
						if (this.ParentObject.Brain.GetFeeling(gameObject2) <= 0 && this.ParentObject.DistanceTo(gameObject2) > num)
						{
							num = this.ParentObject.DistanceTo(gameObject2);
							gameObject = gameObject2;
						}
					}
					if (gameObject != null)
					{
						this.teleportDestination = gameObject.Physics.CurrentCell.getClosestPassableCell();
					}
				}
				else
				{
					this.teleportDestination = base.PickDestinationCell(this.GetTeleportDistance(base.Level), AllowVis.Any, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Infiltrate", false, null);
				}
				if (this.teleportDestination == null)
				{
					return;
				}
				if (!this.teleportDestination.IsPassable(null, true))
				{
					this.teleportDestination = this.teleportDestination.GetFirstEmptyAdjacentCell(1, 80);
				}
			}
			if (this.teleportDestination != null)
			{
				Event.NewGameObjectList().Add(this.ParentObject);
				foreach (Cell cell in this.ParentObject.Physics.CurrentCell.GetLocalAdjacentCells(this.GetTeleportRadius(base.Level) + this.GetTurnsToCharge(), false))
				{
					foreach (GameObject gameObject3 in cell.LoopObjectsWithPart("Combat"))
					{
						if (gameObject3 != null && gameObject3.IsMemberOfFaction("Templar"))
						{
							gameObject3.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600C834 RID: 51252 RVA: 0x0048DF44 File Offset: 0x0048C144
		public void performInfiltrate(Cell teleportDestination, bool bDoEffect = true)
		{
			if (this.ParentObject.Physics.CurrentCell != null)
			{
				try
				{
					this.ParentObject.DilationSplat();
					Point2D point2D = teleportDestination.Pos2D - this.ParentObject.Physics.CurrentCell.Pos2D;
					List<GameObject> list = Event.NewGameObjectList();
					list.Add(this.ParentObject);
					foreach (Cell cell in this.ParentObject.CurrentCell.GetLocalAdjacentCells(this.GetTeleportRadius(base.Level), false))
					{
						foreach (GameObject gameObject in cell.LoopObjectsWithPart("Combat"))
						{
							if (!list.Contains(gameObject))
							{
								list.Add(gameObject);
							}
						}
					}
					foreach (GameObject gameObject2 in list)
					{
						Cell currentCell = gameObject2.Physics.CurrentCell;
						if (currentCell != null && currentCell.Location != null)
						{
							Point2D point2D2 = currentCell.Pos2D + point2D;
							if (point2D2.x < 0)
							{
								point2D2.x = 0;
							}
							if (point2D2.y > 79)
							{
								point2D2.y = 79;
							}
							if (point2D2.x < 0)
							{
								point2D2.x = 0;
							}
							if (point2D2.y > 24)
							{
								point2D2.y = 24;
							}
							Cell cell2 = currentCell.ParentZone.GetCell(point2D2);
							if (cell2 != null)
							{
								if (!cell2.IsPassable(null, true))
								{
									cell2 = cell2.GetFirstEmptyAdjacentCell(1, 80);
								}
								if (cell2 != null)
								{
									gameObject2.TeleportSwirl(null, "&C", false, null, 'ù', true);
									gameObject2.TeleportTo(cell2, new int?(0), true, false, false, false, "disappear", "appear");
									gameObject2.TeleportSwirl(null, "&C", false, null, 'ù', false);
									if (gameObject2.IsPlayer() && !this.ParentObject.IsPlayer())
									{
										IComponent<GameObject>.AddPlayerMessage("You are teleported by " + this.ParentObject.the + this.ParentObject.ShortDisplayName + ".", null, true);
									}
								}
							}
						}
						this.ParentObject.DilationSplat();
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Infiltrator teleport", ex, "game_exception");
				}
			}
			this.Infiltrating = 0;
			teleportDestination = null;
			this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
		}

		// Token: 0x0600C835 RID: 51253 RVA: 0x0048E26C File Offset: 0x0048C46C
		public int GetTurnsToCharge()
		{
			return 3;
		}

		// Token: 0x0600C836 RID: 51254 RVA: 0x0048E26F File Offset: 0x0048C46F
		public override bool FinalRender(RenderEvent E)
		{
			if (this.teleportDestination != null && this.ParentObject.CurrentCell != null)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600C837 RID: 51255 RVA: 0x0048E290 File Offset: 0x0048C490
		public override bool Render(RenderEvent E)
		{
			if (this.teleportDestination != null && this.ParentObject.CurrentCell != null)
			{
				int num = 500;
				if (IComponent<GameObject>.frameTimerMS % (long)num < (long)(num / 2))
				{
					E.ColorString = "&r^R";
				}
			}
			return true;
		}

		// Token: 0x0600C838 RID: 51256 RVA: 0x0048E2D4 File Offset: 0x0048C4D4
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.teleportDestination != null && this.ParentObject.CurrentCell != null)
			{
				int num = 500;
				if (IComponent<GameObject>.frameTimerMS % (long)num < (long)(num / 2))
				{
					buffer.Goto(this.teleportDestination.X, this.teleportDestination.Y);
					buffer.Write("&RX", true, false, false, null, -1);
					buffer.Buffer[this.teleportDestination.X, this.teleportDestination.Y].TileForeground = The.Color.DarkRed;
					buffer.Buffer[this.teleportDestination.X, this.teleportDestination.Y].Detail = The.Color.DarkRed;
				}
			}
			base.OnPaint(buffer);
		}

		// Token: 0x0600C839 RID: 51257 RVA: 0x0048E3A8 File Offset: 0x0048C5A8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Infiltrating > 0)
				{
					if (this.teleportDestination == null)
					{
						this.PickInfiltrateDestination();
					}
					if (this.teleportDestination == null)
					{
						this.Infiltrating = 0;
					}
					else
					{
						this.Infiltrating--;
						this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
						if (this.Infiltrating > 0)
						{
							return false;
						}
						this.performInfiltrate(this.teleportDestination, true);
						this.teleportDestination = null;
					}
				}
			}
			else if (E.ID == "CommandInfiltrate")
			{
				if (this.ParentObject.OnWorldMap())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You can't infiltrate on the world map.", true, true, true);
					}
					return false;
				}
				this.PickInfiltrateDestination();
				if (this.teleportDestination == null)
				{
					return false;
				}
				this.Infiltrating = this.GetTurnsToCharge();
				this.ParentObject.UseEnergy(1000, "Physical Mutation Infiltrate", null, null, false);
				base.DidX("focus", this.ParentObject.its + " sensor somewhere else", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C83A RID: 51258 RVA: 0x0048E501 File Offset: 0x0048C701
		public override string GetDescription()
		{
			return "You teleport and bring creatures along with you.";
		}

		// Token: 0x0600C83B RID: 51259 RVA: 0x0048E508 File Offset: 0x0048C708
		public int GetTeleportDistance(int Level)
		{
			return 80;
		}

		// Token: 0x0600C83C RID: 51260 RVA: 0x0048E50C File Offset: 0x0048C70C
		public int GetTeleportRadius(int Level)
		{
			return 3 + Level;
		}

		// Token: 0x0600C83D RID: 51261 RVA: 0x0048E511 File Offset: 0x0048C711
		public int GetCooldown(int Level)
		{
			return 100;
		}

		// Token: 0x0600C83E RID: 51262 RVA: 0x0048E518 File Offset: 0x0048C718
		public override string GetLevelText(int Level)
		{
			return "You teleport to a nearby location and bring everyone within radius " + this.GetTeleportRadius(Level).ToString() + " along with you.\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C83F RID: 51263 RVA: 0x0048E564 File Offset: 0x0048C764
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("TeleportRadius", this.GetTeleportRadius(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C840 RID: 51264 RVA: 0x0048E5B4 File Offset: 0x0048C7B4
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Infiltrate", "CommandInfiltrate", "Physical Mutations", null, "\u001d", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C841 RID: 51265 RVA: 0x0048E5FD File Offset: 0x0048C7FD
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E72 RID: 20082
		public int Infiltrating;

		// Token: 0x04004E73 RID: 20083
		[NonSerialized]
		private Cell teleportDestination;
	}
}
