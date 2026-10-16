using System;
using System.Text;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B91 RID: 2961
	[Serializable]
	public class Step : IMovementGoal
	{
		// Token: 0x06007640 RID: 30272 RVA: 0x002C74F4 File Offset: 0x002C56F4
		public Step()
		{
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x002C74FC File Offset: 0x002C56FC
		public Step(string Direction, bool careful = false, bool overridesCombat = false, bool wandering = false, bool juggernaut = false, GameObject Toward = null, bool allowUnbuilt = false)
			: this()
		{
			this.Dir = Direction;
			this.careful = careful;
			this.overridesCombat = overridesCombat;
			this.wandering = wandering;
			this.juggernaut = juggernaut;
			this.Toward = Toward;
			this.allowUnbuilt = allowUnbuilt;
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x002C753C File Offset: 0x002C573C
		public override string GetDetails()
		{
			Step.detailBuilder.Clear().Append(this.Dir);
			if (this.careful)
			{
				Step.detailBuilder.Append('.');
			}
			if (this.allowUnbuilt)
			{
				Step.detailBuilder.Append('+');
			}
			if (this.overridesCombat)
			{
				Step.detailBuilder.Append('!');
			}
			if (this.wandering)
			{
				Step.detailBuilder.Append('?');
			}
			if (this.juggernaut)
			{
				Step.detailBuilder.Append('*');
			}
			return Step.detailBuilder.ToString();
		}

		// Token: 0x06007643 RID: 30275 RVA: 0x002C75D2 File Offset: 0x002C57D2
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x06007644 RID: 30276 RVA: 0x002C75D5 File Offset: 0x002C57D5
		public override bool CanFight()
		{
			return !this.overridesCombat;
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x002C75E0 File Offset: 0x002C57E0
		private bool CellHasHostile(Cell TargetCell)
		{
			if (TargetCell == null)
			{
				return false;
			}
			if (this.ParentBrain == null)
			{
				return false;
			}
			int i = 0;
			int count = TargetCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = TargetCell.Objects[i];
				if (gameObject.IsCombat && this.ParentBrain.IsHostileTowards(gameObject) && base.ParentObject.PhaseAndFlightMatches(gameObject))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06007646 RID: 30278 RVA: 0x002C7648 File Offset: 0x002C5848
		private bool CellHasLedBySame(Cell TargetCell, ref GameObject Skip)
		{
			if (TargetCell == null)
			{
				return false;
			}
			Brain parentBrain = this.ParentBrain;
			GameObject gameObject = ((parentBrain != null) ? parentBrain.GetFinalLeader() : null);
			if (gameObject == null)
			{
				return false;
			}
			int i = 0;
			int count = TargetCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject2 = TargetCell.Objects[i];
				if (gameObject2 != Skip && gameObject2.IsLedBy(gameObject))
				{
					if (base.ParentObject.PhaseAndFlightMatches(gameObject2) && gameObject2.IsPotentiallyMobile())
					{
						return true;
					}
					Skip = gameObject2;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x002C76C4 File Offset: 0x002C58C4
		private bool CellHasSomeoneMobileWithSameTarget(Cell TargetCell, ref GameObject Skip)
		{
			if (TargetCell == null)
			{
				return false;
			}
			GameObject parentObject = base.ParentObject;
			GameObject gameObject = ((parentObject != null) ? parentObject.Target : null);
			if (gameObject == null)
			{
				return false;
			}
			int i = 0;
			int count = TargetCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject2 = TargetCell.Objects[i];
				if (gameObject2 != Skip && gameObject2.Target == gameObject)
				{
					if (base.ParentObject.PhaseAndFlightMatches(gameObject2) && gameObject2.IsPotentiallyMobile())
					{
						return true;
					}
					Skip = gameObject2;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x002C7740 File Offset: 0x002C5940
		private bool CellHasMobileAlly(Cell TargetCell, ref GameObject Skip)
		{
			if (TargetCell == null)
			{
				return false;
			}
			int i = 0;
			int count = TargetCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = TargetCell.Objects[i];
				if (gameObject != Skip && gameObject.IsAlliedTowards(base.ParentObject))
				{
					if (base.ParentObject.PhaseAndFlightMatches(gameObject) && gameObject.IsPotentiallyMobile())
					{
						return true;
					}
					Skip = gameObject;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06007649 RID: 30281 RVA: 0x002C77A8 File Offset: 0x002C59A8
		private bool CellHasMobileNonEnemy(Cell TargetCell, ref GameObject Skip)
		{
			if (TargetCell == null || base.ParentObject == null)
			{
				return false;
			}
			int i = 0;
			int count = TargetCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = TargetCell.Objects[i];
				if (gameObject != Skip && !this.ParentBrain.IsHostileTowards(gameObject))
				{
					if (base.ParentObject.PhaseAndFlightMatches(gameObject) && gameObject.IsPotentiallyMobile())
					{
						return true;
					}
					Skip = gameObject;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600764A RID: 30282 RVA: 0x002C7818 File Offset: 0x002C5A18
		public override void TakeAction()
		{
			if (!base.ParentObject.IsMobile())
			{
				base.Think("I can't move!");
				base.FailToParent();
				return;
			}
			if (this.Toward != null && (!this.Toward.IsValid() || this.Toward.IsNowhere()))
			{
				base.Think("I was trying to go toward someone, but they're gone!");
				base.FailToParent();
				return;
			}
			Cell cellFromDirection = base.ParentObject.CurrentCell.GetCellFromDirection(this.Dir, !this.allowUnbuilt);
			if (cellFromDirection == null)
			{
				base.Think("I can't move there!");
				base.FailToParent();
				return;
			}
			if (this.wandering && cellFromDirection.HasObjectWithTagOrProperty("WanderStopper"))
			{
				base.Think("I shouldn't wander there!");
				base.FailToParent();
				return;
			}
			if (this.careful && cellFromDirection.GetNavigationWeightFor(base.ParentObject, false, false, false, false, false) >= 10)
			{
				base.Think("That's too dangerous!");
				base.FailToParent();
				return;
			}
			if (!this.juggernaut)
			{
				if (this.CellHasHostile(cellFromDirection) || (cellFromDirection == The.Player.CurrentCell && base.ParentObject.PhaseAndFlightMatches(The.Player)))
				{
					base.Think("There's something in my way!");
					base.FailToParent();
					return;
				}
				int num = 100;
				GameObject gameObject = null;
				if (this.CellHasSomeoneMobileWithSameTarget(cellFromDirection, ref gameObject))
				{
					num = 2;
				}
				else if (this.CellHasLedBySame(cellFromDirection, ref gameObject))
				{
					num = 3;
				}
				else if (this.CellHasMobileAlly(cellFromDirection, ref gameObject))
				{
					num = 5;
				}
				else if (this.CellHasMobileNonEnemy(cellFromDirection, ref gameObject))
				{
					num = 30;
				}
				if (!num.in100())
				{
					base.Think("There's something in my way!");
					base.FailToParent();
					return;
				}
			}
			try
			{
				if (base.ParentObject != null && !base.ParentObject.Move(this.Dir, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0))
				{
					if (base.ParentObject.GetIntProperty("AIKeepMoving", 0) > 0)
					{
						base.ParentObject.SetIntProperty("AIKeepMoving", 0, false);
					}
					else
					{
						base.FailToParent();
					}
					return;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Step::MoveDirection", ex, "game_exception");
			}
			base.Pop();
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x002C7A40 File Offset: 0x002C5C40
		public Cell GetDestinationCell()
		{
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				Cell cellFromDirection = currentCell.GetCellFromDirection(this.Dir, true);
				if (cellFromDirection != null)
				{
					return cellFromDirection;
				}
			}
			return null;
		}

		// Token: 0x04003631 RID: 13873
		public string Dir;

		// Token: 0x04003632 RID: 13874
		public bool allowUnbuilt;

		// Token: 0x04003633 RID: 13875
		public bool careful;

		// Token: 0x04003634 RID: 13876
		public bool overridesCombat;

		// Token: 0x04003635 RID: 13877
		public bool wandering;

		// Token: 0x04003636 RID: 13878
		public bool juggernaut;

		// Token: 0x04003637 RID: 13879
		public GameObject Toward;

		// Token: 0x04003638 RID: 13880
		[NonSerialized]
		private static StringBuilder detailBuilder = new StringBuilder();
	}
}
