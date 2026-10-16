using System;
using XRL.Rules;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B96 RID: 2966
	[Serializable]
	public class ClonelingGoal : GoalHandler
	{
		// Token: 0x06007666 RID: 30310 RVA: 0x002C8091 File Offset: 0x002C6291
		public ClonelingGoal(GameObject Target)
		{
			this.Target = Target;
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x002C80A0 File Offset: 0x002C62A0
		public override void Create()
		{
			base.Think("I'm trying to clone someone!");
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x002C80AD File Offset: 0x002C62AD
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x002C80B0 File Offset: 0x002C62B0
		public override bool Finished()
		{
			return this.Done;
		}

		// Token: 0x0600766A RID: 30314 RVA: 0x002C80B8 File Offset: 0x002C62B8
		public override void TakeAction()
		{
			Cloneling part = base.ParentObject.GetPart<Cloneling>();
			if (part == null)
			{
				base.Think("I'm not a cloneling any more!");
				base.FailToParent();
				return;
			}
			if (part.ClonesLeft <= 0)
			{
				base.Think("I'm out of cloning draught!");
				base.FailToParent();
				return;
			}
			if (this.Target == null)
			{
				base.Think("I don't have a target anymore!");
				base.FailToParent();
				return;
			}
			if (!GameObject.Validate(this.Target))
			{
				this.Target = null;
				base.Think("My target has been destroyed!");
				base.FailToParent();
				return;
			}
			if (!part.IsMyActivatedAbilityAIUsable(part.ActivatedAbilityID, null))
			{
				this.Target = null;
				base.Think("My cloning ability is no longer usable!");
				base.FailToParent();
				return;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				this.Target = null;
				base.Think("I no longer have a location!");
				base.FailToParent();
				return;
			}
			Cell currentCell2 = this.Target.CurrentCell;
			if (currentCell2 == null || currentCell2.ParentZone.ZoneID == null)
			{
				this.Target = null;
				base.Think("My target no longer exists!");
				base.FailToParent();
				return;
			}
			int num = currentCell2.PathDistanceTo(currentCell);
			if (num > 80 || this.Target.OnWorldMap())
			{
				this.LastSeen++;
				if (this.LastSeen > 5)
				{
					base.Think("I can't find my target...");
					this.Target = null;
					base.FailToParent();
					return;
				}
			}
			if (base.ParentObject.Brain.Staying && num > 1)
			{
				this.Target = null;
				base.Think("I'm staying put and I'd have to move to reach my target.");
				base.FailToParent();
				return;
			}
			if (!part.CanBeCloned(this.Target))
			{
				this.Target = null;
				base.Think("I can no longer clone my target!");
				base.FailToParent();
				return;
			}
			if (num == 1)
			{
				base.Think("I'm going to clone my target!");
				if (part.PerformCloning(this.Target))
				{
					this.Done = true;
					return;
				}
				base.FailToParent();
				return;
			}
			else
			{
				if (!base.ParentObject.IsMobile())
				{
					base.ParentObject.UseEnergy(1000);
					base.Think("My target is too far and I'm immobile.");
					base.FailToParent();
					return;
				}
				base.Think("I'm going to move towards my target.");
				FindPath findPath = new FindPath(currentCell, currentCell2, this.Target.IsPlayer(), true, base.ParentObject, 100, false, false, false, false, false, null);
				if (findPath.Usable)
				{
					for (int i = Math.Min(Stat.Random(0, 8), findPath.Directions.Count - 2); i >= 0; i--)
					{
						this.PushChildGoal(new Step(findPath.Directions[i], false, false, false, false, null, false));
					}
					return;
				}
				base.FailToParent();
				return;
			}
		}

		// Token: 0x04003643 RID: 13891
		public GameObject Target;

		// Token: 0x04003644 RID: 13892
		public bool Done;

		// Token: 0x04003645 RID: 13893
		private int LastSeen;
	}
}
