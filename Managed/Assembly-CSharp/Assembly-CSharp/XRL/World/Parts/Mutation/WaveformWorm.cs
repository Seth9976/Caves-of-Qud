using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001429 RID: 5161
	[Serializable]
	public class WaveformWorm : BaseMutation
	{
		// Token: 0x0600C91F RID: 51487 RVA: 0x00494413 File Offset: 0x00492613
		public WaveformWorm()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C920 RID: 51488 RVA: 0x00494426 File Offset: 0x00492626
		public override string GetDescription()
		{
			return "You dash along a waveform.";
		}

		// Token: 0x0600C921 RID: 51489 RVA: 0x00494430 File Offset: 0x00492630
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", this.GetRange(Level), false, 0);
			stats.Set("Damage", this.GetDamage(Level), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldownTurns(Level), 0);
		}

		// Token: 0x0600C922 RID: 51490 RVA: 0x00494480 File Offset: 0x00492680
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C923 RID: 51491 RVA: 0x00494498 File Offset: 0x00492698
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Distance <= this.GetRange(base.Level) - 1 && !E.Actor.OnWorldMap() && GameObject.Validate(E.Target))
			{
				Cell currentCell = E.Target.CurrentCell;
				Cell currentCell2 = E.Actor.CurrentCell;
				if (currentCell != null && currentCell2 != null && (currentCell.X == currentCell2.X || currentCell.Y == currentCell2.Y || Math.Abs(currentCell.X - currentCell2.X) == Math.Abs(currentCell.Y - currentCell2.Y)))
				{
					E.Add("CommandWaveformWorm", 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C924 RID: 51492 RVA: 0x00494562 File Offset: 0x00492762
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandWaveformWorm");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C925 RID: 51493 RVA: 0x00494578 File Offset: 0x00492778
		public override string GetLevelText(int Level)
		{
			string text = "You dash in a direction, dealing damage to creatures you pass through.\n";
			text = text + "Range: " + this.GetRange(Level).ToString() + "\n";
			text = text + "Damage: " + this.GetDamage(Level) + "\n";
			int cooldownTurns = this.GetCooldownTurns(Level);
			return string.Concat(new string[]
			{
				text,
				"Cooldown: ",
				cooldownTurns.ToString(),
				" ",
				(cooldownTurns == 1) ? "round" : "rounds",
				"\n"
			});
		}

		// Token: 0x0600C926 RID: 51494 RVA: 0x00494611 File Offset: 0x00492811
		public int GetRange(int Level)
		{
			return 5 + Level / 2;
		}

		// Token: 0x0600C927 RID: 51495 RVA: 0x00494618 File Offset: 0x00492818
		public virtual int GetCooldownTurns(int Level)
		{
			return 20;
		}

		// Token: 0x0600C928 RID: 51496 RVA: 0x0049461C File Offset: 0x0049281C
		public virtual string GetDamage(int Level)
		{
			return Level.ToString() + "d4";
		}

		// Token: 0x0600C929 RID: 51497 RVA: 0x00494630 File Offset: 0x00492830
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandWaveformWorm")
			{
				if (this.ParentObject.OnWorldMap())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You cannot do that on the world map.", true, true, true);
					}
					return false;
				}
				Cell cell = base.PickDirection("Waveform Dash", null);
				if (cell == null)
				{
					return false;
				}
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_physical_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				base.UseEnergy(1000, "Physical Mutation Waveform");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldownTurns(base.Level), null, null, false);
				base.DidX("dash", "through a tunnel in spacetime", null, null, null, this.ParentObject, null, false, false, null, null, false, true, false, false, false, null);
				string directionFromCell = this.ParentObject.CurrentCell.GetDirectionFromCell(cell, false);
				List<Cell> list = new List<Cell>();
				List<Cell> list2 = new List<Cell>();
				Cell cell2 = null;
				Cell cell3 = this.ParentObject.Physics.CurrentCell;
				int num = 0;
				int i = 0;
				int range = this.GetRange(base.Level);
				while (i < range)
				{
					cell3 = cell3.GetCellFromDirection(directionFromCell, true);
					list2.Add(cell3);
					if (cell3 == null)
					{
						break;
					}
					if (cell3.IsEmpty())
					{
						cell2 = cell3;
						num = i;
						list.AddRange(list2);
						list2.Clear();
					}
					i++;
				}
				if (num > 0)
				{
					if (this.ParentObject.InActiveZone())
					{
						ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
						for (int j = 0; j <= num + 5; j++)
						{
							scrapBuffer.RenderBase();
							for (int k = j; k > j - 5; k--)
							{
								if (k > 0 && k < list.Count)
								{
									string text = null;
									if (k - j == 0)
									{
										text = "&K.";
									}
									if (k - j == -1)
									{
										text = "&wo";
									}
									if (k - j == -2)
									{
										text = "&wO";
									}
									if (k - j == -3)
									{
										text = "&yo";
									}
									if (k - j == -4)
									{
										text = "&K.";
									}
									if (text != null)
									{
										scrapBuffer.Goto(list[k].X, list[k].Y);
										scrapBuffer.Write(text, true, false, false, null, -1);
									}
								}
							}
							scrapBuffer.Draw();
							Thread.Sleep(10);
						}
					}
					string damage = this.GetDamage(base.Level);
					foreach (Cell cell4 in list)
					{
						foreach (GameObject gameObject in cell4.GetObjectsViaEventList())
						{
							if (gameObject != this.ParentObject)
							{
								gameObject.TakeDamage(damage.RollCached(), "from %t passage!", "Crushing", null, null, null, this.ParentObject, null, null, null, false, false, false, true, false, false, false, false, false, 5, null);
							}
						}
					}
					this.ParentObject.DirectMoveTo(cell2, 0, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C92A RID: 51498 RVA: 0x0049497C File Offset: 0x00492B7C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Waveform Dash", "CommandWaveformWorm", "Physical Mutations", this.GetDescription(), "~", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C92B RID: 51499 RVA: 0x004949CA File Offset: 0x00492BCA
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
