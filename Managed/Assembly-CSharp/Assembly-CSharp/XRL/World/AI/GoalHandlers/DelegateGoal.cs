using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B7E RID: 2942
	[Serializable]
	public class DelegateGoal : GoalHandler
	{
		// Token: 0x060075C3 RID: 30147 RVA: 0x002C2F27 File Offset: 0x002C1127
		public DelegateGoal()
		{
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x002C2F2F File Offset: 0x002C112F
		public DelegateGoal(Action<GoalHandler> Action, Func<GoalHandler, bool> Finished = null, Action<GoalHandler> Failed = null)
		{
			this.OnAction = Action;
			this.OnFinished = Finished;
			this.OnFailed = Failed;
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x002C2F4C File Offset: 0x002C114C
		public override bool Finished()
		{
			return (this.OnAction == null && this.OnFinished == null) || (this.OnFinished != null && this.OnFinished(this));
		}

		// Token: 0x060075C6 RID: 30150 RVA: 0x002C2F76 File Offset: 0x002C1176
		public override void TakeAction()
		{
			if (this.OnAction != null)
			{
				this.OnAction(this);
			}
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x002C2F8C File Offset: 0x002C118C
		public override void Failed()
		{
			if (this.OnFailed != null)
			{
				this.OnFailed(this);
			}
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x002C2FA4 File Offset: 0x002C11A4
		public override bool CanFight()
		{
			bool? setCanFight = this.SetCanFight;
			if (setCanFight == null)
			{
				return base.CanFight();
			}
			return setCanFight.GetValueOrDefault();
		}

		// Token: 0x060075C9 RID: 30153 RVA: 0x002C2FD0 File Offset: 0x002C11D0
		public override bool IsNonAggressive()
		{
			bool? setNonAggressive = this.SetNonAggressive;
			if (setNonAggressive == null)
			{
				return base.IsNonAggressive();
			}
			return setNonAggressive.GetValueOrDefault();
		}

		// Token: 0x040035E8 RID: 13800
		[NonSerialized]
		public Action<GoalHandler> OnAction;

		// Token: 0x040035E9 RID: 13801
		[NonSerialized]
		public Func<GoalHandler, bool> OnFinished;

		// Token: 0x040035EA RID: 13802
		[NonSerialized]
		public Action<GoalHandler> OnFailed;

		// Token: 0x040035EB RID: 13803
		[NonSerialized]
		public bool? SetCanFight;

		// Token: 0x040035EC RID: 13804
		[NonSerialized]
		public bool? SetNonAggressive;
	}
}
