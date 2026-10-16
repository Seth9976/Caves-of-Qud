using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EFC RID: 3836
	[Serializable]
	public class RunOver : IPart
	{
		// Token: 0x06009916 RID: 39190 RVA: 0x00378C48 File Offset: 0x00376E48
		public override void Initialize()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Run Over", RunOver.COMMAND_NAME, "Maneuvers", null, "\u00af", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			base.Initialize();
		}

		// Token: 0x06009917 RID: 39191 RVA: 0x00378C8F File Offset: 0x00376E8F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == LeftCellEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009918 RID: 39192 RVA: 0x00378CC8 File Offset: 0x00376EC8
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Damage", this.Damage, false, 0);
			stats.Set("Range", this.MaxTargetDistance, false, 0);
			stats.Set("Knockdown", string.Format("{0} {1}", this.KnockdownSaveStat, this.KnockdownSaveTarget), false, 0);
			stats.Set("Daze", string.Format("{0} {1}", this.DazeSaveStat, this.DazeSaveTarget), false, 0);
		}

		// Token: 0x06009919 RID: 39193 RVA: 0x00378D4C File Offset: 0x00376F4C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == RunOver.COMMAND_NAME)
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell == null || currentCell.OnWorldMap())
				{
					return E.Actor.Fail("You can't do that here.");
				}
				this.PickChargeTarget();
				if (this.ChargeCells == null || this.ChargeCells.Count <= 0)
				{
					return false;
				}
				this.Charging = this.GetTurnsToCharge();
				this.ParentObject.UseEnergy(1000, "Physical Ability RunOver", null, null, false);
				base.PlayWorldSound("sfx_ability_longBeam_attack_chargeUp", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				base.DidX("stare", null, null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				if (base.Visible() && AutoAct.IsInterruptable())
				{
					AutoAct.Interrupt(null, null, this.ParentObject, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600991A RID: 39194 RVA: 0x00378E50 File Offset: 0x00377050
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600991B RID: 39195 RVA: 0x00378E74 File Offset: 0x00377074
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Charging <= 0 && E.Distance <= this.MaxTargetDistance && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.PhaseMatches(E.Target) && E.Actor.FlightCanReach(E.Target) && !E.Target.IsInStasis() && E.Actor.HasLOSTo(E.Target, true, false, false, true, null))
			{
				E.Add(RunOver.COMMAND_NAME, 10, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600991C RID: 39196 RVA: 0x00378F1C File Offset: 0x0037711C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Charging > 0)
			{
				if (this.ChargeCells == null || this.ChargeCells.Count == 0)
				{
					this.Charging = 0;
				}
				else
				{
					this.Charging--;
					this.ParentObject.UseEnergy(1000, "Physical Ability RunOver", null, null, false);
					if (this.Charging > 0)
					{
						if (this.ParentObject.IsPlayer())
						{
							The.Core.RenderDelay(500, true, true, false);
						}
						return false;
					}
					this.PerformCharge(this.ChargeCells, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600991D RID: 39197 RVA: 0x00378FC3 File Offset: 0x003771C3
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.ChargeCells = null;
			this.Charging = 0;
			return base.HandleEvent(E);
		}

		// Token: 0x0600991E RID: 39198 RVA: 0x00378FDA File Offset: 0x003771DA
		private bool ValidChargeTarget(GameObject obj)
		{
			return obj != null && obj.IsCombatObject();
		}

		// Token: 0x0600991F RID: 39199 RVA: 0x00378FE8 File Offset: 0x003771E8
		public void PickChargeTarget()
		{
			if (this.Charging <= 0)
			{
				this.ChargeCells = base.PickLine(this.MaxTargetDistance, AllowVis.OnlyVisible, new Predicate<GameObject>(this.ValidChargeTarget), false, false, true, false, this.ParentObject, null, "Run Over", false, true);
				if (this.ChargeCells == null)
				{
					return;
				}
				this.ChargeCells = new List<Cell>(this.ChargeCells);
				this.ChargeCells.Insert(0, this.ParentObject.CurrentCell);
				if (this.ChargeCells.Count > 1 && this.ChargeCells[0].ParentZone == this.ChargeCells[1].ParentZone)
				{
					while (this.ChargeCells.Count <= this.MaxTargetDistance)
					{
						int num = 0;
						while (num < this.ChargeCells.Count - 1 && this.ChargeCells.Count <= this.MaxTargetDistance && this.ChargeCells[num].ParentZone == this.ChargeCells[num + 1].ParentZone)
						{
							string directionFromCell = this.ChargeCells[num].GetDirectionFromCell(this.ChargeCells[num + 1], false);
							this.ChargeCells.Add(this.ChargeCells[this.ChargeCells.Count - 1].GetCellFromDirection(directionFromCell, false));
							num++;
						}
					}
				}
			}
		}

		// Token: 0x06009920 RID: 39200 RVA: 0x00379155 File Offset: 0x00377355
		public string PathDirectionAtStep(int n, List<Cell> path)
		{
			if (path.Count <= 1)
			{
				return ".";
			}
			n %= path.Count;
			return path[n].GetDirectionFromCell(path[(n + 1) % path.Count], false);
		}

		// Token: 0x06009921 RID: 39201 RVA: 0x00379190 File Offset: 0x00377390
		public Cell CellAtStep(int n, List<Cell> path)
		{
			if (path.Count <= 1)
			{
				return path[0];
			}
			if (n < path.Count)
			{
				return path[n];
			}
			Cell cell = path[path.Count - 1];
			int num = n % path.Count;
			n %= path.Count;
			for (int i = 0; i <= n; i++)
			{
				cell = cell.GetCellFromDirection(this.PathDirectionAtStep(num, path), false);
				num++;
			}
			return cell;
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x00379204 File Offset: 0x00377404
		public void PerformCharge(List<Cell> ChargePath, bool bDoEffect = true)
		{
			this.Charging = 0;
			base.DidX("charge", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			bool flag = this.ParentObject.HasPart<Robot>();
			int i = 1;
			int num = ChargePath.Count;
			while (i < num)
			{
				Cell cell = this.CellAtStep(i, ChargePath);
				if (cell == null)
				{
					break;
				}
				if (flag)
				{
					GameObject firstObjectWithPropertyOrTag = cell.GetFirstObjectWithPropertyOrTag("RobotStop");
					if (firstObjectWithPropertyOrTag != null)
					{
						base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", firstObjectWithPropertyOrTag, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						break;
					}
				}
				foreach (GameObject gameObject in Event.NewGameObjectList(cell.Objects))
				{
					if (gameObject != this.ParentObject)
					{
						bool flag2 = gameObject.IsCombatObject();
						if (flag2)
						{
							num = Math.Max(num, i + 2);
						}
						if (flag2 && this.ParentObject.PhaseMatches(gameObject) && this.ParentObject.FlightCanReach(gameObject))
						{
							base.DidXToY("run", "over", gameObject, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							if (!gameObject.MakeSave(this.KnockdownSaveStat, this.KnockdownSaveTarget, null, null, this.KnockdownSaveVs, false, false, false, false, null))
							{
								gameObject.ApplyEffect(new Prone(), null);
							}
							if (!gameObject.MakeSave(this.DazeSaveStat, this.DazeSaveTarget, null, null, this.DazeSaveVs, false, false, false, false, null))
							{
								gameObject.ApplyEffect(new Dazed(), null);
							}
							int num2 = this.Damage.RollCached();
							if (num2 > 0)
							{
								gameObject.TakeDamage(num2, "being run over by %O.", null, null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
							}
						}
						else if (gameObject.ConsiderSolidFor(this.ParentObject, false))
						{
							if (this.BreakSolidAV.RollCached() < Stats.GetCombatAV(gameObject))
							{
								base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", gameObject, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
								break;
							}
							if (IComponent<GameObject>.Visible(gameObject))
							{
								CombatJuice._cameraShake(0.25f);
								gameObject.DustPuff("&y", 15);
							}
							int j = 0;
							int count = gameObject.Count;
							while (j < count)
							{
								gameObject.Destroy(null, false, false, null);
								j++;
							}
						}
					}
				}
				this.ParentObject.DirectMoveTo(cell, 0, false, true, true, null);
				if (cell.IsVisible())
				{
					The.Core.RenderDelay(10, false, true, false);
				}
				i++;
			}
			if (this.ParentObject.ShouldShunt())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				Cell cell2 = ((currentCell != null) ? currentCell.GetFirstEmptyAdjacentCell() : null);
				if (cell2 != null)
				{
					this.ParentObject.DirectMoveTo(cell2, 0, false, true, true, null);
				}
			}
			this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_charge_mega", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			this.ParentObject.Gravitate();
		}

		// Token: 0x06009923 RID: 39203 RVA: 0x00379590 File Offset: 0x00377790
		public int GetTurnsToCharge()
		{
			return 1;
		}

		// Token: 0x06009924 RID: 39204 RVA: 0x00379594 File Offset: 0x00377794
		public override bool Render(RenderEvent E)
		{
			if (this.ChargeCells != null && this.ChargeCells.Count > 0 && this.ParentObject.CurrentCell != null)
			{
				int num = 500;
				if (IComponent<GameObject>.frameTimerMS % (long)num < (long)(num / 2))
				{
					E.ColorString = "&r^R";
				}
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x06009925 RID: 39205 RVA: 0x003795EC File Offset: 0x003777EC
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.ChargeCells == null)
			{
				return;
			}
			int num = 1000;
			int num2 = num / Math.Max(this.ChargeCells.Count, 1);
			int num3 = (int)(IComponent<GameObject>.frameTimerMS % (long)num / (long)Math.Max(num2, 1));
			if (num3 > 0 && num3 < this.ChargeCells.Count && this.ChargeCells[num3].ParentZone == this.ParentObject.CurrentZone && this.ChargeCells[num3].IsVisible())
			{
				buffer.Goto(this.ChargeCells[num3].X, this.ChargeCells[num3].Y);
				buffer.Write(this.ParentObject.Render.RenderString, true, false, false, null, -1);
				buffer.Buffer[this.ChargeCells[num3].X, this.ChargeCells[num3].Y].Tile = this.ParentObject.Render.Tile;
				buffer.Buffer[this.ChargeCells[num3].X, this.ChargeCells[num3].Y].TileForeground = The.Color.DarkRed;
				buffer.Buffer[this.ChargeCells[num3].X, this.ChargeCells[num3].Y].Detail = The.Color.Red;
				buffer.Buffer[this.ChargeCells[num3].X, this.ChargeCells[num3].Y].SetForeground('r');
			}
			base.OnPaint(buffer);
		}

		// Token: 0x04003FC6 RID: 16326
		public static readonly string COMMAND_NAME = "CommandRunOver";

		// Token: 0x04003FC7 RID: 16327
		public string Damage = "40-60";

		// Token: 0x04003FC8 RID: 16328
		public string BreakSolidAV = "30";

		// Token: 0x04003FC9 RID: 16329
		public int MaxTargetDistance = 6;

		// Token: 0x04003FCA RID: 16330
		public int KnockdownSaveTarget = 35;

		// Token: 0x04003FCB RID: 16331
		public string KnockdownSaveStat = "Strength";

		// Token: 0x04003FCC RID: 16332
		public string KnockdownSaveVs = "RunOver Knockdown";

		// Token: 0x04003FCD RID: 16333
		public int DazeSaveTarget = 35;

		// Token: 0x04003FCE RID: 16334
		public string DazeSaveStat = "Toughness";

		// Token: 0x04003FCF RID: 16335
		public string DazeSaveVs = "RunOver Daze";

		// Token: 0x04003FD0 RID: 16336
		public int Charging;

		// Token: 0x04003FD1 RID: 16337
		public Guid ActivatedAbilityID;

		// Token: 0x04003FD2 RID: 16338
		[NonSerialized]
		private List<Cell> ChargeCells;
	}
}
