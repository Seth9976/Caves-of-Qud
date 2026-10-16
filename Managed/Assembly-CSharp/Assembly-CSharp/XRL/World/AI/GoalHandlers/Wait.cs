using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B92 RID: 2962
	[Serializable]
	public class Wait : GoalHandler
	{
		// Token: 0x0600764D RID: 30285 RVA: 0x002C7A7C File Offset: 0x002C5C7C
		public Wait(int Duration)
		{
			this.TicksLeft = Duration;
		}

		// Token: 0x0600764E RID: 30286 RVA: 0x002C7A8B File Offset: 0x002C5C8B
		public Wait(int Duration, string Reason)
			: this(Duration)
		{
			this.Reason = Reason;
		}

		// Token: 0x0600764F RID: 30287 RVA: 0x002C7A9C File Offset: 0x002C5C9C
		public override void Create()
		{
			if (!string.IsNullOrEmpty(this.Reason))
			{
				base.Think(string.Concat(new string[]
				{
					"I'll wait ",
					this.TicksLeft.ToString(),
					" ticks because ",
					this.Reason,
					"."
				}));
				return;
			}
			base.Think("I'll wait " + this.TicksLeft.ToString() + " ticks.");
		}

		// Token: 0x06007650 RID: 30288 RVA: 0x002C7B17 File Offset: 0x002C5D17
		public override bool IsBusy()
		{
			return false;
		}

		// Token: 0x06007651 RID: 30289 RVA: 0x002C7B1A File Offset: 0x002C5D1A
		public override bool Finished()
		{
			return this.TicksLeft <= 0;
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x002C7B28 File Offset: 0x002C5D28
		public override void TakeAction()
		{
			base.ParentObject.UseEnergy(1000);
			this.TicksLeft--;
			if (this.TicksLeft <= 0)
			{
				base.Pop();
			}
		}

		// Token: 0x04003639 RID: 13881
		public int TicksLeft;

		// Token: 0x0400363A RID: 13882
		public string Reason;
	}
}
