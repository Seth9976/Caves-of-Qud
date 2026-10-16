using System;
using System.Collections.Generic;
using System.Linq;

namespace XRL.World.Effects
{
	// Token: 0x02000CF6 RID: 3318
	[Serializable]
	public class CookingDomainLowtierRegen_RemoveDebuff_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008361 RID: 33633 RVA: 0x002FAB95 File Offset: 0x002F8D95
		public override string GetDescription()
		{
			return "one of @their minor negative status effects is removed at random.";
		}

		// Token: 0x06008362 RID: 33634 RVA: 0x002FAB9C File Offset: 0x002F8D9C
		public override string GetNotification()
		{
			return "@they feel less afflicted.";
		}

		// Token: 0x06008363 RID: 33635 RVA: 0x002FABA4 File Offset: 0x002F8DA4
		public override void Apply(GameObject go)
		{
			IEnumerable<Effect> enumerable = go.YieldEffects((Effect FX) => FX.IsOfType(16777216) && FX.IsOfTypes(100663296) && !FX.IsOfType(134217728));
			if (enumerable.Any<Effect>())
			{
				go.RemoveEffect(enumerable.GetRandomElement<Effect>(null), true);
			}
		}
	}
}
