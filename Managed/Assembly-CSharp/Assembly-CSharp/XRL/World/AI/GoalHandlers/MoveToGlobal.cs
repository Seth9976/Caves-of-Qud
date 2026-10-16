using System;
using System.Collections.Generic;
using XRL.World.AI.Pathfinding;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B8B RID: 2955
	[Serializable]
	public class MoveToGlobal : IMovementGoal
	{
		// Token: 0x0600761E RID: 30238 RVA: 0x002C5E2B File Offset: 0x002C402B
		public MoveToGlobal()
		{
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x002C5E42 File Offset: 0x002C4042
		public MoveToGlobal(string ZoneID, int Cx, int Cy)
			: this()
		{
			this.dZone = ZoneID;
			this.dCx = Cx;
			this.dCy = Cy;
		}

		// Token: 0x06007620 RID: 30240 RVA: 0x002C5E5F File Offset: 0x002C405F
		public MoveToGlobal(string ZoneID, int Cx, int Cy, int Turns)
			: this(ZoneID, Cx, Cy)
		{
			this.MaxTurns = Turns;
		}

		// Token: 0x06007621 RID: 30241 RVA: 0x002C5E72 File Offset: 0x002C4072
		public MoveToGlobal(GlobalLocation Target)
			: this(Target.ZoneID, Target.CellX, Target.CellY)
		{
		}

		// Token: 0x06007622 RID: 30242 RVA: 0x002C5E8C File Offset: 0x002C408C
		public MoveToGlobal(GlobalLocation Target, int Turns)
			: this(Target.ZoneID, Target.CellX, Target.CellY, Turns)
		{
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x002C5EA7 File Offset: 0x002C40A7
		public override bool Finished()
		{
			return this.Tries > 0;
		}

		// Token: 0x06007624 RID: 30244 RVA: 0x002C5EB4 File Offset: 0x002C40B4
		public override void TakeAction()
		{
			this.Tries++;
			if (!base.ParentObject.IsMobile())
			{
				base.FailToParent();
				return;
			}
			if (base.ParentObject.CurrentZone.ZoneID == null)
			{
				base.Pop();
				return;
			}
			if (this.dZone == null)
			{
				base.Pop();
				return;
			}
			if (base.ParentObject.InZone(this.dZone))
			{
				base.ParentObject.UseEnergy(1000);
				FindPath findPath = new FindPath(base.ParentObject.CurrentZone.ZoneID, base.ParentObject.CurrentCell.X, base.ParentObject.CurrentCell.Y, this.dZone, this.dCx, this.dCy, true, false, base.ParentObject, false, false, false, false, this.MaxWeight);
				if (findPath.Usable)
				{
					findPath.Directions.Reverse();
					int num = 0;
					if (this.MaxTurns > -1)
					{
						base.Pop();
					}
					using (List<string>.Enumerator enumerator = findPath.Directions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							this.PushGoal(new Step(text, false, false, false, false, null, true));
							num++;
							if (this.MaxTurns > -1 && num >= this.MaxTurns)
							{
								break;
							}
						}
						return;
					}
				}
				base.FailToParent();
				return;
			}
			base.ParentObject.UseEnergy(1000);
			string[] array = this.dZone.Split('.', StringSplitOptions.None);
			int num2 = Convert.ToInt32(array[1]) * 3 + Convert.ToInt32(array[3]);
			int num3 = Convert.ToInt32(array[2]) * 3 + Convert.ToInt32(array[4]);
			int num4 = Convert.ToInt32(array[5]);
			List<string> list = new List<string>();
			if (base.ParentObject.CurrentZone.wX * 3 + base.ParentObject.CurrentZone.X < num2)
			{
				list.Add("E");
			}
			if (base.ParentObject.CurrentZone.wX * 3 + base.ParentObject.CurrentZone.X > num2)
			{
				list.Add("W");
			}
			if (base.ParentObject.CurrentZone.wY * 3 + base.ParentObject.CurrentZone.Y < num3)
			{
				list.Add("S");
			}
			if (base.ParentObject.CurrentZone.wY * 3 + base.ParentObject.CurrentZone.Y > num3)
			{
				list.Add("N");
			}
			if (base.ParentObject.CurrentZone.Z < num4)
			{
				GameObject gameObject = base.ParentObject.CurrentZone.FindClosestObjectWithPart(base.ParentObject, "StairsDown", false, true);
				if (gameObject != null)
				{
					base.ParentObject.Brain.PushGoal(new Step("D", false, this.OverridesCombat, false, false, null, true));
					base.ParentObject.Brain.PushGoal(new MoveTo(gameObject, false, this.OverridesCombat, 0, false, false, false, -1, -1, 95));
					return;
				}
			}
			if (base.ParentObject.CurrentZone.Z > num4)
			{
				GameObject gameObject2 = base.ParentObject.CurrentZone.FindClosestObjectWithPart(base.ParentObject, "StairsUp", false, true);
				if (gameObject2 != null)
				{
					base.ParentObject.Brain.PushGoal(new Step("U", false, this.OverridesCombat, false, false, null, true));
					base.ParentObject.Brain.PushGoal(new MoveTo(gameObject2, false, this.OverridesCombat, 0, false, false, false, -1, -1, 95));
					return;
				}
			}
			if (base.ParentObject.CurrentZone.Z != num4 || list.Count == 0)
			{
				list.Add("N");
				list.Add("S");
				list.Add("E");
				list.Add("W");
			}
			string text2 = list[new Random().Next(0, list.Count - 1)];
			List<Cell> list2 = new List<Cell>();
			if (text2 == "N")
			{
				for (int i = 0; i < base.CurrentZone.Width; i++)
				{
					list2.Add(base.CurrentZone.GetCell(i, 0));
				}
			}
			if (text2 == "S")
			{
				for (int j = 0; j < base.CurrentZone.Width; j++)
				{
					list2.Add(base.CurrentZone.GetCell(j, base.CurrentZone.Height - 1));
				}
			}
			if (text2 == "W")
			{
				for (int k = 0; k < base.CurrentZone.Height; k++)
				{
					list2.Add(base.CurrentZone.GetCell(0, k));
				}
			}
			if (text2 == "E")
			{
				for (int l = 0; l < base.CurrentZone.Height; l++)
				{
					list2.Add(base.CurrentZone.GetCell(base.CurrentZone.Width - 1, l));
				}
			}
			list2.ShuffleInPlace<Cell>(null);
			for (int m = 0; m < list2.Count; m++)
			{
				if (list2[m].IsReachable())
				{
					FindPath findPath2 = new FindPath(base.ParentObject.CurrentCell, list2[m], true, true, base.ParentObject, this.MaxWeight, false, false, false, false, false, null);
					if (findPath2.Usable)
					{
						findPath2.Directions.Reverse();
						this.PushChildGoal(new Step(text2, false, this.OverridesCombat, false, false, null, true));
						foreach (string text3 in findPath2.Directions)
						{
							this.PushChildGoal(new Step(text3, false, this.OverridesCombat, false, false, null, true));
						}
					}
					return;
				}
			}
		}

		// Token: 0x04003619 RID: 13849
		public const int DEFAULT_MAX_WEIGHT = 95;

		// Token: 0x0400361A RID: 13850
		private string dZone;

		// Token: 0x0400361B RID: 13851
		private int dCx;

		// Token: 0x0400361C RID: 13852
		private int dCy;

		// Token: 0x0400361D RID: 13853
		private int Tries;

		// Token: 0x0400361E RID: 13854
		private int MaxTurns = -1;

		// Token: 0x0400361F RID: 13855
		public bool OverridesCombat;

		// Token: 0x04003620 RID: 13856
		public int MaxWeight = 95;
	}
}
