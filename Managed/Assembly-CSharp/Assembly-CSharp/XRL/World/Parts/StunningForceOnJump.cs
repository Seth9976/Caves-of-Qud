using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000E91 RID: 3729
	[Serializable]
	public class StunningForceOnJump : IPart
	{
		// Token: 0x06009626 RID: 38438 RVA: 0x00366385 File Offset: 0x00364585
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<JumpedEvent>.ID;
		}

		// Token: 0x06009627 RID: 38439 RVA: 0x0036639B File Offset: 0x0036459B
		public override bool HandleEvent(JumpedEvent E)
		{
			StunningForce.Concussion(E.TargetCell, E.Actor, this.Level, this.Distance, E.Actor.GetPhase(), null, true, true);
			return base.HandleEvent(E);
		}

		// Token: 0x04003E2C RID: 15916
		public int Level = 1;

		// Token: 0x04003E2D RID: 15917
		public int Distance = 1;
	}
}
