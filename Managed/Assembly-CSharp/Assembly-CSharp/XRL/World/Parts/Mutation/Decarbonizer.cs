using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001415 RID: 5141
	[Serializable]
	public class Decarbonizer : BaseMutation
	{
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600C7BB RID: 51131 RVA: 0x0048B5BD File Offset: 0x004897BD
		private static GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref Decarbonizer._Projectile))
				{
					Decarbonizer._Projectile = GameObject.CreateUnmodified("ProjectileDecarbonizer");
				}
				return Decarbonizer._Projectile;
			}
		}

		// Token: 0x0600C7BC RID: 51132 RVA: 0x0048B5DF File Offset: 0x004897DF
		public Decarbonizer()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C7BD RID: 51133 RVA: 0x0048B615 File Offset: 0x00489815
		public bool IsTargetingActive()
		{
			return this.Aiming > 0;
		}

		// Token: 0x0600C7BE RID: 51134 RVA: 0x0048B620 File Offset: 0x00489820
		public bool ValidateTargeting()
		{
			if (this.Aiming <= 0)
			{
				this.beamCells.Clear();
				return false;
			}
			if (!this.IsTargetingValid())
			{
				this.ShutDownTargeting();
				return false;
			}
			return true;
		}

		// Token: 0x0600C7BF RID: 51135 RVA: 0x0048B64C File Offset: 0x0048984C
		public bool ShutDownTargeting()
		{
			this.beamCells.Clear();
			if (!this.IsTargetingActive())
			{
				return false;
			}
			this.Aiming = 0;
			if (IComponent<GameObject>.Visible(this.ParentObject))
			{
				IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Poss("molecular cannon", true) + " goes offline.", ColorCoding.ConsequentialColor(null, this.ParentObject), true);
				base.PlayWorldSound("Sounds/Interact/sfx_interact_powerSwitch_off", 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x0600C7C0 RID: 51136 RVA: 0x0048B6DC File Offset: 0x004898DC
		public void AlertBeamCells()
		{
			if (this.beamCells != null)
			{
				int i = 0;
				int count = this.beamCells.Count;
				while (i < count)
				{
					Cell cell = this.beamCells[i];
					int j = 0;
					int count2 = cell.Objects.Count;
					while (j < count2)
					{
						GameObject gameObject = cell.Objects[j];
						if (gameObject.IsPlayer())
						{
							AutoAct.Interrupt("you are in the path of a decarbonizer's molecular cannon", cell, null, true);
						}
						else if (gameObject.IsPotentiallyMobile())
						{
							gameObject.Brain.PushGoal(new FleeLocation(cell, 2, false, false));
						}
						j++;
					}
					i++;
				}
			}
		}

		// Token: 0x0600C7C1 RID: 51137 RVA: 0x0048B778 File Offset: 0x00489978
		public bool IsTargetingValid()
		{
			if (!this.IsTargetingActive())
			{
				return false;
			}
			if (base.IsEMPed())
			{
				return false;
			}
			if (this.ParentObject.HasEffect<Stun>())
			{
				return false;
			}
			if (this.ParentObject.IsConfused)
			{
				return false;
			}
			GlobalLocation targetedFrom = this.TargetedFrom;
			return targetedFrom != null && targetedFrom.Is(this.ParentObject.CurrentCell);
		}

		// Token: 0x0600C7C2 RID: 51138 RVA: 0x0048B7DC File Offset: 0x004899DC
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("BeamDistance", this.GetBeamDistance(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C7C3 RID: 51139 RVA: 0x0048B82C File Offset: 0x00489A2C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<CommandTakeActionEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C7C4 RID: 51140 RVA: 0x0048B888 File Offset: 0x00489A88
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Aiming <= 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Distance <= this.GetBeamDistance(base.Level))
			{
				E.Add("CommandDecarbonizer", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7C5 RID: 51141 RVA: 0x0048B8DC File Offset: 0x00489ADC
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandDecarbonizer")
			{
				if (this.IsTargetingActive())
				{
					this.ShutDownTargeting();
				}
				else
				{
					this.PickBeamTarget();
					if (this.beamCells.Count <= 0)
					{
						return false;
					}
					this.Aiming = this.GetTurnsToCharge();
					this.ParentObject.UseEnergy(1000, "Physical Mutation Decarbonizer", null, null, false);
					if (!this.ParentObject.IsPlayer())
					{
						base.DidX("spin", "up " + this.ParentObject.its + " molecular cannon", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
					this.AlertBeamCells();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7C6 RID: 51142 RVA: 0x0048B9B0 File Offset: 0x00489BB0
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			if (this.Aiming > 0)
			{
				if (!this.IsTargetingActive())
				{
					this.PickBeamTarget();
				}
				if (this.ValidateTargeting())
				{
					this.Aiming--;
					this.ParentObject.UseEnergy(1000, "Physical Mutation Decarbonizer", null, null, false);
					if (this.Aiming <= 0)
					{
						this.fireBeam(this.beamCells, true);
					}
					else
					{
						if (this.ParentObject.IsPlayer())
						{
							The.Core.RenderDelay(500, true, true, false);
						}
						this.AlertBeamCells();
					}
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7C7 RID: 51143 RVA: 0x0048BA56 File Offset: 0x00489C56
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.ValidateTargeting();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7C8 RID: 51144 RVA: 0x0048BA66 File Offset: 0x00489C66
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.ValidateTargeting();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7C9 RID: 51145 RVA: 0x0048BA76 File Offset: 0x00489C76
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ValidateTargeting();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7CA RID: 51146 RVA: 0x0048BA86 File Offset: 0x00489C86
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.ShutDownTargeting();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7CB RID: 51147 RVA: 0x0048BA96 File Offset: 0x00489C96
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C7CC RID: 51148 RVA: 0x0048BAB9 File Offset: 0x00489CB9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("StopFighting");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C7CD RID: 51149 RVA: 0x0048BACE File Offset: 0x00489CCE
		public override bool FireEvent(Event E)
		{
			if (E.ID == "StopFighting")
			{
				this.ShutDownTargeting();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C7CE RID: 51150 RVA: 0x0048BAF0 File Offset: 0x00489CF0
		private bool ValidGazeTarget(GameObject obj)
		{
			return obj != null && obj.IsCombat;
		}

		// Token: 0x0600C7CF RID: 51151 RVA: 0x0048BB00 File Offset: 0x00489D00
		public List<Cell> adjustBeamPath(List<Cell> adjPath)
		{
			GameObject target = this.ParentObject.Target;
			List<Cell> list = Event.NewCellList();
			int i;
			for (i = 0; i < adjPath.Count; i++)
			{
				if (adjPath[i].ParentZone != adjPath[0].ParentZone)
				{
					i--;
					break;
				}
			}
			if (i == adjPath.Count)
			{
				i--;
			}
			float num = (float)(adjPath[i].X - adjPath[0].X) / (float)adjPath[i].RealDistanceTo(adjPath[0], true);
			float num2 = (float)(adjPath[i].Y - adjPath[0].Y) / (float)adjPath[i].RealDistanceTo(adjPath[0], true);
			Cell cell = adjPath[adjPath.Count - 1];
			Cell cell2 = adjPath[0];
			foreach (Cell cell3 in adjPath)
			{
				if (cell3.IsSolidForProjectile(Decarbonizer.Projectile, this.ParentObject, null, target, null, null, true))
				{
					break;
				}
				cell2 = cell3;
				list.Add(cell3);
			}
			float num3 = (float)cell2.X;
			float num4 = (float)cell2.Y;
			if (num == 0f)
			{
				if (num2 == 0f)
				{
					return list;
				}
			}
			while (list.Count < this.GetBeamDistance(base.Level))
			{
				Cell cellFromDelta = cell.GetCellFromDelta(ref num3, ref num4, num, num2, false);
				if (cellFromDelta == null)
				{
					break;
				}
				string directionFromCell = cell.GetDirectionFromCell(cellFromDelta, false);
				if (cellFromDelta.IsSolidForProjectile(Decarbonizer.Projectile, this.ParentObject, null, target, null, null, true))
				{
					if (directionFromCell.Contains("N") || directionFromCell.Contains("S"))
					{
						num2 = -num2;
					}
					if (directionFromCell.Contains("W") || directionFromCell.Contains("E"))
					{
						num = -num;
					}
				}
				cell = cellFromDelta;
				list.Add(cell);
			}
			return list;
		}

		// Token: 0x0600C7D0 RID: 51152 RVA: 0x0048BD34 File Offset: 0x00489F34
		public bool PickBeamTarget()
		{
			if (this.Aiming <= 0)
			{
				GameObject target = this.ParentObject.Target;
				int num = this.GetBeamDistance(base.Level);
				List<Cell> list = base.PickLine(num, AllowVis.OnlyVisible, new Predicate<GameObject>(this.ValidGazeTarget), false, false, true, false, null, null, "Decarbonize", false, true);
				if (list == null || list.Count <= 0)
				{
					return false;
				}
				this.beamCells.Clear();
				this.beamCells.AddRange(list);
				if (this.beamCells[0] != this.ParentObject.CurrentCell)
				{
					this.beamCells.Insert(0, this.ParentObject.CurrentCell);
				}
				if (this.beamCells.Count > 1)
				{
					num++;
					bool flag = false;
					while (!flag && this.beamCells.Count < num)
					{
						int num2 = 0;
						while (num2 < this.beamCells.Count - 1 && this.beamCells.Count < num)
						{
							string directionFromCell = this.beamCells[num2].GetDirectionFromCell(this.beamCells[num2 + 1], false);
							Cell cellFromDirection = this.beamCells[this.beamCells.Count - 1].GetCellFromDirection(directionFromCell, true);
							if (cellFromDirection == null)
							{
								flag = true;
								break;
							}
							this.beamCells.Add(cellFromDirection);
							num2++;
						}
					}
				}
				for (int i = 0; i < this.beamCells.Count; i++)
				{
					if (this.beamCells[i].IsSolidForProjectile(Decarbonizer.Projectile, this.ParentObject, null, target, null, null, true))
					{
						this.beamCells.RemoveRange(i, this.beamCells.Count - i);
						break;
					}
				}
				if (this.beamCells.Count <= 0)
				{
					return false;
				}
				base.PlayWorldSound("Sounds/Abilities/sfx_ability_longBeam_attack_chargeUp", 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				List<Cell> list2 = this.adjustBeamPath(this.beamCells);
				this.beamCells.Clear();
				this.beamCells.AddRange(list2);
				this.TargetedFrom.SetCell(this.ParentObject.CurrentCell);
				this.TargetedTo.SetCell(this.beamCells[this.beamCells.Count - 1]);
			}
			return true;
		}

		// Token: 0x0600C7D1 RID: 51153 RVA: 0x0048BFA5 File Offset: 0x0048A1A5
		public string pathDirectionAtStep(int n, List<Cell> path)
		{
			if (path.Count <= 1)
			{
				return ".";
			}
			n %= path.Count;
			return path[n].GetDirectionFromCell(path[(n + 1) % path.Count], false);
		}

		// Token: 0x0600C7D2 RID: 51154 RVA: 0x0048BFE0 File Offset: 0x0048A1E0
		public Cell cellAtStep(int n, List<Cell> path)
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
			for (int i = 0; i < n; i++)
			{
				cell = cell.GetCellFromDirection(this.pathDirectionAtStep(num, path), false);
				num++;
			}
			return cell;
		}

		// Token: 0x0600C7D3 RID: 51155 RVA: 0x0048C052 File Offset: 0x0048A252
		public int GetHowManyDismemberedLimbs()
		{
			return Stat.Random(1, 4);
		}

		// Token: 0x0600C7D4 RID: 51156 RVA: 0x0048C05C File Offset: 0x0048A25C
		public void fireBeam(List<Cell> beamPath, bool DoEffect = true)
		{
			this.Aiming = 0;
			GameObject target = this.ParentObject.Target;
			if (DoEffect)
			{
				if (!beamPath.Any<Cell>((Cell c) => c.IsVisible()))
				{
					DoEffect = false;
				}
			}
			int i;
			for (i = 0; i < beamPath.Count; i++)
			{
				if (beamPath[i].ParentZone != beamPath[0].ParentZone)
				{
					i--;
					break;
				}
			}
			if (i == beamPath.Count)
			{
				i--;
			}
			float num = (float)(beamPath[i].X - beamPath[0].X) / (float)beamPath[i].RealDistanceTo(beamPath[0], true);
			float num2 = (float)(beamPath[i].Y - beamPath[0].Y) / (float)beamPath[i].RealDistanceTo(beamPath[0], true);
			Cell cell = beamPath[beamPath.Count - 1];
			Cell cell2 = beamPath[0];
			foreach (Cell cell3 in beamPath)
			{
				if (cell3.IsSolidForProjectile(Decarbonizer.Projectile, this.ParentObject, null, target, null, null, false))
				{
					break;
				}
				cell2 = cell3;
			}
			float num3 = (float)cell2.X;
			float num4 = (float)cell2.Y;
			if (num == 0f)
			{
				if (num2 == 0f)
				{
					goto IL_0236;
				}
			}
			while (beamPath.Count < this.GetBeamDistance(base.Level))
			{
				Cell cellFromDelta = cell.GetCellFromDelta(ref num3, ref num4, num, num2, false);
				if (cellFromDelta == null)
				{
					break;
				}
				string directionFromCell = cell.GetDirectionFromCell(cellFromDelta, false);
				if (cellFromDelta.IsSolidForProjectile(Decarbonizer.Projectile, this.ParentObject, null, target, null, null, false))
				{
					if (directionFromCell.Contains("N") || directionFromCell.Contains("S"))
					{
						num2 = -num2;
					}
					if (directionFromCell.Contains("W") || directionFromCell.Contains("E"))
					{
						num = -num;
					}
				}
				cell = cellFromDelta;
				beamPath.Add(cell);
			}
			IL_0236:
			base.DidX("fire", this.ParentObject.its + " molecular cannon", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			base.PlayWorldSound("burn_blast", 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			for (int j = 0; j < beamPath.Count; j++)
			{
				foreach (GameObject gameObject in beamPath[j].GetObjectsWithPart("Body"))
				{
					if (gameObject != this.ParentObject && !gameObject.MakeSave("Agility", 20, this.ParentObject, "Agility", "Decarbonizer Beam", false, false, false, false, null))
					{
						int howManyDismemberedLimbs = this.GetHowManyDismemberedLimbs();
						for (int k = 0; k < howManyDismemberedLimbs; k++)
						{
							Axe_Dismember.Dismember(this.ParentObject, gameObject, null, null, null, null, "sfx_characterTrigger_dismember", true, false, false, false);
							if (!this.ParentObject.IsValid() || this.ParentObject.IsNowhere())
							{
								break;
							}
						}
					}
				}
				if (beamPath[j] != null && beamPath[j].ParentZone == beamPath[0].ParentZone && beamPath[j].IsVisible())
				{
					scrapBuffer.RenderBase();
					if (j > 0)
					{
						scrapBuffer.Goto(beamPath[j - 1].X, beamPath[j - 1].Y);
						scrapBuffer.Write("&b*", true, false, false, null, -1);
					}
					if (j > 1)
					{
						scrapBuffer.Goto(beamPath[j - 2].X, beamPath[j - 2].Y);
						scrapBuffer.Write("&K*", true, false, false, null, -1);
					}
					scrapBuffer.Goto(beamPath[j].X, beamPath[j].Y);
					scrapBuffer.Write("&B*", true, false, false, null, -1);
					scrapBuffer.Draw();
					Thread.Sleep(10);
				}
			}
			this.beamCells.Clear();
			this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 200, null, null, false);
		}

		// Token: 0x0600C7D5 RID: 51157 RVA: 0x0048C548 File Offset: 0x0048A748
		public int GetTurnsToCharge()
		{
			return 3;
		}

		// Token: 0x0600C7D6 RID: 51158 RVA: 0x0048C54B File Offset: 0x0048A74B
		public override bool FinalRender(RenderEvent E)
		{
			E.WantsToPaint = true;
			return true;
		}

		// Token: 0x0600C7D7 RID: 51159 RVA: 0x0048C558 File Offset: 0x0048A758
		public override bool Render(RenderEvent E)
		{
			if (this.IsTargetingActive() && this.ParentObject.CurrentCell != null)
			{
				int num = 500;
				if (IComponent<GameObject>.frameTimerMS % (long)num < (long)(num / 2))
				{
					E.ColorString = "&r^R";
				}
			}
			return true;
		}

		// Token: 0x0600C7D8 RID: 51160 RVA: 0x0048C59C File Offset: 0x0048A79C
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.IsTargetingActive() && this.ParentObject.CurrentCell != null && this.beamCells.Count > 0)
			{
				int num = 500;
				int num2 = num / this.beamCells.Count;
				int num3 = (int)(IComponent<GameObject>.frameTimerMS % (long)num / (long)num2);
				for (int i = 0; i < this.beamCells.Count; i++)
				{
					if (i > 0 && i < this.beamCells.Count && this.beamCells[i].ParentZone == this.ParentObject.Physics.CurrentCell.ParentZone && this.beamCells[i].IsVisible())
					{
						buffer.Goto(this.beamCells[i].X, this.beamCells[i].Y);
						if (i == num3)
						{
							buffer.Buffer[this.beamCells[i].X, this.beamCells[i].Y].TileForeground = The.Color.Red;
							buffer.Buffer[this.beamCells[i].X, this.beamCells[i].Y].Detail = The.Color.Red;
						}
						else
						{
							buffer.Buffer[this.beamCells[i].X, this.beamCells[i].Y].TileForeground = The.Color.DarkRed;
							buffer.Buffer[this.beamCells[i].X, this.beamCells[i].Y].Detail = The.Color.DarkRed;
						}
						buffer.Buffer[this.beamCells[i].X, this.beamCells[i].Y].SetForeground('r');
						if (AutoAct.IsActive(false))
						{
							AutoAct.Interrupt("you see a decarbonizer targeting beam", this.beamCells[i], null, true);
						}
					}
				}
			}
			base.OnPaint(buffer);
		}

		// Token: 0x0600C7D9 RID: 51161 RVA: 0x0048C7E5 File Offset: 0x0048A9E5
		public override string GetDescription()
		{
			return "You extract carbon from living material.";
		}

		// Token: 0x0600C7DA RID: 51162 RVA: 0x0048C7EC File Offset: 0x0048A9EC
		public int GetBeamDistance(int Level)
		{
			return 40 + Level;
		}

		// Token: 0x0600C7DB RID: 51163 RVA: 0x0048C7F2 File Offset: 0x0048A9F2
		public int GetCooldown(int Level)
		{
			return 200;
		}

		// Token: 0x0600C7DC RID: 51164 RVA: 0x0048C7FC File Offset: 0x0048A9FC
		public override string GetLevelText(int Level)
		{
			return "Shoots a beam with a 3-round windup that dismembers the limbs of its targets.\n" + "Beam distance: {{rules|" + this.GetBeamDistance(Level).ToString() + "}} spaces\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C7DD RID: 51165 RVA: 0x0048C84C File Offset: 0x0048AA4C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Decarbonize", "CommandDecarbonizer", "Physical Mutations", null, "ê", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C7DE RID: 51166 RVA: 0x0048C895 File Offset: 0x0048AA95
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E65 RID: 20069
		public int Aiming;

		// Token: 0x04004E66 RID: 20070
		public GlobalLocation TargetedFrom = new GlobalLocation();

		// Token: 0x04004E67 RID: 20071
		public GlobalLocation TargetedTo = new GlobalLocation();

		// Token: 0x04004E68 RID: 20072
		[NonSerialized]
		private List<Cell> beamCells = new List<Cell>(16);

		// Token: 0x04004E69 RID: 20073
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
