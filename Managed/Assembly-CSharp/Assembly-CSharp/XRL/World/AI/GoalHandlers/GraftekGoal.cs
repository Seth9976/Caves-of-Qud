using System;
using System.Collections.Generic;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B98 RID: 2968
	[Serializable]
	public class GraftekGoal : GoalHandler
	{
		// Token: 0x06007677 RID: 30327 RVA: 0x002C8677 File Offset: 0x002C6877
		public GraftekGoal(GameObject Target)
		{
			this.Target = Target;
		}

		// Token: 0x06007678 RID: 30328 RVA: 0x002C8688 File Offset: 0x002C6888
		public override void Create()
		{
			base.Think("I'm trying to graft " + this.Target.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "!");
		}

		// Token: 0x06007679 RID: 30329 RVA: 0x002C86D3 File Offset: 0x002C68D3
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x002C86D6 File Offset: 0x002C68D6
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x0600767B RID: 30331 RVA: 0x002C86D9 File Offset: 0x002C68D9
		public override void Push(Brain pBrain)
		{
			base.Push(pBrain);
		}

		// Token: 0x0600767C RID: 30332 RVA: 0x002C86E4 File Offset: 0x002C68E4
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
				base.Think("My target has been destroyed!");
				this.Target = null;
				base.FailToParent();
				return;
			}
			Cell currentCell = this.Target.CurrentCell;
			Cell currentCell2 = base.ParentObject.CurrentCell;
			int num = ((currentCell != null && currentCell2 != null) ? currentCell.PathDistanceTo(currentCell2) : (-1));
			if (currentCell != null)
			{
				Zone parentZone = currentCell.ParentZone;
				if (((parentZone != null) ? parentZone.ZoneID : null) != null && num <= 80)
				{
					goto IL_0093;
				}
			}
			this.LastSeen++;
			IL_0093:
			if (this.Target.HasPart<GraftekGraft>())
			{
				base.Think("My target is already grafted...");
				this.Target = null;
				base.FailToParent();
				return;
			}
			if (this.LastSeen > 5)
			{
				base.Think("I can't find my target...");
				this.Target = null;
				base.FailToParent();
				return;
			}
			if (currentCell != null)
			{
				Zone parentZone2 = currentCell.ParentZone;
				if (((parentZone2 != null) ? parentZone2.ZoneID : null) != null)
				{
					if (num <= 1)
					{
						base.Think("I'm going to graft my target!");
						this.Target.Sparksplatter();
						this.Target.AddPart<GraftekGraft>(new GraftekGraft(), true, false);
						return;
					}
					if (base.ParentObject.IsMobile())
					{
						base.Think("I'm going to move towards my target.");
						bool flag = false;
						if (this.Target.IsPlayer())
						{
							flag = true;
						}
						if (currentCell.ParentZone.IsWorldMap())
						{
							base.Think("Target's on the world map, can't follow!");
							this.Target = null;
							base.FailToParent();
							return;
						}
						FindPath findPath = new FindPath(currentCell2.ParentZone.ZoneID, currentCell2.X, currentCell2.Y, currentCell.ParentZone.ZoneID, currentCell.X, currentCell.Y, flag, false, this.ParentBrain.ParentObject, false, false, false, false, 100);
						if (findPath.Usable)
						{
							using (List<string>.Enumerator enumerator = findPath.Directions.GetEnumerator())
							{
								if (!enumerator.MoveNext())
								{
									return;
								}
								string text = enumerator.Current;
								this.PushChildGoal(new Step(text, false, false, false, false, null, false));
								return;
							}
						}
						base.FailToParent();
						return;
					}
					else
					{
						this.ParentBrain.ParentObject.UseEnergy(1000);
						base.Think("My target is too far and I'm immobile.");
						base.FailToParent();
					}
					return;
				}
			}
			base.Think("My target has no location!");
			this.Target = null;
			base.FailToParent();
		}

		// Token: 0x04003648 RID: 13896
		public GameObject Target;

		// Token: 0x04003649 RID: 13897
		[NonSerialized]
		private int LastSeen;
	}
}
