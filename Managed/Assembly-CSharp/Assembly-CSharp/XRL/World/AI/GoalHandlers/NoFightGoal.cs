using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B86 RID: 2950
	[Serializable]
	public class NoFightGoal : GoalHandler
	{
		// Token: 0x060075F5 RID: 30197 RVA: 0x002C4004 File Offset: 0x002C2204
		public override bool CanFight()
		{
			return false;
		}
	}
}
