using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleLib.Console;
using Genkit;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200132A RID: 4906
	[Serializable]
	public class Smash_Floor : BaseSkill
	{
		// Token: 0x0600BFCB RID: 49099 RVA: 0x004639A0 File Offset: 0x00461BA0
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Range", this.Range, false, 0);
			stats.Set("Damage", this.Damage, false, 0);
			stats.Set("AdditionalDirectHitDamage", this.DirectHitAdditionalDamage, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Smash_Floor.COOLDOWN, 0);
		}

		// Token: 0x0600BFCC RID: 49100 RVA: 0x004639FF File Offset: 0x00461BFF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BFCD RID: 49101 RVA: 0x00463A1D File Offset: 0x00461C1D
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFCE RID: 49102 RVA: 0x00463A40 File Offset: 0x00461C40
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.Range && E.Distance >= this.MinimumDistance && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, false, true, null))
			{
				E.Add("CommandFloorSmash", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFCF RID: 49103 RVA: 0x00463AB5 File Offset: 0x00461CB5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandFloorSmash");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BFD0 RID: 49104 RVA: 0x00463AD5 File Offset: 0x00461CD5
		public override bool Render(RenderEvent E)
		{
			if (this.Throwing > 0)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600BFD1 RID: 49105 RVA: 0x00463AE8 File Offset: 0x00461CE8
		public override void OnPaint(ScreenBuffer buffer)
		{
			int num = (int)(IComponent<GameObject>.frameTimerMS % 1000L / (long)(1000 / this.DamageRadius)) + 1;
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			if (this.smashTarget == null)
			{
				return;
			}
			Cell cell = this.ParentObject.CurrentZone.GetCell(this.smashTarget);
			foreach (Cell cell2 in cell.GetLocalAdjacentCells(num, false))
			{
				if (cell2.Location.Distance(cell.Location) == num)
				{
					buffer.Buffer[cell2.X, cell2.Y].SetBackground('R');
					buffer.Buffer[cell2.X, cell2.Y].Detail = The.Color.Red;
				}
			}
			if (IComponent<GameObject>.frameTimerMS % 1000L <= 500L)
			{
				buffer.Goto(this.smashTarget.X, this.smashTarget.Y);
				buffer.Buffer[this.smashTarget.X, this.smashTarget.Y].Tile = null;
				buffer.Write("&RX", true, false, false, null, -1);
				buffer.Goto(this.ParentObject.Physics.CurrentCell.X, this.ParentObject.Physics.CurrentCell.Y);
				buffer.Buffer[this.ParentObject.Physics.CurrentCell.X, this.ParentObject.Physics.CurrentCell.Y].Tile = null;
				buffer.Write("&RX", true, false, false, null, -1);
			}
		}

		// Token: 0x0600BFD2 RID: 49106 RVA: 0x00463CCC File Offset: 0x00461ECC
		public void SmashOpen(Cell cell)
		{
			if (!cell.HasObject("OpenShaft"))
			{
				Cell cellFromDirection = cell.GetCellFromDirection("D", false);
				cellFromDirection.ClearWalls();
				if (!cellFromDirection.IsPassable(IComponent<GameObject>.ThePlayer, true))
				{
					cellFromDirection.Clear(null, false, false, null);
				}
				cell.AddObject("OpenShaft", null, null, null, null);
			}
		}

		// Token: 0x0600BFD3 RID: 49107 RVA: 0x00463D24 File Offset: 0x00461F24
		public static string getSmokeParticle()
		{
			int num = Stat.RandomCosmetic(1, 3);
			string text = "";
			if (num == 1)
			{
				text = "&K";
			}
			if (num == 1)
			{
				text = "&y";
			}
			if (num == 1)
			{
				text = "&Y";
			}
			int num2 = Stat.RandomCosmetic(1, 3);
			if (num2 == 1)
			{
				text = "°";
			}
			if (num2 == 2)
			{
				text = "±";
			}
			if (num2 == 3)
			{
				text = "²";
			}
			return text;
		}

		// Token: 0x0600BFD4 RID: 49108 RVA: 0x00463D80 File Offset: 0x00461F80
		public static void drawPuff(Cell c)
		{
			if (c != null)
			{
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				scrapBuffer.RenderBase();
				scrapBuffer.Goto(c.X, c.Y);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Draw();
				Thread.Sleep(15);
				scrapBuffer.RenderBase();
				scrapBuffer.Goto(c.X - 1, c.Y - 1);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X, c.Y - 1);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X + 1, c.Y - 1);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X - 1, c.Y);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X + 1, c.Y);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X - 1, c.Y + 1);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X, c.Y + 1);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Goto(c.X + 1, c.Y + 1);
				scrapBuffer.Write(Smash_Floor.getSmokeParticle(), true, false, false, null, -1);
				scrapBuffer.Draw();
				Thread.Sleep(15);
			}
		}

		// Token: 0x0600BFD5 RID: 49109 RVA: 0x00463F20 File Offset: 0x00462120
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Throwing > 0 && this.ParentObject.CurrentZone != null && this.smashTarget != null)
				{
					this.Throwing--;
					if (this.Throwing <= 0)
					{
						Cell cell = this.ParentObject.CurrentZone.GetCell(this.smashTarget);
						List<Cell> localAdjacentCells = cell.GetLocalAdjacentCells(this.DamageRadius, false);
						localAdjacentCells.Add(cell);
						cell.DustPuff();
						if (localAdjacentCells.Any<Cell>((Cell c) => c.IsVisible()))
						{
							Smash_Floor.drawPuff(this.ParentObject.Physics.CurrentCell);
							Smash_Floor.drawPuff(cell);
						}
						foreach (Cell cell2 in localAdjacentCells)
						{
							foreach (GameObject gameObject in cell2.GetObjectsWithPart("Physics"))
							{
								if (gameObject != this.ParentObject)
								{
									int num = this.Damage.RollCached();
									if (cell2 == cell)
									{
										num += this.DirectHitAdditionalDamage.RollCached();
									}
									gameObject.TakeDamage(num, "from %t projectile.", null, null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
								}
							}
							if (cell2 != cell && this.AdjacentPitChance.in100())
							{
								this.SmashOpen(cell2);
							}
						}
						this.SmashOpen(cell);
						this.smashTarget = null;
						this.Throwing = 0;
						if (base.Visible())
						{
							base.DidX("smash", "through the floor with a block of fulcrete", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
						}
						cell.PlayWorldSound("Sounds/Abilities/sfx_ability_catapult", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					else if (AutoAct.IsActive(false))
					{
						Cell cell3 = this.ParentObject.CurrentZone.GetCell(this.smashTarget);
						if (cell3 != null)
						{
							if (cell3.Objects.Any<GameObject>((GameObject o) => o.IsPlayerControlled() && o.IsVisible()))
							{
								AutoAct.Interrupt(null, null, this.ParentObject, true);
							}
							else
							{
								foreach (Cell cell4 in cell3.GetLocalAdjacentCells(this.DamageRadius, false))
								{
									if (cell4.Objects.Any<GameObject>((GameObject o) => o.IsPlayerControlled() && o.IsVisible()))
									{
										AutoAct.Interrupt(null, null, this.ParentObject, true);
										break;
									}
								}
							}
						}
					}
					this.ParentObject.UseEnergy(1000, "Skill", null, null, false);
				}
			}
			else if (E.ID == "CommandFloorSmash")
			{
				if (this.ParentObject.CurrentCell == null)
				{
					return false;
				}
				if (this.ParentObject.Target != null || this.ParentObject.IsPlayer())
				{
					if (this.ParentObject.IsPlayer())
					{
						List<Cell> list = base.PickBurst(this.DamageRadius, this.Range, true, AllowVis.OnlyVisible, "Catapult");
						if (list == null || list.Count == 0)
						{
							return false;
						}
						this.smashTarget = list[0].Location;
					}
					else
					{
						this.smashTarget = this.ParentObject.Target.CurrentCell.Location;
						if (this.ParentObject.Target.IsPlayer())
						{
							AutoAct.Interrupt(null, null, this.ParentObject, true);
						}
					}
					this.Throwing = 3;
					this.ParentObject.UseEnergy(1000, "Skill Smash Floor", null, null, false);
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Smash_Floor.COOLDOWN, null, null, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BFD6 RID: 49110 RVA: 0x00464374 File Offset: 0x00462574
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Catapult", "CommandFloorSmash", "Skills", null, "\u001f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BFD7 RID: 49111 RVA: 0x004643BC File Offset: 0x004625BC
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D2B RID: 19755
		public int Range = 15;

		// Token: 0x04004D2C RID: 19756
		public int AdjacentPitChance = 10;

		// Token: 0x04004D2D RID: 19757
		public int DamageRadius = 1;

		// Token: 0x04004D2E RID: 19758
		public int MinimumDistance = 2;

		// Token: 0x04004D2F RID: 19759
		public int Windup = 3;

		// Token: 0x04004D30 RID: 19760
		public int Throwing;

		// Token: 0x04004D31 RID: 19761
		public string Damage = "4d6";

		// Token: 0x04004D32 RID: 19762
		public string DirectHitAdditionalDamage = "4d6";

		// Token: 0x04004D33 RID: 19763
		public static readonly int COOLDOWN = 100;

		// Token: 0x04004D34 RID: 19764
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D35 RID: 19765
		public Location2D smashTarget;
	}
}
