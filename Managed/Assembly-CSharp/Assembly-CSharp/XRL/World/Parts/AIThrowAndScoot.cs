using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E2E RID: 3630
	[Serializable]
	public class AIThrowAndScoot : AIBehaviorPart
	{
		// Token: 0x06009370 RID: 37744 RVA: 0x0035194B File Offset: 0x0034FB4B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIAfterThrowEvent>.ID;
		}

		// Token: 0x06009371 RID: 37745 RVA: 0x00351961 File Offset: 0x0034FB61
		public override bool HandleEvent(AIAfterThrowEvent E)
		{
			this.FleeFrom(E.Target);
			return base.HandleEvent(E);
		}

		// Token: 0x06009372 RID: 37746 RVA: 0x00351976 File Offset: 0x0034FB76
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009373 RID: 37747 RVA: 0x00351979 File Offset: 0x0034FB79
		private void FleeFrom(GameObject Target)
		{
			if (GameObject.Validate(ref Target))
			{
				this.ParentObject.Brain.PushGoal(new Flee(Target, this.Duration.RollCached(), false, true));
			}
		}

		// Token: 0x04003CFB RID: 15611
		public string Duration = "1d3";
	}
}
