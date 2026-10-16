using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C5A RID: 3162
	[Serializable]
	public class Heal20_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060080C4 RID: 32964 RVA: 0x002F521F File Offset: 0x002F341F
		public override string GetDescription()
		{
			return "heal 20 hp";
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x002F5226 File Offset: 0x002F3426
		public override void Apply(GameObject go)
		{
			go.Statistics["Hitpoints"].Penalty -= 20;
		}
	}
}
