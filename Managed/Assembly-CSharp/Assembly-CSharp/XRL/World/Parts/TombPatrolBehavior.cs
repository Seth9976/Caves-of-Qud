using System;
using Genkit;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E3C RID: 3644
	[Serializable]
	public class TombPatrolBehavior : IPart
	{
		// Token: 0x060093D2 RID: 37842 RVA: 0x0035386D File Offset: 0x00351A6D
		public Point2D GetNextWaypoint()
		{
			return new Point2D(0, 0);
		}

		// Token: 0x060093D3 RID: 37843 RVA: 0x00353876 File Offset: 0x00351A76
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID;
		}

		// Token: 0x060093D4 RID: 37844 RVA: 0x0035388C File Offset: 0x00351A8C
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return base.HandleEvent(E);
			}
			if (this.ParentObject.IsPlayerControlled())
			{
				return base.HandleEvent(E);
			}
			Zone parentZone = this.ParentObject.Physics.CurrentCell.ParentZone;
			if (parentZone != null)
			{
				int wX = parentZone.wX;
				int wY = parentZone.wY;
				int z = parentZone.Z;
				int num = parentZone.X;
				int num2 = parentZone.Y;
				if (num == 0 && num2 == 0)
				{
					num = 1;
				}
				else if (num == 1 && num2 == 0)
				{
					num = 2;
				}
				else if (num == 2 && num2 == 0)
				{
					num2 = 1;
				}
				else if (num == 2 && num2 == 1)
				{
					num2 = 2;
				}
				else if (num == 2 && num2 == 2)
				{
					num = 1;
				}
				else if (num == 1 && num2 == 2)
				{
					num = 0;
				}
				else if (num == 0 && num2 == 2)
				{
					num2 = 1;
				}
				else if (num == 0 && num2 == 1)
				{
					num2 = 0;
				}
				this.ParentObject.Brain.PushGoal(new TombPatrolGoal(string.Concat(new string[]
				{
					"JoppaWorld.",
					wX.ToString(),
					".",
					wY.ToString(),
					".",
					num.ToString(),
					".",
					num2.ToString(),
					".",
					z.ToString()
				})));
			}
			return false;
		}

		// Token: 0x060093D5 RID: 37845 RVA: 0x003539F8 File Offset: 0x00351BF8
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (E.TargetLeader == null)
			{
				if (E.Target.HasMarkOfDeath(null))
				{
					E.Feeling = 0;
					return false;
				}
				if (E.Target.Blueprint != this.ParentObject.Blueprint)
				{
					E.Feeling = -100;
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093D6 RID: 37846 RVA: 0x00353A52 File Offset: 0x00351C52
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
		}
	}
}
