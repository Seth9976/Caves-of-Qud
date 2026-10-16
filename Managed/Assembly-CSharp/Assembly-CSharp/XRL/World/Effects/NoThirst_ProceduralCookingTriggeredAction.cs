using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C5D RID: 3165
	[Serializable]
	public class NoThirst_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060080CD RID: 32973 RVA: 0x002F5346 File Offset: 0x002F3546
		public override string GetDescription()
		{
			return "@they don't thirst for the next 12 hours.";
		}

		// Token: 0x060080CE RID: 32974 RVA: 0x002F534D File Offset: 0x002F354D
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<NoThirst_ProceduralCookingTriggeredAction_Effect>())
			{
				go.ApplyEffect(new NoThirst_ProceduralCookingTriggeredAction_Effect(), null);
			}
		}
	}
}
