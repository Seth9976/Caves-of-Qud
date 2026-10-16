using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E2A RID: 3626
	[Serializable]
	public class AIShootAndScoot : AIBehaviorPart
	{
		// Token: 0x0600935D RID: 37725 RVA: 0x003515B3 File Offset: 0x0034F7B3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIAfterMissileEvent>.ID || ID == PooledEvent<AIAfterThrowEvent>.ID;
		}

		// Token: 0x0600935E RID: 37726 RVA: 0x003515D1 File Offset: 0x0034F7D1
		public override bool HandleEvent(AIAfterMissileEvent E)
		{
			this.FleeFrom(E.Target);
			return base.HandleEvent(E);
		}

		// Token: 0x0600935F RID: 37727 RVA: 0x003515E6 File Offset: 0x0034F7E6
		public override bool HandleEvent(AIAfterThrowEvent E)
		{
			this.FleeFrom(E.Target);
			return base.HandleEvent(E);
		}

		// Token: 0x06009360 RID: 37728 RVA: 0x003515FB File Offset: 0x0034F7FB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009361 RID: 37729 RVA: 0x00351600 File Offset: 0x0034F800
		private void FleeFrom(GameObject Target)
		{
			if (GameObject.Validate(ref Target) && !this.ParentObject.HasGoal("Flee"))
			{
				this.ParentObject.Brain.PushGoal(new Flee(Target, this.Duration.RollCached(), false, true));
			}
		}

		// Token: 0x04003CF7 RID: 15607
		public string Duration = "1d3";
	}
}
