using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C5B RID: 3163
	[Serializable]
	public class Str3_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060080C7 RID: 32967 RVA: 0x002F524E File Offset: 0x002F344E
		public override string GetDescription()
		{
			return "+3 Str for the next three hours";
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x002F5255 File Offset: 0x002F3455
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<Str3_ProceduralCookingTriggeredAction_Effect>())
			{
				go.ApplyEffect(new Str3_ProceduralCookingTriggeredAction_Effect(), null);
			}
		}
	}
}
