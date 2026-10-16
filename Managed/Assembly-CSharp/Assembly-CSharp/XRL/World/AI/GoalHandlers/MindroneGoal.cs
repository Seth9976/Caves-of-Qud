using System;
using XRL.World.AI.Pathfinding;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B99 RID: 2969
	[Serializable]
	public class MindroneGoal : GoalHandler
	{
		// Token: 0x0600767D RID: 30333 RVA: 0x002C8954 File Offset: 0x002C6B54
		public MindroneGoal()
		{
		}

		// Token: 0x0600767E RID: 30334 RVA: 0x002C895C File Offset: 0x002C6B5C
		public MindroneGoal(GameObject Target)
			: this()
		{
			this.Target = Target;
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x002C896B File Offset: 0x002C6B6B
		public override void Create()
		{
			base.Think("I'm trying to heal " + this.Target.Blueprint + "!");
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x002C898D File Offset: 0x002C6B8D
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x002C8990 File Offset: 0x002C6B90
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x06007682 RID: 30338 RVA: 0x002C8994 File Offset: 0x002C6B94
		public override void TakeAction()
		{
			if (this.Target == null)
			{
				base.Think("I don't have a target anymore!");
				base.FailToParent();
				return;
			}
			if (this.Target.IsInvalid())
			{
				this.Target = null;
				base.Think("My target has been destroyed!");
				base.FailToParent();
				return;
			}
			if (this.Target.IsNowhere())
			{
				base.Think("My target is dead!");
				this.Target = null;
				base.FailToParent();
				return;
			}
			int num = this.Target.DistanceTo(base.ParentObject);
			if (num > 80)
			{
				this.LastSeen++;
			}
			if (this.LastSeen > 5)
			{
				base.Think("I can't find my target...");
				this.Target = null;
				base.FailToParent();
				return;
			}
			if (num <= 1)
			{
				base.Think("I'm going to heal my target!");
				this.Target.Sparksplatter();
				this.Target.Heal(3, true, true, false);
				if (!this.Target.isDamaged())
				{
					base.Pop();
				}
				return;
			}
			if (!base.ParentObject.IsMobile())
			{
				base.ParentObject.UseEnergy(1000);
				base.Think("My target is too far and I'm immobile.");
				base.FailToParent();
				return;
			}
			base.Think("I'm going to move towards my target.");
			if (this.Target.OnWorldMap())
			{
				base.Think("Target's on the world map, can't follow!");
				this.Target = null;
				base.FailToParent();
				return;
			}
			FindPath findPath = new FindPath(base.ParentObject.CurrentCell, this.Target.CurrentCell, this.Target.IsPlayer(), true, base.ParentObject, 100, false, false, false, false, false, null);
			if (findPath.Usable)
			{
				this.PushChildGoal(new Step(findPath.Directions[0], false, false, false, false, null, false));
				return;
			}
			base.Think("I can't find a path to my target!");
			base.FailToParent();
		}

		// Token: 0x0400364A RID: 13898
		public GameObject Target;

		// Token: 0x0400364B RID: 13899
		public int LastSeen;
	}
}
