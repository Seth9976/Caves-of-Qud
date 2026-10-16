using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E71 RID: 3697
	[Serializable]
	public class FeralLahPod : IPart
	{
		// Token: 0x06009555 RID: 38229 RVA: 0x00360451 File Offset: 0x0035E651
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == BeforeRenderEvent.ID && this.exploding) || ID == GetDifficultyEvaluationEvent.ID;
		}

		// Token: 0x06009556 RID: 38230 RVA: 0x00360478 File Offset: 0x0035E678
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.exploding)
			{
				int num = XRLCore.CurrentFrame % 18 / 6;
				if (num == 0)
				{
					this.ParentObject.Render.TileColor = "&R^r";
					this.ParentObject.Render.DetailColor = "W";
					this.ParentObject.Render.ColorString = "&R^r";
				}
				if (num == 1)
				{
					this.ParentObject.Render.TileColor = "&W^R";
					this.ParentObject.Render.DetailColor = "r";
					this.ParentObject.Render.ColorString = "&W^R";
				}
				if (num == 2)
				{
					this.ParentObject.Render.TileColor = "&r^W";
					this.ParentObject.Render.DetailColor = "R";
					this.ParentObject.Render.ColorString = "&r^W";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009557 RID: 38231 RVA: 0x00360568 File Offset: 0x0035E768
		public override bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			if (GameObject.Validate(E.Actor))
			{
				E.MinimumRating(5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009558 RID: 38232 RVA: 0x00360585 File Offset: 0x0035E785
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDie");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("EndAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009559 RID: 38233 RVA: 0x003605B0 File Offset: 0x0035E7B0
		public void Explode()
		{
			if (this.blowingup)
			{
				return;
			}
			if (this.ParentObject.HasStat("XP") && !this.ParentObject.IsHero())
			{
				this.ParentObject.GetStat("XPValue").BaseValue = 0;
			}
			this.blowingup = true;
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell != null)
			{
				List<Cell> list = new List<Cell>();
				currentCell.GetAdjacentCells(2, list, true, true);
				List<Cell> list2 = new List<Cell>();
				foreach (Cell cell in list)
				{
					if (this.ParentObject.HasUnobstructedLineTo(cell, false, false))
					{
						list2.Add(cell);
					}
				}
				if (currentCell.ParentZone.IsActive())
				{
					for (int i = 0; i < 3; i++)
					{
						TextConsole textConsole = Look._TextConsole;
						ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
						XRLCore.Core.RenderMapToBuffer(scrapBuffer);
						foreach (Cell cell2 in list2)
						{
							if (cell2.PathDistanceTo(currentCell) == i && cell2.IsVisible())
							{
								scrapBuffer.Goto(cell2.X, cell2.Y);
								if (Stat.RandomCosmetic(1, 2) == 1)
								{
									scrapBuffer.Write("&R*", true, false, false, null, -1);
								}
								else
								{
									scrapBuffer.Write("&W*", true, false, false, null, -1);
								}
							}
						}
						textConsole.DrawBuffer(scrapBuffer, null, false);
						Thread.Sleep(10);
					}
				}
				foreach (Cell cell3 in list2)
				{
					foreach (GameObject gameObject in cell3.GetObjectsInCell())
					{
						if (gameObject.PhaseMatches(this.ParentObject))
						{
							gameObject.TakeDamage(this.damage.RollCached(), "from %t explosion!", "Explosion", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
						}
					}
				}
			}
			this.ParentObject.Destroy(null, false, false, null);
		}

		// Token: 0x0600955A RID: 38234 RVA: 0x0036082C File Offset: 0x0035EA2C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDie")
			{
				this.Explode();
			}
			else if (E.ID == "BeginTakeAction")
			{
				if (XRLCore.Core.Calm)
				{
					return true;
				}
				if (this.ParentObject.Brain.Target != null && this.ParentObject.Brain.Target.DistanceTo(this.ParentObject) <= 1)
				{
					this.exploding = true;
				}
				if (this.exploding)
				{
					this.countdown--;
					if (this.countdown <= 0)
					{
						this.Explode();
					}
					return false;
				}
			}
			else if (E.ID == "EndAction")
			{
				if (XRLCore.Core.Calm)
				{
					return true;
				}
				if (this.ParentObject.Brain.Target != null && this.ParentObject.Brain.Target.DistanceTo(this.ParentObject) <= 1)
				{
					this.exploding = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DD7 RID: 15831
		public string damage = "1d10";

		// Token: 0x04003DD8 RID: 15832
		public bool exploding;

		// Token: 0x04003DD9 RID: 15833
		public int countdown = 2;

		// Token: 0x04003DDA RID: 15834
		public bool blowingup;
	}
}
