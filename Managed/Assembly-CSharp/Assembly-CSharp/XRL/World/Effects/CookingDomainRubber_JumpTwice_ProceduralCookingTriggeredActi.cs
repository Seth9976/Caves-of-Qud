using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000CFE RID: 3326
	[Serializable]
	public class CookingDomainRubber_JumpTwice_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008387 RID: 33671 RVA: 0x002FAF30 File Offset: 0x002F9130
		public override string GetDescription()
		{
			return "@they jump twice in a row, each time at +1 range.";
		}

		// Token: 0x06008388 RID: 33672 RVA: 0x002FAF37 File Offset: 0x002F9137
		public override string GetNotification()
		{
			return "@they feel a springiness inside.";
		}

		// Token: 0x06008389 RID: 33673 RVA: 0x002FAF40 File Offset: 0x002F9140
		public override void Apply(GameObject go)
		{
			int num = 0;
			while (num < 2 && Acrobatics_Jump.Jump(go, 1, null, base.GetType().Name))
			{
				num++;
			}
		}
	}
}
