using System;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B84 RID: 2948
	[Serializable]
	public class Guard : GoalHandler
	{
		// Token: 0x060075F0 RID: 30192 RVA: 0x002C3D39 File Offset: 0x002C1F39
		public override void Create()
		{
			base.Think("I'm guarding this place.");
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x002C3D46 File Offset: 0x002C1F46
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x002C3D4C File Offset: 0x002C1F4C
		public override void TakeAction()
		{
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			GameObject player = The.Player;
			if (((player != null) ? player.CurrentCell : null) == null)
			{
				return;
			}
			base.Think("I'm guarding this place.");
			if (this.ParentBrain.PartyLeader != null)
			{
				if (this.ParentBrain.GoToPartyLeader())
				{
					return;
				}
				if (this.ParentBrain.Target == null && currentCell.ParentZone.IsActive())
				{
					GameObject gameObject = this.ParentBrain.FindProspectiveTarget(currentCell, false);
					if (gameObject != null)
					{
						this.ParentBrain.WantToKill(gameObject, "to guard this place for my leader", false);
						if (this.ParentBrain.Target == null)
						{
							return;
						}
					}
				}
				if (base.ParentObject.IsMobile())
				{
					if (this.ParentBrain.Staying)
					{
						if (!this.ParentBrain.Wanders && !this.ParentBrain.WandersRandomly && this.ParentBrain.StartingCell != null && !this.ParentBrain.StartingCell.Equals(base.ParentObject.CurrentCell))
						{
							this.PushChildGoal(new MoveTo(this.ParentBrain.StartingCell, true, false, 0, false, false, false, -1, -1, 95));
						}
						else
						{
							this.ParentBrain.ParentObject.UseEnergy(1000);
						}
					}
					else
					{
						int num = 5;
						if (this.ParentBrain.PartyLeader.IsPlayer())
						{
							num = 1;
						}
						if (this.ParentBrain.ParentObject.DistanceTo(this.ParentBrain.PartyLeader) > num)
						{
							Cell currentCell2 = base.ParentObject.CurrentCell;
							Cell currentCell3 = this.ParentBrain.PartyLeader.CurrentCell;
							FindPath findPath = new FindPath(currentCell2, currentCell3, false, true, this.ParentBrain.ParentObject, 95, false, false, false, false, false, null);
							if (findPath.Usable)
							{
								int num2 = Stat.Random(1, 5);
								foreach (string text in findPath.Directions)
								{
									this.PushChildGoal(new Step(text, false, false, false, false, null, false));
									num2--;
									if (num2 <= 0)
									{
										return;
									}
								}
							}
						}
					}
				}
				this.ParentBrain.ParentObject.UseEnergy(1000);
				return;
			}
			else
			{
				if (currentCell.ParentZone.IsActive() && this.ParentBrain.Target == null)
				{
					GameObject gameObject2 = this.ParentBrain.FindProspectiveTarget(currentCell, false);
					if (gameObject2 != null)
					{
						this.ParentBrain.WantToKill(gameObject2, "to guard this place", false);
						if (this.ParentBrain.Target != null)
						{
							return;
						}
					}
				}
				this.ParentBrain.ParentObject.UseEnergy(1000);
			}
		}
	}
}
