using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;
using XRL.Rules;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B9A RID: 2970
	[Serializable]
	public class DropOffStolenGoods : GoalHandler
	{
		// Token: 0x06007684 RID: 30340 RVA: 0x002C8B6F File Offset: 0x002C6D6F
		public override bool Finished()
		{
			return this.ParentBrain.ParentObject.Inventory.Objects.Count == 0;
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x002C8B8E File Offset: 0x002C6D8E
		public override void Create()
		{
		}

		// Token: 0x06007686 RID: 30342 RVA: 0x002C8B90 File Offset: 0x002C6D90
		public void MoveToDropoff()
		{
			List<GameObject> list = base.CurrentZone.FindObjects(this.TargetObject);
			list.Sort((GameObject a, GameObject b) => a.DistanceTo(base.ParentObject).CompareTo(b.DistanceTo(base.ParentObject)));
			GameObject gameObject = list.FirstOrDefault<GameObject>();
			if (gameObject != null)
			{
				if (gameObject.DistanceTo(this.ParentBrain.ParentObject) <= 1 && this.ParentBrain.ParentObject.IsVisible())
				{
					GameObject randomElement = this.ParentBrain.ParentObject.Inventory.Objects.GetRandomElement<GameObject>(null);
					if (randomElement != null)
					{
						base.ParentObject.Inventory.RemoveObject(randomElement, null);
						gameObject.CurrentCell.AddObject(randomElement, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						GoalHandler.AddPlayerMessage(string.Concat(new string[]
						{
							base.ParentObject.Does("drop", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" ",
							randomElement.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" down ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							"."
						}), 'W', true);
					}
				}
				List<Cell> localAdjacentCells = gameObject.CurrentCell.GetLocalAdjacentCells(1, false);
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

		// Token: 0x06007687 RID: 30343 RVA: 0x002C8D8D File Offset: 0x002C6F8D
		public override void TakeAction()
		{
			this.MoveToDropoff();
		}

		// Token: 0x0400364C RID: 13900
		public string TargetObject = "OpenShaft";
	}
}
