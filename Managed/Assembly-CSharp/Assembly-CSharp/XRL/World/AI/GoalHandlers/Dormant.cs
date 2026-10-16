using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B80 RID: 2944
	[Serializable]
	public class Dormant : GoalHandler
	{
		// Token: 0x060075CF RID: 30159 RVA: 0x002C31DC File Offset: 0x002C13DC
		public Dormant(int Duration)
		{
			this.Duration = Duration;
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x002C31F2 File Offset: 0x002C13F2
		public override void Create()
		{
			base.Think("I'm going dormant for " + this.Duration.Things("turn", null) + ".");
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x002C321A File Offset: 0x002C141A
		public override bool Finished()
		{
			return this.Duration == 0;
		}

		// Token: 0x060075D2 RID: 30162 RVA: 0x002C3227 File Offset: 0x002C1427
		public override void TakeAction()
		{
			if (this.Duration > 0)
			{
				this.Duration--;
			}
			this.ParentBrain.ParentObject.UseEnergy(1000);
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x002C3255 File Offset: 0x002C1455
		public override void PushChildGoal(GoalHandler Child)
		{
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x002C3257 File Offset: 0x002C1457
		public override void PushGoal(GoalHandler Goal)
		{
		}

		// Token: 0x040035F3 RID: 13811
		public int Duration = -1;
	}
}
