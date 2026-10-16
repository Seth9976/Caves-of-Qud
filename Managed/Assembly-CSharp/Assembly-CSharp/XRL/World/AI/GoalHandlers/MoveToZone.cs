using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using XRL.World.AI.Pathfinding;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B8D RID: 2957
	[Serializable]
	public class MoveToZone : IMovementGoal
	{
		// Token: 0x06007629 RID: 30249 RVA: 0x002C6591 File Offset: 0x002C4791
		public MoveToZone()
		{
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x002C65A1 File Offset: 0x002C47A1
		public MoveToZone(GlobalLocation Target, bool OverridesCombat = false, int MaxWeight = 95)
			: this()
		{
			this.Target = Target;
			this.OverridesCombat = OverridesCombat;
		}

		// Token: 0x0600762B RID: 30251 RVA: 0x002C65B7 File Offset: 0x002C47B7
		public MoveToZone(string ZoneID, bool OverridesCombat = false, int MaxTurns = -1, int MaxWeight = 95)
			: this(new GlobalLocation(), OverridesCombat, MaxWeight)
		{
			this.Target.ZoneID = ZoneID;
		}

		// Token: 0x0600762C RID: 30252 RVA: 0x002C65D3 File Offset: 0x002C47D3
		public override bool Finished()
		{
			return base.ParentObject.InZone(this.Target.ZoneID);
		}

		// Token: 0x0600762D RID: 30253 RVA: 0x002C65EC File Offset: 0x002C47EC
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
			if (this.Target == null)
			{
				base.Pop();
				return;
			}
			if (base.ParentObject.InZone(this.Target.ZoneID))
			{
				base.Pop();
				return;
			}
			base.ParentObject.UseEnergy(1000);
			int num = this.Target.ParasangX * 3 + this.Target.ZoneX;
			int num2 = this.Target.ParasangY * 3 + this.Target.ZoneY;
			int zoneZ = this.Target.ZoneZ;
			List<string> list = new List<string>();
			if (base.ParentObject.CurrentZone.wX * 3 + base.ParentObject.CurrentZone.X < num)
			{
				list.Add("E");
			}
			if (base.ParentObject.CurrentZone.wX * 3 + base.ParentObject.CurrentZone.X > num)
			{
				list.Add("W");
			}
			if (base.ParentObject.CurrentZone.wY * 3 + base.ParentObject.CurrentZone.Y < num2)
			{
				list.Add("S");
			}
			if (base.ParentObject.CurrentZone.wY * 3 + base.ParentObject.CurrentZone.Y > num2)
			{
				list.Add("N");
			}
			if (base.ParentObject.CurrentZone.Z < zoneZ)
			{
				GameObject gameObject = base.ParentObject.CurrentZone.FindClosestObjectWithPart(base.ParentObject, "StairsDown", false, true);
				if (gameObject != null)
				{
					base.ParentObject.Brain.PushGoal(new Step("D", false, this.OverridesCombat, false, false, null, true));
					base.ParentObject.Brain.PushGoal(new MoveTo(gameObject, false, false, 0, false, false, false, -1, -1, 95));
					return;
				}
			}
			if (base.ParentObject.CurrentZone.Z > zoneZ)
			{
				GameObject gameObject2 = base.ParentObject.CurrentZone.FindClosestObjectWithPart(base.ParentObject, "StairsUp", false, true);
				if (gameObject2 != null)
				{
					base.ParentObject.Brain.PushGoal(new Step("U", false, this.OverridesCombat, false, false, null, true));
					base.ParentObject.Brain.PushGoal(new MoveTo(gameObject2, false, false, 0, false, false, false, -1, -1, 95));
					return;
				}
			}
			List<ValueTuple<Cell, string>> list2 = new List<ValueTuple<Cell, string>>();
			this.FillExits(list, list2);
			if (list2.IsNullOrEmpty<ValueTuple<Cell, string>>() && (base.ParentObject.CurrentZone.Z != zoneZ || list.Count == 0))
			{
				list.Add("N");
				list.Add("S");
				list.Add("E");
				list.Add("W");
				this.FillExits(list, list2);
			}
			list2.ShuffleInPlace<ValueTuple<Cell, string>>(null);
			for (int i = 0; i < list2.Count; i++)
			{
				ValueTuple<Cell, string> valueTuple = list2[i];
				if (valueTuple.Item1.IsReachable())
				{
					FindPath findPath = new FindPath(base.ParentObject.CurrentZone.ZoneID, base.ParentObject.CurrentCell.X, base.ParentObject.CurrentCell.Y, base.ParentObject.CurrentZone.ZoneID, valueTuple.Item1.X, valueTuple.Item1.Y, true, false, base.ParentObject, false, false, false, false, this.MaxWeight);
					if (findPath.Usable)
					{
						findPath.Directions.Reverse();
						this.PushChildGoal(new Step(valueTuple.Item2, false, false, false, false, null, true));
						foreach (string text in findPath.Directions)
						{
							this.PushChildGoal(new Step(text, false, this.OverridesCombat, false, false, null, true));
						}
					}
					return;
				}
			}
		}

		// Token: 0x0600762E RID: 30254 RVA: 0x002C6A20 File Offset: 0x002C4C20
		public void FillExits(List<string> Directions, [TupleElementNames(new string[] { "Cell", "Dir" })] List<ValueTuple<Cell, string>> Exits)
		{
			Exits.Clear();
			if (Directions.Contains("N"))
			{
				for (int i = 0; i < base.CurrentZone.Width; i++)
				{
					Cell cell = base.CurrentZone.GetCell(i, 0);
					if (cell.IsReachable())
					{
						Exits.Add(new ValueTuple<Cell, string>(cell, "N"));
					}
				}
			}
			if (Directions.Contains("S"))
			{
				for (int j = 0; j < base.CurrentZone.Width; j++)
				{
					Cell cell2 = base.CurrentZone.GetCell(j, base.CurrentZone.Height - 1);
					if (cell2.IsReachable())
					{
						Exits.Add(new ValueTuple<Cell, string>(cell2, "S"));
					}
				}
			}
			if (Directions.Contains("W"))
			{
				for (int k = 0; k < base.CurrentZone.Height; k++)
				{
					Cell cell3 = base.CurrentZone.GetCell(0, k);
					if (cell3.IsReachable())
					{
						Exits.Add(new ValueTuple<Cell, string>(cell3, "W"));
					}
				}
			}
			if (Directions.Contains("E"))
			{
				for (int l = 0; l < base.CurrentZone.Height; l++)
				{
					Cell cell4 = base.CurrentZone.GetCell(base.CurrentZone.Width - 1, l);
					if (cell4.IsReachable())
					{
						Exits.Add(new ValueTuple<Cell, string>(cell4, "E"));
					}
				}
			}
		}

		// Token: 0x04003623 RID: 13859
		public const int DEFAULT_MAX_WEIGHT = 95;

		// Token: 0x04003624 RID: 13860
		private GlobalLocation Target;

		// Token: 0x04003625 RID: 13861
		private int Tries;

		// Token: 0x04003626 RID: 13862
		public bool OverridesCombat;

		// Token: 0x04003627 RID: 13863
		public int MaxWeight = 95;
	}
}
