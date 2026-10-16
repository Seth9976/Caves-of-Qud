using System;
using System.Linq;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E68 RID: 3688
	[Serializable]
	public class BoneWorm : IPart
	{
		// Token: 0x06009505 RID: 38149 RVA: 0x0035E607 File Offset: 0x0035C807
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == PooledEvent<BeforeAITakingActionEvent>.ID;
		}

		// Token: 0x06009506 RID: 38150 RVA: 0x0035E625 File Offset: 0x0035C825
		public override bool HandleEvent(AIBoredEvent E)
		{
			return this.CheckTarget() && base.HandleEvent(E);
		}

		// Token: 0x06009507 RID: 38151 RVA: 0x0035E638 File Offset: 0x0035C838
		public override bool HandleEvent(BeforeAITakingActionEvent E)
		{
			this.CheckTarget();
			return base.HandleEvent(E);
		}

		// Token: 0x06009508 RID: 38152 RVA: 0x0035E648 File Offset: 0x0035C848
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009509 RID: 38153 RVA: 0x0035E64B File Offset: 0x0035C84B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIBeginKill");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600950A RID: 38154 RVA: 0x0035E660 File Offset: 0x0035C860
		public void enclose()
		{
			int r;
			int r2;
			for (r = 2; r < 4; r = r2 + 1)
			{
				Cell cell = (from c in this.ParentObject.Brain.Target.CurrentCell.GetAdjacentCells(r, true, true)
					where c.DistanceTo(this.ParentObject.Brain.Target) == r
					where c.IsPassable(null, true)
					orderby c.DistanceTo(this.ParentObject)
					select c).FirstOrDefault<Cell>();
				if (cell != null)
				{
					this.ParentObject.Brain.PushGoal(new MoveTo(cell, false, false, 0, false, false, false, -1, -1, 95));
					return;
				}
				r2 = r;
			}
		}

		// Token: 0x0600950B RID: 38155 RVA: 0x0035E735 File Offset: 0x0035C935
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AIBeginKill" && this.ParentObject.HasEffect<Burrowed>() && this.ParentObject.Target != null)
			{
				this.enclose();
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600950C RID: 38156 RVA: 0x0035E774 File Offset: 0x0035C974
		public bool CheckTarget()
		{
			if (this.ParentObject.HasEffect<Burrowed>())
			{
				GameObject target = this.ParentObject.Target;
				if (target != null)
				{
					if (this.ParentObject.Brain.Goals.Count == 0 || (this.ParentObject.Brain.Goals.Peek().GetType() != typeof(MoveTo) && this.ParentObject.Brain.Goals.Peek().GetType() != typeof(Step)))
					{
						this.ParentObject.Brain.Goals.Clear();
						this.ParentObject.Target = target;
						this.enclose();
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003DC5 RID: 15813
		public string Duration = "1d3";
	}
}
