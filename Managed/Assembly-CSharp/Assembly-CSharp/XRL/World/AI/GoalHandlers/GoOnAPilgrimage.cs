using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Collections;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B75 RID: 2933
	[Serializable]
	public class GoOnAPilgrimage : GoalHandler
	{
		// Token: 0x06007583 RID: 30083 RVA: 0x002C19FA File Offset: 0x002BFBFA
		public GoOnAPilgrimage()
		{
		}

		// Token: 0x06007584 RID: 30084 RVA: 0x002C1A24 File Offset: 0x002BFC24
		public GoOnAPilgrimage(int Wx, int Wy, int Xx, int Yx, int Zx, string targetObject, string zoneID, string entranceZoneID)
		{
			GoOnAPilgrimage.TargetWx = Wx;
			GoOnAPilgrimage.TargetWy = Wy;
			GoOnAPilgrimage.TargetXx = Xx;
			GoOnAPilgrimage.TargetYx = Yx;
			GoOnAPilgrimage.TargetZx = Zx;
			this.TargetObject = targetObject;
			this.TargetZoneID = zoneID;
			this.TargetEntranceZoneID = entranceZoneID;
		}

		// Token: 0x06007585 RID: 30085 RVA: 0x002C1A90 File Offset: 0x002BFC90
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x002C1A93 File Offset: 0x002BFC93
		public override void Create()
		{
		}

		// Token: 0x06007587 RID: 30087 RVA: 0x002C1A98 File Offset: 0x002BFC98
		public void MoveToTargetZone()
		{
			if (!(base.CurrentZone.ZoneID == this.TargetEntranceZoneID))
			{
				this.ParentBrain.PushGoal(new MoveToGlobal(this.TargetEntranceZoneID, 37 + Stat.Random(0, 3), 0));
				return;
			}
			if (base.CurrentCell.X == 38 && base.CurrentCell.Y == 0)
			{
				this.ParentBrain.PushGoal(new Step("N", false, false, false, false, null, false));
				return;
			}
			this.ParentBrain.PushGoal(new MoveTo(this.TargetEntranceZoneID, 37 + Stat.Random(0, 3), 0, false, false, 0, false, false, false, -1, -1, 95));
		}

		// Token: 0x06007588 RID: 30088 RVA: 0x002C1B48 File Offset: 0x002BFD48
		private bool HasAdjacentObject(string Blueprint)
		{
			foreach (Cell cell in base.ParentObject.CurrentCell.GetLocalAdjacentCells())
			{
				using (Container<GameObject>.Enumerator enumerator2 = cell.Objects.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Blueprint == Blueprint)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x002C1BEC File Offset: 0x002BFDEC
		public void MoveToWell()
		{
			if (this.TargetObject == null)
			{
				this.TargetObject = base.CurrentZone.GetObjectsWithPart("Brain").GetRandomElement<GameObject>(null).GetBlueprint(true)
					.Name;
			}
			if (this.HasAdjacentObject(this.TargetObject))
			{
				base.ParentObject.GetPart<AIPilgrim>().FoundTarget = true;
				base.Pop();
				this.ParentBrain.PushGoal(new WanderRandomly(6));
				return;
			}
			GameObject gameObject = base.CurrentZone.FindObjectExcludingSelf(this.TargetObject, base.ParentObject);
			if (gameObject != null)
			{
				List<Cell> localAdjacentCells = gameObject.Physics.CurrentCell.GetLocalAdjacentCells();
				Algorithms.RandomShuffleInPlace<Cell>(localAdjacentCells, Stat.Rand);
				for (int i = 0; i < localAdjacentCells.Count; i++)
				{
					if (localAdjacentCells[i].IsEmpty())
					{
						this.ParentBrain.PushGoal(new MoveTo(base.CurrentZone.ZoneID, localAdjacentCells[i].X, localAdjacentCells[i].Y, false, false, 0, false, false, false, -1, -1, 95));
						return;
					}
				}
				this.ParentBrain.PushGoal(new WanderRandomly(6));
				return;
			}
			base.FailToParent();
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x002C1D14 File Offset: 0x002BFF14
		public override void TakeAction()
		{
			if (base.ParentObject.HasEffect<Lost>() || (base.ParentObject.HasEffect<Confused>() && !base.ParentObject.HasEffect<FuriouslyConfused>()))
			{
				base.FailToParent();
				return;
			}
			if (base.ParentObject.InZone(this.TargetZoneID))
			{
				this.MoveToWell();
				return;
			}
			this.MoveToTargetZone();
		}

		// Token: 0x040035CB RID: 13771
		public static int TargetWx = 5;

		// Token: 0x040035CC RID: 13772
		public static int TargetWy = 2;

		// Token: 0x040035CD RID: 13773
		public static int TargetXx = 1;

		// Token: 0x040035CE RID: 13774
		public static int TargetYx = 1;

		// Token: 0x040035CF RID: 13775
		public static int TargetZx = 10;

		// Token: 0x040035D0 RID: 13776
		public string TargetObject = "StiltWell";

		// Token: 0x040035D1 RID: 13777
		public string TargetZoneID = "JoppaWorld.5.2.1.1.10";

		// Token: 0x040035D2 RID: 13778
		public string TargetEntranceZoneID = "JoppaWorld.5.2.1.2.10";
	}
}
