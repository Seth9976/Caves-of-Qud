using System;
using System.Collections.Generic;
using System.Linq;

namespace XRL.World.Effects
{
	// Token: 0x02000CF7 RID: 3319
	[Serializable]
	public class CookingDomainHightierRegen_RemoveDebuff_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008365 RID: 33637 RVA: 0x002FABF6 File Offset: 0x002F8DF6
		public override string GetDescription()
		{
			return "one of @their major negative status effects is removed at random. If no major effects, then a minor effect is removed instead.";
		}

		// Token: 0x06008366 RID: 33638 RVA: 0x002FABFD File Offset: 0x002F8DFD
		public override string GetNotification()
		{
			return "@they feel less afflicted.";
		}

		// Token: 0x06008367 RID: 33639 RVA: 0x002FAC04 File Offset: 0x002F8E04
		public override void Apply(GameObject go)
		{
			IEnumerable<Effect> enumerable = go.YieldEffects((Effect FX) => !FX.IsOfType(16777216) && FX.IsOfTypes(100663296) && !FX.IsOfType(134217728));
			if (enumerable.Any<Effect>())
			{
				go.RemoveEffect(enumerable.GetRandomElement<Effect>(null), true);
				return;
			}
			enumerable = go.YieldEffects((Effect FX) => FX.IsOfType(16777216) && FX.IsOfTypes(100663296) && !FX.IsOfType(134217728));
			if (enumerable.Any<Effect>())
			{
				go.RemoveEffect(enumerable.GetRandomElement<Effect>(null), true);
			}
		}
	}
}
